using KG.Viewports;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelStarterPopup : Window, ISingleWindow
{
    [field: SerializeField] private ButtonBehaviour _startBtn;
    [field: SerializeField] private Slider _levelSlider;
    [SerializeField] private TMP_Text _levelSetting;


    private LevelHandler _levelHandler;

    [Inject]
    public void Construct(LevelHandler levelHandler)
    {
        _levelHandler = levelHandler;
    }


    public override void Init()
    {
        _startBtn.Init(OnClickStartBtn);

        _levelSlider.onValueChanged.AddListener(ValueChangeCheck);
    }

    private void OnClickStartBtn()
    {
        _levelHandler.StartLevel();
        Hide();
    }

    private void ValueChangeCheck(float value)
    {
        _levelSetting.text = $"Level: {_levelSlider.value}";
    }
}
