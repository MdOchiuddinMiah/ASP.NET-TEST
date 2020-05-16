using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAuthentication.ViewModel
{
    public class ResultView
    {
        public bool Success { get; set; }
        public bool Exception { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
