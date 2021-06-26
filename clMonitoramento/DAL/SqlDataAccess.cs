using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace clMonitoramento.DAL
{
    public static class SqlDataAccess
    {

        public static string GetConnectionString(string connectionName = "Database")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> LoadData<T>(string sql)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(GetConnectionString()))
                {
                    return conn.Query<T>(sql).ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<T> LoadData<T>(string sql, object data)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(GetConnectionString()))
                {
                    return conn.Query<T>(sql, data).ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static int SaveData<t>(string sql, t data)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(GetConnectionString()))
                {
                    return conn.Execute(sql, data);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Save the new data, and return the id
        /// </summary>
        /// <typeparam name="t"></typeparam>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <param name="returnNewId"></param>
        /// <returns></returns>
        public static Guid SaveData<t>(string sql, t data, bool returnNewId = true)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(GetConnectionString()))
                {
                    return (Guid)conn.ExecuteScalar(sql, data);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool OpenTransactionSameParameter<t>(string[] commands, t data)
        {
            try
            {
                bool ok = true;
                using (IDbConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    using (var transaction = conn.BeginTransaction())
                    {
                        for(int i = 0; i < commands.Length && ok; i++)
                        {
                            ok = conn.Execute(commands[i], data, transaction: transaction) >= 0;
                        }

                        if(ok)
                            transaction.Commit();
                    }
                }
                return ok;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
