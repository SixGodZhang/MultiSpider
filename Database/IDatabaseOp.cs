using Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Database
{
    public interface IDatabaseOp
    {
        void SaveData<T>(T data) where T : IRepo;
        void SaveRangeData<T>(IEnumerable<T> datas) where T : IRepo;
    }
}
