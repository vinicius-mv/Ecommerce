namespace Catalog.Core.Specifications
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageIndex, int pageSize, long count, IReadOnlyCollection<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public Pagination()
        {
        }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public long Count { get; private set; }

        public IReadOnlyCollection<T> Data { get; private set; }
    }
}