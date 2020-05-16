using System;
using System.Collections.Generic;

namespace AppAuthenticationModel.Models
{
    public partial class Comment
    {
        public Comment()
        {
            UserFeedback = new HashSet<UserFeedback>();
        }

        public int Id { get; set; }
        public int PostId { get; set; }
        public string CommentTitle { get; set; }
        public int CommentedBy { get; set; }
        public DateTime CommentedTime { get; set; }

        public Users CommentedByNavigation { get; set; }
        public Post Post { get; set; }
        public ICollection<UserFeedback> UserFeedback { get; set; }
    }
}
