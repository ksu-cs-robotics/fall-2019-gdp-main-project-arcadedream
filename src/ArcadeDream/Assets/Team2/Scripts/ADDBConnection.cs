using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using FastMember;
using UnityEngine;

/// <summary>
/// Simplifies the connection to and from an sql database, as well as the queries thereof
/// Josh Dotson
/// Simplicity Software Design LLC.
/// Notes: requires FastMember library
///
/// Example Usage:
/// 
/// // The idea is that each member in this class has to map to some column in a particular database table
/// public class ADLeaderBoardEntry
/// {
///     // Each of these members are the same name of a column in the database table
///     public string PlayerID { get; set; }
///     public int Score { get; set; }
///     ...
/// }
/// 
/// ...
/// using (ADDBConnection connect = new ADDBConnection())
/// {
///     try
///     {
///         connect.Open()
///         var leaderBoard = 
///             connect.Query<ADLeaderBoardEntry>($"SELECT * FROM {(Table Name)}");
///         ...
///         
///         // leaderBoard will now be a List<ADLeaderBoardEntry>, each element corrensponding to a tuple in the table
///         FirstPlace = leaderBoard[0].Score
///         SecondPlace = leaderBoard[1].Score
///         ...
///         
///         connect.Query($"INSERT INTO {(Table Name)} VALUES ('{Player.PlayerID}', {Score})");
///         ...
///     }
///     catch (Exception ex)
///     {
///         throw ex;
///     }
///     finally 
///     {
///         connect.Close();
///     }
/// }
/// </summary>

/*
public class ADDBConnection : IDisposable
{
    private SqlConnection _sqlConnect;

    public bool IsOpen { get; set; }
    public bool IsElevated { get; }

    public bool Failed { get; set; }
    public string ErrorMessage { get; set; }

    // Insert your connection string here
    public static string ConnectionString = "";

    public ADDBConnection()
    {
        _sqlConnect = new SqlConnection();

        IsOpen = false;

        Failed = false;
        ErrorMessage = string.Empty;
    }
    public ADDBConnection(string newConnectionString) : this()
    {
        // If you'd wish to make the connectionString non static
        // ConnectionString = newConnectionString;
    }

    // Used for write queries...
    public void Query(string queryString)
    {
        try
        {
            var _command = _sqlConnect.CreateCommand();
            _command.CommandText = queryString;
            var _reader = _command.ExecuteReader();

            _reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    // Used for read queries...
    public List<T> Query<T>(string queryString) where T : new()
    {
        List<T> result = new List<T>();

        try
        {
            var _command = _sqlConnect.CreateCommand();
            _command.CommandText = queryString;
            var _reader = _command.ExecuteReader();

            while (_reader.Read())
            {
                Type type = typeof(T);

                // Create FastMember type accessor for field to member mapping
                var accessor = TypeAccessor.Create(type);
                // Stores a list of all member names within a class
                var typeMembers = accessor.GetMembers();
                // Create a new instance of T to store a tuple into
                var tuple = new T();

                for (int i = 0; i < _reader.FieldCount; i++)
                {
                    if (!_reader.IsDBNull(i))
                    {
                        // Get the name of a db field in the table...
                        string fieldName = _reader.GetName(i);

                        // Look for any members inside T with the same name, ignoring the case...
                        if (typeMembers.Any(m => string.Equals(m.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
                        {
                            // If there is a member in T that maps to a field column, use the FastMember accessor to map it into tuple
                            accessor[tuple, fieldName] = _reader.GetValue(i);
                        }
                    }
                }

                // Add this tuple into the full result, and move on
                result.Add(tuple);
            }

            _reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return result;
    }

    public void Open()
    {
        _sqlConnect = new SqlConnection(ConnectionString);

        try
        {
            _sqlConnect.Open();
            IsOpen = true;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.ToString();
            IsOpen = false;

            throw ex;
        }
    }
    public void Close()
    {
        _sqlConnect.Close();
        _sqlConnect.Dispose();
    }
    public void Dispose()
    {
        _sqlConnect.Dispose();
    }
} */

