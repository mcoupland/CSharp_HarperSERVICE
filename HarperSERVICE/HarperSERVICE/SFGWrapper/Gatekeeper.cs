using System;
using System.Collections.Generic;
using SupportClasses;
using SFGWrapper.SFG.GateKeeperSvc;
using System.Configuration;
using SFGWrapper.Helpers;

namespace SFGWrapper
{
    /// <summary>
    /// 
    /// </summary>
    public static class Gatekeeper
    {
        public static BaseResponse GetMemberByMemberId(GetMemberByMemberIdRequest ahRequest)
        {
            string className = "SFGWrapper.Gatekeeper.GetMemberByMemberId";
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                using (GateKeeperService svc = new GateKeeperService())
                {
                    svc.Timeout = 20000;
                    System.Net.ServicePointManager.Expect100Continue = false;
                    svc.Credentials = new System.Net.NetworkCredential(ahRequest.ServiceUsername, ahRequest.ServicePassword);
                    argtype sfgRequest = GateKeeperTranslators.TranslateToGetMemberByMemberIdRequest(ahRequest);
                    baseResponse = GateKeeperTranslators.GetMemberByMemberId(svc.process_wsdl(sfgRequest));
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
                EventLogger.LogError(className,
                    string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
            return baseResponse;
        }
        public static BaseResponse GetMemberByUserName(GetMemberByUserNameRequest ahRequest)
        {
            string className = "SFGWrapper.Gatekeeper.GetMemberByUserName";
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                using (GateKeeperService svc = new GateKeeperService())
                {
                    svc.Timeout = 20000;
                    svc.Credentials = new System.Net.NetworkCredential(ahRequest.ServiceUsername, ahRequest.ServicePassword);
                    argtype sfgRequest = GateKeeperTranslators.TranslateToGetMemberByUsernameRequest(ahRequest);
                    baseResponse = GateKeeperTranslators.GetMemberByUsername(svc.process_wsdl(sfgRequest));
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
                EventLogger.LogError(className, 
                    string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
            return baseResponse;
        }
        public static BaseResponse GetOffer(GetOfferRequest ahRequest)
        {
            string className = "SFGWrapper.Gatekeeper.GetOffer";
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                using (GateKeeperService svc = new GateKeeperService())
                {
                    svc.Timeout = 20000; 
                    svc.Credentials = new System.Net.NetworkCredential(ahRequest.ServiceUsername, ahRequest.ServicePassword);
                    argtype sfgRequest = GateKeeperTranslators.TranslateToGetOfferRequest(ahRequest);
                    baseResponse = GateKeeperTranslators.GetOffer(svc.process_wsdl(sfgRequest));
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
                EventLogger.LogError(className,
                    string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
            return baseResponse;
        }

        /// <summary>
        /// login tries to load history (so we only have to make one call to sfg)
        /// if authentication fails, the response from sfg doesn't send the correct order history
        /// that throws an error when parsing
        /// login catches that error and sets auth to false, but it cant set any other values
        /// </summary>
        /// <param name="ahRequest"></param>
        /// <returns></returns>
        public static BaseResponse Login(LoginRequest ahRequest)
        {
            string className = "SFGWrapper.Gatekeeper.Login";
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                using (GateKeeperService svc = new GateKeeperService())
                {
                    svc.Timeout = 20000;
                    svc.Credentials = new System.Net.NetworkCredential(ahRequest.ServiceUsername, ahRequest.ServicePassword);
                    argtype sfgRequest = GateKeeperTranslators.TranslateToLoginRequest(ahRequest);
                    baseResponse = GateKeeperTranslators.Login(svc.process_wsdl(sfgRequest));
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
                    EventLogger.LogError(className,
                        string.Format("Message: {0} \r\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
            return baseResponse;
        }
    }
}