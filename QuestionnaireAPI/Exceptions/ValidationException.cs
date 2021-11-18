using System;

namespace QuestionnaireAPI.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message):base(message)
        {
            
        }
    }
}