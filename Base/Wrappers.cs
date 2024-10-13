namespace No_Overspend_Api.Base
{
    public class Response<T>
    {
        public T? Result { get; set; }
        public string[]? Errors { get; set; }

        public string? Message { get; set; }
        public Response() { }
        public Response(T result)
        {
            Result = result;
        }
    }

    public class PagedResponse<T> : Paged
    {
        public PagedResponse() { }
        public PagedResponse(List<T> items)
        {
            Items = items;
        }
        public List<T>? Items { get; set; }
    }

    public class Paging
    {
        public Paging() { }
        public int page_size { get; set; } = 10;
        public int page_index { get; set; } = 1;
        public string? keyword { get; set; }
    }
    public class Paged : Paging
    {
        public int total_items { get; set; }
    }
}
