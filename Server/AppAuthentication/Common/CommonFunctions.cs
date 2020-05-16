using AppAuthentication.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AppAuthentication.Common
{
    public class CommonFunctions
    {
        public static CustomError HandleException(Exception exception,string message="")
        {
            CustomError error = new CustomError();
            var errorMessage = "Error!";
            var errorCode = HttpStatusCode.BadRequest;
            var exceptionType = exception.GetType();

            switch (exception)
            {
                case Exception e when exceptionType == typeof(UnauthorizedAccessException):
                    errorCode = HttpStatusCode.Unauthorized;
                    break;
                case ApplicationException e when exceptionType == typeof(ApplicationException):
                    errorCode = HttpStatusCode.BadRequest;
                    errorMessage = (message=="")? e.Message: message;
                    break;
                case SqlException e when exceptionType == typeof(SqlException):
                    error.ErrorCode = e.ErrorCode;
                    errorMessage = (message == "") ? "Database Error!":message;
                    break;
                default:
                    errorCode = HttpStatusCode.InternalServerError;
                    errorMessage = (message == "") ? "Internal Server Error!":message;
                    break;
            }
            error.ErrorCode = error.ErrorCode > 0 ? error.ErrorCode : Convert.ToInt32(errorCode);
            error.Message = errorMessage;

            return error;
        }
    }
}
