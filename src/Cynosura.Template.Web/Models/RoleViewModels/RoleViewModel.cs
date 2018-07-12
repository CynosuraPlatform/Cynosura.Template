using System.ComponentModel.DataAnnotations;

namespace Cynosura.Template.Web.Models.RoleViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
