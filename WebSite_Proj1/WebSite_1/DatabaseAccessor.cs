using Class.Database;
using System.Data.SqlClient;

namespace WebSite_1
{
    public class DatabaseAccessor
    {
        private static readonly ClassEntities entities;

        static DatabaseAccessor()
        {
            entities = new ClassEntities();
            entities.Database.Connection.Open();
        }

        public static ClassEntities Instance
        {
            get
            {
                return entities;
            }
        }

        public static void To_UserClass(int userId, int classId)
        {
            string sqlStr = "Insert Into [mini-cstructor].[dbo].[UserClass] VALUES(@UserId, @ClassId);";
            const string connetionString = "Data Source=localhost\\DATACOLLECTION;Initial Catalog=mini-cstructor;Integrated Security=SSPI";
            SqlConnection connection;
            connection = new SqlConnection(connetionString);
            connection.Open();
            using (SqlCommand command = new SqlCommand(sqlStr, connection))
            {
                command.Parameters.Add(new SqlParameter("UserId", userId));
                command.Parameters.Add(new SqlParameter("ClassId", classId));
                command.ExecuteNonQuery();
            }
            connection.Close();
            
            //RetrieveClassesForStudent_Result test1 = new RetrieveClassesForStudent_Result();
            //test1.UserId = userId;
            //test1.ClassId = classId;


        }


    }
}
