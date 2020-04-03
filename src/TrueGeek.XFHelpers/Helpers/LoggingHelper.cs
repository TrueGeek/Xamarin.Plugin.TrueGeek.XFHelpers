using System.Collections.Generic;
using TrueGeek.XFHelpers.Models;

namespace TrueGeek.XFHelpers.Helpers
{

    public static class LoggingHelper
    {        

        public static void Log(string messageTemplate, LogLevel logLevel, Dictionary<string, object> properties = null)
        {

            Init.LoggingReference?.Invoke(messageTemplate, (int)logLevel, properties);

        }

    }

}