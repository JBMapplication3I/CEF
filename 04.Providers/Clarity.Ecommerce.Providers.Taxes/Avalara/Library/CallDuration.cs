// <copyright file="CallDuration.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the call duration class</summary>
namespace Avalara.AvaTax.RestClient
{
    using System;
    using System.Diagnostics;
#if PORTABLE
    using System.Threading.Tasks;
    using System.Linq;
#endif

    /// <summary>Track time spent on an API call.</summary>
    public class CallDuration
    {
        /// <summary>Tracks the amount of time spent setting up a REST API call.</summary>
        /// <value>The setup duration.</value>
        public TimeSpan SetupDuration { get; set; }

        /// <summary>Tracks the amount of time the server took processing the request.</summary>
        /// <value>The server duration.</value>
        public TimeSpan ServerDuration { get; set; }

        /// <summary>Tracks the amount of time the service engine took processing the request.</summary>
        /// <value>The service duration.</value>
        public TimeSpan ServiceDuration { get; set; }

        /// <summary>Tracks the amount of time the data caching engine took processing the request.</summary>
        /// <value>The data duration.</value>
        public TimeSpan DataDuration { get; set; }

        /// <summary>Tracks the amount of time the API call was in flight.</summary>
        /// <value>The transit duration.</value>
        public TimeSpan TransitDuration { get; set; }

        /// <summary>Tracks the amount of time it took to parse results from the API call.</summary>
        /// <value>The parse duration.</value>
        public TimeSpan ParseDuration { get; set; }

        #region Implementation
        /// <summary>Initializes a new instance of the <see cref="CallDuration"/> class.</summary>
        public CallDuration()
        {
            _timer = new();
            _timer.Start();
        }

        /// <summary>Add another call's duration time to ours.</summary>
        /// <param name="otherDuration">Duration of the other.</param>
        public void Combine(CallDuration otherDuration)
        {
            SetupDuration += otherDuration.SetupDuration;
            ServerDuration += otherDuration.ServerDuration;
            TransitDuration += otherDuration.TransitDuration;
            ParseDuration += otherDuration.ParseDuration;
        }

        /// <summary>Call this when the API has finished setting up and is about to transmit.</summary>
        public void FinishSetup()
        {
            SetupDuration = Checkpoint();
        }

        /// <summary>Call this when the API call has returned all its results.</summary>
        /// <param name="serverDuration"> Duration of the server.</param>
        /// <param name="dataDuration">   Duration of the data.</param>
        /// <param name="serviceDuration">Duration of the service.</param>
        public void FinishReceive(TimeSpan? serverDuration, TimeSpan? dataDuration, TimeSpan? serviceDuration)
        {
            var ts = Checkpoint();
            if (serverDuration != null)
            {
                ServerDuration = serverDuration.Value;
            }
            if (dataDuration != null)
            {
                DataDuration = dataDuration.Value;
            }
            if (serviceDuration != null)
            {
                ServiceDuration = serviceDuration.Value;
            }
            TransitDuration = ts - ServerDuration;
        }

        /// <summary>Call this when the results have been fully parsed.</summary>
        public void FinishParse()
        {
            ParseDuration = Checkpoint();
        }

        /// <summary>
        /// Print out call duration in a friendly manner
        /// </summary>
        public override string ToString()
        {
            return $"Setup: {SetupDuration} Server: {ServerDuration} [Service: {ServiceDuration} Data: {DataDuration}] Transit: {TransitDuration} Parse: {ParseDuration}";
        }

        /// <summary>Keep track of time since last checkpoint.</summary>
        private readonly Stopwatch _timer;

        /// <summary>Determine time since last checkpoint, and advance checkpoint.</summary>
        /// <returns>A TimeSpan.</returns>
        private TimeSpan Checkpoint()
        {
            return _timer.Elapsed;
        }
        #endregion
    }
}
