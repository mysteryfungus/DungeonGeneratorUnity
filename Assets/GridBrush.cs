using UnityEngine;
using System.Collections.Generic;

public class GridBrush : MonoBehaviour
{
    public Camera mainCamera;       // Ссылка на камеру
    public float cellSize = 1f;     // Размер ячейки сетки (соответствует размеру спрайта в мире)
    public Transform gridParent;
    public GameObject brushPrefab;  // Префаб кисти или отметки на сетке

    
    private GameObject currentBrush;
    private Dictionary<Vector3, GameObject> drawnCells; // Словарь для отслеживания нарисованных клеток

    void Start()
    {
        drawnCells = new Dictionary<Vector3, GameObject>(); // Инициализация словаря

        // Создаем кисть, если она ещё не существует
        if (brushPrefab != null)
        {
            currentBrush = Instantiate(brushPrefab);
            currentBrush.SetActive(false);
        }
    }

    void Update()
    {
        // Определяем координаты мыши в мировом пространстве
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        // Округляем позицию до ближайшего центра ячейки
        float gridX = Mathf.Floor(mouseWorldPosition.x / cellSize) * cellSize + cellSize / 2;
        float gridY = Mathf.Floor(mouseWorldPosition.y / cellSize) * cellSize + cellSize / 2;
        Vector3 gridPosition = new Vector3(gridX, gridY, 0);

        // Для визуализации можно включить курсор-кисть
        if (currentBrush != null)
        {
            currentBrush.SetActive(true);
            currentBrush.transform.position = gridPosition;
        }

        // Проверяем, зажата ли кнопка мыши
        if (Input.GetMouseButton(0))
        {
            PlaceBrush(gridPosition);
        }

        // Если ПКМ зажата — стираем
        if (Input.GetMouseButton(1))
        {
            UseEraser(gridPosition);
        }
    }

    void PlaceBrush(Vector3 position)
    {
        // Создаём временный экземпляр префаба кисти для проверки ячеек
        GameObject tempBrush = Instantiate(brushPrefab, position, Quaternion.identity);

        // Перебираем все дочерние объекты (клетки) в префабе кисти
        foreach (Transform cell in tempBrush.transform)
        {
            // Вычисляем позицию каждой клетки
            Vector3 cellPosition = new Vector3(
                Mathf.Floor(cell.position.x / cellSize) * cellSize + cellSize / 2,
                Mathf.Floor(cell.position.y / cellSize) * cellSize + cellSize / 2,
                0);

            // Проверяем, нет ли клетки в этой позиции
            if (!drawnCells.ContainsKey(cellPosition))
            {
                // Если клетки нет, создаём её и добавляем в словарь
                GameObject newCell = Instantiate(cell.gameObject, cellPosition, Quaternion.identity, gridParent);
                drawnCells.Add(cellPosition, newCell);
            }
        }

        // Удаляем временный экземпляр префаба
        Destroy(tempBrush);
    }

    void EraseBrushCell(Vector3 position)
    {
        // Находим и удаляем клетки кисти в указанной позиции
        if (drawnCells.TryGetValue(position, out GameObject existingCell))
        {
            Destroy(existingCell); // Удаляем клетку
            drawnCells.Remove(position); // Убираем её из словаря
        }
    }

    void UseEraser(Vector3 basePosition)
    {
        // Создаём временный экземпляр ластика для проверки ячеек
        GameObject tempEraser = Instantiate(brushPrefab, basePosition, Quaternion.identity);

        foreach (Transform cell in tempEraser.transform)
        {
            Vector3 cellPosition = GetCellPosition(cell.position);
            if (drawnCells.TryGetValue(cellPosition, out GameObject existingCell))
            {
                Destroy(existingCell);       // Удаляем клетку из сцены
                drawnCells.Remove(cellPosition); // Убираем её из словаря
            }
        }

        Destroy(tempEraser);
    }

    Vector3 GetCellPosition(Vector3 worldPosition)
    {
        // Округление позиции до ближайшей ячейки
        float gridX = Mathf.Floor(worldPosition.x / cellSize) * cellSize + cellSize / 2;
        float gridY = Mathf.Floor(worldPosition.y / cellSize) * cellSize + cellSize / 2;
        return new Vector3(gridX, gridY, 0);
    }
}
