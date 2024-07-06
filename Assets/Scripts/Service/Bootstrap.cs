using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс для Точки входа. В свой запуск начинает последовательный запуск систем
/// </summary>

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private List<InitClass> _initClasses = new List<InitClass>();

    void Start()
    {
        foreach (var initClass in _initClasses) 
        {
            Debug.Log($"{initClass.name} инициализован");
            initClass.Init();
        }
    }

}
