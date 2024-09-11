namespace DemoApp.Domain.Paging.Models
{
    public class TableMetadata
    {
        public List<FilterMetadata>? FilterMetadata { get; set; }
        public List<SortMetadata>? SortMetadata { get; set; }
        public PagingMetadata? PagingMetadata { get; set; }
    }
}
