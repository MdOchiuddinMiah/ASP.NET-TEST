using System;
using System.Collections.Generic;

namespace AppAuthenticationModel.Models
{
    public partial class Users
    {
        public Users()
        {
            Comment = new HashSet<Comment>();
            Post = new HashSet<Post>();
            UserFeedback = new HashSet<UserFeedback>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }

        public ICollection<Comment> Comment { get; set; }
        public ICollection<Post> Post { get; set; }
        public ICollection<UserFeedback> UserFeedback { get; set; }
    }
}
