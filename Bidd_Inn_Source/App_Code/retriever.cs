using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Interface for any class the retrieves all information from a database table
/// </summary>
public interface retriever
{
    List<DBConnector> retrieve();
}