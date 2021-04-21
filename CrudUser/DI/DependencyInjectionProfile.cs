using CrudUser.Application;
using CrudUser.Application.Contracts;
using CrudUser.Domain;
using CrudUser.Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudUser.DI
{
    public static class DependencyInjectionProfile
    {
        public static void RegisterProfile(IServiceCollection services)
        {
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<IUserDomainService, UserDomainService>();
        }
    }
}
