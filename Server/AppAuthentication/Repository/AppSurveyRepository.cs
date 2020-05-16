using Microsoft.EntityFrameworkCore;
using AppAuthenticationModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AppAuthentication.Interface;
using AppAuthentication.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AppAuthentication.Repository;
using AppAuthentication.Common;

namespace AppAuthentication.Repository
{
    public class AppSurveyRepository : BaseRepository, IAppSurveyRepository
    {
        private AppSurveyContext _context;
        private readonly IConfiguration _configuration;

        public AppSurveyRepository(AppSurveyContext appSurveyContext, IConfiguration configuration) : base(appSurveyContext, configuration)
        {
            _context = appSurveyContext;
            _configuration = configuration;
        }

        public async Task<List<PostView>> Posts(string searchValue, int page, int pageSize)
        {
            int start = ((page - 1) * pageSize) + 1;
            int end = page * pageSize;
            List<PostView> postViews = new List<PostView>();
            postViews = await _context.Post.Include(y => y.Comment).Where(x =>
              ((searchValue == "0") ? true : x.PostTitle.Equals(searchValue)) &&
              ((x.Comment.Where(z => z.Id >= start && z.Id <= end).Count() == 0) ? false : true))
                .Select(data => new PostView
                {
                    Id = data.Id,
                    PostTitle = data.PostTitle,
                    PostedBy = data.PostedByNavigation.Name,
                    PostedTime = data.PostedTime,
                    CommentViews = data.Comment.Where(z => z.Id >= start && z.Id <= end).Select(ndata => new CommentView
                    {
                        Id = ndata.Id,
                        PostId = ndata.PostId,
                        CommentTitle = ndata.CommentTitle,
                        CommentedBy = ndata.CommentedByNavigation.Name,
                        CommentedTime = ndata.CommentedTime,
                        CommentId = ndata.Id,
                        CommentLike = ndata.UserFeedback.Where(y => y.IsLike).Count(),
                        CommentDislike = ndata.UserFeedback.Where(y => y.IsDislike).Count()
                    }).ToList()
                }).ToListAsync();

            return postViews;
        }

        public async Task<string> LikeDislikeComment(CommentFeedback commentFeedback)
        {
            string message = "Feedback updated successfully";
            var feedback = await _context.UserFeedback.Where(x => x.CommentId == commentFeedback.CommentId && x.UserId == commentFeedback.UserId).FirstOrDefaultAsync();

            if (feedback == null)
            {
                UserFeedback userFeedback = new UserFeedback();
                userFeedback.CommentId = commentFeedback.CommentId;
                userFeedback.UserId = commentFeedback.UserId;
                userFeedback.IsLike = commentFeedback.IsLike;
                userFeedback.IsDislike = !commentFeedback.IsLike;
                _context.UserFeedback.Add(userFeedback);
                await Save();
            }
            else
            {
                if (feedback.IsLike == commentFeedback.IsLike || feedback.IsDislike == !commentFeedback.IsLike)
                {

                    throw new Exception("You already " + (commentFeedback.IsLike ? "liked" : "disliked"));
                }

                feedback.IsLike = commentFeedback.IsLike;
                feedback.IsDislike = !commentFeedback.IsLike;
                _context.Entry(feedback).State = EntityState.Modified;
                await Save();
            }


            return message;

        }

    }
}
