using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Interfaces
{
    public interface IDb
    {
        SqlDataReader executeSelectQuery(string query, SqlParameter[] sqlParameter);
        bool executeInsertQuery(string query, SqlParameter[] sqlParameter);
        bool executeUpdateQuery(string query, SqlParameter[] sqlParameter);

        SqlDataReader executeStoredProcedure(string query, SqlParameter[] sqlParameter);
    }
}

