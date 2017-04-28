// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Microsoft.Azure.WebJobs.Script.Scaling
{
    public class ScaleManager
    {
        private readonly IWorkerTable _table;
        private readonly IScaleHandler _eventHandler;
        private readonly IScaleTracer _tracer;

        public ScaleManager(IWorkerTable table, IScaleHandler eventHandler, IScaleTracer tracer)
        {
            _table = table;
            _eventHandler = eventHandler;
            _tracer = tracer;
        }

        public void UpdateWorkerInfo(string activityId, IWorkerInfo info)
        {
            // TODO, suwatch: to add actual codes
            if (_table != null && _eventHandler != null && _tracer != null && activityId != null && info != null)
            {
            }
        }
    }
}
