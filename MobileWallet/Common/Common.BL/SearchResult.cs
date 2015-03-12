using System.Collections.Generic;

namespace Common.BL
{
    public class SearchResult<TEntity> where TEntity : class 
    {
        public IEnumerable<TEntity> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
