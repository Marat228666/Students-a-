using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4.Repository
{
    internal interface IRepository<Value>
    {
        List<Value> GetAll();
        int Insert(Value value);   
        int Update(int id, Value value);
        int Delete(int id);
    }
}
