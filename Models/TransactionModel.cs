﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace oa.Models
{
    public class TransactionModel
    {
        public int? id { get; set; }

        [Required]
        [DisplayName("Journal Id:")]
        public int? journal_id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 1)]
        [DisplayName("Debit Account:")]
        public int? debit_account { get; set; } = 0;

        [IntegerValidator(MinValue = 1, MaxValue = 1000000000, ExcludeRange = true)]
        [DisplayName("Dr.")]
        public double? debit_amount { get; set; } = 0;

        [NotMapped]
        [ValidateNever]
        [DisplayName("Dr. Account")]
        public string? dr_description { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 1)]
        [DisplayName("Credit Account")]
        public int? credit_account { get; set; } = 0;

        [Required]
        [IntegerValidator(MinValue = 1, MaxValue = 1000000000, ExcludeRange = true)]
        [DisplayName("Cr.")]
        public double? credit_amount { get; set; } = 0;

        [NotMapped]
        [ValidateNever]
        [DisplayName("Cr. Account")]
        public string? cr_description { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [DisplayName("Transaction date:")]
        public DateTime? transaction_date { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 1)]
        [DisplayName("Created by:")]
        public string? created_by { get; set; }

        [DisplayName("Is Opening:")]
        public bool? is_opening { get; set; } = false;

        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 1)]
        [DisplayName("Status:")]
        public string? status { get; set; } = "Pending";

        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 0)]
        [DisplayName("Description:")]
        public string? description { get; set; } = "";

        [Required]
        [StringLength(100, MinimumLength = 0)]
        [DisplayName("Journal Id:")]
        public int? transaction_number { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(1000, MinimumLength = 0)]
        [DisplayName("Journal Description:")]
        public string? journal_description { get; set; } = "";

        [Required]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [DisplayName("Journal Date:")]
        public DateTime? journal_date { get; set; }

        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 0)]
        [DisplayName("Supporting Document:")]
        public string? supporting_document { get; set; } = "";
    }
}
