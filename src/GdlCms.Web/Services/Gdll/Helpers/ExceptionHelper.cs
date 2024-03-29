﻿using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace GdlCms.Web.Services.Gdll.Helpers
{
    public static class ExceptionHelper
    {
        public static void Log(this Exception exception, string username)
        {
            if (exception is AggregateException aggregateException)
            {
                exception = aggregateException.Flatten();
            }

            var exceptionMessage = exception.GetRecursiveExceptionMessage(out string stackTrace);
            
            var trace = new StackTrace();
            var method = trace.GetFrame(1).GetMethod();
            var fullQualifiedMethodName = method.DeclaringType + "." + method.Name;

            var frameLevel2 = trace.GetFrame(2);
            MethodBase methodLevel2 = null;
            if (frameLevel2 != null) methodLevel2 = frameLevel2.GetMethod();
            if (methodLevel2 != null)
            {
                fullQualifiedMethodName = fullQualifiedMethodName + "--->" + methodLevel2.DeclaringType + "." +
                                          methodLevel2.Name;
            }

            var errorMessage = $"Exception in {fullQualifiedMethodName}\r\n{exceptionMessage}\r\n\r\nSTACKTRACE:\r\n{stackTrace}";

            if (!string.IsNullOrWhiteSpace(username))
            {
                errorMessage = $"{username} - {errorMessage}";
            }
            
        }

       private static string GetRecursiveExceptionMessage(this Exception exception, out string stackTrace)
        {
            var aggregateExceptionMessage = string.Empty;
            if (exception is AggregateException)
            {
                exception = GetAggregateException(exception, ref aggregateExceptionMessage);
            }

            var exceptionMessage = GetExceptionMessage(exception, out stackTrace, aggregateExceptionMessage);

            return exceptionMessage;
        }
        
        private static Exception GetAggregateException(Exception exception, ref string aggregateExceptionMessage)
        {
            var aggregateException = (AggregateException)exception;
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(aggregateExceptionMessage);
            foreach (var innerException in aggregateException.InnerExceptions)
            {
                var aggregateMessage = innerException + ". " + innerException.StackTrace;
                if (!aggregateExceptionMessage.Contains(aggregateMessage))
                    stringBuilder.Append(aggregateMessage + ". ");
            }

            aggregateExceptionMessage = stringBuilder.ToString();

            exception = ((AggregateException)exception).Flatten();
            return exception;
        }
        
        private static string GetExceptionMessage(Exception exception, out string stackTrace, string aggregateExceptionMessage)
        {
            var exceptionMessage = new StringBuilder();
            exceptionMessage.Append(exception.Message);
            var stackTraceStringBuilder = new StringBuilder();
            stackTraceStringBuilder.Append(exception.StackTrace);

            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                exceptionMessage.Append(Environment.NewLine + exception.Message);
                stackTraceStringBuilder.Append(Environment.NewLine + exception.StackTrace);
            }

            if (!string.IsNullOrEmpty(aggregateExceptionMessage))
                exceptionMessage.Append(". " + aggregateExceptionMessage);

            stackTrace = stackTraceStringBuilder.ToString();
            return exceptionMessage.ToString();
        }
    }
}