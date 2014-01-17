using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Common.Extensions;
using Pass.Processing.Web.Models;

namespace Pass.Processing.Web.Controllers
{
    [RoutePrefix("v1")]
    public class PassbookController : ApiController
    {
        [HttpPost]
        [Route("devices/{deviceLibraryIdentifier}/registrations/{passTypeIdentifier}/{serialNumber}")]
        public HttpResponseMessage RegisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, [FromBody]string pushTokenJson)
        {
            HttpResponseMessage response;
            try
            {
                string authHeader = HttpContext.Current.Request.Headers["Authorization"];

                //The Authorization header is supplied; its value is the word “ApplePass”, 
                //followed by a space, followed by the pass’s authorization token as specified in the pass.
                if (authHeader != null && authHeader.Contains("ApplePass "))
                {
                    string authToken = authHeader.Replace("ApplePass ", String.Empty);
                    var pushToken = pushTokenJson.JsonToObject<DevicePushToken>();

                    //TODO Call method with params: deviceLibraryIdentifier, passTypeIdentifier, serialNumber, pushToken, authToken

                    //If the serial number is already registered for this device, return HTTP status 200.
                    response = Request.CreateResponse(HttpStatusCode.OK);

                    //If registration succeeds, return HTTP status 201
                    response = Request.CreateResponse(HttpStatusCode.Created);
                }
                //If the request is not authorized, return HTTP status 401
                response = Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            catch
            {
                //Otherwise, return the appropriate standard HTTP status
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Device were not registered.");
            }
            return response;
        }

        [HttpDelete]
        [Route("devices/{deviceLibraryIdentifier}/registrations/{passTypeIdentifier}/{serialNumber}")]
        public HttpResponseMessage UnregisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, [FromBody]string pushTokenJson)
        {
            HttpResponseMessage response;
            try
            {
                string authHeader = HttpContext.Current.Request.Headers["Authorization"];

                //The Authorization header is supplied; its value is the word “ApplePass”, 
                //followed by a space, followed by the pass’s authorization token as specified in the pass.
                if (authHeader != null && authHeader.Contains("ApplePass "))
                {
                    string authToken = authHeader.Replace("ApplePass ", String.Empty);
                    var pushToken = pushTokenJson.JsonToObject<DevicePushToken>();

                    //TODO Call method with params: deviceLibraryIdentifier, passTypeIdentifier, serialNumber, authToken

                    //If the device is unregistered, return HTTP status 200.
                    response = Request.CreateResponse(HttpStatusCode.OK);
                }
                //If the request is not authorized, return HTTP status 401
                response = Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            catch
            {
                //Otherwise, return the appropriate standard HTTP status
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Device were not unregistered.");
            }
            return response;
        }
       
        //Getting the Serial Numbers for Passes Associated with a Device
        [HttpGet]
        [Route("devices/{deviceLibraryIdentifier}/registrations/{passTypeIdentifier}")]
        public HttpResponseMessage GetSerialNumbersOfPasses(string deviceLibraryIdentifier, string passTypeIdentifier, string passesUpdatedSince = null)
        {
            HttpResponseMessage response;
            try
            {
                //If the passesUpdatedSince parameter is present, return only the passes that have been updated since
                //the time indicated by tag. Otherwise, return all passes.
                DateTime passesUpdatedSinceTime;
                DateTime.TryParse(passesUpdatedSince, out passesUpdatedSinceTime);

                //TODO Call method with params: deviceLibraryIdentifier, passTypeIdentifier, passesUpdatedSinceTime
                //Return:
                //{ 
                    //“serialNumbers” : [ <serialNo.>, <serialNo.>, ... ],
                    //“lastUpdated” : <tag>
                //}

                //If there are matching passes, return HTTP status 200 with a JSON dictionary
                response = Request.CreateResponse(HttpStatusCode.OK, new object());

                //If there are no matching passes, return HTTP status 204.
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch
            {
                //Otherwise, return the appropriate standard HTTP status
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Device were not registered.");
            }
            return response;
        }
        
        [HttpGet]
        [Route("passes/{passTypeIdentifier}/{serialNumber}")]
        public HttpResponseMessage GetPass(string passTypeIdentifier, string serialNumber)
        {
            HttpResponseMessage response;
            try
            {
                string authHeader = HttpContext.Current.Request.Headers["Authorization"];

                //The Authorization header is supplied; its value is the word “ApplePass”, 
                //followed by a space, followed by the pass’s authorization token as specified in the pass.
                if (authHeader != null && authHeader.Contains("ApplePass "))
                {
                    string authToken = authHeader.Replace("ApplePass ", String.Empty);

                    //TODO Call method with params:  passTypeIdentifier, serialNumber

                    //Support standard HTTP caching on this endpoint: check for the If-Modified-Since header and return HTTP
                    //status code 304 if the pass has not changed.
                    string ifModifiedSinceHeader = HttpContext.Current.Request.Headers["If-Modified-Since"];
                    response = Request.CreateResponse(HttpStatusCode.NotModified);

                    //If the serial number is already registered for this device, return HTTP status 200.
                    response = Request.CreateResponse(HttpStatusCode.OK);

                    //If registration succeeds, return HTTP status 201
                    response = Request.CreateResponse(HttpStatusCode.Created);
                }
                //If the request is not authorized, return HTTP status 401
                response = Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            catch
            {
                //Otherwise, return the appropriate standard HTTP status
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Device were not registered.");
            }
            return response;
        }

        [HttpPost]
        [Route("log")]
        public HttpResponseMessage Log([FromBody]string logMessage)
        {
            HttpResponseMessage response;
            try
            {
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                //Otherwise, return the appropriate standard HTTP status
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Device were not registered.");
            }
            return response;
        }
    }
}
