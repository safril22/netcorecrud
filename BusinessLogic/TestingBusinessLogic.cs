using safriltest1.Helper;
using safriltest1.Models;
using System.Data.SqlClient;

namespace safriltest1.BusinessLogic
{
    public class TestingBusinessLogic
    {
        public static List<TestingName> GetDataTestingName()
        {
            try
            {
                string query = "SELECT * FROM TestingName order by ID;";

                return DBHelper.ExecuteQuery(query, reader => new TestingName
                {
                    ID = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            
        }

        public static bool InsertTestingName(TestingName testingName)
        {
            try
            {
                string query = "INSERT INTO TestingName (Name,IsActive,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) VALUES (@Name,@IsActive,@CreatedDate,@CreatedBy,@UpdatedDate,@UpdatedBy);";

                var parameters = new Dictionary<string, object>
                {
                    { "@Name", testingName.Name },
                    { "@IsActive", 1 },
                    { "@CreatedDate", DateTime.Now },
                    { "@CreatedBy", "SYSTEM" },
                    { "@UpdatedDate", DateTime.Now },
                    { "@UpdatedBy", "SYSTEM" }
                };

                int rowsAffected = DBHelper.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public static bool UpdateTestingName(TestingName testingName)
        {
            try
            {
                string query = "UPDATE TestingName SET Name = @Name WHERE ID = @ID;";

                var parameters = new Dictionary<string, object>
                {
                    { "@ID", testingName.ID },
                    { "@Name", testingName.Name },
                    { "@UpdatedDate", DateTime.Now },
                    { "@UpdatedBy", "SYSTEM" }
                };

                int rowsAffected = DBHelper.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool DeleteTestingName(int id)
        {
            try
            {
                string query = "DELETE FROM TestingName WHERE ID = @ID;";

                var parameters = new Dictionary<string, object>
                {
                    { "@ID", id }
                };

                int rowsAffected = DBHelper.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
