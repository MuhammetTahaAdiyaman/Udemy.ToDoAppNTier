using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy.ToDoAppNTier.Common.ResponseObjects
{
    public class Response<T> : Response,IResponse<T>
    {
        public T Data { get; set; }

        public Response(ResponseType responseType, T data) : base(responseType)
        {
            Data = data;
        }

        //not found için
        public Response(ResponseType responseType,string message) : base(responseType,message)
        {
            
        }


        //validationerror için
        public Response(ResponseType responseType, T data, List<CustomValidationError> errors) : base(responseType)
        {
            Data = data;
            ValidationErrors = errors;
        }
        
            
        
        public List<CustomValidationError> ValidationErrors { get; set; }
    }
}
