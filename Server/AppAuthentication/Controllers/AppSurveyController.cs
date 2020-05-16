using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAuthentication.ViewModel;
using AppAuthentication.Interface;
using AppAuthenticationModel.Models;
using AppAuthentication.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AppAuthentication.ViewModel.Shared;

namespace AppAuthentication.Controllers
{
    [Produces("application/json")]
    [Route("app-survey")]
    [Auth]
    public class AppSurveyController : ControllerBase
    {
        private readonly IAppSurveyRepository _appSurveyRepository;

        public AppSurveyController(IAppSurveyRepository appSurveyRepository,
            IConfiguration configuration)
        {
            _appSurveyRepository = appSurveyRepository;
        }

        [HttpGet, Route("posts/{searchValue}/{page}/{pageSize}")]
        public async Task<ResultView> Posts(string searchValue,int page,int pageSize)
        {
            ResultView oResult = new ResultView();
            try
            {
                var user = await _appSurveyRepository.Posts(searchValue,page, pageSize);
                oResult.Data = user;
                oResult.Success = true;
                oResult.Message = "Retrive data successfully";
            }
            catch (Exception ex)
            {
                CustomError err = CommonFunctions.HandleException(ex, ex.Message);
                oResult.Success = false;
                oResult.Exception = true;
                oResult.ErrorCode = err.ErrorCode;
                oResult.Message = err.Message;
            }
            return oResult;
        }

        [HttpPut, Route("feedback")]
        public async Task<ResultView> LikeDislikeComment([FromBody]CommentFeedback commentFeedback)
        {
            ResultView oResult = new ResultView();
            try
            {
                var user = await _appSurveyRepository.LikeDislikeComment(commentFeedback);
                oResult.Data = user;
                oResult.Success = true;
                oResult.Message = user;
            }
            catch (Exception ex)
            {
                CustomError err = CommonFunctions.HandleException(ex, ex.Message);
                oResult.Success = false;
                oResult.Exception = true;
                oResult.ErrorCode = err.ErrorCode;
                oResult.Message = err.Message;
            }
            return oResult;
        }

    }
}
