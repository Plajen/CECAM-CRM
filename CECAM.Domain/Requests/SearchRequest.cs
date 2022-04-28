namespace CECAM.Domain.Requests
{
    public class SearchRequest : BaseRequest
    {
        public string? SearchProperty { get; set; }
        public string? SearchValue { get; set; }
        public string? OrderBy { get; set; }
        public int? Take { get; set; }
        public int? Skip { get; set; }

        public SearchRequest()
        {
            OrderBy = "Id ASC";
            Take = 50;
        }
    }
}
