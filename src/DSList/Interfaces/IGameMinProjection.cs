using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSList.Interfaces
{
    public interface IGameMinProjection
    {
        long Id { get; }
        string Title { get; }
        int? GameYear { get; }
        string ImgUrl { get; }
        string ShortDescription { get; }
    }

}