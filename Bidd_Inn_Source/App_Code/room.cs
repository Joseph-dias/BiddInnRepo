using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Data.SqlClient;
using System.IO;

/// <summary>
/// Summary description for room
/// </summary>
public class room : DBConnector
{
    private int id;
    private double askedPrice;
    private string name;
    private int quantity;
    hotel thisHotel;
    public room(byte[] pic, double askedPrice, string name, hotel thisHotel, int quantity)
    {
        string[] newName = commaSwitch(name);
        name = newName[0]; //Reassign parameter
        SqlCommand newCommand = new SqlCommand("INSERT INTO Rooms (Picture, AskedPrice, Name, HotelId, Quantity) VALUES (@value, " + askedPrice + ", '" + name + "', " + thisHotel.returnId + ", " + quantity + ")", dbConnect);
        SqlParameter param = new SqlParameter("@value", System.Data.SqlDbType.VarBinary);
        newCommand.Parameters.Add(param);
        param.Value = pic;
        lock (theLock)
        {
            dbConnect.Open();
            newCommand.ExecuteNonQuery();
            dbConnect.Close();
        }
        newCommand = new SqlCommand("SELECT MAX(Id) FROM Rooms", dbConnect);
        SqlDataReader read;
        lock (theLock) {
            dbConnect.Open();
            read = newCommand.ExecuteReader();
            if (read.Read()) {
                id = Convert.ToInt32(read[0]);
            }
            dbConnect.Close();
        }

        this.askedPrice = askedPrice;
        this.name = name;
        this.quantity = quantity;
        this.thisHotel = thisHotel;
    }

    public room(int id) {
        thisHotel = null;
        this.id = id;
        SqlCommand newCommand = new SqlCommand("SELECT AskedPrice, Name, Quantity FROM Rooms WHERE Id = " + id + ";", dbConnect);
        SqlDataReader read;
        lock (theLock) {
            dbConnect.Open();
            read = newCommand.ExecuteReader();
            if (read.Read())
            {
                askedPrice = Convert.ToDouble(read[0]);
                name = (string)read[1];
                quantity = Convert.ToInt32(read[2]);
            }
            dbConnect.Close();
        }
    }

    public int getId {
        get {
            return id;
        }
    }

    public hotel roomHotel {
        get {
            if (thisHotel == null) {
                SqlCommand newCommand = new SqlCommand("SELECT HotelId FROM Rooms WHERE Id = " + id + ";", dbConnect);
                int i = 0; //Id will NOT be null
                lock (theLock) {
                    dbConnect.Open();
                    SqlDataReader read = newCommand.ExecuteReader();
                    if (read.Read()) {
                        i = Convert.ToInt32(read[0]);
                    }
                    dbConnect.Close();
                }
                thisHotel = new hotel(i);
            }
            return thisHotel;
        }
    }

    /// <summary>
    /// Number of available rooms between the specified dates
    /// </summary>
    /// <param name="start">Start date</param>
    /// <param name="finish">End Date</param>
    /// <returns>Number of rooms available between the dates</returns>
    public int numAvailable(DateTime start, DateTime finish) {
        SqlCommand newCommand = new SqlCommand("SELECT Id FROM Offers WHERE RoomId = " + id + " AND Accepted = " + 1 + ";", dbConnect);
        List<int> ids = new List<int>();
        List<offer> theseOffers = new List<offer>();
        lock (theLock) {
            dbConnect.Open();
            SqlDataReader read = newCommand.ExecuteReader();
            while (read.Read()) {
                ids.Add(Convert.ToInt32(read[0])); //Add ids to the list to retrieve offers from later
            }
            dbConnect.Close(); //Close connection
        }
        foreach (int theId in ids) {
            theseOffers.Add(new offer(theId));
        }

        //Now: Count how many rooms are still available between the dates given
        int q = quantity; //Starting point
        foreach (offer o in theseOffers) {
            if (o.active(start, finish)) q -= o.getNumRooms;
        }
        return q;
    }

    /// <summary>
    /// Gets the main picture for this room
    /// </summary>
    public Image getMainPic
    {
        get
        {
            lock (theLock)
            {
                dbConnect.Open();
                SqlCommand newCommand = new SqlCommand("SELECT Picture FROM Rooms WHERE Id = " + id + ";", dbConnect);
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

    public int num {
        get {
            return quantity;
        }
    }

    public string description {
        get {
            SqlCommand newCommand = new SqlCommand("SELECT Description FROM Rooms WHERE Id = " + id + ";", dbConnect);
            lock (theLock)
            {
                dbConnect.Open();
                SqlDataReader read = newCommand.ExecuteReader();
                if (read.Read())
                {
                    try
                    {
                        string toReturn = read[0].ToString();
                        dbConnect.Close();
                        return toReturn;
                    }
                    catch (Exception)
                    {

                    }
                }
                dbConnect.Close();
                return null;
            }
        }

        set {
            string[] toInsert = commaSwitch(value);
            SqlCommand newCommand = new SqlCommand("UPDATE Rooms SET Description= '" + toInsert[0] + "' WHERE Id = " + id + ";", dbConnect);
            lock (theLock) {
                dbConnect.Open();
                newCommand.ExecuteNonQuery();
                dbConnect.Close();
            }
        }
    }

    public double getAskedPrice {
        get {
            return askedPrice;
        }
    }

    /// <summary>
    /// Get the name of the room
    /// </summary>
    /// <returns>Room name</returns>
    public override string ToString()
    {
        return name;
    }
}