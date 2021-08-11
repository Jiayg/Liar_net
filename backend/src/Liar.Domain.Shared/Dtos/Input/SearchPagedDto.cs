namespace Liar.Domain.Shared.Dtos
{
    public abstract class SearchPagedDto : ISearchPagedDto
    {
        private int _limit;
        private int _offset;

        /// <summary>
        /// 页码
        /// </summary>
        public int Limit
        {
            get
            {
                return _limit < 1 ? 1 : _limit;
            }
            set
            {
                _limit = value;
            }
        }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int Offset
        {
            get
            {
                if (_offset < 5) _offset = 5;
                if (_offset > 100) _offset = 100;
                return _offset;
            }
            set
            {
                _offset = value;
            }
        }

        public int SkipRows()
        {
            return (this.Limit - 1) * this.Offset;
        }
    }
}
