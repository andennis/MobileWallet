using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
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
                if (authToken == null)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);

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

                PassProcessingStatus status = _passProcessingService.UnregisterDevice(deviceLibraryIdentifier, passTypeIdentifier, serialNumber, authToken);

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
        public HttpResponseMessage GetSerialNumbersOfPasses(string deviceLibraryIdentifier, object passTypeIdentifier, [FromUri]string passesUpdatedSince = null)
        {
            try
            {
                //Temporary solution to get passTypeIdentifier
                if (passTypeIdentifier == null)
                {
                    const string regStr = "/registrations/";
                    string uri = Request.RequestUri.AbsoluteUri;
                    string passTypeIdentifierStr = uri.Remove(0, uri.LastIndexOf(regStr, StringComparison.InvariantCulture) + regStr.Length);
                    bool isPassesUpdatedSince = passTypeIdentifierStr.IndexOf("/", StringComparison.InvariantCulture) > 0;
                    passTypeIdentifier = isPassesUpdatedSince 
                        ? passTypeIdentifierStr.Remove(passTypeIdentifierStr.IndexOf("/", StringComparison.InvariantCulture))
                        : passTypeIdentifierStr;
                }
                //If the passesUpdatedSince parameter is present, return only the passes that have been updated since
                //the time indicated by tag. Otherwise, return all passes for specified PassTypeId.
                DateTime passesUpdatedSinceTime;
                DateTime.TryParse(passesUpdatedSince, out passesUpdatedSinceTime);//TODO check date format

                ChangedPassesInfo changedPassesInfo;
                PassProcessingStatus status = _passProcessingService.GetChangedPasses(deviceLibraryIdentifier, passTypeIdentifier.ToString(), passesUpdatedSinceTime, out changedPassesInfo);
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
                            serialNumbers = string.Join(", ", changedPassesInfo.SerialNumbers),
                            lastUpdated = changedPassesInfo.LastUpdated
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
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StreamContent(stream);
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        response.Content.Headers.LastModified = packageInfo.UpdatedDate;
                        return response;

                    case PassProcessingStatus.NotModified:
                        return Request.CreateResponse(HttpStatusCode.NotModified);
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
