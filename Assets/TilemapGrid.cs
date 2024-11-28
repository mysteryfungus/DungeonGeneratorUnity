using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGrid : MonoBehaviour
{
    public Tilemap tilemap;            // Ссылка на Tilemap
    public TileBase tilePrefab;        // Ссылка на Tile (созданный ранее)
    public int gridWidth = 20;         // Ширина сетки
    public int gridHeight = 20;        // Высота сетки

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // Генерация тайлов
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0); // Позиция клетки
                tilemap.SetTile(tilePosition, tilePrefab);         // Установка Tile на позицию
            }
        }
    }
}
