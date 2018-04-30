using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for hotelRetriever
/// </summary>
public class hotelRetriever : DBConnector, retriever
{
    public List<DBConnector> retrieve()
    {
        List<DBConnector> retrieved = new List<DBConnector>();
        SqlCommand cmd = new SqlCommand("SELECT Id FROM Hotel", dbConnect);
        lock (theLock) {
            dbConnect.Open();
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read()) {
                retrieved.Add(new hotel(Convert.ToInt32(read[0])));
            }
            dbConnect.Close();
        }
        return retrieved;
    }
}