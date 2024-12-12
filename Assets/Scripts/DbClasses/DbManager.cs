using UnityEngine;
using UnityEngine.UI;
using GenerationClasses;
using System.IO;

namespace DbClasses
{
class DBManager : MonoBehaviour
{
    //static string dbName = "/Dungeon.db";
    static string dbName = "/DungeonGenerator.db";
    public static string dbLink = "URI=file:" + Application.dataPath + dbName;
    [SerializeField] Text dungeonNameField;
    private NameGenerator nameGen;
    private ThreatGenerator threatGen;
    public bool debuggingThreats = true;
    public bool useHumansInBattle = true;
    public bool useHazards = true;

    void Start()
    {
        if (CheckDB()) {
            InitGenerators();
            //SetNameField(nameGen.GenerateName()); //при запуске один раз генерирует название
            //if (debuggingThreats) {
                //threatGen.BuildEncounter(5, 5, 3, useHumansInBattle, useHazards);
            //}
        }
    }

    void InitGenerators()
    {
        nameGen = new NameGenerator();

        threatGen = new ThreatGenerator();
    }

    bool CheckDB()
    {
        if (!File.Exists(Application.dataPath + dbName)) 
        {
            Debug.Log("Dungeon.db Not Found in " + Application.dataPath + ", Everything Will Break");
            return false;
        }
        else return true;
    }

    private void SetNameField(string newName)
    {
        dungeonNameField.text = newName;
    }
}
}