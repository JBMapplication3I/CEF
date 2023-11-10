// <copyright file="WeChatChattingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the we chat chatting provider class</summary>
#pragma warning disable format
namespace Clarity.Ecommerce.Providers.Chatting.WeChatInt
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using ServiceStack;
    using ServiceStack.Text;
    using WeChat.Core.Messages;

    /// <summary>A WeChat chatting provider.</summary>
    public class WeChatChattingProvider : ChattingProviderBase
    {
        /// <summary>Values that represent return codes.</summary>
        // ReSharper disable MultipleSpaces, StyleCop.SA1602
        private enum ReturnCodes
        {
            SystemBusy                                                                                                                         = -0001,
            RequestSucceeded                                                                                                                   = 00000,
            VerificationFailed                                                                                                                 = 40001,
            InvalidCertificateType                                                                                                             = 40002,
            InvalidOpenID                                                                                                                      = 40003,
            InvalidMediaFileType                                                                                                               = 40004,
            InvalidFileType                                                                                                                    = 40005,
            InvalidFileSize                                                                                                                    = 40006,
            InvalidMediaFileID                                                                                                                 = 40007,
            InvalidMessageType                                                                                                                 = 40008,
            InvalidImageFileSize                                                                                                               = 40009,
            InvalidAudioFileSize                                                                                                               = 40010,
            InvalidVideoFileSize                                                                                                               = 40011,
            InvalidThumbnailFileSize                                                                                                           = 40012,
            InvalidAppID                                                                                                                       = 40013,
            InvalidAccessToken                                                                                                                 = 40014,
            InvalidMenuType                                                                                                                    = 40015,
            InvalidButtonQuantity                                                                                                              = 40016,
            InvalidButtonQuantityB                                                                                                             = 40017,
            InvalidButtonNameLength                                                                                                            = 40018,
            InvalidButtonKEYLength                                                                                                             = 40019,
            InvalidButtonURLLength                                                                                                             = 40020,
            InvalidMenuVersion                                                                                                                 = 40021,
            InvalidSubMenuLevels                                                                                                               = 40022,
            InvalidSubMenuButtonQuantity                                                                                                       = 40023,
            InvalidSubMenuButtonType                                                                                                           = 40024,
            InvalidSubMenuButtonNameLength                                                                                                     = 40025,
            InvalidSubMenuButtonKEYLength                                                                                                      = 40026,
            InvalidSubMenuButtonURLLength                                                                                                      = 40027,
            InvalidCustomMenuUser                                                                                                              = 40028,
            InvalidOauthCode                                                                                                                   = 40029,
            InvalidRefreshToken                                                                                                                = 40030,
            InvalidOpenIDList                                                                                                                  = 40031,
            InvalidOpenIDListLength                                                                                                            = 40032,
            InvalidRequestCharactersTheCharacterCannotBeIncluded                                                                               = 40033,
            InvalidParameters                                                                                                                  = 40035,
            InvalidRequestFormat                                                                                                               = 40038,
            InvalidURLLength                                                                                                                   = 40039,
            InvalidGroupID                                                                                                                     = 40050,
            InvalidGroupName                                                                                                                   = 40051,
            ParameterMissingAccessToken                                                                                                        = 41001,
            ParameterMissingAppid                                                                                                              = 41002,
            ParameterMissingRefreshToken                                                                                                       = 41003,
            ParameterMissingSecret                                                                                                             = 41004,
            MultimediaFileDataMissing                                                                                                          = 41005,
            ParameterMissingMediaID                                                                                                            = 41006,
            SubMenuDataMissing                                                                                                                 = 41007,
            ParameterMissingOauthCode                                                                                                          = 41008,
            ParameterMissingOpenid                                                                                                             = 41009,
            AccessTokenTimedOut                                                                                                                = 42001,
            RefreshTokenTimedOut                                                                                                               = 42002,
            OauthCodeTimedOut                                                                                                                  = 42003,
            GETRequestRequired                                                                                                                 = 43001,
            POSTRequestRequired                                                                                                                = 43002,
            HTTPSRequestRequired                                                                                                               = 43003,
            TheOtherUserIsNotYetAFollower                                                                                                      = 43004,
            TheOtherUserIsNotYetAFollowerB                                                                                                     = 43005,
            MultimediaFileIsEmpty                                                                                                              = 44001,
            POSTPackageIsEmpty                                                                                                                 = 44002,
            RichMediaMessageIsEmpty                                                                                                            = 44003,
            TextMessageIsEmpty                                                                                                                 = 44004,
            ErrorSourceMultimediaFileSize                                                                                                      = 45001,
            MessageContentsTooLong                                                                                                             = 45002,
            TitleTooLong                                                                                                                       = 45003,
            DescriptionTooLong                                                                                                                 = 45004,
            URLTooLong                                                                                                                         = 45005,
            ImageURLTooLong                                                                                                                    = 45006,
            AudioPlayTimeOverLimit                                                                                                             = 45007,
            RichMediaMessagesOverLimit                                                                                                         = 45008,
            ErrorSourceInterfaceCall                                                                                                           = 45009,
            MessageQuantityOverLimit                                                                                                           = 45010,
            ResponseTooLate                                                                                                                    = 45015,
            SystemGroupCannotBeChanged                                                                                                         = 45016,
            SystemNameTooLong                                                                                                                  = 45017,
            TooManyGroups                                                                                                                      = 45018,
            MediaDataMissing                                                                                                                   = 46001,
            ThisMenuVersionDoesntExist                                                                                                         = 46002,
            ThisMenuDataDoesntExist                                                                                                            = 46003,
            ThisUserDoesntExist                                                                                                                = 46004,
            ErrorWhileExtractingJSONXMLContents                                                                                                = 47001,
            UnauthorizedAPIFunction                                                                                                            = 48001,
            TheUserIsNotAuthorizedForThisAPI                                                                                                   = 50001,
            SystemErrorSystemError                                                                                                             = 61450,
            InvalidParameterInvalidParameter                                                                                                   = 61451,
            // ReSharper disable InconsistentNaming
            InvalidCustomerServiceAccountInvalidkf_account                                                                                     = 61452,
            ExistingCustomerServiceAccountkf_accountExisted                                                                                    = 61453,
            LengthOfCustomerServiceAccountNameOverLimitTenEnglishCharactersAtAMaximumExcludingAtSymbolAndThePartAfterItInvalidkf_acountLength  = 61454,
            InvalidCharactersInACustomerServiceAccountNameEnglishLettersAndNumbersSupportedOnlyIllegalCharacterInkf_account                    = 61455,
            MaximumNumberOfCustomerServiceAccountsReachedTenCustomerServiceAccountsAtAMaximumkf_accountCountExceeded                           = 61456,
            // ReSharper restore InconsistentNaming
            InvalidImageFileTypeInvalidFileType                                                                                                = 61457,
            DateFormatError                                                                                                                    = 61500,
            DateRangeError                                                                                                                     = 61501,
        }

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => WeChatChattingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <summary>Gets the identifier of the application.</summary>
        /// <value>The identifier of the application.</value>
        private static string AppID => "wx0985c559df84ed06"; // "wx18818513116944f2";

        /// <summary>Gets the application secret.</summary>
        /// <value>The application secret.</value>
        private static string AppSecret => "1bd28e60cc1b975609e5c285f9f4c94c"; // "94362dcb8ce215f3992a71d5a62e6f2f";

        /// <summary>Gets the URL base.</summary>
        /// <value>The URL base.</value>
        private static string UrlBase => "https://api.wechat.com/cgi-bin";

        /// <summary>Gets the token.</summary>
        /// <returns>The token.</returns>
        public CEFActionResponse<string?> GetToken()
        {
            // https://api.wechat.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET
            // {"access_token":"ACCESS_TOKEN","expires_in":7200}
            using (var client = new JsonServiceClient())
            {
                var url = $"{UrlBase}/token?grant_type=client_credential&appid={AppID}&secret={AppSecret}";
                bool? success = null;
                while (!success.HasValue)
                {
                    /* GET https://api.wechat.com/cgi-bin/token?grant_type=client_credential&appid=wx0985c559df84ed06&secret=1bd28e60cc1b975609e5c285f9f4c94c
                     * on 2017/12/19 11:58 AM CST
                     * Responded with
                        {
                            "access_token": "5_bvm-Eq5RUEEuomZx45OpCEsKeXlz4dRQehrAcsVZuW2oLMSILlK57WFdlTYvMZ1wIqwK3bJ-5JcU2VRP6mUH5CACtNuIM-PCROzkJCaOPdBYx1NWAzfWppyovszuxeSUHF9LZu_63_eMNSkjWGRcAFAEOZ",
                            "expires_in": 7200
                        }
                     * Hit again at 11:59 AM CST
                        {
                            "errcode": 45009,
                            "errmsg": "reach max api daily quota limit hint: [aFePMa0323e578]"
                        }
                     */
                    var result = client.Get<GetAccessTokenResponse>(url);
                    if (result.ErrorCode.HasValue)
                    {
                        success = ProcessResult((ReturnCodes)result.ErrorCode.Value);
                        if (success == false)
                        {
                            return new(false, result.ErrorMessage);
                        }
                    }
                    if (success == true)
                    {
                        return new(true, result.AccessToken);
                    }
                }
            }
            return new(false);
        }

        /// <inheritdoc/>
        public override CEFActionResponse PostMessage(string fromUserAccount, string toUserAccount, string message, params object[] additionalData)
        {
            var body =
$@"<xml>
    <ToUserName><![CDATA[{toUserAccount}]]></ToUserName>
    <FromUserName><![CDATA[{fromUserAccount}]]></FromUserName>
    <CreateTime>{DateExtensions.GenDateTime.ToUnixTime()}</CreateTime>
    <MsgType><![CDATA[{new MessageTypes().Text}]]></MsgType>
    <Content><![CDATA[{message}]]></Content>
    <FuncFlag>{0}</FuncFlag>
</xml>";
            using (var client = new JsonServiceClient())
            {
                bool? success = null;
                while (!success.HasValue)
                {
                    var result = client.Post<PostMessageResponse>("TODO: WeChat post message URL with app data injected", body);
                    if (result.ErrorCode.HasValue)
                    {
                        success = ProcessResult((ReturnCodes)result.ErrorCode.Value);
                        if (success == false)
                        {
                            return CEFAR.FailingCEFAR(result.ErrorMessage);
                        }
                    }
                    if (success == true)
                    {
                        return CEFAR.PassingCEFAR();
                    }
                }
            }
            return CEFAR.FailingCEFAR("Failed to post message");
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> PostMessageAsync(
            string? fromUserAccount,
            string? toUserAccount,
            string? message,
            params object?[]? additionalData)
        {
            var body =
$@"<xml>
    <ToUserName><![CDATA[{toUserAccount}]]></ToUserName>
    <FromUserName><![CDATA[{fromUserAccount}]]></FromUserName>
    <CreateTime>{DateExtensions.GenDateTime.ToUnixTime()}</CreateTime>
    <MsgType><![CDATA[{new MessageTypes().Text}]]></MsgType>
    <Content><![CDATA[{message}]]></Content>
    <FuncFlag>{0}</FuncFlag>
</xml>";
            using (var client = new JsonServiceClient())
            {
                bool? success = null;
                while (!success.HasValue)
                {
                    var result = await client.PostAsync<PostMessageResponse>("TODO: WeChat post message URL with app data injected", body).ConfigureAwait(false);
                    if (result.ErrorCode.HasValue)
                    {
                        success = ProcessResult((ReturnCodes)result.ErrorCode.Value);
                        if (success == false)
                        {
                            return CEFAR.FailingCEFAR(result.ErrorMessage);
                        }
                    }
                    if (success == true)
                    {
                        return CEFAR.PassingCEFAR();
                    }
                }
            }
            return CEFAR.FailingCEFAR("Failed to post message");
        }

        /// <summary>Process the result described by result.</summary>
        /// <param name="result">The result.</param>
        /// <returns>A bool?.</returns>
        private static bool? ProcessResult(ReturnCodes result)
        {
#pragma warning disable IDE0066 // Convert switch statement to expression
            switch (result)
            {
                case ReturnCodes.SystemBusy:
                    // TODO: Retry
                    return null;
                case ReturnCodes.RequestSucceeded:
                    // TODO: Good to go
                    return true;
                // Everything after this is an error
                case ReturnCodes.VerificationFailed:
                case ReturnCodes.InvalidCertificateType:
                case ReturnCodes.InvalidOpenID:
                case ReturnCodes.InvalidMediaFileType:
                case ReturnCodes.InvalidFileType:
                case ReturnCodes.InvalidFileSize:
                case ReturnCodes.InvalidMediaFileID:
                case ReturnCodes.InvalidMessageType:
                case ReturnCodes.InvalidImageFileSize:
                case ReturnCodes.InvalidAudioFileSize:
                case ReturnCodes.InvalidVideoFileSize:
                case ReturnCodes.InvalidThumbnailFileSize:
                case ReturnCodes.InvalidAppID:
                case ReturnCodes.InvalidAccessToken:
                case ReturnCodes.InvalidMenuType:
                case ReturnCodes.InvalidButtonQuantity:
                case ReturnCodes.InvalidButtonQuantityB:
                case ReturnCodes.InvalidButtonNameLength:
                case ReturnCodes.InvalidButtonKEYLength:
                case ReturnCodes.InvalidButtonURLLength:
                case ReturnCodes.InvalidMenuVersion:
                case ReturnCodes.InvalidSubMenuLevels:
                case ReturnCodes.InvalidSubMenuButtonQuantity:
                case ReturnCodes.InvalidSubMenuButtonType:
                case ReturnCodes.InvalidSubMenuButtonNameLength:
                case ReturnCodes.InvalidSubMenuButtonKEYLength:
                case ReturnCodes.InvalidSubMenuButtonURLLength:
                case ReturnCodes.InvalidCustomMenuUser:
                case ReturnCodes.InvalidOauthCode:
                case ReturnCodes.InvalidRefreshToken:
                case ReturnCodes.InvalidOpenIDList:
                case ReturnCodes.InvalidOpenIDListLength:
                case ReturnCodes.InvalidRequestCharactersTheCharacterCannotBeIncluded:
                case ReturnCodes.InvalidParameters:
                case ReturnCodes.InvalidRequestFormat:
                case ReturnCodes.InvalidURLLength:
                case ReturnCodes.InvalidGroupID:
                case ReturnCodes.InvalidGroupName:
                case ReturnCodes.ParameterMissingAccessToken:
                case ReturnCodes.ParameterMissingAppid:
                case ReturnCodes.ParameterMissingRefreshToken:
                case ReturnCodes.ParameterMissingSecret:
                case ReturnCodes.MultimediaFileDataMissing:
                case ReturnCodes.ParameterMissingMediaID:
                case ReturnCodes.SubMenuDataMissing:
                case ReturnCodes.ParameterMissingOauthCode:
                case ReturnCodes.ParameterMissingOpenid:
                case ReturnCodes.AccessTokenTimedOut:
                case ReturnCodes.RefreshTokenTimedOut:
                case ReturnCodes.OauthCodeTimedOut:
                case ReturnCodes.GETRequestRequired:
                case ReturnCodes.POSTRequestRequired:
                case ReturnCodes.HTTPSRequestRequired:
                case ReturnCodes.TheOtherUserIsNotYetAFollower:
                case ReturnCodes.TheOtherUserIsNotYetAFollowerB:
                case ReturnCodes.MultimediaFileIsEmpty:
                case ReturnCodes.POSTPackageIsEmpty:
                case ReturnCodes.RichMediaMessageIsEmpty:
                case ReturnCodes.TextMessageIsEmpty:
                case ReturnCodes.ErrorSourceMultimediaFileSize:
                case ReturnCodes.MessageContentsTooLong:
                case ReturnCodes.TitleTooLong:
                case ReturnCodes.DescriptionTooLong:
                case ReturnCodes.URLTooLong:
                case ReturnCodes.ImageURLTooLong:
                case ReturnCodes.AudioPlayTimeOverLimit:
                case ReturnCodes.RichMediaMessagesOverLimit:
                case ReturnCodes.ErrorSourceInterfaceCall:
                case ReturnCodes.MessageQuantityOverLimit:
                case ReturnCodes.ResponseTooLate:
                case ReturnCodes.SystemGroupCannotBeChanged:
                case ReturnCodes.SystemNameTooLong:
                case ReturnCodes.TooManyGroups:
                case ReturnCodes.MediaDataMissing:
                case ReturnCodes.ThisMenuVersionDoesntExist:
                case ReturnCodes.ThisMenuDataDoesntExist:
                case ReturnCodes.ThisUserDoesntExist:
                case ReturnCodes.ErrorWhileExtractingJSONXMLContents:
                case ReturnCodes.UnauthorizedAPIFunction:
                case ReturnCodes.TheUserIsNotAuthorizedForThisAPI:
                case ReturnCodes.SystemErrorSystemError:
                case ReturnCodes.InvalidParameterInvalidParameter:
                case ReturnCodes.InvalidCustomerServiceAccountInvalidkf_account:
                case ReturnCodes.ExistingCustomerServiceAccountkf_accountExisted:
                case ReturnCodes.LengthOfCustomerServiceAccountNameOverLimitTenEnglishCharactersAtAMaximumExcludingAtSymbolAndThePartAfterItInvalidkf_acountLength:
                case ReturnCodes.InvalidCharactersInACustomerServiceAccountNameEnglishLettersAndNumbersSupportedOnlyIllegalCharacterInkf_account:
                case ReturnCodes.MaximumNumberOfCustomerServiceAccountsReachedTenCustomerServiceAccountsAtAMaximumkf_accountCountExceeded:
                case ReturnCodes.InvalidImageFileTypeInvalidFileType:
                case ReturnCodes.DateFormatError:
                case ReturnCodes.DateRangeError:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
#pragma warning restore IDE0066 // Convert switch statement to expression
        }
    }
}
