// Copyright © 2017-2019 SOFTINUX. All rights reserved.
// Licensed under the MIT License, Version 2.0. See LICENSE file in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SoftinuxBase.Barebone.Controllers
{
    public class AboutController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return await Task.Run(() => View());
        }
    }
}
