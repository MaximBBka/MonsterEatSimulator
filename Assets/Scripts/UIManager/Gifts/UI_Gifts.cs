using UnityEngine.UI;
using UnityEngine;
using TMPro;
using YG;

public class UI_Gifts : View
{
    [field: SerializeField] public Button Background { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Title { get; private set; }
    [field: SerializeField] public Image Icon { get; private set; }
    [field: SerializeField] public Button Button { get; private set; }
    [field: SerializeField] public TextMeshProUGUI ButtonText { get; private set; }

    public delegate void OnGiftsDelegate(int index);
    public event OnGiftsDelegate OnGiftsSelected;

    private int _index;

    public void Init(GiftData giftData, int index)
    {
        _index = index;
        Title.SetText(giftData.Description);
        Icon.sprite = giftData.Icon;
    }

    public void Applay()
    {
        if (YandexGame.EnvironmentData.language == "ru")
        {
            ButtonText.SetText("Забрано!");
        }
        else if (YandexGame.EnvironmentData.language == "en")
        {
            ButtonText.SetText("Taken!");
        }
        else if (YandexGame.EnvironmentData.language == "tr")
        {
            ButtonText.SetText("Alındı! alındı!");
        }      
        Background.interactable = false;
        MainUI.Instance.ButtonPlay();
        OnGiftsSelected?.Invoke(_index);
    }
}