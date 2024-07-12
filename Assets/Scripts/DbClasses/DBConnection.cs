using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using SQLite;

namespace DbClasses
{
class DBConnection
{
    private string dbName = "URL=file:Dungeon.db";
    private SQLiteConnection connection;

    void ConnectDB(){
        if (connection == null) connection = new SQLiteConnection(dbName);
    }

    void CloseDB()
    {
        if (connection != null) connection.Close();
    }

}
}