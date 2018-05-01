using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Data;

/// <summary>
/// Class represents the user as a hotel
/// </summary>
public class hotel : siteUser
{
    private string name;
    private string phone;
    private hotel()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Returns full name of the hotel
    /// </summary>
    public string getName {
        get {
            return name;
        }
    }

    /// <summary>
    /// Retrieve the hotel from the database
    /// </summary>
    /// <param name="username">Username of user</param>
    /// <param name="password">Password of user</param>
    public hotel(string username, string password)
    { //Authenticate user
        if (authenticated(username, password))
        {
            if (!retrieve()) throw new Exception("USER IS NOT A HOTEL!");
        }
        else { throw new Exception("NO USER FOUND"); }
    }

    /// <summary>
    /// Returns City, State
    /// </summary>
    public string cityState
    {
        get
        {
            return city + ", " + state;
        }
    }

    /// <summary>
    /// Get hotel by id
    /// </summary>
    /// <param name="id">Id # of hotel</param>
    public hotel(int id) {
        this.id = id;
        if (!retrieve()) throw new Exception("No hotel found");
    }

    /// <summary>
    /// Return list of this hotel's listed rooms
    /// </summary>
    public List<room> myRooms {
        get {
            List<room> toReturn = new List<room>();
            SqlCommand newCommand = new SqlCommand("SELECT Id FROM Rooms WHERE HotelId = " + id + ";", dbConnect);
            SqlDataReader read;
            lock (theLock) {
                dbConnect.Open();
                read = newCommand.ExecuteReader();
                while (read.Read()) {
                    toReturn.Add(new room(Convert.ToInt32(read[0])));
                }
                dbConnect.Close();
            }
            return toReturn;
        }
    }

    /// <summary>
    /// Add room to this hotel
    /// </summary>
    /// <param name="pic">Picture of the room</param>
    /// <param name="askedPrice">Asked price for the room.  Per day.</param>
    /// <param name="name">Name of the room</param>
    /// <param name="quantity">Number of rooms</param>
    /// <param name="description">Description of room</param>
    public void addRoom(byte[] pic, double askedPrice, string name, int quantity, string description = null) {
        room added = new room(pic, askedPrice, name, this, quantity);
        if (description != null) {
            added.description = description;
        }
    }

    /// <summary>
    /// Add a new hotel to the database
    /// </summary>
    /// <param name="username">Username of user</param>
    /// <param name="password">Password of user</param>
    /// <param name="name">Name of hotel</param>
    /// <param name="address">Address of hotel</param>
    /// <param name="city">City where hotel is located</param>
    /// <param name="state">State where hotel is located</param>
    /// <param name="phone">Phone # of hotel</param>
    /// <returns>New hotel object</returns>
    public static hotel add(string username, string password, string name, string address, string city, string state, string phone, int zip)
    {
        hotel newHot = new hotel();
        if (newHot.addUser(username, password))
        {
            string[] param = commaSwitch(name, address, city);
            name = param[0]; //Reassign parameters
            address = param[1];
            city = param[2];
            SqlCommand newCommand = new SqlCommand("INSERT INTO Hotel (Id, Name, Address, City, State, Phone, Zip) VALUES (" + newHot.id + ", '" + name + "', '" + address + "', '" + city + "', '" + state + "', '" + phone + "', " + zip + ")", newHot.dbConnect);
            lock (theLock)
            {
                newHot.dbConnect.Open();
                newCommand.ExecuteNonQuery();
                newHot.dbConnect.Close();
            }
            newHot.retrieve();
            return newHot;
        }
        else return null;
    }

    public Image getMainPic
    {
        get
        {
            lock (theLock)
            {
                dbConnect.Open();
                SqlCommand newCommand = new SqlCommand("SELECT Picture FROM Hotel WHERE Id = " + id + ";", dbConnect);
                SqlDataReader reader = newCommand.ExecuteReader();
                if (reader.Read())
                {
                    try
                    {
                        MemoryStream stream = new MemoryStream((byte[])reader[0]);
                        dbConnect.Close();
                        return Image.FromStream(stream);
                    }
                    catch (Exception) { }
                }
                dbConnect.Close();
                return null;
            }
        }
    }

    public byte[] setMainPic
    {
        set
        {
            SqlCommand newCommand = new SqlCommand("UPDATE Hotel SET Picture = @value WHERE Id = " + id + ";", dbConnect);
            SqlParameter param = new SqlParameter("@value", SqlDbType.VarBinary);
            newCommand.Parameters.Add(param);
            param.Value = value;

            lock (theLock)
            {
                dbConnect.Open();
                newCommand.ExecuteNonQuery();
                dbConnect.Close();
            }
        }
    }

    public string fullAddress {
        get {
            string newPhone = removeAll(phone, "(", ")", "-");
            return address + "\n" + city + ", " + state + " " + zip.ToString() + "\n" + /* Format string as phone number */ string.Format("{0:(###) ###-####}", Convert.ToInt64(newPhone));
        }
    }

    public int outstandingOffers {
        get {
            int x = 0;
            retriever retrieve = new offerRetriever();
            List<DBConnector> list = retrieve.retrieve();
            foreach (DBConnector theList in list) {
                if (theList is offer) {
                    if ((theList as offer).offerHotel.returnId == id && (theList as offer).returnState == offer.state.NOTHING) x++;
                }
            }
            return x;
        }
    }

    /// <summary>
    /// Returns list of reviews for this hotel
    /// </summary>
    public List<review> myReviews {
        get {
            retriever r = new reviewRetriever();
            List<DBConnector> reviews = r.retrieve();
            List<review> toReturn = new List<review>();
            foreach (DBConnector c in reviews) {
                if (c is review) {
                    review me = c as review; //Convert c to review
                    if (me.rHotel.id == id) toReturn.Add(me);
                }
            }
            return toReturn;
        }
    }

    public void addReview(double rating, string comments, customer cust) {
        review newRev = new review(this, cust, rating, comments);
    }

    /// <summary>
    /// Has this customer reviewed this hotel?
    /// </summary>
    /// <param name="c">Customer to check for</param>
    /// <returns>Whether or not this customer has reviewed this hotel</returns>
    public bool hasReviewed(customer c) {
        List<review> r = myReviews;
        foreach (review theReview in r) {
            if (theReview.rCustomer.returnId == c.returnId) return true;
        }
        return false;
    }



    protected override bool retrieve()
    {
        lock (theLock)
        {
            dbConnect.Open();
            SqlCommand newCommand = new SqlCommand("SELECT Name, Address, City, State, Phone, Zip FROM Hotel WHERE Id=" + id, dbConnect);
            SqlDataReader read = newCommand.ExecuteReader();
            if (read.Read())
            { //Get information
                name = (string)read[0];
                address = (string)read[1];
                city = (string)read[2];
                state = (string)read[3];
                phone = (string)read[4];
                zip = Convert.ToInt32(read[5]);
                read.Close();
                dbConnect.Close();
                return true;
            }
            read.Close();
            dbConnect.Close();
            return false;
        }
    }

    public override string outputInfo()
    {
        return "HOTEL ADDRESS:\n" + fullAddress;
    }

    public override string outputName()
    {
        return name;
    }
}