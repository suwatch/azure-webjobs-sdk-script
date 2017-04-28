// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Azure.WebJobs.Script.Scaling
{
    public interface IWorkerTable
    {
        Task<IEnumerable<IWorkerInfo>> List();

        Task AddOrUpdate(IWorkerInfo info);

        Task Delete(IWorkerInfo info);
    }
}
