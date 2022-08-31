using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game.DbConstants
{
    public class Db
    {
        private static SqlConnection connection = new SqlConnection(ConnectionString);
        public const string ConnectionString = @"Server=DESKTOP-4UB2N3P;Database=Snake;Trusted_Connection=True";

        public static bool Insert(string name, string score)
        {
            string commandStr = $"INSERT INTO Users ([Name],[Score]) VALUES ('{name}','{score}')";
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(commandStr, connection);
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();

            command.Dispose();
            connection.Close();

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                return true;
            }
            return false;
        }

        public static string[] Read(string user)
        {
            string[] array = new string[2];
            connection.Open();
            string commandStr = $"SELECT [Name],[Score] FROM Users WHERE [Name] = '{user}'";
            SqlCommand command = new SqlCommand(commandStr, connection);
            SqlDataReader rd = command.ExecuteReader();
            while (rd.Read())
            {
                array[0] = rd.GetString(0);
                array[1] = rd.GetString(1);
            }
            rd.Close();
            connection.Close();

            return array;
        }

        public static bool UserExists(string name)
        {
            bool exists = false;
            connection.Open();
            string commandStr = "SELECT [Name] FROM Users";
            SqlCommand command = new SqlCommand(commandStr, connection);
            SqlDataReader rd = command.ExecuteReader();
            while (rd.Read())
            {
                if (rd.GetString(0) == name)
                {
                    exists = true;
                    break; 
                }
            }
            rd.Close();
            connection.Close();

            return exists;
        }

        public static void UpdateDb(string newScore, string name)
        {
            connection.Open();
            SqlCommand command = new SqlCommand($"UPDATE Users SET [Score] = '{newScore}' WHERE [Name] = '{name}'", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}