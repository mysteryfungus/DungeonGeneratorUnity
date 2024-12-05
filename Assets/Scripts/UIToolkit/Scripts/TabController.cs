using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TabController
{
    private List<Tab> tabs = new List<Tab>();

    public void AddTab(Tab element)
    {
        tabs.Add(element);
        element.Button.clicked += () =>
        {
            ActiveTab(element);
        };
    }

    public void RemoveTab(Tab element) 
    { 
        tabs.Remove(element); 
    }

    public void ActiveTab(Tab element)
    {
        if (tabs.Exists((x) => x == element))
        {
            //Debug.Log("Элемент есть в списке вкладок");
            foreach (var v in tabs)
            {
                v.InactiveScrollView();
            }
            element.ActiveScrollView();
        }
    }
}
