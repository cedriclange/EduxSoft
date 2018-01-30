﻿// Copyright © 2017 SOFTINUX. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Security.Data.Abstractions;
using Security.Data.Entities;

namespace Security.Data.EntityFramework
{
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {
        public virtual Group FindBy(string code_, string originExtensionAssemblyName_)
        {
            return dbSet.FirstOrDefault(e_ => e_.Name == code_);
        }

        public virtual IEnumerable<Group> All()
        {
            return dbSet.ToList();
        }

        public virtual void Create(Group entity_)
        {
            dbSet.Add(entity_);
        }

        public virtual void Edit(Group entity_)
        {
            storageContext.Entry(entity_).State = EntityState.Modified;
        }

        public virtual void Delete(string entityId_)
        {
            var entity = dbSet.FirstOrDefault(e_ => e_.Id == entityId_);
            if(entity != null)
                dbSet.Remove(entity);
        }
    }
}