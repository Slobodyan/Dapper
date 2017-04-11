using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DapperORM.Models
{
	public class UserRepository
	{
		private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

		public List<User> GetUsers()
		{
			List<User> users;
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				users = db.Query<User>("SELECT * FROM [User]").ToList();
			}
			return users;
		}

		public User Get(int id)
		{
			User user;
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				user = db.Query<User>("SELECT * FROM [User] WHERE [Id] = @id", new {id}).FirstOrDefault();
			}
			return user;
		}

		public User Create(User user)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var sqlQuery =
					"INSERT INTO [User] ([FirstName], [LastName]) VALUES(@FirstName, @LastName); SELECT CAST(SCOPE_IDENTITY() AS INT)";
				var userId = db.Query<int>(sqlQuery, user).FirstOrDefault();
				user.Id = userId;
			}
			return user;
		}

		public void Update(User user)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var sqlQuery = "UPDATE [User] SET [FirstName] = @FirstName, [LastName] = @LastName WHERE [Id] = @Id";
				db.Execute(sqlQuery, user);
			}
		}

		public void Delete(int id)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var sqlQuery = "DELETE FROM [User] WHERE [Id] = @id";
				db.Execute(sqlQuery, new {id});
			}
		}
	}
}