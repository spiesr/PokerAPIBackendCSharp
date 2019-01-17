using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using NpgsqlTypes;
using Npgsql;
using System.Data;

namespace WebApplication1.Controllers
{
    public class GamePersistence
    {
        public void saveGame(string gameJson, string winnerString)
        {
            // Create and open a connection to our database using the proper config parameters (AWS deployment)
            var appConfig = ConfigurationManager.AppSettings;
            string dbname = appConfig["RDS_DB_NAME"];
            string username = appConfig["RDS_USERNAME"];
            string password = appConfig["RDS_PASSWORD"];
            string hostname = appConfig["RDS_HOSTNAME"];
            string port = appConfig["RDS_PORT"];
            string connectionString = String.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};", hostname, dbname, username, password);

            // Here is the connection string for the local system
            // string connectionString = ConfigurationManager.ConnectionStrings["AppDatabaseConnectionString"].ConnectionString;
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            conn.Open();

            // Check to see if the table we need is there. If it isn't it will be created
            NpgsqlCommand tableCheck = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS apirecords(id serial PRIMARY KEY,usage_time timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,request_body JSON NOT NULL,winner VARCHAR (200));", conn);
            tableCheck.ExecuteNonQuery();

            // Create an insert command that will insert the data into the database into the columns that don't have default values
            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO apirecords (request_body, winner) VALUES (@param1,@param2);", conn);
            cmd.Parameters.Add(new NpgsqlParameter("param1", NpgsqlDbType.Json) { Value = gameJson });
            cmd.Parameters.Add(new NpgsqlParameter("param2", NpgsqlDbType.Varchar) { Value = winnerString });

            // Execute the command, then wait for it to be done before closing the connection
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}