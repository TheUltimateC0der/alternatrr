namespace alternatrr.Models
{
    public class AddMappingInputModel
    {
        public Series Series { get; set; }

        public long SeriesId { get; set; }

        public string SearchTerm { get; set; }
    }
}