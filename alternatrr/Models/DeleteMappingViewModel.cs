using System.ComponentModel.DataAnnotations;

namespace alternatrr.Models
{
    public class DeleteMappingViewModel
    {
        [Required]
        public long MappingId { get; set; }

        public Series Series { get; set; }

        public SceneMapping SceneMapping { get; set; }
    }
}