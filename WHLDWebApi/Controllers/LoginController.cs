using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WHLDWebApi.Models;

namespace WHLDWebApi.Controllers
{
    public class LoginController : ApiController
    {
        private DbEntities db = new DbEntities();

        [System.Web.Http.HttpGet]
        public HttpResponseMessage Login(string username,string password)
        {
            var tData = db.Users;
            foreach (var data in tData) {
                if (data.UserName == username)
                {
                    if (data.PassWord == password)
                    {
                        return new HttpResponseMessage { Content = new StringContent(data.Remark.ToJsonString(), System.Text.Encoding.UTF8, "application/json") };
                    }
                    else
                    {
                        throw new NotImplementedException("密码错误");
                    }
                }
                //else {
                //    throw new NotImplementedException("用户名不存在");
                //}
            }
            throw new NotImplementedException("用户名不存在");
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetUserinfo(string token)
        {
            var tData = db.Users;

            var userInfo = tData.Where(a => a.Remark == token).ToArray();

            return new HttpResponseMessage { Content = new StringContent(userInfo.ToJsonString(), System.Text.Encoding.UTF8, "application/json") };
        }

        [System.Web.Http.HttpPost]
        public string Logout() {
            return "success";
        }
    }
}