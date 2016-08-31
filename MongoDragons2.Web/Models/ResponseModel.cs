using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDragons2.Models
{
    public class ResponseModel<T> where T: class
    {
        public bool status { get; set; }
        public int statusCode { get; set; }
        public string statusMessage { get; set;  }
        public T data { get; set; }
    }
}