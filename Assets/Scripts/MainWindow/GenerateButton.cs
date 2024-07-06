using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Кнопка старта генерации. Будет ссылаться на поля. Отправлять данные полей в верификаторы. Запускать 
/// генерацию, если все нормально
/// </summary>

public class GenerateButton : MonoBehaviour, IButton
{
    [SerializeField] private Text _xMapSize;
    [SerializeField] private Text _yMapSize;
    [SerializeField] private Text _xRoomSize;
    [SerializeField] private Text _yRoomSize;

    /// и прочие параметры, которые будут добавляться...
    public void OnClick()
    {
        var mapVerificator = new MapSizeVerificator();
        var roomVerificator = new RoomSizeVerificator();

        if(mapVerificator.Check(_xMapSize.text) && mapVerificator.Check(_yMapSize.text))
        {
			if (roomVerificator.Check(_xRoomSize.text) && mapVerificator.Check(_yRoomSize.text))
            {
                EventBus.OnStartGeneration?.Invoke();
            }
		}
    }
}
