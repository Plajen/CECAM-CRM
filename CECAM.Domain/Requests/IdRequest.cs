using CECAM.Domain.Enums;

namespace CECAM.Domain.Requests
{
    public class IdRequest : BaseRequest
    {
        public int Id { get; set; }

        public IdRequest(int id, CommandEnum command)
        {
            Id = id;
            Command = command;
        }

        public IdRequest() { }
    }
}
