namespace Liar.Domain.Shared.Dtos
{
    public interface IOutputDto : IDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }
    }
}
