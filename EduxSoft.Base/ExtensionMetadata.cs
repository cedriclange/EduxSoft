﻿using SoftinuxBase.Infrastructure;
using SoftinuxBase.Infrastructure.Interfaces;
using SoftinuxBase.Security.Common.Attributes;
using SoftinuxBase.Security.Common.Enums;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace EduxSoft.Base
{
    public class ExtensionMetadata : IExtensionMetadata
    {
        public IEnumerable<StyleSheet> StyleSheets => new[]
        {
            new StyleSheet("/Styles.base.css",610),
        };

        public IEnumerable<Script> Scripts => new Script[]
        {
            new Script("/Scripts.base.js", 720)
        };

        public IEnumerable<MenuGroup> MenuGroups
        {
            get
            {
                MenuItem[] menuItems = new[]
                {
                    new MenuItem("/configuration","General", 100, null,new List<PermissionRequirementAttribute>(new []{new PermissionRequirementAttribute(Permission.Admin), } )),
                    new MenuItem("/configuration/chl","Interne",200, null,new List<PermissionRequirementAttribute>(new []{new PermissionRequirementAttribute(Permission.Admin), new PermissionRequirementAttribute(Permission.Read), new PermissionRequirementAttribute(Permission.Write) } ))
                };
                return new MenuGroup[]
                {
                    new MenuGroup(
                        "Configuration",
                        50, // always last
                        menuItems,
                        "fa-cog")
                };
            }
        }

        bool IExtensionMetadata.IsAvailableForPermissions => true;

        public Assembly CurrentAssembly => Assembly.GetExecutingAssembly();

        public string CurrentAssemblyPath => CurrentAssembly.Location;

        public string Name => CurrentAssembly.GetName().Name;

        public string Description => Attribute.GetCustomAttribute(CurrentAssembly, typeof(AssemblyDescriptionAttribute)).ToString();


        public string Url => Attribute.GetCustomAttribute(CurrentAssembly, typeof(AssemblyTitleAttribute)).ToString();

        public string Version => Attribute.GetCustomAttribute(CurrentAssembly, typeof(AssemblyVersionAttribute)).ToString();

        public string Authors => Attribute.GetCustomAttribute(CurrentAssembly, typeof(AssemblyCompanyAttribute)).ToString();
    }
}
