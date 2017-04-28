// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Microsoft.Azure.WebJobs.Script.Scaling
{
    public enum WorkerStatus
    {
        /// <summary>
        /// This worker may be removed
        /// </summary>
        Free = 0,

        /// <summary>
        /// This worker handle load normally
        /// </summary>
        Normal,

        /// <summary>
        /// This worker handle is busy
        /// </summary>
        Busy
    }
}
