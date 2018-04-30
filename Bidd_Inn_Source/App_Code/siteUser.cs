using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Abstract class for the users of the site to inherit from
/// </summary>
public abstract class siteUser: DBConnector
{
    protected int id;
    protected string address;
    protected string city;
    protected string state;
    protected int zip;
    public siteUser()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int returnId {
        get {
            return id;
        }
    }

    public string username {
        get {
            SqlCommand newCommand = new SqlCommand("SELECT Username FROM Users WHERE Id = " + id + ";", dbConnect);
            lock (theLock) {
                dbConnect.Open();
                SqlDataReader read = newCommand.ExecuteReader();
                if (read.Read())
                {
                    string u = read[0].ToString();
                    dbConnect.Close();
                    return u;
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Authenticate the user
    /// </summary>
    /// <param name="username">Username of user</param>
    /// <param name="password">Password of user</param>
    /// <returns></returns>
    protected bool authenticated(string username, string password) {
        lock (theLock)
        {
            dbConnect.Open();
            SqlCommand newCommand = new SqlCommand("SELECT Password, UserGuid FROM Users WHERE Username='" + username.ToLower() + "'", dbConnect);
            SqlDataReader read = newCommand.ExecuteReader();
            if (read.Read())
            {
                string enteredPass = hashPass(password, Guid.Parse(read[1].ToString()));
                if (enteredPass == read[0].ToString())
                {
                    read.Close();
                    newCommand = new SqlCommand("SELECT Id FROM Users WHERE Username='" + username + "'", dbConnect);
                    read = newCommand.ExecuteReader();
                    if (read.Read())
                    {
                        id = (int)read[0]; //Set id here!
                        read.Close();
                        dbConnect.Close();
                        return true;
                    }
                    else
                    {
                        read.Close();
                        dbConnect.Close();
                        return false;
                    }
                }
                else
                {
                    read.Close();
                    dbConnect.Close();
                    return false;
                }
            }
            else
            {
                read.Close();
                dbConnect.Close();
                return false;
            }
        }
    }

    /// <summary>
    /// Encrypt the password
    /// </summary>
    /// <param name="plain">Password in plain text</param>
    /// <param name="theGuid">Guid to use</param>
    /// <returns></returns>
    private string hashPass(string plain, Guid theGuid) {
        SHA1 sha1 = SHA1.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(plain + theGuid.ToString());
        byte[] hash = sha1.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();
        for (var i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }

    protected void changePassword(string pass) {
        Guid newGuid = Guid.NewGuid();
        string hashed = hashPass(pass, newGuid);
        SqlCommand newCommand = new SqlCommand("UPDATE Users SET Password= '" + hashed + "', UserGuid= '" + newGuid.ToString() + "' WHERE Id = " + id + ";", dbConnect);
        lock (theLock) {
            dbConnect.Open();
            newCommand.ExecuteNonQuery();
            dbConnect.Close();
        }
    }

    protected bool addUser(string username, string password)
    {
        Guid newGuid = Guid.NewGuid();
        string pass = hashPass(password, newGuid);
        string[] user = commaSwitch(username); //ADD EXTRA COMMAS
        username = user[0];
        SqlCommand newCommand = new SqlCommand("SELECT Username FROM Users WHERE Username='" + username.ToLower() + "'", dbConnect);
        lock (theLock)
        {
            dbConnect.Open();
            SqlDataReader read = newCommand.ExecuteReader();
            if (read.Read())
            {
                read.Close();
                dbConnect.Close();
                return false;
            }
            read.Close();
            newCommand = new SqlCommand("INSERT INTO Users (Username, Password, UserGuid) VALUES ('" + username.ToLower() + "', '" + pass + "', '" + newGuid.ToString() + "')", dbConnect);
            newCommand.ExecuteNonQuery();
            newCommand = new SqlCommand("SELECT max(Id) FROM Users", dbConnect);
            read = newCommand.ExecuteReader();
            if (read.Read())
            {
                id = (int)read[0]; //Get current user id
            }
            read.Close();
            dbConnect.Close();
            return true;
        }
    }

    protected abstract bool retrieve();
    public abstract string outputInfo();
    public abstract string outputName();
}