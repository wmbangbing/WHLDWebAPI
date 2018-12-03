using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using WHLDWebApi.Models;


namespace WHLDWebApi.Controllers
{
    public class XBInfoController : ApiController
    {
        private DbEntities db = new DbEntities();
        // GET: api/XBInfo
        public HttpResponseMessage Get() {
            var tData = db.XBInfos.ToList();
            return new HttpResponseMessage { Content = new StringContent(tData.ToJsonString(), System.Text.Encoding.UTF8, "application/json") };
        }
        //public IEnumerable<XBInfo> Get()
        //{
        //    var query = db.XBInfos;
        //    return query;
        //}

        // GET: api/XBInfo/5
        public HttpResponseMessage GetByRole(string role)
        {
            var tData = db.XBInfos.ToList();

            if (role == "All")
            {
                return new HttpResponseMessage { Content = new StringContent(tData.ToJsonString(), System.Text.Encoding.UTF8, "application/json") };
            }
            else {
                tData = db.XBInfos.Where(v => v.DWMC == role).ToList();
            }
            return new HttpResponseMessage { Content = new StringContent(tData.ToJsonString(), System.Text.Encoding.UTF8, "application/json") };
        }

        // POST: api/XBInfo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/XBInfo/5
        public void Put([FromBody]JObject json)
        {
            //db.Configuration.ValidateOnSaveEnabled = false;
            //XBInfo newLine = new XBInfo
            //{
            //    XBH = json.Value<int>("XBH"),

            //};
            var s = json.Value<Int32>("XBH");
            XBInfo x = db.XBInfos.Where(a => a.XBH == s).FirstOrDefault();
            HistXBInfo H = new HistXBInfo()
            {
                XBH= x.XBH,
                DWMC = x.DWMC,
                XIANG = x.XIANG,
                CUN = x.CUN,
                XBMJ= x.XBMJ,
                PD = x.PD,
                HB = x.HB,
                JD = x.JD,
                WD = x.WD,
                LDQS = x.LDQS,
                LMQS = x.LMQS,
                DL = x.DL,
                LL = x.LL,
                QLLX = x.QLLX,
                ZLSJ = x.ZLSJ,
                SZZC = x.SZZC,
                YSSZ = x.YSSZ,
                PJSG = x.PJSG,
                PJXJ = x.PJXJ,
                HJSZ = x.HJSZ,
                HJPJSG = x.HJPJSG,
                HJPJXJ = x.HJPJXJ,
                MMXJ = x.MMXJ,
                LZU = x.LZU,
                MMZS = x.MMZS,
                YBD = x.YBD,
                SLJKDJ = x.SLJKDJ,
                QLJG = x.QLJG,
                GMZL = x.GMZL,
                GMGD = x.GMGD,
                CBZL = x.CBZL,
                CBGD = x.CBGD,
                ZBGD = x.ZBGD,
                RWGR = x.RWGR,
                YYGHCS = x.YYGHCS,
                GHXS = x.GHXS,
                Area = x.Area,
                Merge = x.Merge,      
                Time = DateTime.Now
            };

            db.HistXBInfos.Add(H);

            x.SLJKDJ = json.Value<string>("SLJKDJ");
            x.PJSG = json.Value<double>("PJSG");
            x.HJSZ = json.Value<string>("HJSZ");
            x.MMZS = json.Value<double>("MMZS");
            
            db.SaveChanges();
        }

        // DELETE: api/XBInfo/5
        public void Delete(int id)
        {
        }
    }
}
