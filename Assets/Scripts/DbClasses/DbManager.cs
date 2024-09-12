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
    [SerializeField] Text nameField;
    private NameGenerator nameGen;

    void Start()
    {
        InitGenerators();
        if (CheckDB()) ChangeName(nameGen.GenerateName()); //при запуске один раз генерирует название, если существует дб
    }

    void InitGenerators()
    {
        nameGen = new NameGenerator(dbName);
        //threatGen (в нем уже monter + hazard)
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
        nameField.text = newName;
    }
}
}