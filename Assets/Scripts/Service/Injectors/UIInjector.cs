using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class UIInjector : MonoInstaller
{
    [SerializeField] private UIDocument _ui;
    [SerializeField] private InputFieldsVerificator _inputFieldsVerificator;

    private CustomInputField _countRooms;
    private CustomInputField _maxSizeRooms;
    private CustomInputField _minSizeRooms;
    private CustomInputField _levelHeros;
    private CustomInputField _countHeros;

    private Button _import;
    private Button _export;
    private Button _change;
    private Button _submit;


    public override void InstallBindings()
    {
        BindUIElements();

        Container.Bind<CustomInputField>().WithId("count-rooms").FromInstance(_countRooms);
        Container.Bind<CustomInputField>().WithId("max-size-rooms").FromInstance(_maxSizeRooms);
        Container.Bind<CustomInputField>().WithId("min-size-rooms").FromInstance(_minSizeRooms);
        Container.Bind<CustomInputField>().WithId("level-heros").FromInstance(_levelHeros);
        Container.Bind<CustomInputField>().WithId("count-heros").FromInstance(_countHeros);

        Container.Bind<Button>().WithId("import").FromInstance(_import);
        Container.Bind<Button>().WithId("export").FromInstance(_export);
        Container.Bind<Button>().WithId("change").FromInstance(_change);
        Container.Bind<Button>().WithId("submit").FromInstance(_submit);

        Container.Bind<InputFieldsVerificator>().FromInstance(_inputFieldsVerificator);
    }

    private void BindUIElements()
    {
        _countRooms = FindElements("count-rooms") as CustomInputField;
        _maxSizeRooms = FindElements("max-size-rooms") as CustomInputField;
        _minSizeRooms = FindElements("min-size-rooms") as CustomInputField;
        _levelHeros = FindElements("level-heros") as CustomInputField;
        _countHeros = FindElements("count-heros") as CustomInputField;

        _import = FindElements("import") as Button;
        _export = FindElements("export") as Button;
        _change = FindElements("change") as Button;
        _submit = FindElements("submit") as Button;
    }

    private VisualElement FindElements(string name)
    {
        var root = _ui.rootVisualElement;

        return root.Q(name);
    }
}