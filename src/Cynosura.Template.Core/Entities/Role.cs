using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Cynosura.Template.Core.Entities
{
    public class Role : IdentityRole<int>
    {
        [Required()]
        public DateTime CreationDate { get; set; }

        [Required()]
        public DateTime ModificationDate { get; set; }
    }
}
