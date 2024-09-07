using System.Text.Json.Serialization;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Domain.Paging
{
    public record PagedListDto<T>
    {
        public IEnumerable<T> PagedData { get; set; } = new List<T>();
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PagingMetadata? PagingMetadata { get; set; } = default;
    }
}
