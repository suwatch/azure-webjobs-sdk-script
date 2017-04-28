// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Microsoft.Azure.WebJobs.Script.Scaling
{
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "By design")]
    public interface IScaleHandler
    {
        Task ScaleOut(string activityId, int workers, IWorkerInfo workerInfo);

        Task ScaleIn(string activityId, IWorkerInfo workerInfo);

        Task PingWorker(string activityId, IWorkerInfo workerInfo);
    }
}
