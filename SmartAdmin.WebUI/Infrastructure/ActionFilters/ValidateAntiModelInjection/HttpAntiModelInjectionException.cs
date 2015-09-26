﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartAdmin.WebUI.Infrastructure.ActionFilters
{
    [Serializable]
    public sealed class HttpAntiModelInjectionException : HttpException
    {

        public HttpAntiModelInjectionException()
        {
        }

        private HttpAntiModelInjectionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public HttpAntiModelInjectionException(string message)
            : base(message)
        {
        }

        public HttpAntiModelInjectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}