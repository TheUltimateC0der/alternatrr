#nullable disable

namespace alternatrr
{
    public partial class Series
    {
        public long Id { get; set; }
        public long TvdbId { get; set; }
        public long TvRageId { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string TitleSlug { get; set; }
        public string CleanTitle { get; set; }
        public long Status { get; set; }
        public string Overview { get; set; }
        public string AirTime { get; set; }
        public string Images { get; set; }
        public string Path { get; set; }
        public long Monitored { get; set; }
        public long SeasonFolder { get; set; }
        public byte[] LastInfoSync { get; set; }
        public byte[] LastDiskSync { get; set; }
        public long Runtime { get; set; }
        public long SeriesType { get; set; }
        public string Network { get; set; }
        public long UseSceneNumbering { get; set; }
        public byte[] FirstAired { get; set; }
        public byte[] NextAiring { get; set; }
        public long? Year { get; set; }
        public string Seasons { get; set; }
        public string Actors { get; set; }
        public string Ratings { get; set; }
        public string Genres { get; set; }
        public string Certification { get; set; }
        public string SortTitle { get; set; }
        public long? QualityProfileId { get; set; }
        public string Tags { get; set; }
        public byte[] Added { get; set; }
        public string AddOptions { get; set; }
        public long TvMazeId { get; set; }
    }
}
