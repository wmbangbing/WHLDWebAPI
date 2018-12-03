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
    public class HistXBInfoController : ApiController
    {
        private DbEntities db = new DbEntities();

        public HttpResponseMessage GetHistoryTime()
        {
            var timeList = db.HistXBInfos.Select(s => new { s.Time }).ToList().Distinct().ToList();

            return new HttpResponseMessage { Content = new StringContent(timeList.ToJsonString(), System.Text.Encoding.UTF8, "application/json") };
        }

        public HttpResponseMessage GetXBInfoByTime(string Time)
        {
            var time = Convert.ToDateTime(Time);
            var transitionList = db.HistXBInfos.Where(a => a.Time <= time).ToList();

            //只返回了最大值
            //var dataList = transitionList.GroupBy(m => m.XBH).Select(m => m.Maold(o => o.Time));

            //返回最大值对应的行
            var dataList = transitionList.GroupBy(m => m.XBH).Select(g => g.OrderByDescending(e => e.Time).FirstOrDefault()).ToList();

            var newData = db.XBInfos.ToList();

            XBInfo config = new XBInfo();
            PropertyInfo[] propertys = config.GetType().GetProperties();

            foreach (XBInfo row in newData)
            {
                foreach (var old in dataList)
                {
                    if (row.XBH == old.XBH)
                    {     
                        row.DWMC = old.DWMC;
                        row.XIANG = old.XIANG;
                        row.CUN = old.CUN;
                        row.XBMJ = old.XBMJ;
                        row.PD = old.PD;
                        row.HB = old.HB;
                        row.JD = old.JD;
                        row.WD = old.WD;
                        row.LDQS = old.LDQS;
                        row.LMQS = old.LMQS;
                        row.DL = old.DL;
                        row.LL = old.LL;
                        row.QLLX = old.QLLX;
                        row.ZLSJ = old.ZLSJ;
                        row.SZZC = old.SZZC;
                        row.YSSZ = old.YSSZ;
                        row.PJSG = old.PJSG;
                        row.PJXJ = old.PJXJ;
                        row.HJSZ = old.HJSZ;
                        row.HJPJSG = old.HJPJSG;
                        row.HJPJXJ = old.HJPJXJ;
                        row.MMXJ = old.MMXJ;
                        row.LZU = old.LZU;
                        row.MMZS = old.MMZS;
                        row.YBD = old.YBD;
                        row.SLJKDJ = old.SLJKDJ;
                        row.QLJG = old.QLJG;
                        row.GMZL = old.GMZL;
                        row.GMGD = old.GMGD;
                        row.CBZL = old.CBZL;
                        row.CBGD = old.CBGD;
                        row.ZBGD = old.ZBGD;
                        row.RWGR = old.RWGR;
                        row.YYGHCS = old.YYGHCS;
                        row.GHXS = old.GHXS;
                        row.Area = old.Area;
                        row.Merge = old.Merge;
                    }
                }
            }

            return new HttpResponseMessage { Content = new StringContent(newData.ToJsonString(), System.Text.Encoding.UTF8, "application/json") };
        }
    }
}
