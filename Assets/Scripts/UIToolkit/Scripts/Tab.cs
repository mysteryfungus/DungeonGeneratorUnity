using UnityEngine.UIElements;

public class Tab
{
    private ScrollView _scrollView;
    private Button _button;

    public Button Button => _button;

    public Tab(ScrollView scrollView, Button button)
    {
        _scrollView = scrollView;
        _button = button;
    }

    public void InactiveScrollView()
    {
        _scrollView.AddToClassList("settings-panel-disabled");
    }

    public void ActiveScrollView()
    {
        _scrollView.RemoveFromClassList("settings-panel-disabled");
    }
}

