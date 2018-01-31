using System;
using System.Collections.Generic;
using System.Configuration;
using SupportClasses;
using SFGWrapper.SFG.CustomerUpdateSvc;
using SFGWrapper.Helpers;

namespace SFGWrapper
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerUpdate
    {
        public static BaseResponse UpdateMember(CustomerUpdateServiceRequest ahRequest)
        {
            string className = "SFGWrapper.CustomerUpdate";
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                using (CustomerUpdateService svc = new CustomerUpdateService())
                {
                    svc.Timeout = 20000;
                    svc.Credentials = new System.Net.NetworkCredential(ahRequest.ServiceUsername, ahRequest.ServicePassword);
                    argtype sfgRequest = CustomerUpdateTranslators.TranslateToSfgRequest(ahRequest);
                    baseResponse = CustomerUpdateTranslators.UpdateCustomer(svc.process_wsdl(sfgRequest));
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
                EventLogger.LogError(string.Format("{0}.UpdateMember()", className),
                    string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
            return baseResponse;
        }
    }
}
