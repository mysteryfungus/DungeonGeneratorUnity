using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

[RequireComponent(typeof(TabController))]
public class TabButtonUIToolkit : AbstractButton
{
    [SerializeField] protected string nameOfTab;
    protected VisualElement tab;
    protected TabController controller;

    protected void OnValidate()
    {
        if (controller == null) 
        { 
            controller = GetComponent<TabController>();
        }
    }

    public override void Action()
    {
        Debug.Log($"{nameOfElement} вызывает событие кнопки");
        if (tab == null)
        {
            tab = root.Q<ScrollView>(nameOfTab);
            controller.AddTab(tab);

            Debug.Log($"{nameOfTab} был добавлен в контроллер");
        }

        controller.ActiveTab(tab);
    }
}
