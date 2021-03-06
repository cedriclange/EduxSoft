// Copyright © 2017-2019 SOFTINUX. All rights reserved.
// Licensed under the MIT License, Version 2.0. See LICENSE file in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Identity;
using SoftinuxBase.Security.Data.Abstractions;
using SoftinuxBase.Security.Data.Entities;
using SoftinuxBase.Security.ViewModels.Account;

namespace SoftinuxBase.Security.Tools
{
    /*
        The main RegsiterUser class
        Contains all methods for registering new users
    */
    internal static class RegisterUser
    {
        /// <summary>
        /// Check if user exists by this name or e-mail.
        /// </summary>
        /// <param name="storage_">the data storage instance.</param>
        /// <param name="value_">the user name or email.</param>
        /// <param name="userManager_">the users manager instance.</param>
        /// <returns>true if exist, false if not exist.</returns>
        internal static bool IsUserExist(IStorage storage_, string value_, UserManager<User> userManager_)
        {
            return storage_.GetRepository<IAspNetUsersRepository>().FindByNormalizedUserNameOrEmail(userManager_.NormalizeKey(value_));
        }

        /// <summary>
        /// Create user into application.
        /// </summary>
        /// <param name="storage_">the data storage instance.</param>
        /// <param name="viewModel_">user object from view.</param>
        /// <param name="userManager_">the users manager instance.</param>
        /// <returns>true if create, false if fail.</returns>
        internal static bool CreateNewUser(IStorage storage_, SignUpViewModel viewModel_, UserManager<User> userManager_)
        {
            return !IsUserExist(storage_, viewModel_.UserName, userManager_) && !IsUserExist(storage_, viewModel_.Email, userManager_);
        }
    }
}