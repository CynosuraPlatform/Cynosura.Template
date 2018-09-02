using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cynosura.Template.Web.Models.RoleViewModels
{
    public class RoleUpdateViewModel
    {
        [StringLength(256)]
        public string Name { get; set; }


    }
}
