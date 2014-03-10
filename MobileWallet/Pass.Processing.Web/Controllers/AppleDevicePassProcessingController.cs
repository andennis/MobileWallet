using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Common.Extensions;
using FileStorage.BL;
using Pass.Container.BL;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Factory;
using Pass.Processing.Web.Models;

namespace Pass.Processing.Web.Controllers
{
    [RoutePrefix("v1")]
    public class AppleDevicePassProcessingController : ApiController
    {
        [HttpPost]
        [Route("devices/{deviceLibraryIdentifier}/registrations/{passTypeIdentifier}/{serialNumber}")]
        public HttpResponseMessage RegisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, [FromBody]string pushTokenJson)
        {
            HttpResponseMessage response = null;
            try
            {
                string authHeader = HttpContext.Current.Request.Headers["Authorization"];

                //The Authorization header is supplied; its value is the word “ApplePass”, 
                //followed by a space, followed by the pass’s authorization token as specified in the pass.
                if (authHeader != null && authHeader.Contains("ApplePass "))
                {
                    string authToken = authHeader.Replace("ApplePass ", String.Empty);
                    var pushToken = pushTokenJson.JsonToObject<DevicePushToken>();

                    PassProcessingStatus status;
                    using (IApplePassProcessingService passProcessingService = GetAppleDevicePassProcessingService())
                    {
                        passProcessingService.RegisterDevice(deviceLibraryIdentifier, passTypeIdentifier, serialNumber, pushToken.PushToken, authToken, out status);
                    }
                    

                    //If the serial number is already registered for this device, return HTTP status 200.
                    if (status == PassProcessingStatus.AlreadyDone)
                        response = Request.CreateResponse(HttpStatusCode.OK);

                    //If registration succeeds, return HTTP status 201
                    if (status == PassProcessingStatus.Succeed)
                        response = Request.CreateResponse(HttpStatusCode.Created);

                    if (status == PassProcessingStatus.Unauthorized)
                        response = Request.CreateResponse(HttpStatusCode.Unauthorized);

                    if (status == PassProcessingStatus.NotFound)
                        response = Request.CreateResponse(HttpStatusCode.NotFound);

                    if (status == PassProcessingStatus.Error)
                        response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Device were not registered.");
                }
                else//If the request is not authorized, return HTTP status 401
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
            HttpResponseMessage response = null;
            try
            {
                string authHeader = HttpContext.Current.Request.Headers["Authorization"];

                //The Authorization header is supplied; its value is the word “ApplePass”, 
                //followed by a space, followed by the pass’s authorization token as specified in the pass.
                if (authHeader != null && authHeader.Contains("ApplePass "))
                {
                    string authToken = authHeader.Replace("ApplePass ", String.Empty);
                    var pushToken = pushTokenJson.JsonToObject<DevicePushToken>();

                    PassProcessingStatus status;
                    using (IApplePassProcessingService passProcessingService = GetAppleDevicePassProcessingService())
                    {
                        passProcessingService.UnregisterDevice(deviceLibraryIdentifier, passTypeIdentifier, serialNumber, pushToken.PushToken, authToken, out status);
                    }

                    //If the device is unregistered, return HTTP status 200.
                    if (status == PassProcessingStatus.Succeed)
                        response = Request.CreateResponse(HttpStatusCode.OK);

                    if (status == PassProcessingStatus.Unauthorized)
                        response = Request.CreateResponse(HttpStatusCode.Unauthorized);

                    if (status == PassProcessingStatus.NotFound)
                        response = Request.CreateResponse(HttpStatusCode.NotFound);

                    if (status == PassProcessingStatus.Error)
                        response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Device were not unregistered.");
                }
                else//If the request is not authorized, return HTTP status 401
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
            HttpResponseMessage response = null;
            try
            {
                //If the passesUpdatedSince parameter is present, return only the passes that have been updated since
                //the time indicated by tag. Otherwise, return all passes.
                DateTime passesUpdatedSinceTime;
                DateTime.TryParse(passesUpdatedSince, out passesUpdatedSinceTime);

                PassProcessingStatus status;
                var lastUpdated = new DateTime();
                IList<string> passSerialNumbers;
                using (IApplePassProcessingService passProcessingService = GetAppleDevicePassProcessingService())
                {
                   passSerialNumbers = passProcessingService.GetSerialNumbersOfPasses(deviceLibraryIdentifier, passTypeIdentifier, ref lastUpdated, out status, passesUpdatedSinceTime);
                }
                //Return:
                //{ 
                    //“serialNumbers” : [ <serialNo.>, <serialNo.>, ... ],
                    //“lastUpdated” : <tag>
                //}

                //If there are matching passes, return HTTP status 200 with a JSON dictionary
                if (status == PassProcessingStatus.Succeed)
                {
                    var obj = new 
                        {
                            //serialNumbers = String.Join(", ", passSerialNumbers.ToArray()),
                            lastUpdated = lastUpdated
                        };
                    response = Request.CreateResponse(HttpStatusCode.OK, obj);
                }

                //If there are no matching passes, return HTTP status 204.

                if (status == PassProcessingStatus.NoContent)
                    response = Request.CreateResponse(HttpStatusCode.NoContent);

                if (status == PassProcessingStatus.NotFound)
                    response = Request.CreateResponse(HttpStatusCode.NotFound);

                if (status == PassProcessingStatus.Error)
                    response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Server error.");
            }
            catch
            {
                //Otherwise, return the appropriate standard HTTP status
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Server Error.");
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

                    PassProcessingStatus status;
                    using (IApplePassProcessingService passProcessingService = GetAppleDevicePassProcessingService())
                    {
                        ////TODO GET PASS STREAM
                        passProcessingService.GetPass(passTypeIdentifier, serialNumber, authToken, out status);
                    }

                    //Support standard HTTP caching on this endpoint: check for the If-Modified-Since header and return HTTP
                    //status code 304 if the pass has not changed.
                    string ifModifiedSinceHeader = HttpContext.Current.Request.Headers["If-Modified-Since"];
                    response = Request.CreateResponse(HttpStatusCode.NotModified);

                    //If request is authorized, return HTTP status 200 with a payload of the pass data.
                    if (status == PassProcessingStatus.Succeed)
                        response = Request.CreateResponse(HttpStatusCode.OK);

                    if (status == PassProcessingStatus.Unauthorized)
                        response = Request.CreateResponse(HttpStatusCode.Unauthorized);

                    if (status == PassProcessingStatus.NotFound)
                        response = Request.CreateResponse(HttpStatusCode.NotFound);

                    if (status == PassProcessingStatus.Error)
                        response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Server Error.");
                }
                else//If the request is not authorized, return HTTP status 401
                    response = Request.CreateResponse(HttpStatusCode.Unauthorized);

                //var stream = new FileStream(path, FileMode.Open);
                //result.Content = new StreamContent(stream);
                //result.Content.Headers.ContentType =
                //    new MediaTypeHeaderValue("application/octet-stream");
                //return result;
            }
            catch
            {
                //Otherwise, return the appropriate standard HTTP status
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Server Error");
            }
            return response;
        }

        [HttpPost]
        [Route("log")]
        public HttpResponseMessage Log([FromBody]string logMessage)
        {
            try
            {
                using (IApplePassProcessingService passProcessingService = GetAppleDevicePassProcessingService())
                {
                    passProcessingService.Log(logMessage);
                }
            }
            catch (Exception)
            {
                //Todo write to log
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private IApplePassProcessingService GetAppleDevicePassProcessingService()
        {
            return PassContainerFactory.CreateAppleDevicePassProcessingService(new PassContainerConfig(), new FileStorageConfig());
        }
    }
}
