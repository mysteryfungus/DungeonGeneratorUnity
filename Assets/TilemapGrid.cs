using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGrid : MonoBehaviour
{
    public Tilemap tilemap; // Ссылка на Tilemap
    public TileBase tilePrefab; // Ссылка на Tile (созданный ранее)
    [SerializeField] private RectTransform bounds;
    void OnEnable()
    {
        DungeonGenerator.OnGeneration += GenerateGrid;
    }

    void OnDisable()
    {
        DungeonGenerator.OnGeneration -= GenerateGrid;
    }

    public void GenerateGrid(DungeonGenerator dungeonGenerator)
    {
        bounds.transform.position = dungeonGenerator.dungeonSize / 2;
        bounds.rect.size.Set(dungeonGenerator.dungeonSize.x, dungeonGenerator.dungeonSize.y);
        tilemap.ClearAllTiles();
        tilemap.transform.position = bounds.transform.position;
        GenerateGrid();
    }

    void GenerateGrid()
    {
        Rect area = bounds.rect;
        int tilesX = Mathf.CeilToInt(area.width / tilemap.cellSize.x);
        int tilesY = Mathf.CeilToInt(area.height / tilemap.cellSize.y);

        Vector3 rectCenter = bounds.position;
        Vector3Int startTile = tilemap.WorldToCell(rectCenter - new Vector3(area.width / 2, area.height / 2, 0));

        for (int x = 0; x < tilesX; x++)
        {
            for (int y = 0; y < tilesY; y++)
            {
                Vector3Int tilePosition = startTile + new Vector3Int(x, y, 0);
                tilemap.SetTile(tilePosition, tilePrefab);
            }
        }
    }
}
