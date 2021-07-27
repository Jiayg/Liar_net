using System.Collections.Generic;
using Liar.Domain.Shared.BaseModels;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Liar.HttpApi.Host.Controllers
{
    public class BaseController : AbpController
    {
        [NonAction]
        public ResultModel<T> Success<T>(T data, string msg = "成功")
        {
            return new ResultModel<T>()
            {
                success = true,
                msg = msg,
                data = data,
            };
        }

        [NonAction]
        public ResultModel Success(string msg = "成功")
        {
            return new ResultModel()
            {
                success = true,
                msg = msg,
                data = null,
            };
        }

        [NonAction]
        public ResultModel<string> Failed(string msg = "失败", int status = 500)
        {
            return new ResultModel<string>()
            {
                success = false,
                status = status,
                msg = msg,
                data = null,
            };
        }

        [NonAction]
        public ResultModel<T> Failed<T>(string msg = "失败", int status = 500)
        {
            return new ResultModel<T>()
            {
                success = false,
                status = status,
                msg = msg,
                data = default,
            };
        }

        [NonAction]
        public ResultModel<PageModel<T>> SuccessPage<T>(int page, int dataCount, List<T> data, int pageCount, string msg = "获取成功")
        {
            return new ResultModel<PageModel<T>>()
            {
                success = true,
                msg = msg,
                data = new PageModel<T>()
                {
                    page = page,
                    dataCount = dataCount,
                    data = data,
                    pageCount = pageCount,
                }
            };
        }

        [NonAction]
        public ResultModel<PageModel<T>> SuccessPage<T>(PageModel<T> pageModel, string msg = "获取成功")
        {
            return new ResultModel<PageModel<T>>()
            {
                success = true,
                msg = msg,
                data = new PageModel<T>()
                {
                    page = pageModel.page,
                    dataCount = pageModel.dataCount,
                    data = pageModel.data,
                    pageCount = pageModel.pageCount,
                }
            };
        }

    }
}
