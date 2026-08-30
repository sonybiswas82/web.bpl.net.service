using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace BplService.Utility
{
    public class DataReader
    {
        public int ExecuteNonQueryForSPWithParam(string spName, Dictionary<string, object> Parameters, SqlConnection conn)
        {
            SqlTransaction Trans = null;
            try
            {
                conn.Open();
                Trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();

                    foreach (KeyValuePair<string, object> Parameter in Parameters)
                    {
                        cmd.Parameters.AddWithValue("@" + Parameter.Key, Parameter.Value);
                    }

                    cmd.Transaction = Trans;
                    cmd.ExecuteNonQuery();
                    Trans.Commit();
                }
            }
            catch (Exception ex)
            {
                Trans.Rollback();
                return 1;
            }
            finally
            {
                conn.Close();
            }
            return 0;
        }

        public int ExecuteNonQueryForSP(string spName, string conStr)
        {
            SqlTransaction Trans = null;
            SqlConnection conn = new SqlConnection();
            conn = new SqlConnection(conStr);
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                Trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);

                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Transaction = Trans;
                    cmd.ExecuteNonQuery();
                    Trans.Commit();
                }
            }
            catch (Exception ex)
            {
                Trans.Rollback();
                conn.Close();
                return 0;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return 1;
        }
    }
}
