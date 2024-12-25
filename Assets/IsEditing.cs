using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsEditing : MonoBehaviour
{
    [SerializeField] private TilemapEditor editor;
    [SerializeField] private Color targetColor;
    Image image;
    Color startingColor;

    void Start()
    {
        image = this.GetComponent<Image>();
        startingColor = image.color;
    }

    public void EditingModeButtonChanger()
    {
        if(editor.isEditingMode)
        {
            image.color = targetColor;
        } else {
            image.color = startingColor;
        }
    }
}
