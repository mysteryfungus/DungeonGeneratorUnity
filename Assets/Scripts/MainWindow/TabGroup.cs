using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//Занимается управлением всеми кнопками вкладок
public class TabGroup : InitClass
{
    [SerializeField] private List<TabButton> _tabGroup = new();
    
    //Набор цветов для разных состояний кнопок вкладок
    [SerializeField] private Color _selectedColor;
	[SerializeField] private Color _enteredColor;
	[SerializeField] private Color _exitedColor;

	//Событие для сброса кнопок вкладок
	private Action<Color> ResetButtons;

    [SerializeField] private TabButton _activeButton;

    public override void Init()
    {
        foreach (var tab in _tabGroup) 
        {

            tab.Click += OnClickButton;
            tab.Exit += OnExitButton;
            tab.Enter += OnEnterButton;
            tab.ChangeActive += OnChangeActive;
            ResetButtons += tab.OnReset;
        }

        Debug.Log($"{this.name} инициализирован");
    }

    private Color OnEnterButton()
    {
        ResetButtons?.Invoke(_exitedColor);

        return _enteredColor;
    }

    private Color OnExitButton() 
    {
		ResetButtons?.Invoke(_exitedColor);

        return _exitedColor;
	}

    private Color OnClickButton()
    {
		ResetButtons?.Invoke(_exitedColor);

        return _selectedColor;
	}

    private void OnChangeActive(TabButton newActiveButton)
    {
        _activeButton.isActive = false;
        _activeButton.DisablePanel();

        _activeButton = newActiveButton;
    }
}
