using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Runtime.Serialization;

namespace MongoDragons2.Models
{

    public class Metadata<T> where T : class
    {

        public int totalResults { get; set; }


        public int returnedResults { get; set; }


        public T[] data { get; set; }


        public DateTime timestamp { get; set; }


        public bool status { get; set; }

        public Metadata(HttpResponseMessage httpResponse, bool isIQueryable)
        {
            this.timestamp = DateTime.Now;

            if (httpResponse.Content != null && httpResponse.IsSuccessStatusCode)
            {
                this.totalResults = 1;
                this.returnedResults = 1;
                this.status = true;

                if (isIQueryable)
                {
                    IEnumerable<T> enumResponseObject;
                    httpResponse.TryGetContentValue<IEnumerable<T>>(out enumResponseObject);
                    this.data = enumResponseObject.ToArray();
                    this.returnedResults = enumResponseObject.Count();
                }
                else
                {
                    T responseObject;
                    httpResponse.TryGetContentValue<T>(out responseObject);
                    this.data = new T[] { responseObject };
                }
            }

            else
            {
                this.status = false;
                this.returnedResults = 0;
            }
        }
    }
}
