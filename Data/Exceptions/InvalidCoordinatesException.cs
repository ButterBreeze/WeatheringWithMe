using System;

namespace Data.Exceptions
{
   
    [Serializable]
    public class InvalidCoordinatesException : Exception
    {
        public InvalidCoordinatesException ()
        {}

        public InvalidCoordinatesException (string message) 
            : base(message)
        {}

        public InvalidCoordinatesException (string message, Exception innerException)
            : base (message, innerException)
        {}    
    }
}