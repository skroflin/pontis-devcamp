namespace DemoApp.Domain.Paging.Models
{
    public class PagingMetadata
    {
        public int Offset { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }

        public PagingMetadata() { }

        public PagingMetadata(int count, PagingMetadata pagedMetadata)
        {
            Offset = pagedMetadata.Offset;
            PageCount = count != 0 ? count / pagedMetadata.PageSize : 0;
            PageIndex = pagedMetadata.PageIndex;
            PageSize = pagedMetadata.PageSize;
            TotalCount = count;
        }
    }
}

