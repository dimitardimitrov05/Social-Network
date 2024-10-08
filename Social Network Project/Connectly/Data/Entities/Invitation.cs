﻿using Connectly.Data.Account;
using System.ComponentModel.DataAnnotations;

namespace Connectly.Data.Entities
{
    public class Invitation
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreationOfInvite { get; set; }
        [Required]
        public string UserCreatedTheInvite { get; set; } = null!;
        public User CreatorUser { get; set; }
        [Required]
        public DateTime ExpirationOfInvite { get; set; }
        [Required]
        public string UserRegistratedFromInvite { get; set; } = null!;
        [Required]
        public string VerificationCode { get; set; } = null!;
    }
}
