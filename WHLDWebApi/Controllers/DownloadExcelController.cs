using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WHLDWebApi.Controllers
{
    public class DownloadExcelController : ApiController
    {
        // GET: DownloadExcel
        public HttpResponseMessage GetExcelFromWebApi()
        {
            var browser = String.Empty;
            if (HttpContext.Current.Request.UserAgent != null)
            {
                browser = HttpContext.Current.Request.UserAgent.ToUpper();
            }
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content", "standard.xlsx");
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            FileStream fileStream = System.IO.File.OpenRead(filePath);
            httpResponseMessage.Content = new StreamContent(fileStream);
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName =
                    browser.Contains("FIREFOX")
                        ? Path.GetFileName(filePath)
                        : HttpUtility.UrlEncode(Path.GetFileName(filePath))
            };

            return httpResponseMessage;
        }
    }
}