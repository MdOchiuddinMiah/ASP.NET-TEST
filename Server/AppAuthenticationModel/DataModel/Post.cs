using System;
using System.Collections.Generic;

namespace AppAuthenticationModel.Models
{
    public partial class Post
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string PostTitle { get; set; }
        public int PostedBy { get; set; }
        public DateTime PostedTime { get; set; }

        public Users PostedByNavigation { get; set; }
        public ICollection<Comment> Comment { get; set; }
    }
}
