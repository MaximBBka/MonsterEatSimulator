using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class LoacalizationMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTextMenu;
    [SerializeField] private TextMeshProUGUI textOnButtonAdsMenu;



    private void Start()
    {
        if (YandexGame.EnvironmentData.language == "ru")
        {
            if (titleTextMenu != null && textOnButtonAdsMenu != null)
            {
                titleTextMenu.text = $"коснитесь экрана или нажмите 'пробел' что бы начать";
                textOnButtonAdsMenu.text = $"cмотрите рекламу и начните с 5 тысяч";
            }
        }
        else if (YandexGame.EnvironmentData.language == "en")
        {
            if (titleTextMenu != null && textOnButtonAdsMenu != null)
            {
                titleTextMenu.text = $"tap the screen or press 'space bar' to get started";
                textOnButtonAdsMenu.text = $"watch the ads and start with 5 thousand";
            }
        }
        else if (YandexGame.EnvironmentData.language == "tr")
        {
            if (titleTextMenu != null && textOnButtonAdsMenu != null)
            {
                titleTextMenu.text = $"ekrana dokunun veya 'boşluk çubuğu' başlamak için";
                textOnButtonAdsMenu.text = $"reklamları izleyin ve 5 bin ile başlayın";
            }
        }
    }
}
