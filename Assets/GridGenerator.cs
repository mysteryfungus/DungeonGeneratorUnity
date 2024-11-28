using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject gridTilePrefab;  // Префаб спрайта
    public int gridWidth = 20;         // Ширина сетки
    public int gridHeight = 20;        // Высота сетки
    public float tileSpacing = 1.1f;   // Расстояние между спрайтами

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Создаем спрайт на позиции x, y
                Vector3 position = new Vector3(x * tileSpacing, y * tileSpacing, 0);
                GameObject tile = Instantiate(gridTilePrefab, position, Quaternion.identity, transform);
                tile.name = $"Tile_{x}_{y}";  // Назначаем имя для удобства
            }
        }
    }
}

