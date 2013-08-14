﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Twilio.Mvc;
using Twilio.TwiML;
using TwilioPOC.Data;

namespace TwilioPOC.Controllers
{
    public class CallLookupController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post(VoiceRequest request)
        {
            var response = new TwilioResponse();
            var option = request.Digits;

            var item = DataStore.Instance.GetItem(option);

            if (item == null)
            {
                response.Say("Item not found.");
                response.Redirect("api/CallHome");
            } else
            {
                response.Say(string.Format("The status of item {0} is {1}.", item.Id, item.Status));
            }
            
            return this.Request.CreateResponse(
                HttpStatusCode.OK, response.Element, new XmlMediaTypeFormatter());
        }
    }
}