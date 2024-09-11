using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using SQLite;
using UnityEngine.UI;
using GenerationClasses;

namespace DbClasses
{
class DBManager : MonoBehaviour
{
    private string dbName = "URI=file:" + Application.dataPath + "/Dungeon.db";
    [SerializeField] Text nameField;
    private NameGenerator nameGen;

    void Start()
    {
        InitGenerators();
        string dungeonName = nameGen.GenerateName();
        nameField.text = dungeonName;
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

    void InitGenerators()
    {
        nameGen = new NameGenerator(dbName);

    }
}
}