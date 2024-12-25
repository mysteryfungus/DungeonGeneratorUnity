using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TabController : MonoBehaviour
{
    private List<VisualElement> tabs = new List<VisualElement>();

    public void AddTab(VisualElement element)
    {
        tabs.Add(element);
    }

    public void RemoveTab(VisualElement element) 
    { 
        tabs.Remove(element); 
    }

    public void ActiveTab(VisualElement element)
    {
        if (tabs.Exists((x) => x == element))
        {
            Debug.Log("Элемент есть в списке вкладок");
            foreach (var v in tabs)
            {
                v.AddToClassList("settings-panel-disabled");
            }
            element.RemoveFromClassList("settings-panel-disabled");
        }
    }
}
