﻿using GdlCms.Web.Services;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace GdlCms.Web.Composing
{
    public class RegisterServicesComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<ISmtpService, SmtpService>(Lifetime.Singleton);
        }
    }
}