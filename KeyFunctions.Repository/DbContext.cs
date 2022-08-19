using Dapper;
using KeyFunctions.Common.Enums;
using KeyFunctions.Common.Utils;
using KeyFunctions.Repository.Models;
using System.Data;
using System.Data.SQLite;

namespace KeyFunctions.Repository
{
    public class DbContext
    {
        // source links:
        // https://www.youtube.com/watch?v=ayp3tHEkRc0

        public static IDbConnection GetConnection()
        {
            return new SQLiteConnection(AppSettings.ConnectionString);
        }
    }
}