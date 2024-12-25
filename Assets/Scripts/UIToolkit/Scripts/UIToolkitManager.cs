using UnityEngine;
using UnityEngine.UIElements;
using Zenject;


public class UIToolkitManager : MonoBehaviour
{

    [SerializeField] private Export exporter;
    [SerializeField] private TilemapEditor editor;
    [SerializeField] private DungeonGenerator generator;
    [SerializeField] private Color toggledButtonColor;
    private StyleColor buttonDefaultColor;

    [Inject] private UIDocument _document;
    [Inject] private InputFieldsVerificator _inputFieldsVerificator;
    private TabController _tabController;

    // Complex Elements
    private CustomInputField _countRooms;
    private CustomInputField _maxSizeRooms;
    private CustomInputField _minSizeRooms;
    private CustomInputField _levelHeros;
    private CustomInputField _countHeros;
    private Toggle _trap;
    private Toggle _human;

    //Simple Elements
    private DropdownField _sizeOfRoom;
    private DropdownField _countOfRooms;
    private Toggle _trapSMP;
    private Toggle _humanSMP;
    private CustomInputField _levelHerosSMP;
    private CustomInputField _countHerosSMP;

    //General
    private Tab _tabSimple;
    private Tab _tabComplex;

    private Button _submit;
    private Button _export;
    private Button _import;
    private Button _change;

    public AbstractButton Submit;

    private void Start()
    {
        InstallUIElementsForComplex();
        InstallUIElementsForSimple();

        _tabComplex = new Tab(FindElement("hard-settings") as ScrollView, FindElement("Complex") as Button);
        _tabSimple = new Tab(FindElement("easy-settings") as ScrollView, FindElement("Simple") as Button);

        InstallTabController();

        buttonDefaultColor = _change.style.color;
    }

    private void InstallUIElementsForComplex()
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
        _export.RegisterCallback<ClickEvent>(ExportButtonClick);
        _import = FindElement("import") as Button;
        _change = FindElement("change") as Button;
        _change.RegisterCallback<ClickEvent>(EditButtonClick);

        _trap = FindElement("trap-toggle").Q<Toggle>();
        _human = FindElement("hero-enemy-toggle").Q<Toggle>();

        _tabComplex = new Tab(FindElement("hard-settings") as ScrollView, FindElement("Complex") as  Button);
        _tabSimple = new Tab(FindElement("easy-settings") as ScrollView, FindElement("Simple") as Button);
    }

    private void InstallUIElementsForSimple()
    {
        _levelHerosSMP = FindElement("level-heros-simple").Q<CustomInputField>();
        _levelHerosSMP.OnInjectValidator(_inputFieldsVerificator.ValidateLevelOfHeros);

        _countHerosSMP = FindElement("count-heros-simple").Q<CustomInputField>();
        _countHerosSMP.OnInjectValidator(_inputFieldsVerificator.ValidateCountOfHeros);

        _trapSMP = FindElement("trap-toggle-simple").Q<Toggle>();
        _humanSMP = FindElement("hero-enemy-toggle-simple").Q<Toggle>();

        _sizeOfRoom = FindElement("simple-room-size").Q<DropdownField>();
        _countOfRooms = FindElement("simple-room-count").Q<DropdownField>();
    }

    private void InstallTabController()
    {
        _tabController = new TabController();
        _tabSimple.Button.clicked += () =>
        {
            if (Submit != null) Submit.Unlink(_submit);
            Submit = new SubmitButton(_sizeOfRoom, _countOfRooms, _levelHerosSMP, _countHerosSMP, _trapSMP, _humanSMP);
            Submit.Link(_submit);
        };

        _tabComplex.Button.clicked += () =>
        {
            if (Submit != null) Submit.Unlink(_submit);
            Submit = new SubmitButton(_countRooms, _maxSizeRooms, _minSizeRooms, _levelHeros, _countHeros, _trap, _human);
            Submit.Link(_submit);
        };

        _tabController.AddTab(_tabSimple);
        _tabController.AddTab(_tabComplex);

        Submit = new SubmitButton(_sizeOfRoom, _countOfRooms, _levelHerosSMP, _countHerosSMP, _trapSMP, _humanSMP);
        Submit.Link(_submit);
    }

    private VisualElement FindElement(string name)
    {
        return _document.rootVisualElement.Q(name);
    }

    private void ExportButtonClick(ClickEvent click)
    {
        exporter.OnClick();
    }

    private void EditButtonClick(ClickEvent click)
    {
        editor.isEditingMode = !editor.isEditingMode;
        if(editor.isEditingMode){
            _change.style.color = (StyleColor)toggledButtonColor;
        } else {
            _change.style.color = buttonDefaultColor;
        }
        
    }
}
