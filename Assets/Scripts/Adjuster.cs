using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adjuster : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    float CalculateCameraSize(DungeonGenerator dungeonGenerator) => (dungeonGenerator.dungeonSize.x + dungeonGenerator.dungeonSize.y) / 2 - Math.Min(dungeonGenerator.dungeonSize.x, dungeonGenerator.dungeonSize.y) / 2;

    public void AdjustPosition(DungeonGenerator dungeonGenerator)
    {
        Camera.transform.position = dungeonGenerator.dungeonSize / 2;
        Camera.orthographicSize = CalculateCameraSize(dungeonGenerator);
        Camera.transform.Translate(Vector3.back);
    }
}
