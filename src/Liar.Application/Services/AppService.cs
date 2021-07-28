using System;
using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using JetBrains.Annotations;
using Liar.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Services;

namespace Liar.Application
{
    public class AppService : ApplicationService, IAppService
    {
        public ResultDetails Fail(string msg)
        {
            return new ResultDetails()
            {
                IsSuccess = false,
                Status = (int)HttpStatusCode.NoContent,
                Data = default,
                Msg = msg
            };
        }

        //public ResultDetails Fail(HttpStatusCode? httpCode, string msg)
        //{
        //    return new ResultDetails()
        //    {
        //        IsSuccess = false,
        //        Status = (int)httpCode.Value,
        //        Data = default,
        //        Msg = msg
        //    };
        //}

        public ResultDetails<T> Fail<T>(HttpStatusCode httpCode, string msg)
        {
            return new ResultDetails<T>()
            {
                IsSuccess = true,
                Status = (int)httpCode,
                Msg = string.Empty,
                Data = default,
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

    }
}
