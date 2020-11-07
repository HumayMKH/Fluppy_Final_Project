using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class CommentReply
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Reply { get; set; }
    }
}