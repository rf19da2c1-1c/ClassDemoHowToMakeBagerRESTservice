using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BagerLib.DBUitl;
using BagerLib.model;

namespace BagerREST.DBUtil
{
    public class ManageKager2
    {
        public ManageKager2() 
        {
        }

        public Kage ReadNextElement(SqlDataReader reader)
        {
            Kage kage = new Kage();
            kage.Name = reader.GetString(0);
            kage.Price = reader.GetDouble(1);
            kage.NoOfPieces = reader.GetInt32(2);

            return kage;
        }

        public string GenerateInsertSQL(string insertIntoPart)
        {
            return insertIntoPart + $"(Name, Price, NoOfPieces) values(@Name, @Price, @Pieces)";
        }

        

        public void SetParameterIntoInsertSQL(SqlCommand cmd, Kage kage)
        {
            cmd.Parameters.AddWithValue("@Name", kage.Name);
            cmd.Parameters.AddWithValue("@Price", kage.Price);
            cmd.Parameters.AddWithValue("@Pieces", kage.NoOfPieces);
        }
        public string GenerateUpdateSQL(string updateTablePart)
        {
            return updateTablePart + $"set Name = @Name, Price = @Price, NoOfPieces = @Pieces where Name = @OriginalName";
        }

        public void SetParameterIntoUpdateSQL(SqlCommand cmd, int Id, Kage kage)
        {
            cmd.Parameters.AddWithValue("@OriginalName", Id);
            cmd.Parameters.AddWithValue("@Name", kage.Name);
            cmd.Parameters.AddWithValue("@Price", kage.Price);
            cmd.Parameters.AddWithValue("@Pieces", kage.NoOfPieces);
        }

        
    }
}