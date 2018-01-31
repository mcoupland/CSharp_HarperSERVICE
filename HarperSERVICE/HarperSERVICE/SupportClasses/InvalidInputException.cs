using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupportClasses
{
    public class InvalidInputException : Exception
    {
        private string _Message;
        public override string Message
        {
            get
            {
                return _Message;
            }
        }
        public InvalidInputException(string message)
        {
            _Message = message;
        }
    }
}
