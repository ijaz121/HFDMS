using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DbConnectionLogic : IDbConnectionLogic
    {
        private IConfiguration _config;
        private string DefaultConnectionString { get; set; }

        public DbConnectionLogic(IConfiguration config) 
        {
            _config = config;
            DefaultConnectionString = _config.GetSection("ConnectionStrings:HFDMS").Value;
        }

        //public async Task<string> IUD(string query, Hashtable parameters)
        //{
        //    try
        //    {
        //        using (SqlConnection sqlConnection = new SqlConnection(DefaultConnectionString))
        //        {
        //            await sqlConnection.OpenAsync();

        //            using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
        //            {
        //                sqlCommand.CommandText = query;
        //                sqlCommand.CommandType = CommandType.StoredProcedure;
        //                sqlCommand.CommandTimeout = 60;

        //                foreach (DictionaryEntry param in parameters)
        //                {
        //                    sqlCommand.Parameters.AddWithValue(param.Key.ToString(), param.Value);
        //                }

        //                int result = await sqlCommand.ExecuteNonQueryAsync();

        //                return result > 0 ? "SUCCESS" : "NO ROW EFFECTED";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception here
        //        throw;
        //    }
        //}

        public async Task<string> IUD(string query, Hashtable parameters)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DefaultConnectionString))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = 60;

                        foreach (DictionaryEntry param in parameters)
                        {
                            sqlCommand.Parameters.AddWithValue(param.Key.ToString(), param.Value);
                        }

                        await sqlCommand.ExecuteNonQueryAsync();

                        return "SUCCESS";
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle specific SQL exceptions, such as unique constraint violations
                if (sqlEx.Number == 50000) // Custom error number for THROW statements
                {
                    return sqlEx.Message; // Return the custom error message from the stored procedure
                }
                else if (sqlEx.Number == 2627 || sqlEx.Number == 2601) // Unique constraint violation numbers
                {
                    return "DUPLICATE ENTRY: A record with the same unique value already exists.";
                }
                else
                {
                    // Log SQL exception details here
                    return "DATABASE ERROR: An error occurred while processing your request.";
                }
            }
            catch (Exception ex)
            {
                // Log general exception details here
                return $"ERROR: {ex.Message}";
            }
        }


        public async Task<DataTable> ExecuteSelectQueryAsync(string query, Hashtable parameters)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DefaultConnectionString))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = 60;

                        foreach (DictionaryEntry param in parameters)
                        {
                            sqlCommand.Parameters.AddWithValue(param.Key.ToString(), param.Value);
                        }

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            DataTable dataTable = new DataTable();
                            dataAdapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception here
                throw;
            }
        }

        public async Task<DataSet> ExecuteSelectQueryToDataSetAsync(string query, Hashtable parameters)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DefaultConnectionString))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = 60;

                        foreach (DictionaryEntry param in parameters)
                        {
                            sqlCommand.Parameters.AddWithValue(param.Key.ToString(), param.Value);
                        }

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            DataSet dataSet = new DataSet();
                            dataAdapter.Fill(dataSet);
                            return dataSet;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception here
                throw;
            }
        }

    }
}
