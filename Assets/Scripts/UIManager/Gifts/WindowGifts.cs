using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;
using System.Reflection;
using YG;

[Serializable]
public struct GiftData
{
    public string Description;
    public Sprite Icon;
    public float Duration;
}

public class WindowGifts : View
{
    [SerializeField] private Canvas Canvas;
    [SerializeField] private Transform _content;
    [SerializeField] private UI_Gifts _ui_Gifts_Prefab;
    [SerializeField] private GiftData[] Gifts;

    public int NoTake = 0;

    public delegate void GiftsGelegate();
    public event GiftsGelegate OnUpdate;

    private List<UI_Gifts> _ui_GiftsSelected;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _ui_GiftsSelected ??= new(Gifts.Length);
        for (int i = 0; i < Gifts.Length; i++)
        {
            UI_Gifts temp = Instantiate(_ui_Gifts_Prefab, _content);
            temp.Init(Gifts[i], i);
            _ui_GiftsSelected.Add(temp);
            if (YandexGame.EnvironmentData.language == "ru")
            {
                temp.ButtonText.text = $"выбрать";
            }
            else if (YandexGame.EnvironmentData.language == "en")
            {
                temp.ButtonText.text = $"choose";
            }
            else if (YandexGame.EnvironmentData.language == "tr")
            {
                temp.ButtonText.text = $"seçmek";
            }
            StartCoroutine(Timer(Gifts[i].Duration, temp.Background, temp.ButtonText));
        }
    }

    public override void Show()
    {
        Canvas.enabled = true;

        for (int i = 0; i < _ui_GiftsSelected.Count; i++)
        {
            _ui_GiftsSelected[i].OnGiftsSelected += ApplayGifts;
            _ui_GiftsSelected[i].Show();
        }
    }

    public override void Hide()
    {
        Canvas.enabled = false;

        for (int i = _ui_GiftsSelected.Count - 1; i >= 0; i--)
        {
            _ui_GiftsSelected[i].Hide();
            _ui_GiftsSelected[i].OnGiftsSelected -= ApplayGifts;
        }
    }

    public void ApplayGifts(int index)
    {
        NoTake--;
        switch (index)
        {
            case 0:
                if (MainUI.Instance.player != null)
                {
                    MainUI.Instance.player.Eat(2000);
                }
                if (MainUI.Instance.skin != null)
                {
                    MainUI.Instance.skin.Eat(2000);
                }
                break;
            case 1:
                MainUI.Instance.UpdateSpin(3);
                break;
            case 2:
                if (MainUI.Instance.player != null)
                {
                    MainUI.Instance.player.Eat(5000);
                }
                if (MainUI.Instance.skin != null)
                {
                    MainUI.Instance.skin.Eat(5000);
                }
                break;
            case 3:
                if (MainUI.Instance.player != null)
                {
                    MainUI.Instance.player.Eat(8000);
                }
                if (MainUI.Instance.skin != null)
                {
                    MainUI.Instance.skin.Eat(8000);
                }
                break;
            case 4:
                MainUI.Instance.UpdateSpin(7);
                break;
            case 5:
                if (MainUI.Instance.player != null)
                {
                    MainUI.Instance.player.Eat(10000);
                }
                if (MainUI.Instance.skin != null)
                {
                    MainUI.Instance.skin.Eat(10000);
                }
                break;
            case 6:
                MainUI.Instance.MoneyKill += 30;
                break;
            case 7:
                if (MainUI.Instance.player != null)
                {
                    MainUI.Instance.player.Eat(15000);
                }
                if (MainUI.Instance.skin != null)
                {
                    MainUI.Instance.skin.Eat(15000);
                }
                break;
        }

        OnUpdate?.Invoke();
    }

    private IEnumerator Timer(float time, Button button, TextMeshProUGUI textButton)
    {
        while (time > 0)
        {
            time -= 1;
            textButton.SetText(Convert((int)time));
            yield return new WaitForSeconds(1f);
        }

        button.interactable = true;
        NoTake++;
        OnUpdate?.Invoke();
    }

    public string Convert(int time)
    {
        int hours = (int)(time / 3600);
        int minutes = (int)((time % 3600) / 60);
        int seconds = (int)(time % 60);

        // Форматирование строки
        return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }
}