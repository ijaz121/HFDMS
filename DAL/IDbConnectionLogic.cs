using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDbConnectionLogic
    {
        Task<string> IUD(string query, Hashtable parameters);

        Task<DataTable> ExecuteSelectQueryAsync(string query, Hashtable parameters);
        
        Task<DataSet> ExecuteSelectQueryToDataSetAsync(string query, Hashtable parameters);
    }
}
