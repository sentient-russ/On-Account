﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using oa.Areas.Identity.Data;
using oa.Models;
using oa.Services;
using oa.Areas.Identity.Data;


namespace oa.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;
        private readonly DbConnectorService _dbConnectionService;

        public ChangePasswordModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<ChangePasswordModel> logger,
            IConfiguration configuration, DbConnectorService dbConnectorService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _dbConnectionService = dbConnectorService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }


        public class InputModel
        {

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            //edit starts here 9-16-2024 RES
            var currentPasswordStatus = await _userManager.CheckPasswordAsync(user, Input.NewPassword);
            List<PassHashModel> allOldHashes = new List<PassHashModel>();
            allOldHashes = _dbConnectionService.GetPassHashList();
            bool oldPasswordCombinationFound = false;
            for (int i = 0; i < allOldHashes.Count; i++)
            {
                PasswordVerificationResult result = _userManager.PasswordHasher.VerifyHashedPassword(user, allOldHashes[i].passhash, Input.NewPassword);
                if (result.ToString() == "Success")
                {
                    oldPasswordCombinationFound = true;
                    string description = "The password was previously used. Please try again.";
                    ModelState.AddModelError(string.Empty, description);
                    return Page();
                }
            }
            //edit stops here.
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
            //must get the new hash and save it to the new hashes table.
            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            // save the new hash to the db
            if (!oldPasswordCombinationFound)
            {
                var updatedUser = await _userManager.GetUserAsync(User);
                _dbConnectionService.StorePassHash(updatedUser.Id, updatedUser.PasswordHash);
            }

            return RedirectToPage();
        }
    }
}
