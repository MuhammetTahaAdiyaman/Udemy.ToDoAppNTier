﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.ToDoAppNTier.Dtos.Interfaces;

namespace Udemy.ToDoAppNTier.Dtos.WorkDtos
{//neden worklistdto kullanmadık aslında aynı proplar ikisinde de çünkü update yaparken validation işlemleri yapılabilir. bundan dolayı ayrı dto kullandık.
    public class WorkUpdateDto : IDto
    {
        //[Range(1,int.MaxValue,ErrorMessage ="Id is required")]
        public int Id { get; set; }
        //[Required(ErrorMessage ="Definition is required")]
        public string Definition { get; set; }
        public bool IsCompleted { get; set; }
    }
}
