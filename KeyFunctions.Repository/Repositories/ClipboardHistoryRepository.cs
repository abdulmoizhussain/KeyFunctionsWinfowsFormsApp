using Dapper;
using KeyFunctions.Common.Utils;
using KeyFunctions.Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyFunctions.Common.Enums;

namespace KeyFunctions.Repository.Repositories
{
    public class ClipboardHistoryRepository
    {
        public static IList<ClipboardHistoryCore> GetAll()
        {
            using var dbConnection = DbContext.GetConnection();

            var historyList = dbConnection.Query<ClipboardHistoryCore>("select * from ClipboardHistory;", new DynamicParameters());
            return historyList.ToList();
        }

        public static void AddOne()
        {
            using var dbConnection = DbContext.GetConnection();
            //dbConnection.Open();

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
