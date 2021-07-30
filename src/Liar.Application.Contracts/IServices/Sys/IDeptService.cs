using System.Collections.Generic;
using System.Threading.Tasks;
using Liar.Application.Contracts.Dtos.Sys.Dept;
using Liar.Application.Contracts.ServiceResult;
using Volo.Abp.Uow;

namespace Liar.Application.Contracts.IServices.Sys
{
    public interface IDeptService : IAppService
    {
        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<AppSrvResult<long>> CreateAsync(DeptCreationDto input);

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>  
        Task<AppSrvResult> UpdateAsync(long id, DeptUpdationDto input);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns> 
        Task<AppSrvResult> DeleteAsync(long Id);

        /// <summary>
        /// 部门树结构
        /// </summary>
        /// <returns></returns> 
        Task<List<DeptTreeDto>> GetTreeListAsync();
    }
}
