namespace Liar.Domain.Shared.Dtos
{
    public interface ISearchPagedDto : IDto
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int Offset { get; set; }
    }
}
