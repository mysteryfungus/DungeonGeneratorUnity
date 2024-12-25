using System;
using UnityEngine;
using UnityEngine.Tilemaps;

//#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
public class TilemapEditor : MonoBehaviour
{
    public Tilemap tilemap; // Ссылка на Tilemap
    public RuleTile floorTile; // Ссылка на тайл пола. Собсна то, что мы ставим
    public RuleTile wallTile; // Ссылка на тайл стены. Наличие не обязательно
    public bool isEditingMode = false; // Флаг режима редактирования

    private bool isDrawing = false; // Режим рисования (для добавления)
    private bool isErasing = false; // Режим стирания (для удаления)

    void Update()
    {
        if (!isEditingMode) return;

        // Начало рисования (левая кнопка мыши)
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            UpdateTileUnderCursor(floorTile); // Добавляем тайл под курсором
        }

        // Начало стирания (правая кнопка мыши)
        if (Input.GetMouseButtonDown(1))
        {
            isErasing = true;
            UpdateTileUnderCursor(wallTile); // Удаляем тайл под курсором
        }

        // Обработка движения мыши при рисовании/стирании
        if (Input.GetMouseButton(0) && isDrawing)
        {
            UpdateTileUnderCursor(floorTile); // Рисуем тайлы
        }

        if (Input.GetMouseButton(1) && isErasing)
        {
            UpdateTileUnderCursor(wallTile); // Стираем тайлы
        }

        // Окончание рисования или стирания
        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isErasing = false;
        }
    }

    public void ToggleEditingMode() // Включение/выключение режима редактирования
    {
        isEditingMode = !isEditingMode;
        Debug.Log($"Editing mode: {isEditingMode}");
    }

    private void UpdateTileUnderCursor(TileBase tile)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        tilemap.SetTile(cellPosition, tile);
    }
}
