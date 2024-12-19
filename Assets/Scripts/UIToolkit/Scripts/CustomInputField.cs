using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomInputField : VisualElement
{
    protected TextField textField;
    protected DelegateValidator _validator;

    public CustomInputField()
    {
        textField = new TextField();
        Add(textField);
    }

    public void SetBorderSettings(float width, float radious)
    {
        VisualElement field = TextField.Q("unity-text-input");

        field.style.borderBottomWidth = width;
        field.style.borderLeftWidth = width;
        field.style.borderTopWidth = width;
        field.style.borderRightWidth = width;

        field.style.borderBottomLeftRadius = radious;
        field.style.borderBottomRightRadius = radious;
        field.style.borderTopLeftRadius = radious;
        field.style.borderTopRightRadius = radious;
    }
    
    public void SetMarginSettings(float margin)
    {
        VisualElement field = TextField.Q("unity-text-input");
        field.style.marginBottom = margin;
        field.style.marginLeft = margin;
        field.style.marginRight = margin;
        field.style.marginTop = margin;
    }

    public void SetPaddingSettings(float padding)
    {
        VisualElement field = TextField.Q("unity-text-input");
        field.style.paddingLeft = padding;
        field.style.paddingRight = padding;
        field.style.paddingBottom = padding;
        field.style.paddingTop = padding;
    }

    public void OnInjectValidator(DelegateValidator validator)
    {
        _validator = validator;
    }


    public void Validate(ref bool flag, out int newvalue)
    {
        bool answer = _validator.Invoke(Text, out newvalue);

        if (!answer)
        {
            Text = $"{newvalue}";
            flag = false;
        }
    }

    public string Text
    {
        get => textField.value;
        set => textField.value = value;
    }

    public TextField TextField => textField;
}

public class UxmlFactory : UxmlFactory<CustomInputField, UxmlTraits> { }

public class UxmlTraits : VisualElement.UxmlTraits
{
    UxmlStringAttributeDescription _description = new UxmlStringAttributeDescription { name = "text"};
    UxmlFloatAttributeDescription _border = new UxmlFloatAttributeDescription { name = "border" };
    UxmlFloatAttributeDescription _radious = new UxmlFloatAttributeDescription { name = "radious"};
    UxmlFloatAttributeDescription _margin = new UxmlFloatAttributeDescription { name = "margin"};
    UxmlFloatAttributeDescription _padding = new UxmlFloatAttributeDescription { name = "padding"};
    UxmlColorAttributeDescription _colorBackground = new UxmlColorAttributeDescription { name = "BackgroundColor"};
    UxmlColorAttributeDescription _colorBorder = new UxmlColorAttributeDescription { name = "BorderColor"};
    UxmlIntAttributeDescription _fontSize = new UxmlIntAttributeDescription { name = "fontsize" };


    public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
    {
        base.Init(ve, bag, cc);

        var customTextField = ve as CustomInputField;

        customTextField.Text = _description.GetValueFromBag(bag, cc);
        customTextField.TextField.style.height = customTextField.style.height;
        customTextField.TextField.style.width = customTextField.style.width;

        customTextField.TextField.style.flexBasis = 1;
        customTextField.TextField.style.flexGrow = 1;
        customTextField.TextField.style.flexShrink = 1;
        customTextField.TextField.style.alignSelf = Align.Stretch;
        customTextField.TextField.style.fontSize = _fontSize.GetValueFromBag(bag, cc);
        customTextField.TextField.style.overflow = Overflow.Visible;

        customTextField.SetBorderSettings(_border.GetValueFromBag(bag, cc), _radious.GetValueFromBag(bag, cc));
        customTextField.SetMarginSettings(_margin.GetValueFromBag(bag, cc));
        customTextField.SetPaddingSettings(_padding.GetValueFromBag(bag, cc));
    }


}
