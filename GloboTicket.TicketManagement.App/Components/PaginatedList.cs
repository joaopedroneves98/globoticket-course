namespace GloboTicket.TicketManagement.App.Components
{
    public class PaginatedList<T>
    {
        public PaginatedList()
        {

        }

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public List<T> Items { get; set; }

        public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.Items = new List<T>();
            this.Items.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (this.PageIndex > 1);
            }
            set { }
        }

        public bool HasNextPage
        {
            get
            {
                return (this.PageIndex < this.TotalPages);
            }
            set { }
        }
    }
}
