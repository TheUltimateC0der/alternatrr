using System;
using System.Collections.Generic;

#nullable disable

namespace alternatrr
{
    public partial class SceneMapping
    {
        public long Id { get; set; }
        public long TvdbId { get; set; }
        public long? SeasonNumber { get; set; }
        public string SearchTerm { get; set; }
        public string ParseTerm { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public long? SceneSeasonNumber { get; set; }
        public string FilterRegex { get; set; }
        public string SceneOrigin { get; set; }
        public long? SearchMode { get; set; }
        public string Comment { get; set; }
    }
}
