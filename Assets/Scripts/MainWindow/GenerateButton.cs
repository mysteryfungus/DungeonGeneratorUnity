using UnityEngine;
using UnityEngine.UIElements;
using Zenject;


/// <summary>
/// Класс для запуска генерации. В OnClick запускается логика генерации, то есть там нужно работать с 
/// классами генерации карты, монстров и остального - все, что зависит от параметров
/// </summary>
public class GenerateButton : AButton
{
    [Inject(Id = "count-rooms")] private CustomInputField _countRooms;
    [Inject(Id = "count-heros")] private CustomInputField _countHeros;
    [Inject(Id = "max-size-rooms")] private CustomInputField _maxSizeRooms;
    [Inject(Id = "min-size-rooms")] private CustomInputField _minSizeRooms;
    [Inject(Id = "level-heros")] private CustomInputField _levelHeros;

    [Inject] private InputFieldsVerificator _verificator;

    [Inject(Id ="submit")]
    public override void Construct(Button button)
    {
        _button = button;

        _button.clicked += OnClick;

        Debug.Log($"{name} создан");
    }

    public override void OnClick()
    {
        Debug.Log("Generate!");
    }
}
