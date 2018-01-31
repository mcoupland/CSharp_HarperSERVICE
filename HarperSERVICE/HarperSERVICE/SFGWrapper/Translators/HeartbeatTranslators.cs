using System;
using SFGWrapper.SFG.HeartbeatSvc;
using SupportClasses;

namespace SFGWrapper.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class HeartbeatTranslators
    {
        private enum Methods { PING }
        private static BaseResponse GetResponse(Methods methodCalled, returntype sfgReturn)
        {
            string className = "SFGWrapper.HeartbeatTranslators";
            BaseResponse baseResponse = new BaseResponse();
            foreach (var item in sfgReturn.error)
            {
                Message ahError = new Message(item.errno, MessageSources.Heartbeat);
                foreach (string message in item.errmsg)
                {
                    ahError.SfgMessages.Add(message);
                }
                baseResponse.Messages.Add(ahError);
            }

            switch (methodCalled)
            {
                case Methods.PING:
                    PingResponse heartbeatServiceResponse = new PingResponse();
                    heartbeatServiceResponse.Success = sfgReturn.success;
                    baseResponse.TypedResponse = heartbeatServiceResponse;
                    break;
            }
            baseResponse.TypedResponse.Success = sfgReturn.success;
            baseResponse.TypedResponse.Info = Utilities.GetInfo(sfgReturn.response.INFO);
            baseResponse.TypedResponse.MemoryUsed = sfgReturn.response.MEMORY_USED;
            baseResponse.TypedResponse.Protocol = sfgReturn.response.PROTOCOL;
            baseResponse.TypedResponse.RoundtripTime = sfgReturn.response.ROUNDTRIP_TIME;
            baseResponse.TypedResponse.Server = sfgReturn.response.SERVER;
            baseResponse.TypedResponse.TimeElapsed = sfgReturn.response.TIME_ELAPSED;
            baseResponse.TypedResponse.Version = sfgReturn.response.VERSION;

            return baseResponse;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ahRequest"></param>
        /// <returns></returns>
        public static argtype TranslateToSfgRequest(PingRequest ahRequest)
        {
            EventLogger.LogEvent("ENTERING -> SFGWrapper.HeartbeatTranslators.TranslateToSfgRequest()");
            argtype sfgRequest = new argtype();

            #region readonly properties
            sfgRequest.org = ahRequest.Org;
            sfgRequest.test_mode = ahRequest.TestMode ? "Y" : "N";
            sfgRequest.app_version = ahRequest.AppVersion;
            #endregion

            EventLogger.LogEvent("LEAVING -> SFGWrapper.HeartbeatTranslators.TranslateToSfgRequest()");
            return sfgRequest;
        }
        public static BaseResponse Ping(returntype sfgReturn)
        {
            return GetResponse(Methods.PING, sfgReturn);
        }
    }
}
