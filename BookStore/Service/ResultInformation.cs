using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public class ResultInfo : IResultInfo
    {
        public HttpResult SetResult(bool succeeded, List<Errors> errors)
        {
            if(succeeded == false)
            {
                HttpResult result = new HttpResult()
                {
                    succeeded = false,
                    errors = errors
                };
                return result;
            }
            return new HttpResult { succeeded = true, errors = null};
        }
    }
}
