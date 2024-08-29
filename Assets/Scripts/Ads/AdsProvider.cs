using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class AdsProvider : MonoBehaviour
{
    public int reawrdId = -1; // 0 - реклама для старта с 5 к / 1 - рекламма за 10 черепов / 2 - реклма для скорости / 3 - реклама для x2 сайза / c 4 - 9 выбор скинов

    public int StartScoreAds = 0;

    public static AdsProvider Instance { get; private set; }
    private void Awake()
    {
        if (!Instance)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }
    public void Run()
    {
        Time.timeScale = 1;
        reawrdId = -1;
    }
    public void RewardedAbyliti(int index)
    {
        if (reawrdId == -1)
        {
            reawrdId = index;
            YandexGame.RewVideoShow(0);
        }
    }
    public void AddScoreMenu()
    {
        if (reawrdId == 0)
        {
            StartScoreAds = 5000;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            reawrdId = -1;
        }
    }
    public void AddMoney()
    {
        if (reawrdId == 1)
        {
            MainUI.Instance.MoneyKill += 20;
            Time.timeScale = 1f;
            reawrdId = -1;
        }
    }
    public void AddSpeed()
    {
        if (reawrdId == 2)
        {
            if (MainUI.Instance.player != null)
            {
                MainUI.Instance.player.TakeBuff(new BuffSpeed());
            }
            if (MainUI.Instance.skin != null)
            {
                MainUI.Instance.skin.TakeBuff(new BuffSpeed());
            }
            Time.timeScale = 1f;
            reawrdId = -1;
        }
    }
    public void AddSize()
    {
        if (reawrdId == 3)
        {
            if (MainUI.Instance.player != null)
            {
                MainUI.Instance.player.Eat((int)MainUI.Instance.player.player.CountScore);
            }
            if (MainUI.Instance.skin != null)
            {
                MainUI.Instance.skin.Eat((int)MainUI.Instance.skin.skin.CountScore);
            }
            Time.timeScale = 1f;
            reawrdId = -1;
        }
    }
    public void OpenSkin1()
    {
        if (reawrdId == 4)
        {
            MainUI.Instance.ButtonSkinsBuy[0].gameObject.SetActive(false);
            MainUI.Instance.ButtonSkinsSelect[0].gameObject.SetActive(true);
            Time.timeScale = 1f;
            reawrdId = -1;
        }       
    }
    public void OpenSkin2()
    {
        if (reawrdId == 5)
        {
            MainUI.Instance.ButtonSkinsBuy[1].gameObject.SetActive(false);
            MainUI.Instance.ButtonSkinsSelect[1].gameObject.SetActive(true);
            Time.timeScale = 1f;
            reawrdId = -1;
        }
    }
    public void OpenSkin3()
    {
        if (reawrdId == 6)
        {
            MainUI.Instance.ButtonSkinsBuy[2].gameObject.SetActive(false);
            MainUI.Instance.ButtonSkinsSelect[2].gameObject.SetActive(true);
            Time.timeScale = 1f;
            reawrdId = -1;
        }
    }
    public void OpenSkin4()
    {
        if (reawrdId == 7)
        {
            MainUI.Instance.ButtonSkinsBuy[3].gameObject.SetActive(false);
            MainUI.Instance.ButtonSkinsSelect[3].gameObject.SetActive(true);
            Time.timeScale = 1f;
            reawrdId = -1;
        }
    }
    public void OpenSkin5()
    {
        if (reawrdId == 8)
        {
            MainUI.Instance.ButtonSkinsBuy[4].gameObject.SetActive(false);
            MainUI.Instance.ButtonSkinsSelect[4].gameObject.SetActive(true);
            Time.timeScale = 1f;
            reawrdId = -1;
        }
    }
    public void OpenSkin6()
    {
        if (reawrdId == 9)
        {
            MainUI.Instance.ButtonSkinsBuy[5].gameObject.SetActive(false);
            MainUI.Instance.ButtonSkinsSelect[5].gameObject.SetActive(true);
            Time.timeScale = 1f;
            reawrdId = -1;
        }
    }
    public void Reborn()
    {
        if (reawrdId == 10)
        {
            if (MainUI.Instance.player != null)
            {
                MainUI.Instance.player.gameObject.SetActive(true);
                StartCoroutine(Shield(MainUI.Instance.player));
            }
            if (MainUI.Instance.skin != null)
            {
                MainUI.Instance.skin.gameObject.SetActive(true);
                StartCoroutine(Shield(null,MainUI.Instance.skin));
            }
            Time.timeScale = 1f;
            reawrdId = -1;
        }
    }
    public IEnumerator Shield(Player player = null, Skin skin = null)
    {
        if (player != null)
        {
            player.gameObject.layer = 0;
        }
        if(skin != null)
        {
            skin.gameObject.layer = 0;
        }
        yield return new WaitForSeconds(10);
        if (player != null)
        {
            player.gameObject.layer = 8;
        }
        if (skin != null)
        {
            skin.gameObject.layer = 8;
        }
    }
}
