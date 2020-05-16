using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using AppAuthenticationModel.Models;
using AppAuthentication.Repository;
using AppAuthentication.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace AppAuthentication.Common
{
    public class Auth : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ResultView oResult = new ResultView();
            var tokenH = context.HttpContext.Request.Headers["token"];

            if (!tokenH.Any())
            {
                oResult.Success = false;
                oResult.Exception = false;
                oResult.Message = "Invalid token";
                context.Result = new JsonResult(oResult);
            }

        }

    }
}