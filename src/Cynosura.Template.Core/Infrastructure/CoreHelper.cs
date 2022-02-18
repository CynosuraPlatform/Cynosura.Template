﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cynosura.Template.Core.Infrastructure
{
    public static class CoreHelper
    {
        public static Assembly[] GetPlatformAndAppAssemblies()
        {
            var platformAndAppNames = new[] { "Cynosura", "Cynosura.Template" };
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => platformAndAppNames.Any(n => a.FullName!.Contains(n)) ||
                            a.GetReferencedAssemblies()
                                .Any(ra => platformAndAppNames.Any(n => ra.FullName.Contains(n))))
                .ToArray();
        }
    }
}
