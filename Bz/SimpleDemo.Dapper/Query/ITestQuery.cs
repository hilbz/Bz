using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDemo.Dapper.Query
{
    public interface ITestQuery
    {
        Task<string> GetStringAsync(int keyWord);
    }
}
