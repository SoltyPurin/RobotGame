using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using TMPro;

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

    private readonly string BURST_TYPE = "BurstType";


    private void Start()
    {
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
