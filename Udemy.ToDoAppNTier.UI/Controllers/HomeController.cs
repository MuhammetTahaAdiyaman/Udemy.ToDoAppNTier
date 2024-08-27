using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Udemy.ToDoAppNTier.Business.Interfaces;
using Udemy.ToDoAppNTier.Common.ResponseObjects;
using Udemy.ToDoAppNTier.Dtos.WorkDtos;
using Udemy.ToDoAppNTier.UI.Extensions;

namespace Udemy.ToDoAppNTier.UI.Controllers
{
    public class HomeController : Controller
    {
        //şimdi burada bizim işimiz work ile olduğu için workservice üzerinden verilerimizi çağıracağız veya dbye göndereceğiz.
        private readonly IWorkService _workService;
        
        public HomeController(IWorkService workService, IMapper mapper)
        {
            _workService = workService;
            
        }

        //bizim metotlarımız async olduğu için burada da async kullanmalıyız action metodları
        public async Task<IActionResult> Index()
        {
            var response = await _workService.GetAll();
            return View(response.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new WorkCreateDto()); //asp-for kullanmak için bunu dönüyoruz.
        }

        //biz Create viewinde post ile WorkCreateDto gönderiyoruz onu post metodunda parametre ile karşılamamız gerek.
        [HttpPost]
        public async Task<IActionResult> Create(WorkCreateDto dto)
        {
            var response = await _workService.Create(dto);
            return this.ResponseRedirectToAction(response, "Index");
        }

        //get metodunda update sayfamızı oluşturalım.
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            //var dto = await _workService.GetById(id);
            //return View(_mapper.Map<WorkUpdateDto>(dto));
            var response = await _workService.GetById<WorkUpdateDto>(id);
            return this.ResponseView(response); 
        }

        [HttpPost]
        public async Task<IActionResult> Update(WorkUpdateDto dto)
        {
            var response = await _workService.Update(dto);
            return this.ResponseRedirectToAction(response, "Index");
        }

        public async Task<IActionResult> Remove(int id)
        {
            var response =  await _workService.Remove(id);
            return this.ResponseRedirectToAction(response, "Index");
        }

        public IActionResult NotFound(int code)
        {
            return View();
        }
    }
}
