using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Send email to user
/// </summary>
public class emailSender
{
    private static emailSender instance = null;
    private const string fromMail = "Bidd.Inn77@gmail.com";
    private SmtpClient sending;
    private emailSender()
    {
        sending = new SmtpClient("smtp.gmail.com", 587);
        sending.Credentials = new NetworkCredential(fromMail, "Bidd7000");
        sending.EnableSsl = true;
    }
    /// <summary>
    /// Get instance of class
    /// </summary>
    public static emailSender sender {
        get {
            if (instance == null) {
                instance = new emailSender();
            }
            return instance;
        }
    }
    /// <summary>
    /// Authenticate email
    /// </summary>
    /// <param name="cust">Customer to authenticate</param>
    /// <returns>Code sent to customer's email</returns>
    public int authenticate(customer cust) {
        Random rand = new Random();
        int code = rand.Next(10000, 99999);
        MailMessage newMessage = new MailMessage(fromMail, cust.getEmail);
        newMessage.Subject = "Your secret code";
        newMessage.Body = "Your code: " + code;
        sending.Send(newMessage);
        return code;
    }
    /// <summary>
    /// Send email message to customer
    /// </summary>
    /// <param name="to">Customer to send the message to</param>
    /// <param name="subject">Subject of the email message</param>
    /// <param name="body">Body of the email message</param>
    public void send(customer to, string subject, string body) {
        MailMessage newMessage = new MailMessage(fromMail, to.getEmail);
        newMessage.Subject = subject;
        newMessage.Body = body;
        try
        {
            sending.Send(newMessage);
        }
        catch (Exception) { }
    }
}