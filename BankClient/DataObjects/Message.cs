using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
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
