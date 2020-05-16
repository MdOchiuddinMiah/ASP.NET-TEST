using System;
using System.Collections.Generic;

namespace AppAuthenticationModel.Models
{
    public partial class UserFeedback
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public bool IsLike { get; set; }
        public bool IsDislike { get; set; }

        public Comment Comment { get; set; }
        public Users User { get; set; }
    }
}
