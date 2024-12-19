using System;
using UnityEngine;

public class Adjuster : MonoBehaviour //Штука для переноса камеры в центр данжа и зума камеры таким образом, чтоб весь данж был виден
{
    [SerializeField] private Camera Camera;
    float CalculateCameraSize(DungeonGenerator dungeonGenerator) => (dungeonGenerator.dungeonSize.x + dungeonGenerator.dungeonSize.y) / 2 - Math.Min(dungeonGenerator.dungeonSize.x, dungeonGenerator.dungeonSize.y) / 2;
    void OnEnable()
    {
        DungeonGenerator.OnGeneration += AdjustPosition;
    }

    void OnDisable()
    {
        DungeonGenerator.OnGeneration -= AdjustPosition;
    }

    public void AdjustPosition(DungeonGenerator dungeonGenerator)
    {
        Camera.transform.position = dungeonGenerator.dungeonSize / 2;
        Camera.orthographicSize = CalculateCameraSize(dungeonGenerator);
        Camera.transform.Translate(Vector3.back);
    }
}
