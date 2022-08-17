using Dapper;
using KeyFunctions.Common.Enums;
using KeyFunctions.Repository.Models;
using System.Data;
using System.Data.SQLite;

namespace KeyFunctions.Repository
{
    public class DbContext
    {
        public static void Check()
        {
            // source links:
            // https://www.youtube.com/watch?v=ayp3tHEkRc0

            string connString = "Data Source=.\\KeyFunctions.db;Version=3;";
            using IDbConnection dbConnection = new SQLiteConnection(connString);
            dbConnection.Open();

            var data = new ClipboardHistoryCore
            {
                ClipData = "asdfasdf",
                ClipDataType = ClipDataType.Text,
                DateTime = DateTime.Now,
            };

            dbConnection.Execute("insert into ClipboardHistory (ClipData, ClipDataType, DateTimeStamp) values (@ClipData, @ClipDataType, @DateTimeStamp)", data);
        }
    }
}