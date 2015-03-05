// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using AOPForYouAndMe.Part1.Scattered.Models.Caching;
using AOPForYouAndMe.Part1.Scattered.Models.Services;
using Castle.DynamicProxy;

namespace AOPForYouAndMe.DependencyResolution {
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
	
    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
					scan.With(new ControllerConvention());
                });

                // 5
//                For<IReportService>().Use<ReportService>()
//                    .DecorateWith(i => new ReportCacheDecorator(i));

                // 6
//                var proxyGenerator = new ProxyGenerator();
//                For<IReportService>().Use<ReportService>()
//                    .DecorateWith(i => proxyGenerator.CreateInterfaceProxyWithTargetInterface(i, new CacheInterceptor()));
        }

        #endregion
    }
}