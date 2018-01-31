using System;
using SupportClasses;
using System.Configuration;
using System.Collections.Generic;
using SFGWrapper.SFG.UserMaintSvc;
using SFGWrapper.Helpers;

namespace SFGWrapper
{
    public class UserMaintenance
    {
        public enum Methods { CREATELOGIN, UPDATEUSERNAME, UPDATEPASSWORD }
        private static BaseResponse GetResponse(Methods methodCalled, UserMaintenanceServiceRequest ahRequest)
        {
            string className = "SFGWrapper.UserMaintenance";
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                using (UserMaintService svc = new UserMaintService())
                {
                    svc.Timeout = 20000;
                    svc.Credentials = new System.Net.NetworkCredential(ahRequest.ServiceUsername, ahRequest.ServicePassword);
                    argtype sfgRequest = UserMaintTranslators.TranslateToSfgRequest(methodCalled, ahRequest);
                    baseResponse = UserMaintTranslators.GetResponse(methodCalled,svc.process_wsdl(sfgRequest));                    
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

        public static BaseResponse CreateLogin(CreateLoginRequest ahRequest)
        {
            return GetResponse(Methods.CREATELOGIN, ahRequest);
        }
        public static BaseResponse UpdateUsername(UpdateUsernameRequest ahRequest)
        {
            return GetResponse(Methods.UPDATEUSERNAME, ahRequest);
        }
        public static BaseResponse UpdatePassword(UpdatePasswordRequest ahRequest)
        {
            return GetResponse(Methods.UPDATEPASSWORD, ahRequest);
        }
    }
}
