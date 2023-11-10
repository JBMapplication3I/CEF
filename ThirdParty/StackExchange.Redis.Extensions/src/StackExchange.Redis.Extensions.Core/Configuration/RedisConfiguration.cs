﻿namespace StackExchange.Redis.Extensions.Core.Configuration
{
    using System.Net.Security;

    public class RedisConfiguration
    {
        private static ConnectionMultiplexer connection;
        private static ConfigurationOptions options;

        /// <summary>The key separation prefix used for all cache entries.</summary>
        /// <value>The key prefix.</value>
        public string KeyPrefix { get; set; }

        /// <summary>The password or access key.</summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>Specify if the connection can use Admin commands like flush database.</summary>
        /// <value><c>true</c> if can use admin commands; otherwise, <c>false</c>.</value>
        public bool AllowAdmin { get; set; } = false;

        /// <summary>Specify if the connection is a secure connection or not.</summary>
        /// <value><c>true</c> if is secure; otherwise, <c>false</c>.</value>
        public bool Ssl { get; set; } = false;

        /// <summary>The connection timeout.</summary>
        /// <value>The connect timeout.</value>
        public int ConnectTimeout { get; set; } = 5000;

        /// <summary>If true, Connect will not create a connection while no servers are available.</summary>
        /// <value>True if abort on connect fail, false if not.</value>
        public bool AbortOnConnectFail { get; set; }

        /// <summary>Database Id.</summary>
        /// <value>The database id, the default value is 0.</value>
        public int Database { get; set; } = 0;

        /// <summary>The host of Redis Server.</summary>
        /// <value>The ip or name.</value>
        public RedisHost[] Hosts { get; set; }

        /// <summary>The strategy to use when executing server wide commands.</summary>
        /// <value>The server enumeration strategy.</value>
        public ServerEnumerationStrategy ServerEnumerationStrategy { get; set; }

        /// <summary>A RemoteCertificateValidationCallback delegate responsible for validating the certificate supplied
        /// by the remote party; note that this cannot be specified in the configuration-string.</summary>
        public event RemoteCertificateValidationCallback CertificateValidation;

        public ConfigurationOptions ConfigurationOptions
        {
            get
            {
                if (options == null)
                {
                    options = new ConfigurationOptions
                    {
                        Ssl = Ssl,
                        AllowAdmin = AllowAdmin,
                        Password = Password,
                        ConnectTimeout = ConnectTimeout,
                        AbortOnConnectFail = AbortOnConnectFail
                    };
                    foreach (var redisHost in Hosts)
                    {
                        options.EndPoints.Add(redisHost.Host, redisHost.Port);
                    }
                    options.CertificateValidation += CertificateValidation;
                }
                return options;
            }
        }

        public ConnectionMultiplexer Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = ConnectionMultiplexer.Connect(options);
                }
                return connection;
            }
        }
    }
}
