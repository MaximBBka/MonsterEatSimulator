using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class Localization : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextOnButtonSkin;
    [SerializeField] private TextMeshProUGUI TextOnButtonDailyReward;
    [SerializeField] private TextMeshProUGUI TextOnButtonSpeen;
    [SerializeField] private TextMeshProUGUI TextOnButtonGifts;
    [SerializeField] private TextMeshProUGUI textAdsSpeed;
    [SerializeField] private TextMeshProUGUI textAdsSize;
    [SerializeField] private TextMeshProUGUI textTitleWindowAds;
    [SerializeField] private TextMeshProUGUI textButtonWindowAds;
    [SerializeField] private TextMeshProUGUI[] textDay;
    [SerializeField] private TextMeshProUGUI textTitleDailyReward;
    [SerializeField] private TextMeshProUGUI textTurnSpeen;
    [SerializeField] private TextMeshProUGUI textCloseSpeen;
    [SerializeField] private TextMeshProUGUI[] textAdsSkins;
    [SerializeField] private TextMeshProUGUI[] textChooseSkin;
    [SerializeField] private TextMeshProUGUI textTitleWindowSkin;
    [SerializeField] private TextMeshProUGUI textTitleDeath;
    [SerializeField] private TextMeshProUGUI textAdsReborn;
    [SerializeField] private TextMeshProUGUI textRestart;
    [SerializeField] private TextMeshProUGUI textTitleGifts;

    private void Start()
    {
        if (YandexGame.EnvironmentData.language == "ru")
        {
            TextOnButtonSkin.text = $"скины";
            TextOnButtonDailyReward.text = $"бонус";
            TextOnButtonSpeen.text = $"лаки спин";
            TextOnButtonGifts.text = $"дары";
            textAdsSpeed.text = $"смотри рекламу и получи 2x скорость";
            textAdsSize.text = $"смотри рекламу и получи 2x размер";
            textTitleWindowAds.text = $"смотрите рекламу и зарабатывайте 20 черепов";
            textButtonWindowAds.text = $"cмотреть рекламму";
            for(int i = 0; i < textDay.Length; i++)
            {
                textDay[i].text = $"{i + 1} день";
            }
            textTitleDailyReward.text = $"ежедневные награды";
            textTurnSpeen.text = $"ВРАЩАТЬ";
            textCloseSpeen.text = $"ЗАКРЫТЬ";
            for (int i = 0; i < textAdsSkins.Length; i++)
            {
                textAdsSkins[i].text = $"скин за рекламму";
                textChooseSkin[i].text = $"выбрать";
            }
            textTitleWindowSkin.text = $"скины";
            textTitleDeath.text = $"ты умер!";
            textAdsReborn.text = $"смотреть рекламму и возродиться";
            textRestart.text = $"перезапуск";
            textTitleGifts.text = $"дары";
        }
        else if (YandexGame.EnvironmentData.language == "en")
        {
            TextOnButtonSkin.text = $"skins";
            TextOnButtonDailyReward.text = $"bonus";
            TextOnButtonSpeen.text = $"lucky spin";
            TextOnButtonGifts.text = $"gifts";
            textAdsSpeed.text = $"watch the ads and get 2x the speed";
            textAdsSize.text = $"watch the ads and get 2x size";
            textTitleWindowAds.text = $"watch ads and earn 20 skulls";
            textButtonWindowAds.text = $"watch ads";
            for (int i = 0; i < textDay.Length; i++)
            {
                textDay[i].text = $"{textDay[i].text} day";
            }
            textTitleDailyReward.text = $"daily rewards";
            textTurnSpeen.text = $"ROTATE";
            textCloseSpeen.text = $"CLOSE";
            for (int i = 0; i < textAdsSkins.Length; i++)
            {
                textAdsSkins[i].text = $"skin for advertising";
                textChooseSkin[i].text = $"choose";
            }
            textTitleWindowSkin.text = $"skins";
            textTitleDeath.text = $"you're dead!";
            textAdsReborn.text = $"watch ads and be reborn";
            textRestart.text = $"restart";
            textTitleGifts.text = $"gifts";
        }
        else if (YandexGame.EnvironmentData.language == "tr")
        {
            TextOnButtonSkin.text = $"elbise";
            TextOnButtonDailyReward.text = $"bonus";
            TextOnButtonSpeen.text = $"şanslı dönüş";
            TextOnButtonGifts.text = $"hediyeler";
            textAdsSpeed.text = $"reklamları izleyin ve 2x kat hız kazanın";
            textAdsSize.text = $"reklamı izleyin ve 2x beden alın";
            textTitleWindowAds.text = $"reklamları izleyin ve 20 kafatası kazanın";
            textButtonWindowAds.text = $"reklamları izleyin";
            for (int i = 0; i < textDay.Length; i++)
            {
                textDay[i].text = $"{i + 1} gün";
            }
            textTitleDailyReward.text = $"günlük ödüller";
            textTurnSpeen.text = $"DÖNDÜRMEK";
            textCloseSpeen.text = $"KAPAT";
            for (int i = 0; i < textAdsSkins.Length; i++)
            {
                textAdsSkins[i].text = $"reklam için kaplama";
                textChooseSkin[i].text = $"seçmek";
            }
            textTitleWindowSkin.text = $"elbise";
            textTitleDeath.text = $"sen öldün!";
            textAdsReborn.text = $"reklamları izleyin ve yeniden doğun";
            textRestart.text = $"yeniden başlatma";
            textTitleGifts.text = $"hediyeler";
        }
    }
}
