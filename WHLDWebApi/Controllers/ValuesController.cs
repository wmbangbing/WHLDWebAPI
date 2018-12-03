using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WHLDWebApi.Models;

namespace WHLDWebApi.Controllers
{
    public static class ObjectExtentions
    {
        public static string ToJsonString(this Object obj)
        {

            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return JsonConvert.SerializeObject(obj, jsSettings);
        }
    }

    public class ValuesController : ApiController
    {
        private DbEntities db = new DbEntities();

        // GET api/values
        public HttpResponseMessage Get(string form)
        {
            if (form == "task")
            {
                var tData = db.Tasks.ToList();

                //List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

                //foreach (var data in tData)
                //{
                //    Dictionary<string, object> dict = new Dictionary<string, object>();

                //    foreach (var p in data.GetType().GetProperties()) {
                //        object[] attrs = p.GetCustomAttributes(true);
                //    }

                //    foreach (System.Reflection.PropertyInfo p in data.GetType().GetProperties())
                //    {
                //        object[] attrs = p.GetCustomAttributes(true);
                //        foreach (object attr in attrs)
                //        {
                //            DisplayNameAttribute authAttr = attr as DisplayNameAttribute;
                //            if (authAttr != null)
                //            {
                //                string auth = authAttr.DisplayName;
                //                //typeof p.GetValue(data) property = p.GetValue(data);
                //                dict.Add(auth, p.GetValue(data));
                //            }
                //        }
                //    }
                //    list.Add(dict);
                //}

                //string jsonStr = JsonConvert.SerializeObject(list);

                return new HttpResponseMessage { Content = new StringContent(tData.ToJsonString(), System.Text.Encoding.UTF8, "application/json") };
            }
            else if (form == "XBInfo")
            {
                var tData = db.XBInfos.ToList();

                return new HttpResponseMessage { Content = new StringContent(tData.ToJsonString(), System.Text.Encoding.UTF8, "application/json") };
            }
            else {
                return new HttpResponseMessage { Content = new StringContent("no data!", System.Text.Encoding.UTF8, "application/json") };
            }
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
