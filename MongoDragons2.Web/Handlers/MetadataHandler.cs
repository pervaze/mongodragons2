using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using MongoDragons2.Models;

namespace MongoDragons2.Handlers
{
    public class MetadataHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken).ContinueWith(
              task =>
              {
                  if (ResponseIsValid(task.Result))
                  {
                      object responseObject;
                      task.Result.TryGetContentValue(out responseObject);

                      var list = new List<object>();
                      list.Add(responseObject);
                      ProcessObject<object>(responseObject as IEnumerable<object>, task.Result, false);

                  }

                  return task.Result;
              }
            );
        }

        private void ProcessObject<T>(IEnumerable<T> responseObject, HttpResponseMessage response, bool isIQueryable) where T : class
        {
            var metadata = new Metadata<T>(response, isIQueryable);
            var originalSize = new string[1] as IEnumerable<string>;

            response.Headers.TryGetValues("originalSize", out originalSize);
            response.Headers.Remove("originalSize");
            if (originalSize != null)
            {
                metadata.totalResults = Convert.ToInt32(originalSize.FirstOrDefault());
            }

            //uncomment this to preserve content negotation, but remember about typecasting for DataContractSerliaizer
            //var formatter = GlobalConfiguration.Configuration.Formatters.First(t => t.SupportedMediaTypes.Contains(new MediaTypeHeaderValue(response.Content.Headers.ContentType.MediaType)));
            //response.Content = new ObjectContent<Metadata<T>>(metadata, formatter);

            response.Content = new ObjectContent<Metadata<T>>(metadata, GlobalConfiguration.Configuration.Formatters[0]);
        }

        private bool ResponseIsValid(HttpResponseMessage response)
        {
            if (response == null || response.StatusCode != HttpStatusCode.OK || !(response.Content is ObjectContent)) return false;
            return true;
        }
    }
}