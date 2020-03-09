using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using BagerLib.model;

namespace BagerLib.DBUitl
{
    public class AbstractDBManager<T>:IManageDB<T> where T : IDBElement, new()
    {

        private readonly String connString;
        private readonly String tableName;

        public AbstractDBManager(string connString)
        {
            this.connString = connString;
            this.tableName = typeof(T).Name; // find name of class e.g. Hotel

            Get_All_SQL = $"select * from {tableName}";
            Get_One_SQL = $"select * from {tableName} where Id = @id";
            DELETE_SQL = $"delete from {tableName} where Id = @id";
        }

        public void Open()
        {
            
            INSERT_SQL = GenerateInsertSQL($"insert into {tableName} ");
            UPDATE_SQL = GenerateUpdateSQL($"Update {tableName} ");
            
        }

        /*
         * Methods to be specified
         */
        public delegate T ReadNextElementType(SqlDataReader reader);
        public delegate void SetParameterSQLType(SqlCommand cmd, T item);
        public delegate void SetParameterUpdateSQLType(SqlCommand cmd, int key, T item);
        public delegate String GenerateSQLType(String prefix);
        

        public ReadNextElementType ReadNextElement;
        public SetParameterSQLType SetParameterInsertSQL;
        public SetParameterUpdateSQLType SetParameterUpdateSQL;
        public GenerateSQLType GenerateInsertSQL;
        public GenerateSQLType GenerateUpdateSQL;



        /*
         * DB access methods
         */
        private readonly String Get_All_SQL;
        public IList<T> HentAlle()
        {
            IList<T> retListe = new List<T>();

            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(Get_All_SQL, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        retListe.Add((T)ReadNextElement(reader));
                    }
                }
            }

            return retListe;

        }

        

        private readonly String Get_One_SQL;
        public T HentEn(int key)
        {
            T t= new T();

            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(Get_One_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", key);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        t = (T)ReadNextElement(reader);
                    }
                }
            }

            return t;

        }

        private  string INSERT_SQL;
        public bool Opret(T item)
        {
            bool ok = false;

            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(INSERT_SQL, conn))
                {
                    SetParameterInsertSQL(cmd, item); // call abstract method

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


        private  string UPDATE_SQL;
        public bool Update(int key, T item)
        {
            bool ok = false;

            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(UPDATE_SQL, conn))
                {
                    SetParameterUpdateSQL(cmd, key, item);

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

        public readonly String DELETE_SQL;
        public T Delete(int key)
        {
            T t = HentEn(key);

            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(DELETE_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", key);
                    cmd.ExecuteNonQuery();
                }
            }

            return t;
        }
    }
}
