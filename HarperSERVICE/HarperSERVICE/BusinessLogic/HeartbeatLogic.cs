using System;
using SFGWrapper;
using SupportClasses;

namespace BusinessLogic
{
    public class HeartbeatLogic
    {
        BaseResponse baseResponse = new BaseResponse();
        string methodName = string.Empty;

        private void LogMethodError(string methodName, Exception exceptionCaught) { EventLogger.LogError("BusinessLogic.HeartbeatLogic.{0}()", string.Format("Message: {0} \r\nStackTrace: {1}", exceptionCaught.Message, exceptionCaught.StackTrace)); }
        
        public BaseResponse Ping()
        {
            methodName = "Ping";
            
            try
            {
                PingRequest request = new PingRequest();
                baseResponse = Heartbeat.Ping(request);
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

                LogMethodError(methodName, ex);
            }
            
            return baseResponse;
        }
    }
}
