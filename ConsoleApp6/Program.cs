using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Threading;

namespace TransactionApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ThreadStart threadTransaction = new ThreadStart(ThreadTransaction);
            ThreadStart threadTransaction2 = new ThreadStart(ThreadTransaction2);
            Thread thread = new Thread(threadTransaction);
            Thread thread2 = new Thread(threadTransaction2);
            thread.Start();
            thread2.Start();
            // ПОДКЛЮЧЕНИЕ К БД НАСТРАИВАЕТСЯ ЧЕРЕЗ App.config!
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
            for (int i = 0; i <= 100; i++)
            {
                if (sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();
                sqlConnection.Open();
                Random rnd = new Random();
                int value = rnd.Next(1, 4);
                SqlTransaction transaction = sqlConnection.BeginTransaction();
                SqlCommand command = sqlConnection.CreateCommand();
                command.Transaction = transaction;
               try
               {
                    command.CommandText = $"UPDATE Shop SET AmountOfItems=AmountOfItems-{value} WHERE id = '{1}';";
                    command.ExecuteNonQuery();
                    command.CommandText = $"UPDATE UserInfo SET ItemsPurshared=ItemsPurshared+{value} WHERE id = '{1}';";
                    command.ExecuteNonQuery();
                    command.CommandText = $"INSERT INTO TransactionInfo (UserId,TransactionTime,ItemSize) VALUES (1,'{DateTime.Now.ToString()}','{value}')";
                    command.ExecuteNonQuery();
                    transaction.Commit();
                    Console.WriteLine("Покупка успешно выполнена!");
                   
                    sqlConnection.Close();

               }
                catch (Exception)
                {
                    transaction.Rollback();
                    Console.WriteLine("Ошибка совершения покупки.");
                    sqlConnection.Close();
                }
            }
            void ThreadTransaction()   
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
                for (int i = 0; i <= 100; i++)
                {
                    if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
                    sqlConnection.Open();
                    Random rnd = new Random();
                    int value = rnd.Next(1, 4);
                    SqlTransaction transaction = sqlConnection.BeginTransaction();
                    SqlCommand command = sqlConnection.CreateCommand();
                    command.Transaction = transaction;
                    try
                    {
                        command.CommandText = $"UPDATE Shop SET AmountOfItems=AmountOfItems-{value} WHERE id = '{1}';";
                        command.ExecuteNonQuery();
                        command.CommandText = $"UPDATE UserInfo SET ItemsPurshared=ItemsPurshared+{value} WHERE id = '{2}';";
                        command.ExecuteNonQuery();
                        command.CommandText = $"INSERT INTO TransactionInfo (UserId,TransactionTime,ItemSize) VALUES (2,'{DateTime.Now.ToString()}','{value}')";
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        sqlConnection.Close();
                        Console.WriteLine("Покупка успешно выполнена!");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Ошибка совершения покупки.");
                        sqlConnection.Close();
                    }
                }
            }
            void ThreadTransaction2()
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
                for (int i = 0; i <= 100; i++)
                {
                    if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
                    sqlConnection.Open();
                    Random rnd = new Random();
                    int value = rnd.Next(1, 4);
                    SqlTransaction transaction = sqlConnection.BeginTransaction();
                    SqlCommand command = sqlConnection.CreateCommand();
                    command.Transaction = transaction;
                    try
                    {
                        command.CommandText = $"UPDATE Shop SET AmountOfItems=AmountOfItems-{value} WHERE id = '{1}';";
                        command.ExecuteNonQuery();
                        command.CommandText = $"UPDATE UserInfo SET ItemsPurshared=ItemsPurshared+{value} WHERE id = '{3}';";
                        command.ExecuteNonQuery();
                        command.CommandText = $"INSERT INTO TransactionInfo (UserId,TransactionTime,ItemSize) VALUES (3,'{DateTime.Now.ToString()}','{value}')";
                        command.ExecuteNonQuery();
                        transaction.Commit();
                            sqlConnection.Close();
                        Console.WriteLine("Покупка успешно выполнена!");
                    }
                    catch (Exception )
                    {
                        transaction.Rollback();
                        sqlConnection.Close();
                        Console.WriteLine("Ошибка совершения покупки.");
                        
                    }
                }
            }
        }
    }
}