using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGrid : MonoBehaviour
{
    public Tilemap tilemap;            // Ссылка на Tilemap
    public TileBase tilePrefab;        // Ссылка на Tile (созданный ранее)
    [SerializeField] private RectTransform bounds;
    void OnEnable()
    {
        DungeonGenerator.OnRegeneration += RegenerateGrid;
    }

    void OnDisable()
    {
        DungeonGenerator.OnRegeneration -= RegenerateGrid;
    }
    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        int gridWidth = (int)bounds.rect.width;
        int gridHeight = (int)bounds.rect.height;
        Vector2Int gridCenter = new Vector2Int((int)bounds.rect.position.x, (int)bounds.rect.position.y);
        Debug.Log($"W:{gridWidth}; H:{gridHeight}; C:{gridCenter}");
        // Генерация тайлов
        for (int x = -gridWidth; x < gridWidth; x++)
        {
            for (int y = -gridHeight; y < gridHeight; y++)
            {
                Vector3Int tilePosition = new Vector3Int(gridCenter.x + x, gridCenter.y + y, 0); // Позиция клетки
                tilemap.SetTile(tilePosition, tilePrefab);         // Установка Tile на позицию
            }
        }
    }

    public void RegenerateGrid(DungeonGenerator dungeonGenerator)
    {
        tilemap.ClearAllTiles();
        GenerateGrid();
    }
}
