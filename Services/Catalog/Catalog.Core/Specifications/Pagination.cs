namespace Catalog.Core.Specifications
{
    public class Pagination<T> where T : class
    {
        public Pagination()
        {
        }

        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyCollection<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public int Count { get; private set; }

        public IReadOnlyCollection<T> Data { get; private set; }
    }
}