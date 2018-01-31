using System;
using SupportClasses;
using SFGWrapper.SFG.HeartbeatSvc;
using SFGWrapper.Helpers;

namespace SFGWrapper
{
    /// <summary>
    /// Provides access to the SFG Heartbeat service to validate if service is available.
    /// </summary>
    public static class Heartbeat
    {
        private enum Methods { PING }
        private static BaseResponse GetResponse(Methods methodCalled, PingRequest ahRequest)
        {
            string className = "SFGWrapper.Heartbeat";
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                using (HeartbeatService svc = new HeartbeatService())
                {
                    svc.Timeout = 20000;
                    svc.Credentials = new System.Net.NetworkCredential(ahRequest.ServiceUsername, ahRequest.ServicePassword);
                    argtype sfgRequest = HeartbeatTranslators.TranslateToSfgRequest(ahRequest);
                    switch (methodCalled)
                    {
                        case Methods.PING:
                            baseResponse = HeartbeatTranslators.Ping(svc.process_wsdl(sfgRequest));
                            break;
                    }
                }
                if (baseResponse == null)
                {
                    baseResponse = new BaseResponse();
                    FatalErrorResponse fatalError = new FatalErrorResponse();
                    baseResponse.TypedResponse = fatalError;
                    baseResponse.Messages.Add(new Message("SFGFatalError"));
                }

            }
            catch (Exception ex)
            {
                baseResponse = new BaseResponse();
                FatalErrorResponse fatalError = new FatalErrorResponse();
                baseResponse.TypedResponse = fatalError;
                Message error = new Message("UnknownException");
                baseResponse.DebugStringLog.Add(ex.TargetSite.Name);
                baseResponse.DebugStringLog.Add(ex.Message);
                baseResponse.DebugStringLog.Add(ex.StackTrace);
                baseResponse.Messages.Add(error);
                EventLogger.LogError(string.Format("{0}.{1}()", new object[] { className, methodCalled.ToString() }),
                    string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
            return baseResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ahRequest"></param>
        /// <returns></returns>
        public static BaseResponse Ping(PingRequest ahRequest)
        {
            return GetResponse(Methods.PING, ahRequest);
        }
    }
}
