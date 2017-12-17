﻿// Copyright © 2017 SOFTINUX. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Security.Data.Entities
{
    public class User : IdentityUser<int>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime FirstConnection { get; set; }
        public DateTime LastConnection { get; set; }

        public virtual ICollection<IdentityUserRole<int>> UserRoles { get; set; }
        public virtual ICollection<UserGroup> GroupUsers { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        public virtual ICollection<IdentityUserToken<int>> UserTokens { get; set; }
        public virtual ICollection<IdentityUserLogin<int>> UserLogins { get; set; }
    }
}
