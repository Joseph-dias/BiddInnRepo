using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DBConnector
/// </summary>
public abstract class DBConnector
{
    protected static Object theLock = new Object();
    protected const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\stash\source\repos\BiddInn\Bidd_Inn_Source\App_Data\Database.mdf;Integrated Security=True";
    protected SqlConnection dbConnect;
    public DBConnector()
    {
        dbConnect = new SqlConnection(connectionString);
    }

    /// <summary>
    /// Switch all single commas to doubles so they can go into the database
    /// </summary>
    /// <param name="list">List of strings to operate on</param>
    /// <returns>Array of strings with double commas instead of single ones</returns>
    protected static string[] commaSwitch(params string[] list) {
        string[] toReturn = new string[list.Length];
        for(int x = 0; x < list.Length; x++) {
            toReturn[x] = list[x].Replace("'", "''");
        }
        return toReturn;
    }

    /// <summary>
    /// Remove all strings from first string
    /// </summary>
    /// <param name="theString">String to edit</param>
    /// <param name="toRemove">Strings to remove from first string</param>
    /// <returns>First string, with all other strings removed from it</returns>
    protected string removeAll(string theString, params string[] toRemove) {
        string newString = theString;
        foreach (string s in toRemove) {
            newString = newString.Replace(s, "");
        }
        return newString;
    }
}