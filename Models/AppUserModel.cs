﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
/*
 This model serves as the primary module for housing system user information.  
 The fields reflect all attributes in the Users database table.
 Changes here may also impact the identity authentication and authorization process - 
 and will be included in the database midgrations process that changes the users table.
 */
namespace OnAccount.Models
{
    [ApiController]
    [BindProperties(SupportsGet = true)]
    public class AppUserModel
    {

        public string? Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 1)]
        [DisplayName("User Name:")]
        public string? ScreenName { get; set; } = "";
        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 1)]
        [DisplayName("First Name:")]
        public string? FirstName { get; set; } = "";
        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 1)]
        [DisplayName("Last Name:")]
        public string? LastName { get; set; } = "";
        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(11, MinimumLength = 10)]
        [DisplayName("Phone Number:")]
        public string? PhoneNumber { get; set; } = "";

        [Required]
        [DataType(DataType.Text)]
        [StringLength(150, MinimumLength = 1)]
        [DisplayName("Address:")]
        internal string? Address { get; set; } = "";

        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 1)]
        [DisplayName("City:")]
        internal string? City { get; set; } = "";

        [Required]
        [DataType(DataType.Text)]
        [StringLength(2, MinimumLength = 2)]
        [DisplayName("State:")]
        internal string? State { get; set; } = "";

        [Required]
        [DataType(DataType.Text)]
        [StringLength(10, MinimumLength = 1)]
        [DisplayName("Zip:")]
        internal string? Zip { get; set; } = "";

        [DataType(DataType.Date)]
        [StringLength(20, MinimumLength = 20)]
        [DisplayName("Birthday:")]
        internal string? DateofBirth { get; set; } = "";

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 1)]
        [DisplayName("User Role:")]
        public string? UserRole { get; set; } = "";

        [Required]
        [DataType(DataType.Text)]
        [StringLength(10, MinimumLength = 1)]
        [DisplayName("Active Status:")]
        public string? ActiveStatus { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(10, MinimumLength = 1)]
        [DisplayName("User Name:")]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(255, MinimumLength = 1)]
        [DisplayName("Email Address:")]
        public string? Email { get; set; }

        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 1)]
        [DisplayName("Normalized User Name:")]
        public string? NormalizedUserName { get; set; }

        [DisplayFormat(DataFormatString = "{:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [StringLength(100, MinimumLength = 4)]
        [DisplayName("Suspension Date:")]
        public string? AcctSuspensionDate { get; set; }

        [DisplayFormat(DataFormatString = "{:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [StringLength(100, MinimumLength = 4)]
        [DisplayName("Reinstatement Date:")]
        public string? AcctReinstatementDate { get; set; }

        [DisplayFormat(DataFormatString = "{:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [StringLength(100, MinimumLength = 4)]
        [DisplayName("Last Password Change:")]
        public string? LastPasswordChangedDate { get; set; }

        
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 1)]
        [DisplayName("Next Reset Days:")]
        public string PasswordResetDays { get; set; } = "90";

        [ValidateNever]
        [NotMapped]
        public IEnumerable<SelectListItem>? RoleList { get; set; }


    }

}