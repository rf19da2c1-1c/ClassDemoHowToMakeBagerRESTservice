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
         * Hente alle, Hente en spific, slette, opdatere, indsætte
         *
         */

        private const String connString =
            @"Data Source=(localdb)\ProjectsV13;Initial Catalog=ClassDemo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IList<Kage> HentAlle()
        {
            IList<Kage> retListe = new List<Kage>();

            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand("select * from Kage", conn))
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