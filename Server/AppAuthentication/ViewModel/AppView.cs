using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAuthentication.ViewModel
{
    public class PostView
    {
        public int Id { get; set; }
        public string PostTitle { get; set; }
        public string PostedBy { get; set; }
        public DateTime PostedTime { get; set; }
        public List<CommentView> CommentViews { get; set; }
    }

    public class CommentView
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int CommentId { get; set; }
        public string CommentTitle { get; set; }
        public string CommentedBy { get; set; }
        public DateTime CommentedTime { get; set; }
        public int CommentLike { get; set; }
        public int CommentDislike { get; set; }
    }

    public class CommentFeedback
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public Boolean IsLike { get; set; }
    }
}
