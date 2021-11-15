using System;

namespace QuestionnaireAPI.Exceptions
{
    public class ResourceDoesExistException : Exception
    {
        public ResourceDoesExistException(string message) : base(message)
        {
            
        }
    }
}