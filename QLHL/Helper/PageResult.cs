namespace QLHL.Helper
{
    public class PageResult<T>
    {
        public Pagination pagination { get; set; }
        public IEnumerable<T> data { get; set; }

        public PageResult(Pagination _pagination, IEnumerable<T> _data)
        {
            pagination = _pagination;
            data = _data;
        }
        public static IEnumerable<T> ToPageResult(Pagination pagination, IEnumerable<T> data)
        {
            pagination.pageNumber = pagination.pageNumber < 1 ? 1 : pagination.pageNumber;
            data = data.Skip(pagination.pageSize * (pagination.pageNumber - 1)).Take(pagination.pageSize).AsQueryable();
            return data;
        }
    }
}
