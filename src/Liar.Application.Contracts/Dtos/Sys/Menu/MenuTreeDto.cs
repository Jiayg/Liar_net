using System;
using System.Collections.Generic;
using Liar.Domain.Shared.Dtos;

namespace Liar.Application.Contracts.Dtos.Sys.Menu
{
    [Serializable]
    public class MenuTreeDto : IDto
    {
        public IEnumerable<Node<long>> TreeData { get; set; }
        public IEnumerable<long> CheckedIds { get; set; }
    }
}
