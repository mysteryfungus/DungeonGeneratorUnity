using UnityEngine;

public class GridDrawer : MonoBehaviour
{
    public float cellSize = 1f; // Размер ячейки
    public int gridWidth = 10; // Ширина сетки (в ячейках)
    public int gridHeight = 10; // Высота сетки (в ячейках)
    public Color gridColor = Color.white; // Цвет линий
    private Material gridMaterial;

    private void Start()
    {
        // Создаём простой материал для рисования сетки
        gridMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));
        gridMaterial.hideFlags = HideFlags.HideAndDontSave;
        gridMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        gridMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        gridMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        gridMaterial.SetInt("_ZWrite", 0);
    }

    private void OnPostRender()
    {
        if (!gridMaterial) return;

        gridMaterial.SetPass(0);

        GL.PushMatrix();
        GL.Begin(GL.LINES);
        GL.Color(gridColor);

        // Рисуем вертикальные линии
        for (int x = 0; x <= gridWidth; x++)
        {
            float xPos = x * cellSize;
            GL.Vertex3(xPos, 0, 0);
            GL.Vertex3(xPos, gridHeight * cellSize, 0);
        }

        // Рисуем горизонтальные линии
        for (int y = 0; y <= gridHeight; y++)
        {
            float yPos = y * cellSize;
            GL.Vertex3(0, yPos, 0);
            GL.Vertex3(gridWidth * cellSize, yPos, 0);
        }

        GL.End();
        GL.PopMatrix();
    }
}
