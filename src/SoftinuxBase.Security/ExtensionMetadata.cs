﻿// Copyright © 2017-2019 SOFTINUX. All rights reserved.
// Licensed under the MIT License, Version 2.0. See LICENSE file in the project root for license information.

using System.Collections.Generic;
using SoftinuxBase.Infrastructure;
using SoftinuxBase.Infrastructure.Interfaces;
using SoftinuxBase.Security.Common.Attributes;
using SoftinuxBase.Security.Common.Enums;

namespace SoftinuxBase.Security
{
    public class ExtensionMetadata : IExtensionMetadata
    {
        bool IExtensionMetadata.IsAvailableForPermissions => true;
        public IEnumerable<StyleSheet> StyleSheets => new[]
        {
                new StyleSheet("/Styles.Security.css", 510),
        };
        public IEnumerable<Script> Scripts => new Script[]
        {
            new Script("/Scripts.security_user.min.js", 710),
        };

        public IEnumerable<MenuGroup> MenuGroups
        {
            get
            {
                MenuItem[] menuItems_ = new[]
                                    {
                        new MenuItem("/administration", "Main", 100, null, new List<PermissionRequirementAttribute>(new[] { new PermissionRequirementAttribute(Permission.Admin), }))
                                    };
                return new MenuGroup[]
                {
                    new MenuGroup(
                        "Administration",
                        0, // Always first
                        menuItems_,
                        "fa-wrench")
                };
            }
        }
    }
}
