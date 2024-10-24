﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
//#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using oa.Areas.Identity.Data;
using oa.Models;
using oa.Services;

namespace oa.Areas.Identity.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DbConnectorService _dbConnectorService;

        public ResetPasswordModel(UserManager<AppUser> userManager,
            DbConnectorService dbConnectorService)
        {
            _userManager = userManager;
            _dbConnectorService = dbConnectorService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string Code { get; set; }

        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            //edit starts here 9-16-2024 RES
            var currentPasswordStatus = await _userManager.CheckPasswordAsync(user, Input.Password);
            List<PassHashModel> allOldHashes = new List<PassHashModel>();
            allOldHashes = _dbConnectorService.GetPassHashList();
            bool oldPasswordCombinationFound = false;
            for (int i = 0; i < allOldHashes.Count; i++)
            {
                PasswordVerificationResult versificationResult = _userManager.PasswordHasher.VerifyHashedPassword(user, allOldHashes[i].passhash, Input.Password);
                if (versificationResult.ToString() == "Success")
                {
                    oldPasswordCombinationFound = true;
                    string description = "The password was previously used. Please try again.";
                    ModelState.AddModelError(string.Empty, description);
                    return Page();
                }

            }
            //edit stops here.

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);

            // save the new hash to the db
            if (!oldPasswordCombinationFound)
            {
                _dbConnectorService.StorePassHash(user.Id, user.PasswordHash);
            }

            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
