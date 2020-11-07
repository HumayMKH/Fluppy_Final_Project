using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        [Required, MaxLength(30)]
        public string Surname { get; set; }
        [MaxLength(30)]
        public string Username { get; set; }
        [MaxLength(250)]
        public string Password { get; set; }
        [Required, MaxLength(30)]
        public string Email { get; set; }
        [Required, MaxLength(30)]
        public string Phone { get; set; }
        [Required,MaxLength(30)]
        public string City { get; set; }
        [Required, MaxLength(150)]
        public string Address { get; set; }
        [Required,MaxLength(10)]
        public string PostCode { get; set; }
        [MaxLength(300)]
        public string Notes { get; set; }
        [Column(TypeName = "bit")]
        public bool IsRegistered { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<CommentReply> CommentReplies { get; set; }
        public List<Sale> Sales { get; set; }
    }
}