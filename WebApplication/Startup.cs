﻿// Copyright © 2017 SOFTINUX. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for license information.

using System.IO;
using System.Reflection;
using ExtCore.Data.Abstractions;
using ExtCore.Data.EntityFramework;
using ExtCore.WebApplication.Extensions;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Security.Data.EntityFramework;
using Security.Data.EntityFramework.Sqlite;
using Serilog;

namespace WebApplication
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly string _extensionsPath;

        public Startup(IConfiguration configuration_, IHostingEnvironment hostingEnvironment_)
        {
            Configuration = configuration_;
            _extensionsPath = hostingEnvironment_.ContentRootPath + Configuration["Extensions:Path"].Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
        }

        public void ConfigureServices(IServiceCollection services_)
        {
            services_.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            // Note: AddScoped : for services based on EF (once per request),
            // other values : AddTransient (stateless), AddSingleton (avoids to implement singleton pattern ourselves)

            services_.AddSingleton(Configuration);
            services_.Configure<StorageContextOptions>(options_ =>
                {
                    options_.ConnectionString = Configuration["ConnectionStrings:Default"];
                    options_.MigrationsAssembly = typeof(DesignTimeStorageContextFactory).GetTypeInfo().Assembly.FullName;
                }
            );

            services_.Configure<CorporateConfiguration>(
              options_ =>
              {
                  options_.Name = Configuration.GetValue<string>("Corporate:Name");
                  options_.BrandLogo = Configuration.GetValue<string>("Corporate:BrandLogo");
              });

            // Register database-specific storage context implementation.
            // Necessary for IStorage service registration to fully work (see AddAuthorizationPolicies).
            services_.AddScoped<IStorageContext, ApplicationStorageContext>();
            services_.AddScoped<IStorage, Storage>();

            DesignTimeStorageContextFactory.Initialize(services_.BuildServiceProvider());

            services_.AddExtCore(_extensionsPath);

        }

        public void Configure(IApplicationBuilder applicationBuilder_, IHostingEnvironment hostingEnvironment_, ILoggerFactory loggerFactory_, IConfiguration configuration_)
        {
            if (hostingEnvironment_.IsDevelopment())
            {
                applicationBuilder_.UseDeveloperExceptionPage();
                //applicationBuilder.UseDatabaseErrorPage();
                //applicationBuilder.UseBrowserLink();
            }

            // call ConfigureLogger in a centralized place in the code
            // so that we configure the logger factory provided by .NET Core with our configuratioon in Logging class.
            Logging.ConfigureLogger(loggerFactory_, configuration_);
            //set it as the primary LoggerFactory to use everywhere
            Logging.LoggerFactory = loggerFactory_;

#if DEBUG
            Log.Information("#######################################################");
            Log.Information("webroot path: " + hostingEnvironment_.WebRootPath + "\n" + "Content Root path: " + hostingEnvironment_.ContentRootPath);
            Log.Information("#######################################################");
#endif

            applicationBuilder_.UseExtCore();

            System.Console.WriteLine("PID= " + System.Diagnostics.Process.GetCurrentProcess().Id);
        }
    }
}