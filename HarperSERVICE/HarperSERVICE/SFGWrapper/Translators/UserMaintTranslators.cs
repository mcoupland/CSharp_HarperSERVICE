using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFGWrapper.SFG.UserMaintSvc;
using SupportClasses;

namespace SFGWrapper
{
    public class UserMaintTranslators
    {
        public static BaseResponse GetResponse(UserMaintenance.Methods methodCalled, returntype sfgReturn)
        {
            string className = "SFGWrapper.UserMaintTranslators";
            BaseResponse baseResponse = new BaseResponse();
            foreach (var item in sfgReturn.error)
            {
                Message ahError = new Message(item.errno, MessageSources.UserMaint);
                foreach (string message in item.errmsg)
                {
                    ahError.SfgMessages.Add(message);
                }
                baseResponse.Messages.Add(ahError);
            }

            switch (methodCalled)
            {
                case UserMaintenance.Methods.CREATELOGIN:
                    CreateLoginResponse createLoginResponse = new CreateLoginResponse();
                    createLoginResponse.UpdateSucceeded = sfgReturn.response.UPDATE_SUCCEEDED == "Y";
                    baseResponse.TypedResponse = createLoginResponse;
                    break;
                case UserMaintenance.Methods.UPDATEPASSWORD:
                    UpdatePasswordResponse updatePasswordResponse = new UpdatePasswordResponse();
                    updatePasswordResponse.UpdateSucceeded = sfgReturn.response.UPDATE_SUCCEEDED == "Y";
                    baseResponse.TypedResponse = updatePasswordResponse;
                    break;
                case UserMaintenance.Methods.UPDATEUSERNAME:
                    UpdateUsernameResponse updateUserName = new UpdateUsernameResponse();
                    updateUserName.UpdateSucceeded = sfgReturn.response.UPDATE_SUCCEEDED == "Y";
                    baseResponse.TypedResponse = updateUserName;
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
        public static argtype TranslateToSfgRequest(UserMaintenance.Methods methodCalled, UserMaintenanceServiceRequest ahRequest)
        {
            EventLogger.LogEvent("ENTERING -> SFGWrapper.UserMaintTranslators.TranslateToSfgRequest()");
            argtype sfgRequest = new argtype();

            #region common readonly properties
            sfgRequest.org = ahRequest.Org;
            sfgRequest.test_mode = ahRequest.TestMode ? "Y" : "N";
            sfgRequest.app_version = ahRequest.AppVersion;
            sfgRequest.program_type_id = ahRequest.ProgramTypeId;
            #endregion

            switch (methodCalled)
            {
                case UserMaintenance.Methods.CREATELOGIN:
                    sfgRequest.customer_number = ahRequest.MemberId;
                    sfgRequest.new_user = ahRequest.NewUser ? "Y" : "N";
                    sfgRequest.userid = ahRequest.UserName;
                    sfgRequest.pw = ahRequest.Password;
                    sfgRequest.zip = ahRequest.PostalCode;
                    break;
                case UserMaintenance.Methods.UPDATEPASSWORD:
                    sfgRequest.validation_userid = ahRequest.ValidationUserName;
                    sfgRequest.userid = ahRequest.UserName;
                    sfgRequest.pw = ahRequest.Password;
                    break;
                case UserMaintenance.Methods.UPDATEUSERNAME:
                    sfgRequest.userid = ahRequest.UserName;
                    if (string.IsNullOrEmpty(ahRequest.ValidationUserName))
                    {
                        sfgRequest.zip = ahRequest.PostalCode;
                        sfgRequest.customer_number = ahRequest.MemberId;
                    }
                    else
                    {
                       sfgRequest.validation_userid = ahRequest.ValidationUserName; 
                    }
                    sfgRequest.pw = ahRequest.Password;
                    break;
            }
            EventLogger.LogEvent("LEAVING -> SFGWrapper.UserMaintTranslators.TranslateToSfgRequest()");
            return sfgRequest;
        }
    }
}
