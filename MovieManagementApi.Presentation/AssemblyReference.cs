using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MovieManagementApi.Presentation
{
    public class AssemblyReference
    {
    }

    public static class ResponseBuilder
    {
        public static GlobalResponse<T> BuildResponse<T>(ModelStateDictionary errs, T data)
        {
            var listOfErrorItems = new List<ErrorItemModel>();
            var benchMark = new List<string>();

            if (errs != null)
            {
                foreach (var err in errs)
                {
                    ///err.error.errors
                    var key = err.Key;
                    var errValues = err.Value;
                    var errList = new List<string>();
                    foreach (var errItem in errValues.Errors)
                    {
                        errList.Add(errItem.ErrorMessage);
                        if (!benchMark.Contains(key))
                        {
                            listOfErrorItems.Add(new ErrorItemModel { Key = key, ErrorMessages = errList });
                            benchMark.Add(key);
                        }
                    }
                }
            }

            var response = new GlobalResponse<T>
            {
                Data = data,
                Errors = listOfErrorItems
            };

            return response;
        }
    }

    public class ErrorItemModel
    {
        public string Key { get; set; }
        public List<string> ErrorMessages { get; set; }
    }

    public class GlobalResponse<T>
    {
        public T Data { get; set; }
        public List<ErrorItemModel> Errors { get; set; }

        public GlobalResponse()
        {
            Errors = new List<ErrorItemModel>();
        }

        public static GlobalResponse<T> Fail(string errorMessage)
        {
            return new GlobalResponse<T>()
            {
                Errors = new List<ErrorItemModel>()
                {
                    new ErrorItemModel()
                    {
                        Key = "Failed",
                        ErrorMessages = new List<string>(){errorMessage}
                    }
                }
            };
        }
    }

}
