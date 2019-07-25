// <copyright file="ExtensionMetadata.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Reflection;
using SoftinuxBase.Infrastructure;
using SoftinuxBase.Infrastructure.Interfaces;
using SoftinuxBase.Security.Common.Attributes;
using SoftinuxBase.Security.Common.Enums;

namespace EduxSoft.Student
{
    public class ExtensionMetadata : IExtensionMetadata
    {
        public IEnumerable<StyleSheet> StyleSheets => new StyleSheet[]
        {

        };

        public IEnumerable<Script> Scripts => new Script[]
        {
            new Script("/Scripts.student.js", 820)
        };
        public IEnumerable<MenuGroup> MenuGroups
        {
            get
            {
                MenuItem[] menuItems = new[]
                {
                    new MenuItem("/student", "Acceuil", 100, null, new List<PermissionRequirementAttribute>( new []{ new PermissionRequirementAttribute(Permission.Admin), })),
                    new MenuItem("/student/enrollement", "Nouvelle Inscription", 200, null, new List<PermissionRequirementAttribute>( new []{ new PermissionRequirementAttribute(Permission.Admin), })),
                    new MenuItem("/student/list", "Liste des élèves", 300, null, new List<PermissionRequirementAttribute>( new []{ new PermissionRequirementAttribute(Permission.Admin), })),
                };
                return new MenuGroup[]
                {
                    new MenuGroup(
                        "Gestion des Eleves",
                        1,
                        menuItems,
                        "fas fa-users"),
                };
            }
        }


        /// <inheritdoc/>
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
