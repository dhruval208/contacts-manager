using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactsManager.API.Models
{
    public class ErrorInfo
    {
        /// <summary>
        /// Error Code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        public string Message { get; set; }
    }
}