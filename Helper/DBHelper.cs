using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace safriltest1.Helper
{
    public static class DBHelper
    {
        private static string ConnectionString = "Server=DESKTOP-2B8C0N2;Database=testing;User Id=localsafril;Password=4321;"; 

        public static List<T> ExecuteQuery<T>(string query, Func<SqlDataReader, T> map)
        {
            var results = new List<T>();

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(map(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception or handle as needed
                throw new Exception($"Error executing query: {ex.Message}");
            }

            return results;
        }

        public static int ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        // Add parameters if provided
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }

                        // Execute the command and return the number of affected rows
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or rethrow the exception as needed
                throw new Exception($"Error executing non-query: {ex.Message}");
            }
        }

    }
}
