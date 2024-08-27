using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.ToDoAppNTier.Business.Extensions;
using Udemy.ToDoAppNTier.Business.Interfaces;
using Udemy.ToDoAppNTier.Business.ValidationRules;
using Udemy.ToDoAppNTier.Common.ResponseObjects;
using Udemy.ToDoAppNTier.DataAccess.UnitOfWork;
using Udemy.ToDoAppNTier.Dtos.Interfaces;
using Udemy.ToDoAppNTier.Dtos.WorkDtos;
using Udemy.ToDoAppNTier.Entities.Domains;

namespace Udemy.ToDoAppNTier.Business.Services
{
    public class WorkService : IWorkService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<WorkCreateDto> _createDtoValidator;
        private readonly IValidator<WorkUpdateDto> _updateDtoValidator;
        public WorkService(IUow uow, IMapper mapper, IValidator<WorkCreateDto> createDtoValidator, IValidator<WorkUpdateDto> updateDtoValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
        }

        public async Task<IResponse<WorkCreateDto>> Create(WorkCreateDto dto)
        {
            var resultValidation = _createDtoValidator.Validate(dto);
            if(resultValidation.IsValid)
            {
                await _uow.GetRepository<Work>().Create(_mapper.Map<Work>(dto));
                await _uow.SaveChanges();
                return new Response<WorkCreateDto>(ResponseType.Success, dto);
            }
            else
            {
                return new Response<WorkCreateDto>(ResponseType.ValidationError,dto,resultValidation.ConvertToCustomValidationError());
            }


            //fluent validasyonu kullanalım. Tabi burada bağımlıyız bunuda düzelteceğiz.
            //var validator = new WorkCreateDtoValidator();
            //var resultValidation = validator.Validate(dto);
            //if(resultValidation.IsValid) 
            //{
            //    await _uow.GetRepository<Work>().Create(_mapper.Map<Work>(dto));
            //    await _uow.SaveChanges();
            //}
            //elimizde dto var bunu entity'e çevirmemiz gerek.  
            //biz db'ye kayıt atıyoruz burada bundan dolayı dto'yu entity'e çevirmemiz gerekiyor.
            //await _uow.GetRepository<Work>().Create(_mapper.Map<Work>(dto));

            //await _uow.SaveChanges();
        }

        public async Task<IResponse<List<WorkListDto>>> GetAll()
        { 
        //{   //biz db'den kayıt çekiyor bundan dolayı entity'i dto'ya çevirmemiz gerekiyor.
        //    //öncelikle verimizi çekelim buradan entity list dönüyor bunu dtoliste çevirmemiz gerek.
        //    var list = await _uow.GetRepository<Work>().GetAll();
        //    //bizden worklistdto dönmesi için liste oluşturalım.
        //    var workList = new List<WorkListDto>();
        //    //mapping işlemini gerçekleştirelim.
        //    if(list != null && list.Count > 0)
        //    {
        //        foreach (var work in list)
        //        {
        //            workList.Add(new WorkListDto()
        //            {
        //                Definition = work.Definition,
        //                Id = work.Id,
        //                IsCompleted = work.IsCompleted
        //            });
        //        }
        //    }
        //    return workList;
            var data = _mapper.Map<List<WorkListDto>>(await _uow.GetRepository<Work>().GetAll());
            return new Response<List<WorkListDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
           //burada da db'den kayıt çekiyoruz bundan dolayı entity'i dto'ya çevirmemiz gerek.
           //burada entity geliyor bunu dto'ya çevirelim
           //var work = await _uow.GetRepository<Work>().GetByFilter(x=>x.Id == id);

           // return new WorkListDto()
           // {
           //     Definition = work.Definition,
           //     Id = work.Id,
           //     IsCompleted = work.IsCompleted
           // };

            //mapper kullanalım.
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id));
            if(data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success,data);
        }

        public async Task<IResponse> Remove(int id)
        {   
            var removedEntity = await _uow.GetRepository<Work>().GetByFilter(x=>x.Id== id);
            if(removedEntity != null)
            {
                _uow.GetRepository<Work>().Remove(removedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");

            //biz deletedworkü repository içinde çekmemiz daha doğru olur
            //_uow.GetRepository<Work>().Remove(id);
            //await _uow.SaveChanges();


            //ilk önce silinecek nesneyi id üzerinden bulalım
        //    var deletedWork = await _uow.GetRepository<Work>().GetById(id);
        //    _uow.GetRepository<Work>().Remove(deletedWork);
        //    await _uow.SaveChanges();
        }

        public async Task<IResponse<WorkUpdateDto>> Update(WorkUpdateDto dto)
        {
            //fluent validasyon kullanalım.
            var result = _updateDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Work>().Find(dto.Id);
                if(updatedEntity != null)
                {
                    _uow.GetRepository<Work>().Update(_mapper.Map<Work>(dto),updatedEntity);
                    await _uow.SaveChanges();
                    return new Response<WorkUpdateDto>(ResponseType.Success,dto);
                }
                return new Response<WorkUpdateDto>(ResponseType.NotFound, $"{dto.Id} ye ait data bulunamadı");
               
            }
            else
            {
                
                return new Response<WorkUpdateDto>(ResponseType.ValidationError, dto, result.ConvertToCustomValidationError());
            }

            //_uow.GetRepository<Work>().Update(_mapper.Map<Work>(dto));

            //await _uow.SaveChanges();
        }
    }
}
