using System;

namespace QuestionnaireAPI.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message)
        {
            
        }
    }
}