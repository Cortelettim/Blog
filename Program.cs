using System;
using Blog.Models;
using Blog.Repositories;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
namespace Blog
{
    class Program
    {
        private const string CONNECTION_STRING = @"Server=localhost,1433; 
                                                Database=Blog;User ID=sa;Password=1q2w3e4r@#$";

        static void Main(string[] args)
        {
            var connection = new SqlConnection(CONNECTION_STRING);
            connection.Open();
            ReadUsersWithRoles(connection);
            // ReadRoles(connection);
            // ReadTags(connection);
            // ReadUser();
            // CreateUser();
            // UpdateUser();
            // DeleteUser();
            connection.Close();
        }


        public static void ReadUsers(SqlConnection connection)
        {
            var repository = new Repository<User>(connection);
            var items = repository.Get();

            foreach (var item in items)
                Console.WriteLine(item.Name);

        }
        public static void ReadUsersWithRoles(SqlConnection connection)
        {
            var repository = new UserRepository(connection);
            var items = repository.GetWithRoles();

            foreach (var item in items)
            {
                Console.WriteLine(item.Name);

                foreach (var role in item.Roles)
                {
                    Console.WriteLine($"-- {role.Name}");
                }
            }

        }
        public static void ReadRoles(SqlConnection connection)
        {
            var repository = new Repository<Role>(connection);
            var items = repository.Get();

            foreach (var item in items)
                Console.WriteLine(item.Name);

        }
        public static void ReadTags(SqlConnection connection)
        {
            var repository = new Repository<Tag>(connection);
            var items = repository.Get();

            foreach (var item in items)
                Console.WriteLine(item.Name);

        }
        public static void CreateUser()
        {
            var user = new User()
            {
                Bio = "dev 2",
                Email = "kmasdmkasd@email.com",
                Image = "https://",
                Name = "Equipe verdinha",
                PasswordHash = "HASH",
                Slug = "equipe-verdinha"
            };
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Insert<User>(user);
                Console.WriteLine("Cadastro Realizado com sucesso");
            }
        }
        public static void UpdateUser()
        {
            var user = new User()
            {
                Id = 2,
                Bio = "Equipe Verdinha",
                Email = "equipeVerdinha@email.com",
                Image = "https://",
                Name = "Equipe verdinha",
                PasswordHash = "HASH",
                Slug = "equipe-verdinha"
            };
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Update<User>(user);
                Console.WriteLine("Atualizacao Realizado com sucesso");
            }
        }
        public static void DeleteUser()
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                var user = connection.Get<User>(2);
                connection.Delete<User>(user);
                Console.WriteLine("Exclusao Realizada com sucesso");
            }
        }
    }
}
