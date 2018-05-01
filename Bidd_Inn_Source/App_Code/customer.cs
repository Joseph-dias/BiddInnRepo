using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

/// <summary>
/// Represents the user as a customer
/// </summary>
public class customer : siteUser
{
    private string firstName;
    private string lastName;
    private Image idFront;
    private Image idBack;
    private string email;
    private DateTime age;
    private customer()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public customer(int id) {
        this.id = id;
        retrieve();
    }

    public string getFullName {
        get {
            return firstName + " " + lastName;
        }
    }

    public customer(string username, string password) { //Authenticate user
        if (authenticated(username, password))
        {
            retrieve();
        }
        else { throw new Exception("NO USER FOUND"); }
    }

    /// <summary>
    /// Add new customer
    /// </summary>
    /// <param name="username">Customer username</param>
    /// <param name="password">Customer password</param>
    /// <param name="firstName">First name of customer</param>
    /// <param name="lastName">Last name of customer</param>
    /// <param name="address">Home address of customer</param>
    /// <param name="city">Home city of customer</param>
    /// <param name="state">Home state of customer</param>
    /// <param name="email">Email of customer</param>
    /// <param name="dob">Customer date of birth</param>
    /// <returns></returns>
    public static customer add(string username, string password, string firstName, string lastName, string address, string city, string state, string email, DateTime dob, int zip) { //Add DOB To Database and retrieve it
        customer newCust = new customer();
        if (newCust.addUser(username, password))
        {
            string[] newTXT = commaSwitch(firstName, lastName, address, city, email);
            firstName = newTXT[0];
            lastName = newTXT[1];
            address = newTXT[2];
            city = newTXT[3];
            email = newTXT[4];
            SqlCommand newCommand = new SqlCommand("INSERT INTO Customer (Id, FirstName, LastName, StreetAddress, City, State, Email, EmailAuthenticated, DOB, Zip, NumSeen) VALUES (" + newCust.id + ", '" + firstName + "', '" + lastName + "', '" + address + "', '" + city + "', '" + state + "', '" + email + "', 0, @value, " + zip + ", 0)", newCust.dbConnect);
            newCommand.Parameters.AddWithValue("@value", dob.Date);
            lock (theLock)
            {
                newCust.dbConnect.Open();
                newCommand.ExecuteNonQuery();
                newCust.dbConnect.Close();
            }
            newCust.retrieve();
            http://localhost:60921/Scripts/myScript.js
            return newCust;
        }
        else return null;
    }

    public string getEmail {
        get { return email; }
    }

    public void authenticateEmail() {
        SqlCommand newCommand = new SqlCommand("UPDATE Customer SET EmailAuthenticated = 1 WHERE Id = " + id + ";", dbConnect);
        lock (theLock) {
            dbConnect.Open();
            newCommand.ExecuteNonQuery();
            dbConnect.Close();
        }
    }

    /// <summary>
    /// Add to the number of accepted/rejected offers that this customer has seen
    /// </summary>
    /// <param name="num">Number to add</param>
    public void addSeen(int num) {
        int seen = 0;
        SqlCommand newCommand = new SqlCommand("SELECT NumSeen FROM Customer WHERE Id = " + id + ";", dbConnect);
        lock (theLock) {
            dbConnect.Open();
            SqlDataReader read = newCommand.ExecuteReader();
            if (read.Read()) seen = Convert.ToInt32(read[0]);
            dbConnect.Close();
        }
        newCommand = new SqlCommand("UPDATE Customer SET NumSeen = " + (seen + num).ToString() + " WHERE Id = " + id + ";", dbConnect);
        lock (theLock) {
            dbConnect.Open();
            newCommand.ExecuteNonQuery();
            dbConnect.Close();
        }
    }

    public string newPassword {
        set {
            changePassword(value);
        }
    }

    /// <summary>
    /// Set the main picture
    /// </summary>
    public byte[] setMainPic {
        set {
            SqlCommand newCommand = new SqlCommand("UPDATE Customer SET Picture = @value WHERE Id = " + id + ";", dbConnect);
            SqlParameter param = new SqlParameter("@value", SqlDbType.VarBinary);
            newCommand.Parameters.Add(param);
            param.Value = value;

            lock (theLock) {
                dbConnect.Open();
                newCommand.ExecuteNonQuery();
                dbConnect.Close();
            }
        }
    }

    /// <summary>
    /// Returns array of 2 numbers.  First is the number of accepted/declined offers, second is the total number of offers
    /// </summary>
    public int[] numOffersSent {
        get {
            int[] nums = new int[2];
            nums[0] = 0;
            nums[1] = 0;
            SqlCommand newCommand = new SqlCommand("SELECT Accepted FROM Offers WHERE CustomerId = " + id + ";", dbConnect);
            SqlCommand seenCMD = new SqlCommand("SELECT NumSeen FROM Customer WHERE Id = " + id + ";", dbConnect);
            lock (theLock) {
                dbConnect.Open();
                SqlDataReader read1 = newCommand.ExecuteReader();
                while (read1.Read()) {
                    nums[1]++;
                    if (Convert.ToInt32(read1[0]) != 0) {
                        nums[0]++;
                    }
                }
                read1.Close();
                SqlDataReader read2 = seenCMD.ExecuteReader();
                if (read2.Read()) nums[0] -= Convert.ToInt32(read2[0]);
                dbConnect.Close();
            }
            return nums;
        }
    }

    public bool emailAuthenticated {
        get {
            SqlCommand newCommand = new SqlCommand("SELECT EmailAuthenticated FROM Customer WHERE Id = " + id + ";", dbConnect);
            bool toReturn = true;
            lock (theLock) {
                dbConnect.Open();
                SqlDataReader read = newCommand.ExecuteReader();
                if (read.Read()) {
                    if (Convert.ToInt32(read[0]) == 0) toReturn = false;
                }
                dbConnect.Close();
            }
            return toReturn;
        }
    }

    /// <summary>
    /// How many years old is this customer?
    /// </summary>
    public int yearsOld {
        get {
            int myAge = DateTime.Now.Year - age.Year;
            if (age > DateTime.Now.AddYears(-myAge)) {
                myAge--;
            }
            return myAge;
        }
    }

    /// <summary>
    /// Returns City, State
    /// </summary>
    public string homeCity {
        get {
            return city + ", " + state;
        }
    }

    /// <summary>
    /// Gets the main picture for this customer
    /// </summary>
    public Image getMainPic {
        get
        {
            lock (theLock)
            {
                dbConnect.Open();
                SqlCommand newCommand = new SqlCommand("SELECT Picture FROM Customer WHERE Id = " + id + ";", dbConnect);
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

    /// <summary>
    /// Gets information about customer from customer table using customer id
    /// </summary>
    /// <returns>Successful</returns>
    protected override bool retrieve()
    {
        lock (theLock)
        {
            dbConnect.Open();
            SqlCommand newCommand = new SqlCommand("SELECT FirstName, LastName, StreetAddress, City, State, Email, DOB, Zip FROM Customer WHERE Id=" + id, dbConnect);
            SqlDataReader read = newCommand.ExecuteReader();
            if (read.Read())
            { //Get information
                firstName = (string)read[0];
                lastName = (string)read[1];
                address = (string)read[2];
                city = (string)read[3];
                state = (string)read[4];
                email = (string)read[5];
                age = (DateTime)read[6];
                zip = Convert.ToInt32(read[7]);
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
        return "HOME ADDRESS:\n" + address + "\n" + city + ", " + state + " " + zip + "\n\nEmail: " + email + "\nDate of Birth: " + age.ToString("MM/dd/yy");
    }

    public override string outputName()
    {
        return getFullName;
    }
}