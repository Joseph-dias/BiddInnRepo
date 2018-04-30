using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Get all reviews from database
/// </summary>
public class reviewRetriever : DBConnector, retriever
{
    public List<DBConnector> retrieve()
    {
        SqlCommand newCommand = new SqlCommand("SELECT Id FROM Reviews", dbConnect);
        List<DBConnector> toReturn = new List<DBConnector>();
        lock (theLock) {
            dbConnect.Open();
            SqlDataReader read = newCommand.ExecuteReader();
            while (read.Read()) {
                toReturn.Add(new review(Convert.ToInt32(read[0])));
            }
            dbConnect.Close();
        }
        return toReturn;
    }
}