using AppAuthenticationModel.Models;
using AppAuthentication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAuthentication.Interface
{
    public interface IAppSurveyRepository
    {
        Task<List<PostView>> Posts();
        Task<string> LikeDislikeComment(CommentFeedback commentFeedback);
    }
}
