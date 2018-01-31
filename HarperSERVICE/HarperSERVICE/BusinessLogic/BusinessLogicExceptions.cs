using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class BusinessLogicException : Exception
    {
        new public string Message;
    }

    /// <summary>
    /// Thrown when an exception occurs while the data is being loaded into the object from the database
    /// </summary>
    public class DataLoadException : BusinessLogicException
    {
        public DataLoadException(string message)
        {
            this.Message = message;
        }
    }


    /// <summary>
    /// Thrown when an exception occurs while the data is being loaded into the object from user supplied parameters
    /// </summary>
    public class ObjectInitException : BusinessLogicException
    {
        public ObjectInitException(string message)
        {
            this.Message = message;
        }
    }


    /// <summary>
    /// Thrown when an exception occurs while the object is being saved to the database
    /// </summary>
    public class ObjectSaveException : BusinessLogicException
    {
        public ObjectSaveException(string message)
        {
            this.Message = message;
        }
    }
}
