using System;
using JetBrains.Annotations;

namespace Liar.Application.Contracts
{
    /// <summary>
    /// Application返回结果包装类,无返回类型(void,task)
    /// </summary>
    public sealed class AppSrvResult
    {
        public AppSrvResult()
        {
        }

        public AppSrvResult([NotNull] ResultDetails resultDetails)
        {
            ResultDetails = resultDetails;
        }

        public bool IsSuccess
        {
            get
            {
                return ResultDetails == null;
            }
        }

        public ResultDetails ResultDetails { get; set; }

        public static implicit operator AppSrvResult([NotNull] ResultDetails resultDetails)
        {
            return new AppSrvResult
            {
                ResultDetails = resultDetails
            };
        }
    }

    /// <summary>
    /// Application返回结果包装类,有返回类型
    /// </summary>
    [Serializable]
    public sealed class AppSrvResult<TValue>
    {
        public AppSrvResult()
        {
        }

        public AppSrvResult([NotNull] TValue value)
        {
            Content = value;
        }

        public AppSrvResult([NotNull] ResultDetails resultDetails)
        {
            ResultDetails = resultDetails;
        }

        public bool IsSuccess
        {
            get
            {
                return ResultDetails == null && Content != null;
            }
        }

        public TValue Content { get; set; }

        public ResultDetails ResultDetails { get; set; }

        public static implicit operator AppSrvResult<TValue>(AppSrvResult result)
        {
            return new AppSrvResult<TValue>
            {
                Content = default
                ,
                ResultDetails = result.ResultDetails
            };
        }

        public static implicit operator AppSrvResult<TValue>(ResultDetails resultDetails)
        {
            return new AppSrvResult<TValue>
            {
                Content = default
                ,
                ResultDetails = resultDetails
            };
        }

        public static implicit operator AppSrvResult<TValue>(TValue value)
        {
            return new AppSrvResult<TValue>(value);
        }
    }
}
