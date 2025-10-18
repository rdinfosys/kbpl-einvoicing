using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using KBPL.GST.Services.Implementation;
using KBPL.GST.Services.Interfaces;

namespace KBPL.GST.Services
{
    public class SevicesDllInjector
    {
        public static void InjectDependencies(IServiceCollection services, IConfiguration Configurations)
        {

            services.AddScoped<ISalesInvoiceService, SalesInvoiceService>();

        }
    }
}
