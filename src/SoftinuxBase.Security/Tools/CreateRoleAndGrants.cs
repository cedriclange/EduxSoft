// Copyright © 2017-2019 SOFTINUX. All rights reserved.
// Licensed under the MIT License, Version 2.0. See LICENSE file in the project root for license information.

using System;
using System.Linq;
using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Identity;
using SoftinuxBase.Security.Data.Abstractions;
using SoftinuxBase.Security.Data.Entities;
using SoftinuxBase.Security.ViewModels.Permissions;

namespace SoftinuxBase.Security.Tools
{
    /*
        The main CreateRoleAndGrants class
        Contains all methods for reading granting permissions
    */
    public static class CreateRoleAndGrants
    {
        /// <summary>
        /// First, check that a role with this name doesn't already exist.
        /// Second, save new data into database.
        /// </summary>
        /// <param name="storage_">the data storage instance.</param>
        /// <param name="roleManager_">asp identity role manager object.</param>
        /// <param name="model_">model with role name and grand data (extensions and permissions level).</param>
        /// <returns>Not null when something failed, else null when save went ok.</returns>
        public static async Task<string> CheckAndSaveNewRoleAndGrants(IStorage storage_, RoleManager<IdentityRole<string>> roleManager_, SaveNewRoleAndGrantsViewModel model_)
        {
            if (await UpdateRoleAndGrants.CheckThatRoleOfThisNameExists(roleManager_, model_.RoleName))
            {
                return "A role with this name already exists";
            }

            if (model_.Extensions == null || !model_.Extensions.Any())
            {
                return "At least one extension must be selected";
            }

            try
            {
                // Convert the string to the enum
                if (Enum.TryParse<Common.Enums.Permission>(model_.PermissionValue, true, out var permissionEnumValue))
                {
                    var permissionEntity = storage_.GetRepository<IPermissionRepository>().Find(permissionEnumValue);

                    // Save the Role
                    IdentityRole<string> identityRole = new IdentityRole<string>
                    {
                        // Auto-incremented ID
                        Name = model_.RoleName
                    };
                    await roleManager_.CreateAsync(identityRole);

                    // Save the role-extension-permission link
                    if (model_.Extensions != null)
                    {
                        IRolePermissionRepository repo = storage_.GetRepository<IRolePermissionRepository>();
                        foreach (string extension in model_.Extensions)
                        {
                            repo.Create(new RolePermission
                            {
                                RoleId = identityRole.Id,
                                PermissionId = permissionEntity.Id,
                                Extension = extension
                            });
                        }
                    }
                }

                storage_.Save();

                return null;
            }
            catch (Exception e)
            {
                return $"{e.Message} {e.StackTrace}";
            }
        }
    }
}