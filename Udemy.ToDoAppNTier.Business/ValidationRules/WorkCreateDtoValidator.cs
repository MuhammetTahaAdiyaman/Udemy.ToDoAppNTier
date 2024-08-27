using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.ToDoAppNTier.Dtos.WorkDtos;

namespace Udemy.ToDoAppNTier.Business.ValidationRules
{//bu validasyonu uygulamak için workservice gidelim ve workcreatedto ile çalışan bir metot bulalım.
    public class WorkCreateDtoValidator : AbstractValidator<WorkCreateDto>
    {
        public WorkCreateDtoValidator()
        {
            RuleFor(x => x.Definition).NotEmpty();

        }

        /*
        private bool NotBeTest(string arg) //bu metod true dönerse validasyon geçerli false dönerse validasyon geçersiz olur.
        {
            return arg != "Test" && arg != "test";
        }
        */
    }
}
