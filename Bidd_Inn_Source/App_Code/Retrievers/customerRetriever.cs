using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for customerRetriever
/// </summary>
public class customerRetriever : DBConnector, retriever
{
    public List<DBConnector> retrieve()
    {
        SqlCommand newCommand = new SqlCommand("SELECT Id FROM Customer", dbConnect);
        List<int> ids = new List<int>();
        List<DBConnector> toReturn = new List<DBConnector>();
        lock (theLock) {
            dbConnect.Open();
            SqlDataReader read = newCommand.ExecuteReader();
            while (read.Read()) ids.Add(Convert.ToInt32(read[0]));
            dbConnect.Close();
        }
        foreach (int num in ids) {
            toReturn.Add(new customer(num));
        }
        return toReturn;
    }
}