using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGrid : MonoBehaviour
{
    public Tilemap tilemap; // Ссылка на Tilemap
    public RuleTile wallTile; // Ссылка на тайл стены
    [SerializeField] private RectTransform bounds; // Ссылка на RectTransform BoundBox
/*
    void OnEnable()
    {
        DungeonGenerator.OnGeneration += GenerateGrid;
    }

    void OnDisable()
    {
        DungeonGenerator.OnGeneration -= GenerateGrid;
    }
*/

    void GenerateGrid(DungeonGenerator dungeonGenerator)
    {
        Rect area = bounds.rect;
        Vector3 rectCenter = bounds.position;
        Vector3 rectSize = bounds.rect.size;

        // Вычисляем начальную и конечную позиции тайлов
        Vector3Int startTile = tilemap.WorldToCell(rectCenter - rectSize / 2);
        Vector3Int endTile = tilemap.WorldToCell(rectCenter + rectSize / 2);

        for (int x = startTile.x; x <= endTile.x; x++)
        {
            for (int y = startTile.y; y <= endTile.y; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                if (tilemap.GetTile(tilePosition) == null)
                {
                    tilemap.SetTile(tilePosition, wallTile);
                }
            }
        }
    }
}
