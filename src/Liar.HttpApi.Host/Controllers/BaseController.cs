using System.Collections.Generic;
using Liar.Application.Contracts;
using Liar.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Liar.HttpApi.Host.Controllers
{
    public class BaseController : AbpController
    {

        [NonAction]
        public ResultDetails<T> Result<T>(T data)
        {
            return new ResultDetails<T>()
            {
                IsSuccess = true,
                Msg = string.Empty,
                Data = data
            };
        }

        [NonAction]
        public ResultDetails Result(object data)
        {
            return new ResultDetails()
            {
                IsSuccess = true,
                Msg = string.Empty,
                Data = data
            };
        }

        [NonAction]
        public ResultDetails<T> Success<T>(T data, string msg = "成功")
        {
            return new ResultDetails<T>()
            {
                IsSuccess = true,
                Msg = msg,
                Data = data
            };
        }

        [NonAction]
        public ResultDetails Success(string msg = "成功")
        {
            return new ResultDetails()
            {
                IsSuccess = true,
                Msg = msg,
                Data = null
            };
        }

        [NonAction]
        public ResultDetails<string> Failed(string msg = "失败", int status = 500)
        {
            return new ResultDetails<string>()
            {
                IsSuccess = false,
                Status = status,
                Msg = msg,
                Data = null
            };
        }

        [NonAction]
        public ResultDetails<T> Failed<T>(string msg = "失败", int status = 500)
        {
            return new ResultDetails<T>()
            {
                IsSuccess = false,
                Status = status,
                Msg = msg,
                Data = default
            };
        }

        [NonAction]
        public ResultDetails<PageModelDto<T>> SuccessPage<T>(int total, List<T> item, string msg = "请求成功")
        {

            return new ResultDetails<PageModelDto<T>>()
            {
                IsSuccess = true,
                Msg = msg,
                Data = new PageModelDto<T>()
                {
                    Total = total,
                    Item = item
                }
            };
        }

        [NonAction]
        public ResultDetails<PageModelDto<T>> SuccessPage<T>(PageModelDto<T> pageModel, string msg = "请求成功")
        {

            return new ResultDetails<PageModelDto<T>>()
            {
                IsSuccess = true,
                Msg = msg,
                Data = new PageModelDto<T>()
                {
                    Total = pageModel.Total,
                    Item = pageModel.Item
                }
            };
        }
    }
}
