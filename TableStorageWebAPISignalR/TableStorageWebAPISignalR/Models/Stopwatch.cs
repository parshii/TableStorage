using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TableStorageWebAPISignalR.Models
{
    public class Stopwatch
    {
        private static CloudTable Initializer()
        {

            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("stopwatch");

            //  Create the table if it doesn't exist.
            table.CreateIfNotExists();

            return table;
        }
        
        public static StopwatchEntity GetLoggedInStopwatch(string UserName)
        {
            return FetchPartition(Initializer(), UserName);
        }
        public static bool StartStopWatch(string username)
        {
            string startTime = DateTime.Now.ToString("yyyyMMdd hhmmss");
            StopwatchEntity swe = new StopwatchEntity(username, startTime);

            swe.isStarted = true;
            swe.StartTime = startTime;

            CloudTable table = Initializer();

            StopwatchEntity swe2 = FetchPartition(table, username);
            if (swe2 == null)
            {
                // Create the TableOperation object that inserts the customer entity.
                TableOperation insertOperation = TableOperation.Insert(swe);
                // Execute the insert operation.
                TableResult tr = table.Execute(insertOperation);

            }
            else
            {
                StopStopWatch(username);
            }
            return true;

        }
        public static bool StopStopWatch(string username)
        {
            CloudTable table = Initializer();

            StopwatchEntity swe2 = FetchPartition(table, username);
            swe2.StopTime = DateTime.Now.ToString("yyyyMMdd hhmmss");
            swe2.isStarted = false;
            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Merge(swe2);
            // Execute the insert operation.
            TableResult tr = table.Execute(insertOperation);
            return true;
        }
        private static StopwatchEntity FetchPartition(CloudTable table, string username)
        {

            List<StopwatchEntity> lst = new List<StopwatchEntity>();
            if (table.Exists())
            {
                // Construct the query operation for all customer entities where PartitionKey="Harp".
                TableQuery<StopwatchEntity> query =
                    new TableQuery<StopwatchEntity>().Where(
                        TableQuery.GenerateFilterCondition(
                            "PartitionKey", QueryComparisons.Equal, username));
                //DateTime.Now.ToString("yyyyMMdd")
                // Print the fields for each customer.
                foreach (StopwatchEntity entity in table.ExecuteQuery(query))
                {
                    lst.Add(entity);
                }
                if (lst.Count > 0)
                    return lst[0];
                else
                    return null;
            }
            return null;
        }
    }

    public class StopwatchEntity : TableEntity
    {
        public StopwatchEntity(string userName, string startTime)
        {
            this.PartitionKey = userName;
            this.RowKey = userName + "-" + startTime;
        }

        public StopwatchEntity() { }

        public string StartTime { get; set; }

        public string StopTime { get; set; }


        public bool isStarted { get; set; }

    }

    public class StopWatchModel
    {
        public string UserName { get; set; }
        public string DateTimeStamp { get; set; }
    }
}