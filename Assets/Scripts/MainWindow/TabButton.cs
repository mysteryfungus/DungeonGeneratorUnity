using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	[SerializeField] private GameObject _tabPanel;

	public GameObject TabPanel => _tabPanel;

	//Проверка на выбранную кнопку
	public bool isActive = false;

	//Группа событий для сообщения TabGroud о состоянии и реакции кнопки
	public Func<TabButton, Color> Exit;
	public Func<TabButton, Color> Enter;
	public Func<TabButton, Color> Click;
	
	//Событие смены активной кнопки
	public Action<TabButton> ChangeActive;

	private Image _background;

	private void Start()
	{
		_background = GetComponent<Image>();
	}

	//Реакция на выбор
	public void OnPointerClick(PointerEventData eventData)
	{
		//Смена активной кнопки
		ChangeActive.Invoke(this);
		isActive = true;
		_tabPanel.SetActive(true);

		this._background.color = Click.Invoke(this);
	}

	//Реакция на наведения курсора
	public void OnPointerEnter(PointerEventData eventData)
	{
		this._background.color = Enter.Invoke(this);
	}

	//Реакция на выход курсора за пределы кнопки
	public void OnPointerExit(PointerEventData eventData)
	{
		if (!isActive)
		{
			this._background.color = Exit.Invoke(this);
		}
	}


	//Передает по событию сброса спрайт нейтрального состояния кнопки вкладки
	public void OnReset(Color color)
	{
		if(!isActive)
		{
			this._background.color = color;
		}
	}

	public void DisablePanel()
	{
		_tabPanel.SetActive(false);
	}
}
