using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Represents a customer review of a hotel
/// </summary>
public class review : DBConnector
{
    private int id;
    private hotel thisHotel;
    private customer thisCust;
    private double rating;
    private string comment;
    public review(hotel thisHotel, customer thisCust, double rating, string comment)
    {
        comment = commaSwitch(comment)[0];
        SqlCommand newCommand = new SqlCommand("INSERT INTO Reviews (HotelId, CustomerId, StarRating, Comments) VALUES (" + thisHotel.returnId + ", " + thisCust.returnId + ", " + rating + ", '" + comment + "')", dbConnect);
        lock (theLock)
        {
            dbConnect.Open();
            newCommand.ExecuteNonQuery();
            dbConnect.Close();
        }
        SqlCommand getCommand = new SqlCommand("SELECT MAX(Id) FROM Reviews", dbConnect);
        lock (theLock)
        {
            dbConnect.Open();
            SqlDataReader read = getCommand.ExecuteReader();
            if (read.Read()) id = Convert.ToInt32(read[0]);
            dbConnect.Close();
        }
        this.thisHotel = thisHotel;
        this.thisCust = thisCust;
        this.rating = rating;
        this.comment = comment;
    }
    public review(int id) {
        this.id = id;
        SqlCommand newCommand = new SqlCommand("SELECT StarRating, Comments FROM Reviews WHERE Id = " + id + ";", dbConnect);
        lock (theLock) {
            dbConnect.Open();
            SqlDataReader read = newCommand.ExecuteReader();
            if (read.Read()) {
                rating = Convert.ToDouble(read[0]);
                comment = read[1].ToString();
            }
            dbConnect.Close();
        }
        thisHotel = null;
        thisCust = null;
    }

    public int returnId {
        get {
            return id;
        }
    }
    public string getComments {
        get {
            return comment;
        }
    }
    public double getRating {
        get {
            return rating;
        }
    }
    /// <summary>
    /// Returns the hotel that this review is for
    /// </summary>
    public hotel rHotel {
        get {
            if (thisHotel == null) {
                SqlCommand newCommand = new SqlCommand("SELECT HotelId FROM Reviews WHERE Id = " + id + ";", dbConnect);
                int hotId = 0; //WILL NOT BE ZERO
                lock (theLock) {
                    dbConnect.Open();
                    SqlDataReader read = newCommand.ExecuteReader();
                    if (read.Read()) {
                        hotId = Convert.ToInt32(read[0]);
                    }
                    dbConnect.Close();
                }
                thisHotel = new hotel(hotId);
            }
            return thisHotel;
        }
    }
    /// <summary>
    /// Returns the customer that made this review
    /// </summary>
    public customer rCustomer {
        get {
            if (thisCust == null) {
                SqlCommand newCommand = new SqlCommand("SELECT CustomerId FROM Reviews WHERE Id = " + id + ";", dbConnect);
                int custId = 0; //WILL NOT BE ZERO
                lock (theLock) {
                    dbConnect.Open();
                    SqlDataReader read = newCommand.ExecuteReader();
                    if (read.Read()) {
                        custId = Convert.ToInt32(read[0]);
                    }
                    dbConnect.Close();
                }
                thisCust = new customer(custId);
            }
            return thisCust;
        }
    }
}