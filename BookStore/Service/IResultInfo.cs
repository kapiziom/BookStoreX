using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public interface IResultInfo
    {
        HttpResult SetResult(bool succeeded, List<Errors> errors);
    }
}
