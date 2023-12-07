namespace QLHL.Helper
{
    public class Pagination
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public int totalCount { get; set; }
        public int totalPages
        {
            get
            {
                if (pageSize == 0) return 0;
                int total = totalCount / pageSize;
                if (totalCount % pageSize != 0) total++;
                return total;
            }
        }
        public Pagination()
        {
            pageSize = -1;
            pageNumber = 1;
        }
    }
}
