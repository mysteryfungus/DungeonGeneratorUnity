using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIToolkitManager : MonoBehaviour
{
    [SerializeField] private List<AbstractButton> list = new();
    [SerializeField] private UIDocument document;

    private void Start()
    {
        foreach (var link in list)
        {
            link.Link(document);
        }
    }
}
