using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] public int roomCount = 10;
    [SerializeField] public  Vector2 minRoomSize = new Vector2(6, 6);
    [SerializeField] public Vector2 maxRoomSize = new Vector2(21, 21);
    [SerializeField] public Vector2 dungeonSize = new Vector2(100, 100);
    [SerializeField] public float minRoomSeparation = 3f;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private RuleTile dungeonTile; // Тайл для комнат и коридоров
    [SerializeField] private bool CorrectCamera;
    [SerializeField, ButtonInvoke(nameof(RegenerateDungeon))] private bool regenerateDungeon;

    private List<Room> rooms = new List<Room>();
    private HashSet<(Room, Room)> connectedRooms = new HashSet<(Room, Room)>();
    public delegate void Generation(DungeonGenerator dungeonGenerator);
    public static event Generation OnGeneration;
    void Start()
    {
        RegenerateDungeon();
    }

    public void RegenerateDungeon()
    {
        ClearDungeon();
        GenerationRoutine();
    }

    void ClearDungeon()
    {
        rooms.Clear();
        connectedRooms.Clear();
        tilemap.ClearAllTiles();
    }

    void GenerationRoutine()
    {
        PlaceRooms();
        SeparateRooms();
        ConnectRooms();
        OnGeneration?.Invoke(this);
    }

    void PlaceRooms()
    {
        int attempts = 0;
        while (rooms.Count < roomCount && attempts < roomCount * 10)
        {
            Vector2 roomSize = new Vector2(
                (int)Random.Range(minRoomSize.x, maxRoomSize.x),
                (int)Random.Range(minRoomSize.y, maxRoomSize.y)
            );

            Vector2 roomPosition = new Vector2(
                (int)Random.Range(0, dungeonSize.x - roomSize.x),
                (int)Random.Range(0, dungeonSize.y - roomSize.y)
            );

            Room newRoom = new Room(roomPosition, roomSize);

            bool intersects = false;
            foreach (Room room in rooms)
            {
                Bounds roomBounds = new Bounds(room.Position + room.Size / 2, room.Size);
                Bounds newRoomBounds = new Bounds(newRoom.Position + newRoom.Size / 2, newRoom.Size);

                float _separation = Vector2.Distance(newRoom.GetCenter(), room.GetCenter());
                if (roomBounds.Intersects(newRoomBounds) || _separation < minRoomSeparation)
                {
                    intersects = true;
                    break;
                }
            }

            if (!intersects)
            {
                rooms.Add(newRoom);
                CreateRoom(newRoom);
            }

            attempts++;
        }
    }

    void SeparateRooms()
    {
        bool roomsMoved;
        do
        {
            roomsMoved = false;
            for (int i = 0; i < rooms.Count; i++)
            {
                for (int j = i + 1; j < rooms.Count; j++)
                {
                    float _separation = Vector2.Distance(rooms[i].GetCenter(), rooms[j].GetCenter());
                    if (rooms[i].Intersects(rooms[j]) || _separation < minRoomSeparation)
                    {
                        Vector2 direction = (rooms[i].Position - rooms[j].Position).normalized;
                        rooms[i].Position += direction * 0.5f;
                        rooms[j].Position -= direction * 0.5f;
                        roomsMoved = true;
                    }
                }
            }
        } while (roomsMoved);
    }

    void ConnectRooms()
    {
        rooms.Sort((a, b) => Vector2.Distance(Vector2.zero, a.GetCenter()).CompareTo(Vector2.Distance(Vector2.zero, b.GetCenter())));

        for (int i = 0; i < rooms.Count - 1; i++)
        {
            Room roomA = rooms[i];
            Room closestRoom = null;
            float minDistance = float.MaxValue;

            for (int j = i + 1; j < rooms.Count; j++)
            {
                Room roomB = rooms[j];
                float distance = Vector2.Distance(roomA.GetCenter(), roomB.GetCenter());
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestRoom = roomB;
                }
            }

            if (closestRoom != null && !connectedRooms.Contains((roomA, closestRoom)) && !connectedRooms.Contains((closestRoom, roomA)))
            {
                connectedRooms.Add((roomA, closestRoom));
                CreateCorridor(roomA, closestRoom);
            }
        }
    }

    void CreateRoom(Room room)
{
    Vector2Int bottomLeft = Vector2Int.FloorToInt(room.Position);
    Vector2Int topRight = bottomLeft + Vector2Int.FloorToInt(room.Size);

    for (int x = bottomLeft.x; x < topRight.x; x++)
    {
        for (int y = bottomLeft.y; y < topRight.y; y++)
        {
            tilemap.SetTile(new Vector3Int(x, y, 0), dungeonTile);
        }
    }
}


    void CreateCorridor(Room roomA, Room roomB)
    {
        Vector2 start = roomA.GetCenter();
        Vector2 end = roomB.GetCenter();

        Vector2 startPoint = start;
        Vector2 endPoint = end;

        if (Mathf.Abs(startPoint.x - endPoint.x) > Mathf.Abs(startPoint.y - endPoint.y))
        {
            CreateCorridorSegment(startPoint, new Vector2(endPoint.x, startPoint.y));
            CreateCorridorSegment(new Vector2(endPoint.x, startPoint.y), endPoint);
        }
        else
        {
            CreateCorridorSegment(startPoint, new Vector2(startPoint.x, endPoint.y));
            CreateCorridorSegment(new Vector2(startPoint.x, endPoint.y), endPoint);
        }
    }

    void CreateCorridorSegment(Vector2 start, Vector2 end)
    {
        Vector2Int startInt = Vector2Int.FloorToInt(start);
        Vector2Int endInt = Vector2Int.FloorToInt(end);

        if (startInt.x == endInt.x) // Вертикальный коридор
        {
            for (int y = Mathf.Min(startInt.y, endInt.y); y <= Mathf.Max(startInt.y, endInt.y); y++)
            {
                tilemap.SetTile(new Vector3Int(startInt.x, y, 0), dungeonTile);
            }
        }
        else // Горизонтальный коридор
        {
            for (int x = Mathf.Min(startInt.x, endInt.x); x <= Mathf.Max(startInt.x, endInt.x); x++)
            {
                tilemap.SetTile(new Vector3Int(x, startInt.y, 0), dungeonTile);
            }
        }
    }


    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
