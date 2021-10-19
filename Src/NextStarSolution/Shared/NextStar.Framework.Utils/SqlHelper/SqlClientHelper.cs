using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace NextStar.Framework.Utils
{
    public static class SqlClientHelper
    {
        #region Private Function

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        private static SqlCommand PrepareCommand(
            SqlConnection connection,
            CommandType cmdType,
            string cmdText,
            SqlTransaction transaction,
            params SqlParameter[] sqlParameters)
        {

            if (connection.State != ConnectionState.Open) connection.Open();

            SqlCommand sqlCmd = new SqlCommand(cmdText, connection);
            sqlCmd.CommandType = cmdType;

            if (transaction != null) sqlCmd.Transaction = transaction;

            if (sqlParameters != null && sqlParameters.Length > 0)
            {
                foreach (var p in sqlParameters)
                {
                    if (p.Value == null) p.Value = DBNull.Value;
                }
                sqlCmd.Parameters.AddRange(sqlParameters);
            }
            return sqlCmd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        private static SqlCommand PrepareCommand(
            SqlConnection connection,
            CommandType cmdType,
            string cmdText,
            int cmdTimeout,
            SqlTransaction transaction,
            params SqlParameter[] sqlParameters)
        {
            if (connection.State != ConnectionState.Open) connection.Open();

            SqlCommand sqlCmd = new SqlCommand(cmdText, connection);
            sqlCmd.CommandType = cmdType;
            sqlCmd.CommandTimeout = cmdTimeout;
            if (transaction != null) sqlCmd.Transaction = transaction;

            if (sqlParameters != null && sqlParameters.Length > 0)
            {
                foreach (var p in sqlParameters)
                {
                    if (p.Value == null) p.Value = DBNull.Value;
                }
                sqlCmd.Parameters.AddRange(sqlParameters);
            }
            return sqlCmd;
        }

        #endregion

        #region Public Function

        #region ExecuteDataset

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataset(
            SqlConnection connection,
            CommandType cmdType,
            string cmdText,
            SqlTransaction transaction,
            params SqlParameter[] sqlParameters)
        {
            SqlCommand sqlCmd = PrepareCommand(connection, cmdType, cmdText, transaction, sqlParameters);
            DataSet dataSet = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(dataSet);
            return dataSet;
        }

        public static DataSet ExecuteDataset(
            SqlConnection connection,
            CommandType cmdType,
            string cmdText,
            int cmdTimeout,
            SqlTransaction transaction,
            params SqlParameter[] sqlParameters)
        {
            SqlCommand sqlCmd = PrepareCommand(connection, cmdType, cmdText, cmdTimeout, transaction, sqlParameters);
            DataSet dataSet = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(dataSet);
            return dataSet;
        }

        public static DataSet ExecuteDataset(
            SqlConnection connection,
            CommandType cmdType,
            string cmdText,
            params SqlParameter[] sqlParameters)
        {
            return ExecuteDataset(connection, cmdType, cmdText, null, sqlParameters);
        }

        public static DataSet ExecuteDataset(
            SqlConnection connection,
            CommandType cmdType,
            string cmdText,
            int cmdTimeout,
            params SqlParameter[] sqlParameters)
        {
            return ExecuteDataset(connection, cmdType, cmdText, cmdTimeout, null, sqlParameters);
        }

        public static DataSet ExecuteDataset(
            string connectionString,
            CommandType cmdType,
            string cmdText,
            params SqlParameter[] sqlParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return ExecuteDataset(connection, cmdType, cmdText, sqlParameters);
            }
        }

        public static DataSet ExecuteDataset(
            string connectionString,
            CommandType cmdType,
            string cmdText,
            int cmdTimeout,
            params SqlParameter[] sqlParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return ExecuteDataset(connection, cmdType, cmdText, cmdTimeout, sqlParameters);
            }
        }

        #endregion

        #region ExecuteNonQuery

        public static int ExecuteNonQuery(
            SqlConnection connection,
            CommandType cmdType,
            string cmdText,
            SqlTransaction transaction,
            params SqlParameter[] sqlParameters)
        {
            SqlCommand sqlCmd = PrepareCommand(connection, cmdType, cmdText, transaction, sqlParameters);

            return sqlCmd.ExecuteNonQuery();
        }

        public static int ExecuteNonQuery(
            SqlConnection connection,
            CommandType cmdType,
            string cmdText,
            int cmdTimeout,
            SqlTransaction transaction,
            params SqlParameter[] sqlParameters)
        {
            SqlCommand sqlCmd = PrepareCommand(connection, cmdType, cmdText, cmdTimeout, transaction, sqlParameters);
            return sqlCmd.ExecuteNonQuery();
        }

        public static int ExecuteNonQuery(
            SqlConnection connection,
            CommandType cmdType,
            string cmdText,
            params SqlParameter[] sqlParameters)
        {
            return ExecuteNonQuery(connection, cmdType, cmdText, null, sqlParameters);
        }

        public static int ExecuteNonQuery(
            SqlConnection connection,
            CommandType cmdType,
            string cmdText,
            int cmdTimeout,
            params SqlParameter[] sqlParameters)
        {
            return ExecuteNonQuery(connection, cmdType, cmdText, cmdTimeout, null, sqlParameters);
        }

        public static int ExecuteNonQuery(
            string connectionString,
            CommandType cmdType,
            string cmdText,
            params SqlParameter[] sqlParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return ExecuteNonQuery(connection, cmdType, cmdText, sqlParameters);
            }
        }

        public static int ExecuteNonQuery(
            string connectionString,
            CommandType cmdType,
            string cmdText,
            int cmdTimeout,
            params SqlParameter[] sqlParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return ExecuteNonQuery(connection, cmdType, cmdText, cmdTimeout, sqlParameters);
            }
        }

        #endregion

        #region ExecuteScalar

        public static object ExecuteScalar(
            SqlConnection connection,
            CommandType cmdType,
            string cmdText,
            SqlTransaction transaction,
            params SqlParameter[] sqlParameters)
        {
            SqlCommand sqlCmd = PrepareCommand(connection, cmdType, cmdText, transaction, sqlParameters);

            return sqlCmd.ExecuteScalar();
        }

        public static object ExecuteScalar(
            SqlConnection connection,
            CommandType cmdType,
            string cmdText,
            int cmdTimeout,
            SqlTransaction transaction,
            params SqlParameter[] sqlParameters)
        {
            SqlCommand sqlCmd = PrepareCommand(connection, cmdType, cmdText, cmdTimeout, transaction, sqlParameters);

            return sqlCmd.ExecuteScalar();
        }

        public static object ExecuteScalar(
            SqlConnection connection,
            CommandType cmdType,
            string cmdText,
            params SqlParameter[] sqlParameters)
        {
            return ExecuteScalar(connection, cmdType, cmdText, null, sqlParameters);
        }

        public static object ExecuteScalar(
            SqlConnection connection,
            CommandType cmdType,
            string cmdText,
            int cmdTimeout,
            params SqlParameter[] sqlParameters)
        {
            return ExecuteScalar(connection, cmdType, cmdText, cmdTimeout, null, sqlParameters);
        }

        public static object ExecuteScalar(
            string connectionString,
            CommandType cmdType,
            string cmdText,
            params SqlParameter[] sqlParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return ExecuteScalar(connection, cmdType, cmdText, sqlParameters);
            }
        }

        public static object ExecuteScalar(
            string connectionString,
            CommandType cmdType,
            string cmdText,
            int cmdTimeout,
            params SqlParameter[] sqlParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return ExecuteScalar(connection, cmdType, cmdText, cmdTimeout, sqlParameters);
            }
        }

        #endregion 


        #endregion
    }
}