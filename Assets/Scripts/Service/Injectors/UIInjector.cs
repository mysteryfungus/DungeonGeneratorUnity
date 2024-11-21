using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class UIInjector : MonoInstaller
{
    [SerializeField] private UIDocument _uiDocument;
    [SerializeField] private InputFieldsVerificator _inputFieldsVerificator;
    


    public override void InstallBindings()
    {
        Container.Bind<UIDocument>().FromInstance(_uiDocument);
        Container.Bind<InputFieldsVerificator>().FromInstance(_inputFieldsVerificator);
    }
}