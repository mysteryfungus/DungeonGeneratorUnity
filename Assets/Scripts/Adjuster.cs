using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Adjuster : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private RectTransform BoundBox;
    [SerializeField] private float BoundBoxScaleFactor;
    float CalculateCameraSize(DungeonGenerator dungeonGenerator) => (dungeonGenerator.dungeonSize.x + dungeonGenerator.dungeonSize.y) / 2 - Math.Min(dungeonGenerator.dungeonSize.x, dungeonGenerator.dungeonSize.y) / 2;
    void OnEnable()
    {
        DungeonGenerator.OnRegeneration += AdjustPosition;
    }

    void OnDisable()
    {
        DungeonGenerator.OnRegeneration -= AdjustPosition;
    }

    public void AdjustPosition(DungeonGenerator dungeonGenerator)
    {
        Camera.transform.position = dungeonGenerator.dungeonSize / 2;
        Camera.orthographicSize = CalculateCameraSize(dungeonGenerator);
        Camera.transform.Translate(Vector3.back);
        BoundBox.transform.position = dungeonGenerator.dungeonSize / 2;
        BoundBox.rect.size.Set(dungeonGenerator.dungeonSize.x, dungeonGenerator.dungeonSize.y);
    }
}
