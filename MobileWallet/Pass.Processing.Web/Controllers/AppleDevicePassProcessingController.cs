using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Enums;
using Pass.Processing.Web.Models;

namespace Pass.Processing.Web.Controllers
{
    [RoutePrefix("v1")]
    public class AppleDevicePassProcessingController : ApiController
    {
        private readonly IApplePassProcessingService _passProcessingService;

        public AppleDevicePassProcessingController(IApplePassProcessingService passProcessingService)
        {
            _passProcessingService = passProcessingService;
        }

        [HttpPost]
        [Route("devices/{deviceLibraryIdentifier}/registrations/{passTypeIdentifier}/{serialNumber}")]
        public HttpResponseMessage RegisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, [FromBody]DevicePushToken devicePushToken)
        {
            try
            {
                string authToken = GetAuthTokenFromRequestHeader();
                if (authToken == null)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);

                PassProcessingStatus status;
                _passProcessingService.RegisterDevice(deviceLibraryIdentifier, passTypeIdentifier, serialNumber, devicePushToken.PushToken, authToken, out status);

                switch (status)
                {
                    case PassProcessingStatus.AlreadyDone:
                        //If the serial number is already registered for this device, return HTTP status 200.
                        return Request.CreateResponse(HttpStatusCode.OK);
                    case PassProcessingStatus.Succeed:
                        //If registration succeeds, return HTTP status 201
                        return Request.CreateResponse(HttpStatusCode.Created);
                    default:
                        return GetStandardResponse(status, "Device registration failed");
                        /*
                    case PassProcessingStatus.Unauthorized:
                        response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                        break;
                    case PassProcessingStatus.NotFound:
                        response = Request.CreateResponse(HttpStatusCode.NotFound);
                        break;
                    case PassProcessingStatus.Error:
                        response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Device were not registered.");
                        break;
                        */
                }
            }
            catch
            {
                //Otherwise, return the appropriate standard HTTP status
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Device were not registered.");
            }
        }

        [HttpDelete]
        [Route("devices/{deviceLibraryIdentifier}/registrations/{passTypeIdentifier}/{serialNumber}")]
        public HttpResponseMessage UnregisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber/*, [FromBody]DevicePushToken devicePushToken*/)
        {
            try
            {
                string authToken = GetAuthTokenFromRequestHeader();
                if (authToken == null)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);

                PassProcessingStatus status;
                _passProcessingService.UnregisterDevice(deviceLibraryIdentifier, passTypeIdentifier, serialNumber, authToken, out status);

                if (status == PassProcessingStatus.Succeed)
                    return Request.CreateResponse(HttpStatusCode.OK);

                return GetStandardResponse(status, "Device unregistration failed");

                /*
                if (status == PassProcessingStatus.Unauthorized)
                    response = Request.CreateResponse(HttpStatusCode.Unauthorized);

                if (status == PassProcessingStatus.NotFound)
                    response = Request.CreateResponse(HttpStatusCode.NotFound);

                if (status == PassProcessingStatus.Error)
                    response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        "Device were not unregistered.");
                */
            }
            catch
            {
                //Otherwise, return the appropriate standard HTTP status
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Device were not unregistered.");
            }
        }
       
        //Getting the Serial Numbers for Passes Associated with a Device
        [HttpGet]
        [Route("devices/{deviceLibraryIdentifier}/registrations/{passTypeIdentifier}")]
        public HttpResponseMessage GetSerialNumbersOfPasses(string deviceLibraryIdentifier, string passTypeIdentifier, string passesUpdatedSince = null)
        {
            try
            {
                //If the passesUpdatedSince parameter is present, return only the passes that have been updated since
                //the time indicated by tag. Otherwise, return all passes for specified PassTypeId.
                DateTime passesUpdatedSinceTime;
                DateTime.TryParse(passesUpdatedSince, out passesUpdatedSinceTime);//TODO check date format

                PassProcessingStatus status;
                DateTime lastUpdated;
                IList<string> serialNumbers = _passProcessingService.GetSerialNumbersOfPasses(deviceLibraryIdentifier, passTypeIdentifier, out lastUpdated, out status, passesUpdatedSinceTime);
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
                            serialNumbers = string.Join(", ", serialNumbers),
                            lastUpdated
                        };
                    if (obj.serialNumbers.Length > 0)
                        obj.serialNumbers.Remove(obj.serialNumbers.Length - 1);

                    return Request.CreateResponse(HttpStatusCode.OK, obj);
                }

                //If there are no matching passes, return HTTP status 204.
                return GetStandardResponse(status, "Retrieving of serial numbers failed");
                /*
                if (status == PassProcessingStatus.NoContent)
                    response = Request.CreateResponse(HttpStatusCode.NoContent);

                if (status == PassProcessingStatus.NotFound)
                    response = Request.CreateResponse(HttpStatusCode.NotFound);

                if (status == PassProcessingStatus.Error)
                    response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Server error.");
                */
            }
            catch
            {
                //Otherwise, return the appropriate standard HTTP status
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Server Error.");
            }
        }
        
        [HttpGet]
        [Route("passes/{passTypeIdentifier}/{serialNumber}")]
        public HttpResponseMessage GetPass(string passTypeIdentifier, string serialNumber)
        {
            try
            {
                string authToken = GetAuthTokenFromRequestHeader();
                if (authToken == null)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);

                //Support standard HTTP caching on this endpoint: check for the If-Modified-Since header and return HTTP
                //status code 304 if the pass has not changed.
                string modifiedSince = HttpContext.Current.Request.Headers["If-Modified-Since"];
                DateTime? dtModifiedSince = !string.IsNullOrEmpty(modifiedSince) ? Convert.ToDateTime(modifiedSince) : (DateTime?)null;

                PassProcessingStatus status;
                string pkpassFilePath = _passProcessingService.GetPass(passTypeIdentifier, serialNumber, authToken, out status, dtModifiedSince);

                switch (status)
                {
                    case PassProcessingStatus.Succeed:
                        //If request is authorized, return HTTP status 200 with a payload of the pass data.
                        var stream = new FileStream(pkpassFilePath, FileMode.Open);
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StreamContent(stream);
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        return response;

                    case PassProcessingStatus.NotModified:
                        HttpResponseMessage response2 = Request.CreateResponse(HttpStatusCode.NotModified);
                        //response2.Headers.Add("Last-modified", );
                        return response2;

                    default:
                        return GetStandardResponse(status, "Getting pass failed");
                }
                
                /*
                if (status == PassProcessingStatus.Unauthorized)
                    response = Request.CreateResponse(HttpStatusCode.Unauthorized);

                if (status == PassProcessingStatus.NotFound)
                    response = Request.CreateResponse(HttpStatusCode.NotFound);

                if (status == PassProcessingStatus.Error)
                    response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Server Error.");
                */
            }
            catch
            {
                //Otherwise, return the appropriate standard HTTP status
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Server Error");
            }
        }

        [HttpPost]
        [Route("log")]
        public HttpResponseMessage Log([FromBody]LogInformation logMessage)
        {
            try
            {
                //var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
                //bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                //var bodyText = bodyStream.ReadToEnd();
                //_passProcessingService.Log(logMessage.Logs[0]);
            }
            catch (Exception)
            {
                //Todo write to log
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private string GetAuthTokenFromRequestHeader()
        {
            //The Authorization header is supplied; its value is the word “ApplePass”, 
            //followed by a space, followed by the pass’s authorization token as specified in the pass.
            string authHeader = HttpContext.Current.Request.Headers["Authorization"];
            const string AuthApplePass = "ApplePass";
            if (authHeader == null || !authHeader.StartsWith(AuthApplePass) || AuthApplePass.Length + 2 > authHeader.Length)
                return null;

            return authHeader.Substring(AuthApplePass.Length + 1);
        }
        HttpResponseMessage GetStandardResponse(PassProcessingStatus status, string errorMsg)
        {
            switch (status)
            {
                case PassProcessingStatus.Unauthorized:
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);
                case PassProcessingStatus.NotFound:
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                case PassProcessingStatus.NoContent:
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                case PassProcessingStatus.Error:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMsg);
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("Wrong status '{0}'", status));
            }
        }
    }
}
