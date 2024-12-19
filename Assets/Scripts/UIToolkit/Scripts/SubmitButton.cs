using UnityEngine;
using UnityEngine.UIElements;

public class SubmitButton : AbstractButton
{
    protected enum SubmitButtonState
    {
        HARD,
        EASY
    }

    protected CustomInputField _countR;
    protected CustomInputField _maxR;
    protected CustomInputField _minR;

    protected DropdownField _sizeOfRoom;
    protected DropdownField _countOfRoom;

    protected CustomInputField _lvlH;
    protected CustomInputField _countH;
    protected Toggle _humanEnemyToggle;
    protected Toggle _trapToggle;

    protected SubmitButtonState _state;

    public SubmitButton(CustomInputField countR, CustomInputField maxR, CustomInputField minR, CustomInputField lvlH, CustomInputField countH, Toggle _trap, Toggle _human)
    {
        _countR = countR;
        _maxR = maxR;
        _minR = minR;
        _lvlH = lvlH;
        _countH = countH;
        _trapToggle = _trap;
        _humanEnemyToggle = _human;

        _state = SubmitButtonState.HARD;
    }

    public SubmitButton(DropdownField size, DropdownField count, CustomInputField lvl, CustomInputField countH, Toggle _trap, Toggle _human)
    {
        _sizeOfRoom = size;
        _countH = countH;
        _trapToggle = _trap;
        _humanEnemyToggle = _human;
        _lvlH = lvl;
        _countOfRoom = count;

        _state = SubmitButtonState.EASY;
    }

    public override void OnClick()
    {
        bool flag = true;

        switch (_state)
        {
            case SubmitButtonState.HARD:
                flag = true;

                _countR.Validate(ref flag, out int countOfRoom);
                _countH.Validate(ref flag, out int countOfHeros);
                _maxR.Validate(ref flag, out int maxOfRooms);
                _minR.Validate(ref flag, out int minOfRoom);
                _lvlH.Validate(ref flag, out int levelOfHeros);

                if (flag)
                {
                    //Debug.Log("Данные успешно переданы");
                }
                else
                {
                    //Debug.Log("В данных обнаружена ошибка");
                }

                break;
            case SubmitButtonState.EASY:
                flag = true;

                _countH.Validate(ref flag, out int countOfHerosSMP);
                _lvlH.Validate(ref flag, out int levelOfHerosSMP);

                break;
        }
    }
}
