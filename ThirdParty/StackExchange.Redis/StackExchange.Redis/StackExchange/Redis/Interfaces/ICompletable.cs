namespace StackExchange.Redis
{
    using System.Text;

    interface ICompletable
    {
        void AppendStormLog(StringBuilder sb);

        bool TryComplete(bool isAsync);
    }
}
