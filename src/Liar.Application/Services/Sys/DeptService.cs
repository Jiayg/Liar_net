using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Liar.Application.Contracts.Dtos.Sys.Dept;
using Liar.Application.Contracts.IServices.Sys;
using Liar.Application.Contracts.ServiceResult;
using Liar.Core.Helper;
using Liar.Domain.Sys;
using Volo.Abp.Domain.Repositories;

namespace Liar.Application.Services.Sys
{
    public class DeptService : AppService, IDeptService
    {
        private readonly IRepository<SysDept> _deptRepository;

        public DeptService(IRepository<SysDept> deptRepository)
        {
            this._deptRepository = deptRepository;
        }

        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppSrvResult<long>> CreateAsync(DeptCreationDto input)
        {
            var isExists = _deptRepository.Where(x => x.FullName == input.FullName).Any();
            if (isExists)
                return Problem(HttpStatusCode.BadRequest, "该部门全称已经存在");

            var dept = ObjectMapper.Map<DeptCreationDto, SysDept>(input);
            dept.Id = IdGenerater.GetNextId();
            await this.SetDeptPids(dept);
            await _deptRepository.InsertAsync(dept);

            return dept.Id;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<AppSrvResult> DeleteAsync(long Id)
        {
            //TODO 此处应删除缓存
            await _deptRepository.DeleteAsync(d => d.Id == Id);

            return AppSrvResult();
        }

        /// <summary>
        /// 部门树结构
        /// </summary>
        /// <returns></returns>
        public async Task<List<DeptTreeDto>> GetTreeListAsync()
        {
            var result = new List<DeptTreeDto>();

            var depts = await _deptRepository.GetListAsync();

            if (!depts.Any())
                return result;

            var allDeptNodes = depts.Select(x => new DeptTreeDto()
            {
                Id = x.Id,
                FullName = x.FullName,
                Ordinal = x.Ordinal,
                Pid = x.Pid,
                Pids = x.Pids,
                SimpleName = x.SimpleName,
                Tips = x.Tips,
                Version = x.Version
            }).ToList();

            var roots = allDeptNodes.Where(d => d.Pid == 0).OrderBy(d => d.Ordinal);
            foreach (var node in roots)
            {
                GetChildren(node, allDeptNodes);
                result.Add(node);
            }

            void GetChildren(DeptTreeDto currentNode, List<DeptTreeDto> allDeptNodes)
            {
                var childrenNodes = allDeptNodes.Where(d => d.Pid == currentNode.Id).OrderBy(d => d.Ordinal);
                if (childrenNodes.Count() == 0)
                    return;
                else
                {
                    currentNode.Children.AddRange(childrenNodes);
                    foreach (var node in childrenNodes)
                    {
                        GetChildren(node, allDeptNodes);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppSrvResult> UpdateAsync(long id, DeptUpdationDto input)
        { 
            var allDepts = await _deptRepository.GetListAsync();

            var oldDeptDto = allDepts.FirstOrDefault(x => x.Id == id);
            if (oldDeptDto.Pid == 0 && input.Pid > 0)
                return Problem(HttpStatusCode.BadRequest, "一级单位不能修改等级");

            var isExists = allDepts.Exists(x => x.FullName == input.FullName && x.Id != id);
            if (isExists)
                return Problem(HttpStatusCode.BadRequest, "该部门全称已经存在");

            var deptEnity = ObjectMapper.Map<DeptUpdationDto, SysDept>(input);

            deptEnity.Id = id;

            if (oldDeptDto.Pid == input.Pid)
            {
                await _deptRepository.UpdateAsync(deptEnity);
            }
            else
            {
                await this.SetDeptPids(deptEnity);
                await _deptRepository.UpdateAsync(deptEnity);

                //zz.efcore 不支持
                //await _deptRepository.UpdateRangeAsync(d => d.Pids.Contains($"[{dept.ID}]"), c => new SysDept { Pids = c.Pids.Replace(oldDeptPids, dept.Pids) });
                var originalDeptPids = $"{oldDeptDto.Pids}[{deptEnity.Id}],";
                var nowDeptPids = $"{deptEnity.Pids}[{deptEnity.Id}],";
                var subDepts = _deptRepository
                                     .Where(d => d.Pids.StartsWith(originalDeptPids))
                                     .Select(d => new { d.Id, d.Pids })
                                     .ToList();
                foreach (var c in subDepts)
                {
                    await _deptRepository.UpdateAsync(new SysDept { Id = c.Id, Pids = c.Pids.Replace(originalDeptPids, nowDeptPids) });
                }
            }

            return AppSrvResult();
        }


        private async Task<SysDept> SetDeptPids(SysDept sysDept)
        {
            if (sysDept.Pid.HasValue && sysDept.Pid.Value > 0)
            {
                var dept = await _deptRepository.GetAsync(x => x.Pid == sysDept.Pid); ;
                string pids = dept?.Pids ?? "";
                sysDept.Pids = $"{pids}[{sysDept.Pid}],";
            }
            else
            {
                sysDept.Pid = 0;
                sysDept.Pids = "[0],";
            }
            return sysDept;
        }
    }
}
