namespace BankServerApi.DataObjects
{
    public class Message
    {
        public Message()
        {

        }

        public Message(int code, string description)
        {
            Code = code;
            Description = description;
        }

        public int Code { get; set; }

        public string Description { get; set; }
    }
}
