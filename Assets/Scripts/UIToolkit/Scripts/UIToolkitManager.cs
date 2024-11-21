﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class UIToolkitManager : MonoBehaviour
{
    [Inject] private UIDocument _document;
    [Inject] private InputFieldsVerificator _inputFieldsVerificator;

    private CustomInputField _countRooms;
    private CustomInputField _maxSizeRooms;
    private CustomInputField _minSizeRooms;
    private CustomInputField _levelHeros;
    private CustomInputField _countHeros;

    private Button _submit;
    private Button _export;
    private Button _import;
    private Button _change;

    public IButton Submit;


    private void Start()
    {
        InstallUIElements();

        Submit = new SubmitButton(_countRooms, _maxSizeRooms, _minSizeRooms, _levelHeros, _countHeros);
        Submit.Link(_submit);
    }

    private void InstallUIElements()
    {
        _countRooms = FindElement("count-rooms").Q<CustomInputField>();
        _countRooms.OnInjectValidator(_inputFieldsVerificator.ValidateCountOfRooms);

        _countHeros = FindElement("count-heros").Q<CustomInputField>();
        _countHeros.OnInjectValidator(_inputFieldsVerificator.ValidateCountOfHeros);

        _maxSizeRooms = FindElement("max-size-rooms").Q<CustomInputField>();
        _maxSizeRooms.OnInjectValidator(_inputFieldsVerificator.ValidateMaxSizeOfRooms);

        _minSizeRooms = FindElement("min-size-rooms").Q<CustomInputField>();
        _minSizeRooms.OnInjectValidator(_inputFieldsVerificator.ValidateMinSizeOfRooms);

        _levelHeros = FindElement("level-heros").Q<CustomInputField>();
        _levelHeros.OnInjectValidator(_inputFieldsVerificator.ValidateLevelOfHeros);

        _submit = FindElement("submit") as Button;
        _export = FindElement("export") as Button;
        _import = FindElement("import") as Button;
        _change = FindElement("change") as Button;
    }

    private VisualElement FindElement(string name)
    {
        return _document.rootVisualElement.Q(name);
    }
}
