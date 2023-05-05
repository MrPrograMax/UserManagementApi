using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public Guid UserGroupId { get; set; }

        [Required]
        public Guid UserStateId { get; set; }


        [ForeignKey("UserGroupId")]
        public virtual UserGroup UserGroup { get; set; }

        [ForeignKey("UserStateId")]
        public virtual UserState UserState { get; set; }
    }
}
