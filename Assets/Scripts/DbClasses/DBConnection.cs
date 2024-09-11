using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using SQLite;

namespace DbClasses
{
class DBConnection : MonoBehaviour
{
    private string dbName = "URI=file:" + Application.dataPath + "/Dungeon.db";

    void Start()
    {
        DisplayInfoForTesting();
    }

    void DisplayInfoForTesting()
    {
        using (SqliteConnection connection = new SqliteConnection(dbName)) 
        {
            connection.Open();

            using (SqliteCommand command = connection.CreateCommand()) 
            {
                command.CommandText = "SELECT * FROM Adjectives";

                 using (IDataReader reader = command.ExecuteReader()) 
                 {
                    while (reader.Read()) Debug.Log(reader["Base"]);
                 }
            }
            connection.Close();
        }
    }
}
}