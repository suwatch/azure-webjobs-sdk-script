// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;

namespace Microsoft.Azure.WebJobs.Script.Scaling
{
    [EventSource(Guid = "a7044dd6-c8ef-4980-858c-942d972b6250")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:ParameterNamesMustBeginWithLowerCaseLetter", Justification = "<Pending>", Scope = "member", Target = "~M:Microsoft.Azure.WebJobs.Script.WebHost.Diagnostics.EventGenerator.FunctionsSystemLogsEventSource.RaiseFunctionsEventWarning(System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String)")]
    public class AzureScaleTracer : EventSource, IScaleTracer
    {
        // TODO, suwatch: internal static readonly ScaleEventSource Instance = new ScaleEventSource();

        [Event(65600, Level = EventLevel.Informational, Channel = EventChannel.Operational, Version = 1)]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Need to use Pascal Case for MDS column names")]
        public void ScaleOut(string ActivityId, string SiteName, string WorkerName, string Details)
        {
            if (IsEnabled())
            {
                WriteEvent(65600, ActivityId, SiteName, WorkerName, Details);
            }
        }

        [Event(65601, Level = EventLevel.Informational, Channel = EventChannel.Operational, Version = 1)]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Need to use Pascal Case for MDS column names")]
        public void ScaleIn(string ActivityId, string SiteName, string WorkerName, string Details)
        {
            if (IsEnabled())
            {
                WriteEvent(65601, ActivityId, SiteName, WorkerName, Details);
            }
        }

        [Event(65602, Level = EventLevel.Informational, Channel = EventChannel.Operational, Version = 1)]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Need to use Pascal Case for MDS column names")]
        public void UpdateWorker(string ActivityId, string SiteName, string WorkerName, string Status, string Details)
        {
            if (IsEnabled())
            {
                WriteEvent(65602, ActivityId, SiteName, WorkerName, Status, Details);
            }
        }

        [Event(65603, Level = EventLevel.Informational, Channel = EventChannel.Operational, Version = 1)]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Need to use Pascal Case for MDS column names")]
        public void Information(string ActivityId, string SiteName, string WorkerName, string Details)
        {
            if (IsEnabled())
            {
                WriteEvent(65603, ActivityId, SiteName, WorkerName, Details);
            }
        }

        [Event(65604, Level = EventLevel.Warning, Channel = EventChannel.Operational, Version = 1)]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Need to use Pascal Case for MDS column names")]
        public void Warning(string ActivityId, string SiteName, string WorkerName, string Details)
        {
            if (IsEnabled())
            {
                WriteEvent(65604, ActivityId, SiteName, WorkerName, Details);
            }
        }

        [Event(65605, Level = EventLevel.Error, Channel = EventChannel.Operational, Version = 1)]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Need to use Pascal Case for MDS column names")]
        public void Error(string ActivityId, string SiteName, string WorkerName, string Details)
        {
            if (IsEnabled())
            {
                WriteEvent(65605, ActivityId, SiteName, WorkerName, Details);
            }
        }

        [Event(65606, Level = EventLevel.Informational, Channel = EventChannel.Operational, Version = 1)]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Need to use Pascal Case for MDS column names")]
        public void Http(string ActivityId, string SiteName, string WorkerName, string Verb, string Address, int StatusCode, string StartTime, string EndTime, int LatencyInMilliseconds, string RequestContent, string Details)
        {
            if (IsEnabled())
            {
                WriteEvent(65606, ActivityId, SiteName, WorkerName, Verb, Address, StatusCode, StartTime, EndTime, LatencyInMilliseconds, RequestContent, Details);
            }
        }

        void IScaleTracer.TraceScaleOut(string activityId, IWorkerInfo workerInfo, string details)
        {
            ScaleOut(activityId, workerInfo.SiteName, workerInfo.WorkerName, details);
        }

        void IScaleTracer.TraceScaleIn(string activityId, IWorkerInfo workerInfo, string details)
        {
            ScaleIn(activityId, workerInfo.SiteName, workerInfo.WorkerName, details);
        }

        void IScaleTracer.TraceUpdateWorker(string activityId, IWorkerInfo workerInfo, string details)
        {
            UpdateWorker(activityId, workerInfo.SiteName, workerInfo.WorkerName, workerInfo.Status.ToString(), details);
        }

        void IScaleTracer.TraceInformation(string activityId, IWorkerInfo workerInfo, string details)
        {
            Information(activityId, workerInfo.SiteName, workerInfo.WorkerName, details);
        }

        void IScaleTracer.TraceWarning(string activityId, IWorkerInfo workerInfo, string details)
        {
            Warning(activityId, workerInfo.SiteName, workerInfo.WorkerName, details);
        }

        void IScaleTracer.TraceError(string activityId, IWorkerInfo workerInfo, string details)
        {
            Error(activityId, workerInfo.SiteName, workerInfo.WorkerName, details);
        }

        void IScaleTracer.TraceHttp(string activityId, IWorkerInfo workerInfo, string verb, string address, int statusCode, string startTime, string endTime, int latencyInMilliseconds, string requestContent, string details)
        {
            Http(activityId, workerInfo.SiteName, workerInfo.WorkerName, verb, address, statusCode, startTime, endTime, latencyInMilliseconds, requestContent, details);
        }
    }
}