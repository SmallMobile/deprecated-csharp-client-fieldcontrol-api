﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldControlApi.Resources
{
    public abstract class Resource
    {
        public int Id { get; set; }

        public virtual object GetPayload()
        {
            return this;
        }
    }
}
