using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for userManager
/// </summary>
public class userManager
{
    private static Object lockOb = new Object();
    private static userManager instance = null;
    private List<siteUser> userLST;
    private userManager()
    {
        userLST = new List<siteUser>();
    }

    public static siteUser get(int id) { //Get user by id
        lock (lockOb)
        {
            if (instance != null)
            {
                foreach (siteUser thisUser in instance.userLST)
                {
                    if (thisUser.returnId == id)
                    {
                        return thisUser;
                    }
                }
            }
            return null;
        }
    }
    public static bool add(siteUser thisUser) { //Add user
        lock (lockOb)
        {
            if (instance != null)
            {
                foreach (siteUser theUser in instance.userLST)
                {
                    if (theUser.returnId == thisUser.returnId) return false;
                }
            }
            else instance = new userManager();
            instance.userLST.Add(thisUser);
            return true;
        }
    }
}