﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Azure.WebJobs.Script.Binding;
using Microsoft.Azure.WebJobs.Script.Extensibility;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Microsoft.Azure.WebJobs.Script.Tests
{
    public class GeneralScriptBindingProviderTests
    {
        [Theory]
        [InlineData(null, null, typeof(object))]
        [InlineData(null, "many", typeof(object[]))]
        [InlineData("string", null, typeof(string))]
        [InlineData("StRing", null, typeof(string))] // case insenstive
        [InlineData("string", "mANy", typeof(string[]))] // case insensitve
        [InlineData("binary", null, typeof(byte[]))]
        [InlineData("binary", "many", typeof(byte[][]))]
        [InlineData("stream", null, typeof(Stream))]
        [InlineData("stream", "many", typeof(Stream[]))] // nonsense?
        public void Validate(string dataType, string cardinality, Type expectedType)
        {
            var ctx = New(dataType, cardinality);
            var type = GeneralScriptBindingProvider.GetRequestedType(ctx);
            Assert.Equal(expectedType, type);
        }

        private static ScriptBindingContext New(string dataType, string cardinality)
        {
            var jobj = new JObject();
            jobj["type"] = "test";
            jobj["direction"] = "in";
            jobj["datatype"] = dataType;
            jobj["cardinality"] = cardinality;
            return new ScriptBindingContext(jobj);
        }

        [Fact]
        public void ManualTest()
        {
            JobHostConfiguration config = new JobHostConfiguration();
            config.UseScriptExtensions();
            TestTraceWriter traceWriter = new TestTraceWriter(TraceLevel.Verbose);
            JObject hostMetadata = new JObject();
            var provider = new GeneralScriptBindingProvider(config, hostMetadata, traceWriter);
            provider.CompleteInitialization();

            JObject bindingMetadata = new JObject
            {
                { "type", "manualTrigger" },
                { "name", "test" },
                { "direction", "in" }
            };

            ScriptBindingContext context = new ScriptBindingContext(bindingMetadata);
            ScriptBinding binding = null;
            bool created = provider.TryCreate(context, out binding);

            Assert.True(created);
            Assert.Equal(typeof(string), binding.DefaultType);

            var attr = binding.GetAttributes()[0];
            Assert.IsType<ManualTriggerAttribute>(attr);
        }
    }
}
