using UnityEngine;

public class GridBrush : MonoBehaviour
{
    public Camera mainCamera;       // Ссылка на камеру
    public float cellSize = 1f;     // Размер ячейки сетки (соответствует размеру спрайта в мире)

    public GameObject brushPrefab;  // Префаб кисти или отметки на сетке
    private GameObject currentBrush;
    private Vector3 lastBrushPosition; // Последняя позиция кисти

    void Start()
    {
        // Создаем кисть, если она ещё не существует
        if (brushPrefab != null)
        {
            currentBrush = Instantiate(brushPrefab);
            currentBrush.SetActive(false);
        }
    }

void Update()
    {
        // Проверяем, зажата ли кнопка мыши
        if (Input.GetMouseButton(0))
        {
            // Определяем координаты мыши в мировом пространстве
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;

            // Округляем позицию до ближайшего центра ячейки
            float gridX = Mathf.Floor(mouseWorldPosition.x / cellSize) * cellSize + cellSize / 2;
            float gridY = Mathf.Floor(mouseWorldPosition.y / cellSize) * cellSize + cellSize / 2;
            Vector3 gridPosition = new Vector3(gridX, gridY, 0);

            // Проверяем, была ли кисть уже в этой ячейке
            if (gridPosition != lastBrushPosition)
            {
                PlaceBrush(gridPosition);
                lastBrushPosition = gridPosition; // Обновляем позицию кисти
            }

            // Для визуализации можно включить курсор-кисть
            if (currentBrush != null)
            {
                currentBrush.SetActive(true);
                currentBrush.transform.position = gridPosition;
            }
        }
    }

    void PlaceBrush(Vector3 position)
    {
        // Создаём новый экземпляр кисти в позиции сетки
        Instantiate(brushPrefab, position, Quaternion.identity);
    }
}
