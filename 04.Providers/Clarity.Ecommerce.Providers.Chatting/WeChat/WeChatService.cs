// <copyright file="WeChatService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the we chat service class</summary>
// http://admin.wechat.com/wiki/index.php?title=Guide_for_Message_API#Apply_for_Message_API
#pragma warning disable 1591
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable SA1600 // Elements should be documented
// ReSharper disable InconsistentNaming, UnusedAutoPropertyAccessor.Global, UnusedMember.Global, UnusedParameter.Global
namespace Clarity.Ecommerce.Providers.Chatting.WeChatInt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using Newtonsoft.Json;
    using ServiceStack;
    using ServiceStack.Text;
    using WeChat.Core.Messages;
    using WeChat.Core.Messages.Middlewares;
    using WeChat.Core.Utils;

    /// <summary>A WeChat webhook.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    /// <remarks>
    /// See http://admin.wechat.com/wiki/index.php?title=Guide_for_Message_API#URL_Access
    /// </remarks>
    [PublicAPI, Route("/Providers/Chatting/WeChat/Webhook", "GET", Summary = "WeChat Webhook")]
    public class WeChatUrlAccess : IReturn<CEFActionResponse>
    {
        // Time stamp
        [JsonProperty("timestamp"),
            ApiMember(Name = "timestamp", DataType = "string", ParameterType = "query", IsRequired = true,
                Description = "Unix Time stamp")]
        public string Timestamp { get; set; } = null!;

        // Signature for communication encryption
        [JsonProperty("signature"),
            ApiMember(Name = "signature", DataType = "string", ParameterType = "query", IsRequired = true,
                Description = "Signature for communication encryption")]
        public string Signature { get; set; } = null!;

        // A random number
        [JsonProperty("nonce"),
            ApiMember(Name = "nonce", DataType = "string", ParameterType = "query", IsRequired = true,
                Description = "A random number")]
        public string Nonce { get; set; } = null!;

        // A random string
        [JsonProperty("echostr"),
            ApiMember(Name = "echostr", DataType = "string", ParameterType = "query", IsRequired = true,
                Description = "A random string")]
        public string EchoString { get; set; } = null!;
    }

    public abstract class WeChatMessage : IReturn<CEFActionResponse>
    {
        /* When a WeChat user sends a message to an Official Account,
         * WeChat Official Account Admin Platform will POST it to you
         * via the URL you provided.
         * <xml>
         *     <ToUserName><![CDATA[toUser]]></ToUserName>
         *     <FromUserName><![CDATA[fromUser]]></FromUserName>
         *     <CreateTime>1348831860</CreateTime>
         *     <MsgType><![CDATA[text]]></MsgType>
         *     <!-- some other object value -->
         *     <MsgId>1234567890123456</MsgId>
         * </xml> */
        public string ToUserName { get; set; } = null!; // WeChat ID of your app

        public string FromUserName { get; set; } = null!; // a unique ID for the sender

        public long CreateTime { get; set; } // create time of the message

        public string MsgType { get; set; } = null!; // message type ("text" for text messages)
    }

    public abstract class WeChatPushMessage : WeChatMessage
    {
        public long MsgId { get; set; } // a unique ID for the message(64 bit integer)
    }

    [PublicAPI, Route("/Providers/Chatting/WeChat/PushMessage/Image", "POST", Summary = "WeChat Webhook")]
    public class WeChatPushTextMessage : WeChatPushMessage
    {
        /* When a WeChat user sends a message to an Official Account,
         * WeChat Official Account Admin Platform will POST it to you
         * via the URL you provided.
         * <Content><![CDATA[this is a test]]></Content>
         */
        public string Content { get; set; } = null!; // message contents
    }

    [PublicAPI, Route("/Providers/Chatting/WeChat/PushMessage/Text", "POST", Summary = "WeChat Webhook")]
    public class WeChatPushImageMessage : WeChatPushMessage
    {
        /* When a WeChat user sends a message to an Official Account,
         * WeChat Official Account Admin Platform will POST it to you
         * via the URL you provided.
         * <Content><![CDATA[this is a test]]></Content>
         */
        public string PicUrl { get; set; } = null!; // URL for the image
    }

    [PublicAPI, Route("/Providers/Chatting/WeChat/PushMessage/LocationData", "POST", Summary = "WeChat Webhook")]
    public class WeChatPushLocationDataMessage : WeChatPushMessage
    {
        /* When a WeChat user sends a message to an Official Account,
         * WeChat Official Account Admin Platform will POST it to you
         * via the URL you provided.
         * <Location_X>23.134521</Location_X>
         * <Location_Y>113.358803</Location_Y>
         * <Scale>20</Scale>
         * <Label><![CDATA[location]]></Label>
         */
        // ReSharper disable InconsistentNaming
        public double Location_X { get; set; } // latitude of the location

        public double Location_Y { get; set; } // longitude of the location

        // ReSharper restore InconsistentNaming
        public int Scale { get; set; } // scale of the map

        public string Label { get; set; } = null!; // location description
    }

    [PublicAPI, Route("/Providers/Chatting/WeChat/PushMessage/Link", "POST", Summary = "WeChat Webhook")]
    public class WeChatPushLinkMessage : WeChatPushMessage
    {
        /* When a WeChat user sends a message to an Official Account,
         * WeChat Official Account Admin Platform will POST it to you
         * via the URL you provided.
         * <Title><![CDATA[WeChat Official Account Platform portal]]></Title>
         * <Description><![CDATA[The URL of the portal]]></Description>
         * <Url><![CDATA[url]]></Url>
         */
        public double Title { get; set; } // title of the message

        public double Description { get; set; } // description of the message

        public int Url { get; set; } // url which is sent to users
    }

    [PublicAPI, Route("/Providers/Chatting/WeChat/PushMessage/Event", "POST", Summary = "WeChat Webhook")]
    public class WeChatPushEventMessage : WeChatPushMessage
    {
        /* When a WeChat user sends a message to an Official Account,
         * WeChat Official Account Admin Platform will POST it to you
         * via the URL you provided.
         * <Event><![CDATA[EVENT]]></Event>
         * <EventKey><![CDATA[EVENTKEY]]></EventKey>
         */
        public double Event { get; set; } // event type, currently we have 3 types: subscribe, unsubscribe, CLICK(coming soon)

        public double EventKey { get; set; } // for future usage
    }

    public abstract class WeChatPostReplyMessage : WeChatMessage
    {
        /* You can reply to incoming messages. Now the platform
         * supports different kinds of messages, including text,
         * image, voice, video and music. You can also do the
         * operation 'add to my favorites'.
         * If you fail to perform your response within 5 seconds,
         * we will close the connection.
         * The data structure for a reply message:
         * <xml>
         *     <ToUserName><![CDATA[toUser]]></ToUserName>
         *     <FromUserName><![CDATA[fromUser]]></FromUserName>
         *     <CreateTime>12345678</CreateTime>
         *     <MsgType><![CDATA[text]]></MsgType>
         *    <!-- some other object value -->
         *     <FuncFlag>0</FuncFlag>
         * </xml>
         */
        public string FuncFlag { get; set; } = null!; // add a star for the message if the bit (0x0001) is set
    }

    [PublicAPI, Route("/Providers/Chatting/WeChat/PostReplyMessage/Text", "POST", Summary = "WeChat Webhook")]
    public class WeChatPostReplyTextMessage : WeChatPostReplyMessage
    {
        // <Content><![CDATA[content]]></Content>
        public string Content { get; set; } = null!; // reply message contents
    }

    /// <summary>A we chat service.</summary>
    /// <seealso cref="Service"/>
    [PublicAPI]
    public class WeChatService : Service
    {
        public Task<object?> Get(WeChatUrlAccess request)
        {
            ////var svr = Server;
            if (string.Equals(Request.Verb.ToUpper(), "GET", StringComparison.Ordinal))
            {
                // If it is a GET request is regarded as the server verification WeChat configuration
                Response.Write(request.EchoString);
                return Task.FromResult<object?>(null);
            }
            var isValid = CheckSignature(request.Signature, request.Timestamp, request.Nonce);
            if (!isValid)
            {
                Response.Write(request.EchoString);
                return Task.FromResult<object?>(null);
            }
            var responseText = Middleware.Execute(string.Empty, request.Signature, request.Nonce, request.Timestamp).GetResponse();
            Response.Write(responseText);
            return Task.FromResult<object?>(null);
        }

        public Task<object?> Post(WeChatPushTextMessage _)
        {
            var fromUser = string.Empty;
            var toUser = string.Empty;
            var content = "test";
            Response.Write(
$@"<xml>
    <ToUserName><![CDATA[{toUser}]]></ToUserName>
    <FromUserName><![CDATA[{fromUser}]]></FromUserName>
    <CreateTime>{DateExtensions.GenDateTime.ToUnixTime()}</CreateTime>
    <MsgType><![CDATA[{new MessageTypes().Text}]]></MsgType>
    <Content><![CDATA[{content}]]></Content>
    <FuncFlag>{0}</FuncFlag>
</xml>");
            return Task.FromResult<object?>(null);
        }

        private static bool CheckSignature(string signature, string timestamp, string nonce)
        {
            /*
             * To validate the signature is valid
             1. Sort the 3 values of token, timestamp and nonce alphabetically.
             2. Combine the 3 parameters into one string, encrypt it using SHA - 1.
             3. Compare the SHA-1 digest string with the signature from the request.If they are the same, the access request is from WeChat.
             */
            var token = Configurations.Current.Token;
            var tmpArr = new List<string> { token, timestamp, nonce };
            tmpArr.Sort();
            var tmpStr = tmpArr.Aggregate((c, n) => c + n);
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(tmpStr));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (var b in hash)
                {
                    // Can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }
            }
            return tmpStr == signature;
        }

        // private bool IsReusable => false;
    }

    /// <summary>A WeChat feature.</summary>
    /// <seealso cref="IPlugin"/>
    public class WeChatFeature : IPlugin
    {
        /// <summary>Registers this object.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
#if FALSE
public function responseMsg()
{
    // get post data, May be due to the different environments
    $postStr = $GLOBALS["HTTP_RAW_POST_DATA"];
    // extract post data
    if (empty($postStr))
    {
        echo "";
        exit;
        return;
    }
    $postObj = simplexml_load_string($postStr, 'SimpleXMLElement', LIBXML_NOCDATA);
    $fromUsername = $postObj->FromUserName;
    $toUsername = $postObj->ToUserName;
    $keyword = trim($postObj->Content);
    $time = time();
    $textTpl =
"<xml>
    <ToUserName><![CDATA[%s]]></ToUserName>
    <FromUserName><![CDATA[%s]]></FromUserName>
    <CreateTime>%s</CreateTime>
    <MsgType><![CDATA[%s]]></MsgType>
    <Content><![CDATA[%s]]></Content>
    <FuncFlag>0</FuncFlag>
</xml>";
    if (empty($keyword))
    {
        echo "Input something...";
        return;
    }
    $msgType = "text";
    $contentStr = "Welcome to wechat world!";
    $resultStr = sprintf($textTpl, $fromUsername, $toUsername, $time, $msgType, $contentStr);
    echo $resultStr;
}
#endif
