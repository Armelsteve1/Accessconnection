using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; 
namespace Accessconnection
{
    class Program
    {
        static string CONSTR = @"host=localhost;user id=roo;password=;database=test_db";
        static void Main(string[] args)
        {
            char choise = 'o';
            char menu = 'r';
            do
            {
                Console.Clear();
                Console.WriteLine("c= create");
                Console.WriteLine("R= Read");
                Console.WriteLine("U= Update");
                Console.WriteLine("D= Delete");
                Console.Write("votre choix :");
                menu = (char)Console.ReadLine().ToLower()[0];
                switch (menu)
                {
                    case 'c':
                        Create();
                        break;
                    case 'r':
                        Read();
                        break;
                    case 'u':
                        Update();
                        break;
                    case 'd':
                        Delete();
                        break;
                    default:
                        Console.WriteLine("mauvais choix");
                        break;
                }
                Console.WriteLine("continuer[y/n]:");
                choise = (char)Console.ReadLine().ToLower()[0];
            }
            while (choise != 'n');

            Console.ReadKey();
        }
        static void Create()
        {
            try
            {
                //definition de la chaine de caracter
                using (MySqlConnection con = new MySqlConnection(CONSTR))
                {
                    //ouverture de la session
                    con.Open();
                    Console.Write("Matricule :");
                    string matricule = Console.ReadLine();
                    Console.Write("Firts Name :");
                    string firtsName = Console.ReadLine();
                    Console.Write("Last Name :");
                    string lastName = Console.ReadLine();
                    string query = "INSERT INTO Student (Matricule,FirtsName,LastName) VALUES(@matricule,@firtsName,@lastName)";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {

                        var param = new MySqlParameter(@"matricule", MySqlDbType.VarChar);
                        param.Value = matricule;
                        cmd.Parameters.Add(param);

                        param = new MySqlParameter(@"firtsName", MySqlDbType.VarChar);
                        param.Value =firtsName;
                        cmd.Parameters.Add(param);

                        param = new MySqlParameter(@"lastName", MySqlDbType.VarChar);
                        param.Value =lastName;
                        cmd.Parameters.Add(param);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Query done !");
                    }
                }
            }

            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
        static void Read()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(CONSTR))
                {
                    //ouverture de la session
                    con.Open();
                    string query = "SELECT*FROM Student ORDER BY LastName,FirtsName ";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))

                    {
                        //cmd.CommandText = "SELECT*FROM Student ORDER BY LastName,FirtsName  ";

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            Console.WriteLine("Matricule\tFirt name\tlast name");
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader["matricule"]}\t\t{reader["FirtsName"]}\t\t{reader["LastName"]}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
        static void Update()
        {
            try
            {
                //definition de la chaine de caracter
                using (MySqlConnection con = new MySqlConnection(CONSTR))
                {
                    //ouverture de la session
                    con.Open();
                    Console.Write("Old Matricule :");
                    string oldMatricule = Console.ReadLine();
                    Console.Write(" new Matricule :");
                    string newMatricule = Console.ReadLine();

                    Console.Write("Firts Name :");
                    string FirtsName = Console.ReadLine();
                    Console.Write("Last Name :");
                    string LastName = Console.ReadLine();
                    string query = $"UPDATE  Student SET Matricule = @newMatricule, FirtsName = @FirtsName, LastName = @LastName WHERE Matricule=@oldMatricule";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        // lordre des parametre est important. 

                        var param = new MySqlParameter(@"newMatricule", MySqlDbType.VarChar);
                        param.Value = newMatricule;
                        cmd.Parameters.Add(param);

                        param = new MySqlParameter(@"FirtsName", MySqlDbType.VarChar);
                        param.Value = FirtsName;
                        cmd.Parameters.Add(param);

                        param = new MySqlParameter(@"LastName", MySqlDbType.VarChar);
                        param.Value = LastName;
                        cmd.Parameters.Add(param);

                        param = new MySqlParameter(@"oldMatricule", MySqlDbType.VarChar);
                        param.Value = oldMatricule;
                        cmd.Parameters.Add(param);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Query done !");
                    }
                }
            }

            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
        static void Delete()
        {
            try
            {
                //definition de la chaine de caracter
                using (MySqlConnection con = new MySqlConnection(CONSTR))
                {
                    //ouverture de la session
                    con.Open();
                    Console.Write(" Matricule :");
                    string matricule = Console.ReadLine();

                    string query = $"DELETE FROM  Student WHERE Matricule = @matricule ";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {

                        var param = new MySqlParameter(@"matricule", MySqlDbType.VarChar);
                        param.Value = matricule;
                        cmd.Parameters.Add(param);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Query done !");
                    }
                }
            }

            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }


    }
}
