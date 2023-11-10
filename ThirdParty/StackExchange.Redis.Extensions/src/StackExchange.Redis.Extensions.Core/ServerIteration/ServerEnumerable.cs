namespace StackExchange.Redis.Extensions.Core.ServerIteration
{
    using System.Collections;
    using System.Collections.Generic;
    using StackExchange.Redis.Extensions.Core.Configuration;

    public class ServerEnumerable : IEnumerable<IServer>
    {
        private readonly ConnectionMultiplexer multiplexer;
        private readonly ServerEnumerationStrategy.TargetRoleOptions targetRole;
        private readonly ServerEnumerationStrategy.UnreachableServerActionOptions unreachableServerAction;

        public ServerEnumerable(
            ConnectionMultiplexer multiplexer,
            ServerEnumerationStrategy.TargetRoleOptions targetRole,
            ServerEnumerationStrategy.UnreachableServerActionOptions unreachableServerAction)
        {
            this.multiplexer = multiplexer;
            this.targetRole = targetRole;
            this.unreachableServerAction = unreachableServerAction;
        }

        public IEnumerator<IServer> GetEnumerator()
        {
            foreach (var endPoint in multiplexer.GetEndPoints())
            {
                var server = multiplexer.GetServer(endPoint);
                if (targetRole == ServerEnumerationStrategy.TargetRoleOptions.PreferSlave
                        && !server.IsSlave
                    || unreachableServerAction == ServerEnumerationStrategy.UnreachableServerActionOptions.IgnoreIfOtherAvailable
                        && (!server.IsConnected || !server.Features.Scan))
                {
                    continue;
                }
                yield return server;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
