﻿using System.ComponentModel;

namespace oa.Models
{
    public class RoleModel
    {
        [DisplayName("First Name:")]
        public string? firstName { get; set; }
        [DisplayName("Last Name:")]
        public string? lastName { get; set; }
        [DisplayName("Email:")]
        public string? email { get; set; }
    }
}
