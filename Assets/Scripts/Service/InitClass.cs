using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Класс, который ползволит в виде списка принимать классы для инициализации
/// в Bootstrap
/// Пишется так, а не с использованием интерфейсов из-за неспособности юнити отображать интерфейсы в инспекторе
/// </summary>

public abstract class InitClass : MonoBehaviour
{
    public abstract void Init();
}
