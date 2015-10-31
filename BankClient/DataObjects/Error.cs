namespace DataObjects
{
    public class Error
    {
        public Error()
        {
        }

        public Error(string description)
        {
            Description = description;
        }

        public int Code { get; set; }

        public string Description { get; set; }
    }
}
