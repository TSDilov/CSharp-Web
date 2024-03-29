﻿using System.ComponentModel.DataAnnotations;

namespace ForumApp.Web.ViewModels.Users
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(60, MinimumLength = 10)]
        public string Email { get; set; } = null!;

        [Required]
        [RegularExpression("08[7-9][2-9][0-9]{6}")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
