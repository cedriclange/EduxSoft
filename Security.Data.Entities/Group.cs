﻿// Copyright © 2017 SOFTINUX. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Security.Data.Entities
{
    public class Group : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }
}