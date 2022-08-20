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

        public static void AddOrUpdateOne(string clipData)
        {
            var datetimeNow = DateTime.Now;

            using var dbConnection = DbContext.GetConnection();

            string query = "SELECT * from ClipboardHistory WHERE ClipData = @ClipData LIMIT 1";
            var param = new { ClipData = clipData };
            var result = dbConnection.Query<ClipboardHistoryCore>(query, param).FirstOrDefault();

            if (result is null)
            {
                var data = new ClipboardHistoryCore
                {
                    ClipData = clipData,
                    ClipDataType = ClipDataType.Text,
                    DateTime = datetimeNow,
                    LastRepeated = datetimeNow,
                };
                dbConnection.Execute("insert into ClipboardHistory (ClipData, ClipDataType, DateTimeStamp, LastRepeatedTicks) values (@ClipData, @ClipDataType, @DateTimeStamp, @LastRepeatedTicks)", data);
            }
            else
            {
                var updateParam = new { LastRepeatedTicks = datetimeNow.Ticks, Id = result.Id };
                dbConnection.Execute("UPDATE ClipboardHistory SET LastRepeatedTicks = @LastRepeatedTicks WHERE Id = @Id", updateParam);
            }
        }
    }
}
