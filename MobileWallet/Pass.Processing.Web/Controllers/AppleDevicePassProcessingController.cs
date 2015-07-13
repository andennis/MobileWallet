using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Common.Extensions;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Processing.Web.Models;

namespace Pass.Processing.Web.Controllers
{
    [RoutePrefix("v1")]
    public class AppleDevicePassProcessingController : ApiController
    {
        private readonly IPassProcessingAppleService _passProcessingService;

        public AppleDevicePassProcessingController(IPassProcessingAppleService passProcessingService)
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
                PassProcessingStatus status = _passProcessingService.RegisterDevice(deviceLibraryIdentifier, passTypeIdentifier, serialNumber, devicePushToken.PushToken, authToken);

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
                PassProcessingStatus status = _passProcessingService.UnregisterDevice(deviceLibraryIdentifier, passTypeIdentifier, serialNumber, authToken);

                if (status == PassProcessingStatus.Succeed)
                    return Request.CreateResponse(HttpStatusCode.OK);

                return GetStandardResponse(status, "Device unregistration failed");
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
        public HttpResponseMessage GetSerialNumbersOfPasses(string deviceLibraryIdentifier, string passTypeIdentifier, /*[FromUri]*/string passesUpdatedSince = null)
        {
            try
            {
                //If the passesUpdatedSince parameter is present, return only the passes that have been updated since
                //the time indicated by tag. Otherwise, return all passes for specified PassTypeId.
                DateTime? passesUpdatedSinceTime = null;
                if (!string.IsNullOrEmpty(passesUpdatedSince))
                {
                    long unixSeconds = Convert.ToInt64(passesUpdatedSince) + 1;//add one second to "truncate" miliseconds in Pass.UpdatedDate
                    passesUpdatedSinceTime = unixSeconds.UnixTimeSecondsToDateTime().ToLocalTime();
                }

                ChangedPassesInfo changedPassesInfo;
                PassProcessingStatus status = _passProcessingService.GetChangedPasses(deviceLibraryIdentifier, passTypeIdentifier, passesUpdatedSinceTime, out changedPassesInfo);

                //If there are matching passes, return HTTP status 200 with a JSON dictionary
                if (status == PassProcessingStatus.Succeed)
                {
                    var obj = new 
                        {
                            serialNumbers = changedPassesInfo.SerialNumbers.ToArray(),
                            lastUpdated = changedPassesInfo.LastUpdated.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture)
                        };

                    return Request.CreateResponse(HttpStatusCode.OK, obj, new JsonMediaTypeFormatter());
                }

                //If there are no matching passes, return HTTP status 204.
                return GetStandardResponse(status, "Retrieving of serial numbers failed");
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

                //Support standard HTTP caching on this endpoint: check for the If-Modified-Since header and return HTTP
                //status code 304 if the pass has not changed.
                DateTime? dtModifiedSince = this.Request.Headers.IfModifiedSince.HasValue
                    ? this.Request.Headers.IfModifiedSince.Value.LocalDateTime
                    : (DateTime?)null;

                PassPackageInfo packageInfo;
                PassProcessingStatus status = _passProcessingService.GetPassPackage(passTypeIdentifier, serialNumber, authToken, dtModifiedSince, out packageInfo);

                switch (status)
                {
                    case PassProcessingStatus.Succeed:
                        //If request is authorized, return HTTP status 200 with a payload of the pass data.
                        var stream = new FileStream(packageInfo.PackageFilePath, FileMode.Open);
                        //var stream = new FileStream(@"d:\Temp\PassBook\sdx2uusd.pkpass", FileMode.Open);
                        
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StreamContent(stream);
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.apple.pkpass");
                        response.Content.Headers.LastModified = packageInfo.UpdatedDate.ToUniversalTime();
                        response.Content.Headers.ContentLength = stream.Length;
                        
                        return response;

                    case PassProcessingStatus.NotModified:
                        return Request.CreateResponse(HttpStatusCode.NotModified);
                    default:
                        return GetStandardResponse(status, "Getting pass failed");
                }
                
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
                //TODO write to log
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private string GetAuthTokenFromRequestHeader()
        {
            //The Authorization header is supplied; its value is the word “ApplePass”, 
            //followed by a space, followed by the pass’s authorization token as specified in the pass.
            AuthenticationHeaderValue authHeader = this.Request.Headers.Authorization;
            if (authHeader == null || authHeader.Scheme != "ApplePass")
                return null;

            return authHeader.Parameter;
        }
        private HttpResponseMessage GetStandardResponse(PassProcessingStatus status, string errorMsg)
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
