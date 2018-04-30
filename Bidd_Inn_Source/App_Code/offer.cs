using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for offer
/// </summary>
public class offer : DBConnector
{
    public enum state { ACCEPTED = 1, DECLINED = -1, NOTHING = 0}
    private int id;
    private hotel thisHotel;
    private room thisRoom;
    private customer thisCustomer;
    private int numRooms;
    private DateTime start;
    private DateTime end;
    private double Offered_Price_Per_Day;
    private double totalValue;
    state accepted;
    /// <summary>
    /// Create a new offer to insert into the offers table
    /// </summary>
    /// <param name="thisHotel">Hotel that this offer is directed towards</param>
    /// <param name="thisRoom">Room that this offer was made on</param>
    /// <param name="thisCustomer">Customer that sent this offer</param>
    /// <param name="num">Number of rooms in the offer</param>
    /// <param name="start">Start date that this offer would take effect</param>
    /// <param name="end">End date for this offer</param>
    /// <param name="perDay">Offered price per day, per room</param>
    public offer(hotel thisHotel, room thisRoom, customer thisCustomer, int num, DateTime start, DateTime end, double perDay)
    {
        totalValue = (perDay * num) * (end - start).TotalDays;
        SqlCommand newCommand = new SqlCommand("INSERT INTO Offers (HotelId, RoomId, CustomerId, NumberOfRooms, StartDate, EndDate, DailyOffer, TotalOffer, Accepted) VALUES (" + thisHotel.returnId + ", " + thisRoom.getId + ", " + thisCustomer.returnId + ", " + num + ", @sValue, @eValue, " + perDay + ", " + totalValue + ", 0)", dbConnect);
        newCommand.Parameters.AddWithValue("@sValue", start);
        newCommand.Parameters.AddWithValue("@eValue", end);
        lock (theLock) {
            dbConnect.Open();
            newCommand.ExecuteNonQuery();
            dbConnect.Close();
        }
        SqlCommand getCommand = new SqlCommand("SELECT MAX(Id) FROM Offers", dbConnect);
        lock (theLock) {
            dbConnect.Open();
            SqlDataReader read = getCommand.ExecuteReader();
            if (read.Read()) id = Convert.ToInt32(read[0]);
            dbConnect.Close();
        }
        this.thisHotel = thisHotel;
        this.thisRoom = thisRoom;
        this.thisCustomer = thisCustomer;
        numRooms = num;
        this.start = start;
        this.end = end;
        Offered_Price_Per_Day = perDay;
        accepted = state.NOTHING;
    }
    public offer(int id) {
        this.id = id;
        SqlCommand newCommand = new SqlCommand("SELECT NumberOfRooms, StartDate, EndDate, DailyOffer, TotalOffer, Accepted FROM Offers WHERE Id = " + id + ";", dbConnect);
        lock (theLock) {
            dbConnect.Open();
            SqlDataReader read = newCommand.ExecuteReader();
            if (read.Read()) {
                numRooms = Convert.ToInt32(read[0]);
                start = (DateTime)read[1];
                end = (DateTime)read[2];
                Offered_Price_Per_Day = Convert.ToDouble(read[3]);
                totalValue = Convert.ToDouble(read[4]);
                accepted = (state)Convert.ToInt32(read[5]);
            }
            dbConnect.Close();
            thisHotel = null;
            thisRoom = null;
            thisCustomer = null;
        }
    }
    public hotel offerHotel {
        get {
            if (thisHotel == null)
            {
                SqlCommand newCommand = new SqlCommand("SELECT HotelId FROM Offers WHERE Id = " + id + ";", dbConnect);
                int hotId = 0; //WILL NOT BE 0
                lock (theLock) {
                    dbConnect.Open();
                    SqlDataReader read = newCommand.ExecuteReader();
                    if (read.Read()) hotId = Convert.ToInt32(read[0]);
                    dbConnect.Close();
                }
                thisHotel = new hotel(hotId);
            }
            return thisHotel;
        }
    }
    public customer offerCust
    {
        get
        {
            if (thisCustomer == null)
            {
                SqlCommand newCommand = new SqlCommand("SELECT CustomerId FROM Offers WHERE Id = " + id + ";", dbConnect);
                int CustId = 0; //WILL NOT BE 0
                lock (theLock)
                {
                    dbConnect.Open();
                    SqlDataReader read = newCommand.ExecuteReader();
                    if (read.Read()) CustId = Convert.ToInt32(read[0]);
                    dbConnect.Close();
                }
                thisCustomer = new customer(CustId);
            }
            return thisCustomer;
        }
    }
    public void setState(state theState) {
        if (accepted != theState) {
            accepted = theState;
            SqlCommand newCommand = new SqlCommand("UPDATE Offers SET Accepted = " + Convert.ToInt32(accepted) + " WHERE Id = " + id + ";", dbConnect);
            lock (theLock) {
                dbConnect.Open();
                newCommand.ExecuteNonQuery();
                dbConnect.Close();
            }
        }
    }
    public room offerRoom
    {
        get
        {
            if (thisRoom == null)
            {
                SqlCommand newCommand = new SqlCommand("SELECT RoomId FROM Offers WHERE Id = " + id + ";", dbConnect);
                int roomId = 0; //WILL NOT BE 0
                lock (theLock)
                {
                    dbConnect.Open();
                    SqlDataReader read = newCommand.ExecuteReader();
                    if (read.Read()) roomId = Convert.ToInt32(read[0]);
                    dbConnect.Close();
                }
                thisRoom = new room(roomId);
            }
            return thisRoom;
        }
    }
    /// <summary>
    /// Is the offer active at some point between the two given dates?
    /// </summary>
    /// <param name="sDate">Start date to begin check</param>
    /// <param name="fDate">Checkout date</param>
    /// <returns>FALSE if the offer is active at some point between the two given dates</returns>
    public bool active(DateTime sDate, DateTime fDate) {
        if ((start >= sDate && start < fDate) || (end >= sDate && end < fDate)) return true; //Offer is active at some point between those two given dates
        return false;
    }
    public state returnState {
        get {
            return accepted;
        }
    }
    public DateTime fNight {
        get {
            return start;
        }
    }
    public DateTime checkout {
        get {
            return end;
        }
    }
    public int getNumRooms {
        get {
            return numRooms;
        }
    }
    public double getTotal {
        get {
            return totalValue;
        }
    }
    public double GET_PRICE_PER_DAY {
        get {
            return Offered_Price_Per_Day;
        }
    }
    public override string ToString()
    {
        return numRooms.ToString() + " ROOMS\n" + "FIRST NIGHT: " + start.ToString("MM/dd/yy") + "\n" + "CHECKOUT: " + end.ToString("MM/dd/yy") + "\n" + "$" + Offered_Price_Per_Day.ToString() + "/NIGHT";
    }
}