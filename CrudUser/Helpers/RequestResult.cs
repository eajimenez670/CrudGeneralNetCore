using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudUser.Helpers
{
    public sealed class RequestResult<T>
    {
        public RequestResult()
        {

        }

        public bool IsSuccessful { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<string> Messages { get; set; }
        public T Result { get; set; }

        public static RequestResult<T> CreateEntityAlreadyExists(string entityId = null)
        {
            var requestResult = new RequestResult<T>();
            requestResult.IsSuccessful = false;
            requestResult.IsError = false;
            requestResult.ErrorMessage = null;
            requestResult.Messages = new string[] { string.Format("La entidad con id {0} ya existe", entityId) };
            requestResult.Result = default(T);

            return requestResult;
        }

        public static RequestResult<T> CreateEntityNotExists(string entityId = null)
        {
            var requestResult = new RequestResult<T>();
            requestResult.IsSuccessful = false;
            requestResult.IsError = false;
            requestResult.ErrorMessage = null;
            requestResult.Messages = new string[] { string.Format("La entidad con id {0} no existe", entityId) };
            requestResult.Result = default(T);

            return requestResult;
        }
        public static RequestResult<T> CreateError(string errorMessage)
        {
            var requestResult = new RequestResult<T>();
            requestResult.IsSuccessful = false;
            requestResult.IsError = true;
            requestResult.ErrorMessage = errorMessage;
            requestResult.Messages = null;
            requestResult.Result = default(T);

            return requestResult;
        }

        public static RequestResult<T> CreateSuccessful(T result)
        {
            var requestResult = new RequestResult<T>();
            requestResult.IsSuccessful = true;
            requestResult.IsError = false;
            requestResult.ErrorMessage = null;
            requestResult.Messages = null;
            requestResult.Result = result;

            return requestResult;
        }

        public static RequestResult<T> CreateUnsuccessful(IEnumerable<string> messages)
        {
            var requestResult = new RequestResult<T>();
            requestResult.IsSuccessful = false;
            requestResult.IsError = false;
            requestResult.ErrorMessage = null;
            requestResult.Messages = messages;
            requestResult.Result = default(T);

            return requestResult;
        }

    }
}
