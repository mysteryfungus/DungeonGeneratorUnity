using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using SQLite;
using UnityEngine.UI;
using GenerationClasses;
using System.IO;

namespace DbClasses
{
class DBManager : MonoBehaviour
{
    private string dbName = "URI=file:" + Application.dataPath + "/Dungeon.db";
    [SerializeField] Text dungeonNameField;
    private NameGenerator nameGen;
    private ThreatGenerator threatGen;

    void Start()
    {
        if (CheckDB()) {
            InitGenerators();
            ChangeName(nameGen.GenerateName()); //при запуске один раз генерирует название
            threatGen.BuildEncounter(5, 3, 1);
        }
    }

    void InitGenerators()
    {
        nameGen = new NameGenerator(dbName);
        threatGen = new ThreatGenerator(dbName);
    }

    bool CheckDB()
    {
        if (!File.Exists(Application.dataPath + "/Dungeon.db")) 
        {
            Debug.Log("Dungeon.db Not Found in " + Application.dataPath + ", Everything Will Break");
            return false;
        }
        else return true;
    }

    private void ChangeName(string newName)
    {
        dungeonNameField.text = newName;
    }
}
}