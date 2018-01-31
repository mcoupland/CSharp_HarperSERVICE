using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using SupportClasses;
using SFGWrapper.SFG.CreditCardProcessingSvc;
using SFGWrapper.Helpers;

namespace SFGWrapper
{
    /// <summary>
    /// 
    /// </summary>
    public static class CreditCardProcessing
    {
        public static BaseResponse GetResponse(CreditCardServiceRequest ahRequest)
        {
            string className = "SFGWrapper.CreditCardProcessing";
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                using (CCProcessorService svc = new CCProcessorService())
                {
                    svc.Timeout = 20000;
                    svc.Credentials = new System.Net.NetworkCredential(ahRequest.ServiceUsername, ahRequest.ServicePassword);
                    argtype sfgRequest = CreditCardTranslators.TranslateToSfgRequest(ahRequest);
                    baseResponse = CreditCardTranslators.GetResponse(svc.process_wsdl(sfgRequest));
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
                EventLogger.LogError(string.Format("{0}()", new object[] { className }),
                    string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
            return baseResponse;
        }

    }
}
