using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.ToDoAppNTier.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Udemy.ToDoAppNTier.Business.Interfaces;
using Udemy.ToDoAppNTier.Business.Services;
using Udemy.ToDoAppNTier.DataAccess.UnitOfWork;
using AutoMapper;
using Udemy.ToDoAppNTier.Business.Mappings.AutoMapper;
using FluentValidation;
using Udemy.ToDoAppNTier.Dtos.WorkDtos;
using Udemy.ToDoAppNTier.Business.ValidationRules;

namespace Udemy.ToDoAppNTier.Business.DependencyResolvers.Microsoft
{//extension class olması için hem class hem de metot static olmalıdır.
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            //buraya connection string yazabiliriz.
            services.AddDbContext<ToDoContext>(opt =>
            {
                opt.UseSqlServer("server=TAHA\\SQLEXPRESS; database=ToDoDb; integrated security=true;");
            });
            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new WorkProfile());
            });
            var mapper = configuration.CreateMapper();
            //bu mapper'ı dependency injection ile ele alabilmek için
            services.AddSingleton(mapper); //burada şunu diyoruz mapper gördüğün zaman ımapper ile hareket et
            services.AddScoped<IWorkService, WorkService>();
            services.AddScoped<IUow, Uow>();

            //fluent validasyonu dependency injection ile ele almak için
            services.AddTransient<IValidator<WorkCreateDto>, WorkCreateDtoValidator>();
            services.AddTransient<IValidator<WorkUpdateDto>, WorkUpdateDtoValidator>();
        }
    }
}
//şimdi bu metodumuzu gidiğ UI katmanında startup.cs de ConfigureServices içinde çağıralım.
