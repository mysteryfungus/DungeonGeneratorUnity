using UnityEngine;
using UnityEngine.UIElements;

public abstract class AbstractButton : IButton
{
    public void Link(Button element)
    {
        element.clicked += OnClick;
    }

    public void Unlink(Button element)
    {
        element.clicked -= OnClick;
    }

    public abstract void OnClick();
}
