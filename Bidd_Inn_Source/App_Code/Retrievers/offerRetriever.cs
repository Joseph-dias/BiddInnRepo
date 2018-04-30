using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for offerRetriever
/// </summary>
public class offerRetriever : DBConnector, retriever
{
    /// <summary>
    /// Get all offers in the database
    /// </summary>
    /// <returns>List of offers</returns>
    public List<DBConnector> retrieve()
    {
        SqlCommand newCommand = new SqlCommand("SELECT Id FROM Offers", dbConnect);
        List<DBConnector> toReturn = new List<DBConnector>();
        lock (theLock) {
            dbConnect.Open();
            SqlDataReader read = newCommand.ExecuteReader();
            while (read.Read()) {
                toReturn.Add(new offer(Convert.ToInt32(read[0])));
            }
            dbConnect.Close();
        }
        return toReturn;
    }
}