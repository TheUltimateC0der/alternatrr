using System.Collections.Generic;

namespace alternatrr.Models
{
    public class AddMappingViewModel
    {
        public Series Series { get; set; }

        public IList<SceneMapping> SceneMappings { get; set; }

    }
}