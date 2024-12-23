using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSList.Interfaces
{
    public interface ISeedDataBase
    {
        void ExecuteSeed(string query);
    }
}