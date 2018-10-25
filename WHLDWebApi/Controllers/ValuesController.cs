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
        public HttpResponseMessage Get()
        {
            var tData = db.Tasks.ToList();

            //var DBData = from r in db.Plans.ToList()
            //             select r;

            //List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            //foreach (var data in DBData)
            //{
            //    Dictionary<string, object> dict = new Dictionary<string, object>();
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

            //string jsonStr = JsonConvert.SerializeObject(tData);
            return new HttpResponseMessage { Content = new StringContent(tData.ToJsonString(), System.Text.Encoding.UTF8, "application/json") };
            //return jsonStr;
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
