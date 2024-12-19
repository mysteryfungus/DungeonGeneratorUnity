using UnityEditor;
using UnityEngine;
using GenerationClasses;
using System.IO;
using System.Collections.Generic;
using System;

namespace DbClasses
{
class DBManager : MonoBehaviour
{
    //static string dbName = "/Dungeon.db";
    static string dbName = "/DungeonGenerator.db";
    public static string dbLink = "URI=file:" + Application.dataPath + dbName;
    private NameGenerator nameGen;
    private ThreatGenerator threatGen;
    public bool useHumansInBattle = true;
    public bool useHazards = true;

    void Awake()
    {
        if (CheckDB()) {
            InitGenerators();
        }
    }

    void InitGenerators()
    {
        nameGen = new NameGenerator();

        threatGen = new ThreatGenerator();
    }

    static bool CheckDB()
    {
        if (!File.Exists(Application.dataPath + dbName)) 
        {
            Debug.Log("Dungeon.db Not Found in " + Application.dataPath + ", Everything Will Break");
            return false;
        }
        else return true;
    }

    public List<Tuple<int, List<Monster>, List<Hazard>>> GenerateRoomsContent(int partyMemberAmount, int partyLevel, int roomAmount, bool useHumansInBattle, bool useHazards) {
        return threatGen.BuildEncounter(partyMemberAmount, partyLevel, roomAmount,  useHumansInBattle, useHazards);
    }

    public List<Tuple<object, List<Monster>, List<Hazard>>> GenerateRoomsContent(int partyMemberAmount, int partyLevel, List<object> roomCoordinates, bool useHumansInBattle, bool useHazards) {
        return threatGen.BuildEncounter(partyMemberAmount, partyLevel, roomCoordinates,  useHumansInBattle, useHazards);
    }

    public void SaveRoomContentsToFile(List<Tuple<object, List<Monster>, List<Hazard>>> rooms){
        var path = EditorUtility.SaveFilePanel(
            "Сохранить содержимое комнат в текстовом файле",
            "",
            "Room contents.txt",
            "txt");
        if (path != null) {
            Debug.Log("Path exists: " + path);
            StreamWriter writer = new StreamWriter(path, false);
            string dataToWrite = "";
            for (int i = 0; i < rooms.Count; i++){
                dataToWrite+= "------------------------Комната №" + (i+1) +"------------------------";
                if (rooms[i].Item2.Count == 0 && rooms[i].Item3.Count == 0) {
                    dataToWrite+= " - пусто";
                } else {
                    if (rooms[i].Item2.Count > 0) {
                        dataToWrite+="\n------------------------Монстры:------------------------";
                        for (int j = 0; j < rooms[i].Item2.Count; j++){
                            dataToWrite+="\n" + rooms[i].Item2[j].ToTextFileString();
                            if (j < rooms[i].Item2.Count-1) dataToWrite+="---------------------------------------------------------";
                        }
                    }
                    if (rooms[i].Item3.Count > 0) {
                        dataToWrite+="\n------------------------Ловушки:------------------------";
                        for (int j = 0; j < rooms[i].Item3.Count; j++){
                            dataToWrite+="\n" + rooms[i].Item3[j].ToTextFileString();
                            if (j < rooms[i].Item3.Count-1) dataToWrite+="\n---------------------------------------------------------";
                        }
                    }
                }
                dataToWrite+= "\n";
            }
            Debug.Log(dataToWrite);
            writer.Write(dataToWrite);
            writer.Dispose();
        } else {
            Debug.Log("Path did not exist: " + path);
        }
        
    }

    String generateDungeonName(){
        return nameGen.GenerateName();
    }
}
}