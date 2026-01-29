using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using TMPro;
using System.Collections.Generic;

public enum BurstName
{
    Attack,
    Speed,
    Guard,
}
public class BurstSetting : MonoBehaviour
{
    [SerializeField, Header("トグルボタンのグループ")]
    private ToggleGroup _group = default;

    [SerializeField, Header("戻るボタン")]
    private Button _returnButton = default;

    [SerializeField,Header("ボタン、上から攻撃スピード防御で")]
    private Toggle[] _toggles = new Toggle[3];
    private readonly string BURST_TYPE = "BurstType";


    private void Start()
    {
        int firstSelected = PlayerPrefs.GetInt(BURST_TYPE, 0);
        switch (firstSelected)
        {
            case 0:
                _toggles[0].isOn = true;    
                break;

            case 1:
                _toggles[1].isOn = true;
                break;

            case 2:
                _toggles[2].isOn = true;
                break;
        }
        _returnButton.onClick.AddListener(SaveBurst);
    }

    public void SaveBurst()
    {
        string selectedLabel = _group.ActiveToggles()
            .First().GetComponentsInChildren<TextMeshProUGUI>()
            .First(t => t.name == "Label").text;

        Debug.Log("selected " + selectedLabel);

        switch (selectedLabel)
        {
            case "攻撃型":
                PlayerPrefs.SetInt(BURST_TYPE, (int)BurstName.Attack);
                break;

            case "スピード型":
                PlayerPrefs.SetInt(BURST_TYPE, (int)BurstName.Speed);
                break;

            case "防御型":
                PlayerPrefs.SetInt(BURST_TYPE, (int)BurstName.Guard);
                break;
        }

    }
}
