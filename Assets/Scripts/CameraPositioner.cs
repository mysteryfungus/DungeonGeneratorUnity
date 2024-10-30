using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraPositioner : MonoBehaviour
{
    private Camera Camera;
    void Awake()
    {
        Camera = this.GetComponent<Camera>();
    }

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
        this.transform.position = dungeonGenerator.dungeonSize / 2;
        this.transform.Translate(Vector3.back);
    }
}
