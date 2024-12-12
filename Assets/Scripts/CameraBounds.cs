using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public Camera mainCamera;           // Ссылка на основную камеру
    public RectTransform canvasRect;    // Ссылка на RectTransform Canvas

    private Vector2 minBounds;          // Минимальные границы Canvas
    private Vector2 maxBounds;          // Максимальные границы Canvas

    void Start()
    {
        // Вычисляем границы Canvas в мировых координатах
        minBounds = canvasRect.TransformPoint(canvasRect.rect.min);
        maxBounds = canvasRect.TransformPoint(canvasRect.rect.max);
    }

    void LateUpdate()
    {
        ClampCameraPosition();
    }

    void ClampCameraPosition()
    {
        // Рассчитываем половину ширины и высоты видимой области камеры
        float camHalfHeight = mainCamera.orthographicSize;
        float camHalfWidth = mainCamera.aspect * camHalfHeight;

        // Ограничиваем позицию камеры с учётом её текущего масштаба
        float clampedX = Mathf.Clamp(mainCamera.transform.position.x, minBounds.x + camHalfWidth, maxBounds.x - camHalfWidth);
        float clampedY = Mathf.Clamp(mainCamera.transform.position.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight);

        mainCamera.transform.position = new Vector3(clampedX, clampedY, mainCamera.transform.position.z);
    }

    public void SetZoom(float newSize)
    {
        // Устанавливаем новый размер камеры и применяем ограничение
        mainCamera.orthographicSize = Mathf.Clamp(newSize, 2f, 30f); // Настройте минимальный и максимальный зум
        ClampCameraPosition();
    }
}
