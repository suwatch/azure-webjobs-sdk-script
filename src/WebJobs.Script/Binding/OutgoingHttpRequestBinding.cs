// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings.Path;
using Microsoft.Azure.WebJobs.Host.Bindings.Runtime;
using Microsoft.Azure.WebJobs.Script.Description;
using Microsoft.WindowsAzure.Storage.Blob;
using OutgoingHttpRequestWebJobsExtension;

namespace Microsoft.Azure.WebJobs.Script.Binding
{
    public class OutgoingHttpRequestBinding : FunctionBinding
    {
        private readonly BindingTemplate _uriBindingTemplate;

        public OutgoingHttpRequestBinding(ScriptHostConfiguration config, OutgoingHttpRequestBindingMetadata metadata, FileAccess access) : base(config, metadata, access)
        {
            if (string.IsNullOrEmpty(metadata.Uri))
            {
                throw new ArgumentException("The uri cannot be null or empty.");
            }

            Uri = metadata.Uri;
            _uriBindingTemplate = BindingTemplate.FromString(Uri);
        }

        public string Uri { get; private set; }

        public override async Task BindAsync(BindingContext context)
        {
            string boundUri = Uri;
            if (context.BindingData != null)
            {
                boundUri = _uriBindingTemplate.Bind(context.BindingData);
            }

            boundUri = Resolve(boundUri);

            var attribute = new OutgoingHttpRequestAttribute(boundUri);

            var runtimeContext = new RuntimeBindingContext(attribute);
            await BindStreamAsync(context, Access, runtimeContext);
        }

        public override Collection<CustomAttributeBuilder> GetCustomAttributes(Type parameterType)
        {
            FileAccess access = GetAttributeAccess(parameterType);

            var constructorTypes = new Type[] { typeof(string) };
            var constructorArguments = new object[] { Uri };
            var attribute = new CustomAttributeBuilder(typeof(OutgoingHttpRequestAttribute).GetConstructor(constructorTypes), constructorArguments);

            return new Collection<CustomAttributeBuilder>() { attribute };
        }

        private FileAccess GetAttributeAccess(Type parameterType)
        {
            // TODO: is this correct?
            return FileAccess.Write;
        }
    }
}
