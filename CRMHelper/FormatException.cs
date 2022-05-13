using System;

namespace CRMHelper
{
    public static class ExceptionHelper
    {
        public static string FormatException(Exception ex)
        {
            return ex.ToString();
        }
    }
}
