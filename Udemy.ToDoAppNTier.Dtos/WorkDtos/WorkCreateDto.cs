using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.ToDoAppNTier.Dtos.Interfaces;

namespace Udemy.ToDoAppNTier.Dtos.WorkDtos
{
    public class WorkCreateDto : IDto
    {   //bunu yaptığımız zaman single responsibility çiğnemiş oluyoruz ama düzelteceğiz.
        //[Required(ErrorMessage = "Definition is required")]
        public string Definition { get; set; }
        public bool IsCompleted { get; set; }
    }
}
