﻿// Copyright © 2017 SOFTINUX. All rights reserved.
// Licensed under the MIT License, Version 2.0. See LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ExtCore.Data.Abstractions;
using ExtCore.Data.EntityFramework;
using ExtCore.Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BaseTest.Common
{
    /// <summary>
    /// Management of shared context that provides an Entity Framework connection and an IStorage implementation.
    ///
    /// This is used to create a single test context and share it among tests in several test classes,
    /// and have it cleaned up after all the tests in the test classes have finished, by use of ICollectionFixture&lt;DatabaseFixture&gt;.
    ///
    /// The test context class dependds on your entity framework provider so you have to subclass this class and implement GetTestContext().
    /// </summary>
    public class DatabaseFixture : IDisposable
    {
        protected virtual string DatabaseToCopyFromBaseName => "basedb_tests";
        protected virtual string DatabaseToCopyToBaseName => "basedb_tests";
        /// <summary>
        /// Named connection string (path of connection string value in appsettings.json).
        /// </summary>
        protected virtual string ConnectionStringPath => "ConnectionStrings:Default";

        public IRequestHandler DatabaseContext { get; }

        public virtual ITestContext GetTestContext()
        {
            Console.WriteLine("Please provide an implementation of TestContext accepting ConnectionStringPath as parameter.");
            Console.WriteLine("You should see this message only because of dependency injection used by test class constructor.");
            return null;
        }

        public DatabaseFixture()
        {
            // Cleanup EF detailed log file
            File.Delete(EfLoggerProvider.LogFilePath);

            // initialize test file from copy. Root dir is bin/debug/netcoreapp2.0
            // ReSharper disable VirtualMemberCallInConstructor
            File.Copy($"../../../../Artefacts/{DatabaseToCopyFromBaseName}.sqlite", $"../../../../WorkDir/{DatabaseToCopyToBaseName}.sqlite", true);

            DatabaseContext = GetTestContext() as IRequestHandler;
            // ReSharper enable VirtualMemberCallInConstructor

            if(DatabaseContext == null)
            {
                Console.WriteLine("Your ITestContext implementation should also inherit from IRequestHandler");
                Console.WriteLine("You should see this message only because of dependency injection used by test class constructor.");
            }

            List<Assembly> loadedAssemblies = new List<Assembly>();

            foreach (FileInfo file in new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .GetFiles("*.dll")) //loop through all dll files in directory
            {
                try
                {
                    loadedAssemblies.Add(Assembly.Load(AssemblyName.GetAssemblyName(file.FullName)));
                }
                catch (Exception)
                {
                    Console.WriteLine("Error loading assembly from file: " + file.FullName);
                }
            }
            ExtensionManager.SetAssemblies(loadedAssemblies);
        }

        public IDbContextTransaction OpenTransaction()
        {
            return ((StorageContextBase)DatabaseContext.Storage.StorageContext).Database.BeginTransaction();
        }

        public void SaveChanges()
        {
            DatabaseContext.Storage.Save();
        }

        public TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            return DatabaseContext.Storage.GetRepository<TRepository>();
        }

        public void RollbackTransaction()
        {
            ((StorageContextBase)DatabaseContext.Storage.StorageContext).Database.RollbackTransaction();
            DetachAllEntities();
        }

        public void Dispose()
        {
            // nothing yet
        }

        public void DetachAllEntities()
        {
            // Since the transaction was rolled back, the entries' state is Unmodified (I guess)
            var changedEntriesCopy = ((DbContext)DatabaseContext.Storage.StorageContext).ChangeTracker.Entries()
                //.Where(e => e.State == EntityState.Added ||
                //            e.State == EntityState.Modified ||
                //            e.State == EntityState.Deleted)
                .ToList();
            foreach (var entity in changedEntriesCopy)
            {
                ((DbContext)DatabaseContext.Storage.StorageContext).Entry(entity.Entity).State = EntityState.Detached;
            }
        }
    }
}
