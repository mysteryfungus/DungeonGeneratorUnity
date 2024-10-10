using UnityEngine.UIElements;
using UnityEngine;


public abstract class AButton : MonoBehaviour, IButton
{
    protected Button _button;

    public abstract void Construct(Button button);

    public abstract void OnClick();
}
