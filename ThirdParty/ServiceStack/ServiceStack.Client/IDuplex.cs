namespace ServiceStack
{
#if !(SL5 || __IOS__ || XBOX || ANDROID || PCL)
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    [ServiceContract(Namespace = "http://services.servicestack.net/", CallbackContract = typeof(IDuplexCallback))]
    public interface IDuplex
    {
        [OperationContract(Action = "*", ReplyAction = "*")]
        void BeginSend(Message msg);
    }
}
#endif