using DbClasses;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;


public class GameObjectInjector : MonoInstaller
{
    [SerializeField] private DungeonGenerator _dungeonGenerator;
    [SerializeField] private Tilemap  _tilemap;
    [SerializeField] private DBManager _dbManager;

    public static Func<DungeonGenerator> OnGetDungeonGenerator;
    public static Func<DBManager> OnGetDBManager;

    public override void InstallBindings()
    {
        Container.Bind<DungeonGenerator>().FromInstance(_dungeonGenerator);
        Container.Bind<Tilemap>().FromInstance(_tilemap);

        OnGetDungeonGenerator += GetDungeonGenerator;
        OnGetDBManager += GetDBManager;
    }

    public DungeonGenerator GetDungeonGenerator()
    {
        return _dungeonGenerator;
    }

    public DBManager GetDBManager()
    {
        return _dbManager; 
    }
}
