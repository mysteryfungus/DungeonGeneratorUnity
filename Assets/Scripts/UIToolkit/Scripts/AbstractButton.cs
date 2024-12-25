using UnityEngine;
using UnityEngine.UIElements;

public abstract class AbstractButton : MonoBehaviour, ILink
{
    [SerializeField] protected string nameOfElement;
    protected VisualElement root;

    public void Link(UIDocument uIDocument)
    {
        root = uIDocument.rootVisualElement;

        Button button = root.Q<Button>(nameOfElement);

        if (button != null)
        {
            Debug.Log($"{nameOfElement} был привязан");
            button.clicked += Action;
        }
    }

    public abstract void Action();
}
