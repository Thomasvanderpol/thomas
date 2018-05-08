using Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public class DbConnection : IDb
    {
        private SqlDataAdapter myAdapter;
        private SqlConnection conn;

        public DbConnection()
        {
            myAdapter = new SqlDataAdapter();
            //conn = new SqlConnection(connectionString: "server=rikserver.database.windows.net;database=RikDatabase;uid=Thomasvanderpol;pwd=Stroopwafel1.;");
            conn = new SqlConnection(connectionString: @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= C:\Users\thoma\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\KillerAppDB.mdf ;Integrated Security=True");
        }

       

        private SqlConnection openConnection()
        {
            if (conn.State == ConnectionState.Closed || conn.State ==
                        ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }

        public SqlDataReader executeSelectQuery(string query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            SqlDataReader dr;
            dr = null;
            DataSet ds = new DataSet();
            try
            {
                conn.Close();
                myCommand.Connection = openConnection();
                myCommand.CommandText = query;
                myCommand.Parameters.AddRange(sqlParameter);
                myCommand.ExecuteNonQuery();
                dr = myCommand.ExecuteReader();
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeSelectQuery - Query: " + query + " \nException: " + e.StackTrace.ToString());
                return null;
            }
            finally
            {
              
            }
            return dr;
        }

        public bool executeInsertQuery(string query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            try
            {
                conn.Close();
                myCommand.Connection = openConnection();
                myCommand.CommandText = query;
                myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.InsertCommand = myCommand;
                myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeInsertQuery - Query: " + query + " \nException: \n" + e.StackTrace.ToString());
                return false;
            }
            finally
            {
            }
            return true;
        }

        public bool executeUpdateQuery(string query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            try
            {
                conn.Close();
                myCommand.Connection = openConnection();
                myCommand.CommandText = query;
                myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.UpdateCommand = myCommand;
                myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeUpdateQuery - Query: " + query + " \nException: " + e.StackTrace.ToString());
                return false;
            }
            finally
            {
            }
            return true;
        }

        public SqlDataReader executeStoredProcedure(string query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            SqlDataReader dr;
            conn.Close();
            myCommand.Connection = openConnection();
            myCommand.CommandText = query;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddRange(sqlParameter);
            myCommand.ExecuteNonQuery();
            dr = myCommand.ExecuteReader();

            return dr;
        }
    }
}
