using System;
using System.Collections.Generic;

namespace Liar.Application.Contracts.Dtos.Sys.Dept
{
    [Serializable]
    public class DeptTreeDto : DeptDto
    {
        /// <summary>
        /// 子部门
        /// </summary>
        public List<DeptTreeDto> Children { get; private set; } = new List<DeptTreeDto>();
    }
}
