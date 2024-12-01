﻿using UnityEngine;

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
    protected CustomInputField _lvlH;
    protected CustomInputField _countH;

    protected SubmitButtonState _state;

    public SubmitButton(CustomInputField countR, CustomInputField maxR, CustomInputField minR, CustomInputField lvlH, CustomInputField countH)
    {
        _countR = countR;
        _maxR = maxR;
        _minR = minR;
        _lvlH = lvlH;
        _countH = countH;

        _state = SubmitButtonState.HARD;
    }

    public SubmitButton()
    {
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
                    Debug.Log("Данные успешно переданы");
                }
                else
                {
                    Debug.Log("В данных обнаружена ошибка");
                }

                break;
        }
    }
}
