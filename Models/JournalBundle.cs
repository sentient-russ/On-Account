﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using oa.Services;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace oa.Models
{
    public class JournalBundle
    {

        [NotMapped]
        [ValidateNever]
        [DisplayName("Journal Id:")]
        public int? journal_id { get; set; }

        [NotMapped]
        [ValidateNever]
        List<TransactionModel>? currentTransactions { get; set; }

        [NotMapped]
        [ValidateNever]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 0)]
        [DisplayName("Journal Description:")]
        public string? journal_description { get; set; } = "";


        [NotMapped]
        [ValidateNever]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [DisplayName("Journal Date:")]
        public DateTime? journal_date { get; set; }

        [NotMapped]
        [ValidateNever]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 1)]
        [DisplayName("Status:")]
        public string? status { get; set; } = "Pending";

        [NotMapped]
        [ValidateNever]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 1)]
        [DisplayName("Account created by:  (Auto assigned)")]
        public string? created_by { get; set; }

        [ValidateNever]
        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DisplayName("Total:")]
        public double? total_adjustment { get; set; }

        [NotMapped]
        [ValidateNever]
        public List<TransactionModel>? transactions_list { get; set; }


        [NotMapped]
        [ValidateNever]
        public List<string>? supporting_docs_list { get; set; }

        [NotMapped]
        [ValidateNever]

        public string? is_adjusting { get; set; }
    }
}
