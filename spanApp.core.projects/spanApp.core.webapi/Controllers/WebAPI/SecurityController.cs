using Newtonsoft.Json.Linq;
using spanApp.core.foundation.BusinessObjects.Account;
using spanApp.core.projects.BAL.Manager;
using spanApp.core.webapi.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace spanApp.core.webapi.Controllers.WebAPI
{
    [RoutePrefix("security")]
    public class SecurityController : ApiController
    {
        SecurityManager manager = null;
        [HttpPost]
        [Route("checkUser")]       
        public bool CheckIfUserExists(WebAPIRequest request) {
            bool isUserExists = false;
            try
            {
                string emailAddress = request.ValueOf<string>("EmailAddress");
                string phoneNumber = request.ValueOf<string>("PhoneNumber");
                manager = new SecurityManager();

                isUserExists = manager.CheckIfUserExists(emailAddress, phoneNumber);
            }
            catch (Exception ex)
            {
                isUserExists = false;
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
            return isUserExists;
        }


        [HttpPost]
        [Route("register")]
        public UserAccount Register(WebAPIRequest request)
        {
            UserAccount account = null;
            try
            {                
                manager = new SecurityManager();
                account = new UserAccount();
                account = manager.Register(request.ValueOf<UserAccount>("UserAccount"));
            }
            catch (Exception ex)
            {
                account = null;
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
            return account;
        }

        [HttpPost]
        [Route("login")]
        public JObject Login(WebAPIRequest request)
        {
            JObject sessionData = null;
            try
            {
                manager = new SecurityManager();
                sessionData = new JObject();
                sessionData = manager.Login(request.ValueOf<string>("LoginName"), request.ValueOf<string>("Password"));
            }
            catch (Exception ex)
            {
                sessionData = null;
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
            return sessionData;
        }
    }
}
