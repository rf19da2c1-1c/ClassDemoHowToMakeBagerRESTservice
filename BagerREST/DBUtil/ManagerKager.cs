using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BagerLib.model;

namespace BagerREST.DBUtil
{
    public class ManagerKager
    {
        /*
         * Skal kunne minst fem metoder
         *
         * Hente alle, Hente en specifik, slette, opdatere, indsætte
         *
         */

        private const String connString =
            @"Data Source=(localdb)\ProjectsV13;Initial Catalog=ClassDemo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        private const string GET_ALL_SQL = "select * from Kage";
        public IList<Kage> HentAlle()
        {
            IList<Kage> retListe = new List<Kage>();

            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(GET_ALL_SQL, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        retListe.Add(ReadNextKage(reader));
                    }
                }
            }

            return retListe;
        }

        private const string GET_ONE_SQL = "select * from Kage where Name = @Name";

        public Kage HentEn(string name)
        {
            Kage kage = new Kage();

            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(GET_ONE_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        kage = ReadNextKage(reader);
                    }
                }
            }

            return kage;
        }

        private const string INSERT_SQL = "insert into Kage (Name, Price, NoOfPieces) values(@Name, @Price, @Pieces)";

        public bool Opret(Kage kage)
        {
            bool ok = false;
 
            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(INSERT_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", kage.Name);
                    cmd.Parameters.AddWithValue("@Price", kage.Price);
                    cmd.Parameters.AddWithValue("@Pieces", kage.NoOfPieces);

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();

                        ok = rows == 1;
                    }
                    catch (Exception ex)
                    {
                        ok = false;
                    }
                }
            }

            return ok;
        }

        private const string UPDATE_SQL = "update Kage set Name = @Name, Price = @Price, NoOfPieces = @Pieces where Name = @OriginalName";

        public bool Update(string name, Kage kage)
        {
            bool ok = false;

            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(UPDATE_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@OriginalName", name);
                    cmd.Parameters.AddWithValue("@Name", kage.Name);
                    cmd.Parameters.AddWithValue("@Price", kage.Price);
                    cmd.Parameters.AddWithValue("@Pieces", kage.NoOfPieces);

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();

                        ok = rows == 1;
                    }
                    catch (Exception ex)
                    {
                        ok = false;
                    }
                }
            }

            return ok;
        }

        private const string DELETE_SQL = "Delete from Kage where Name = @Name";

        public Kage Delete(string name)
        {
            Kage kage = HentEn(name);

            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(DELETE_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.ExecuteNonQuery();
                }
            }

            return kage;
        }



        private Kage ReadNextKage(SqlDataReader reader)
        {
            Kage kage = new Kage();

            kage.Name = reader.GetString(0);
            kage.Price = reader.GetDouble(1);
            kage.NoOfPieces = reader.GetInt32(2);

            return kage;
        }

    }
}