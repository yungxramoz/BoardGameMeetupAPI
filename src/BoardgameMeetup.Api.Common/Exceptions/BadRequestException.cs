using System;

namespace BoardgameMeetup.Api.Common.Exceptions
{
    public class BadRequestException: Exception
    {
        public BadRequestException(string message): base(message)
        {
        }
    }
}
