using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.ListConverter
{
    public interface IListConverter
    {
        List<T> ConvertDataTable<T>(DataTable dt);
        T GetItem<T>(DataRow dr);
    }
}
