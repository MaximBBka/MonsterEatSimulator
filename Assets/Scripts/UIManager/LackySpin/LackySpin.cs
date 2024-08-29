using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LackySpin : MonoBehaviour
{
    private int numberOfTurns;
    private int WhatWin;
    private float speed;
    public bool isTurn = false;
    [SerializeField] private Button ButtonExit;
    [SerializeField] private Button ButtonSpeen;
    [SerializeField] public TextMeshProUGUI timerText;
    public float timer = 10f;
    public int TotalSpeen = 0;
    public Coroutine Coroutine;

    public delegate void SpeensGelegate();
    public event SpeensGelegate OnUpdate;

    public void Update()
    {
        ButtonSpeen.interactable = TotalSpeen > 0;
    }
    public string Convert(int time)
    {
        int hours = (int)(time / 3600);
        int minutes = (int)((time % 3600) / 60);
        int seconds = (int)(time % 60);

        // Форматирование строки
        return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }
    public IEnumerator Timer(float time, TextMeshProUGUI textTimer)
    {
        while (time > 0)
        {
            time -= 1;
            textTimer.SetText(Convert((int)time));
            yield return new WaitForSeconds(1f);
        }
        TotalSpeen += 1;
        isTurn = true;
        OnUpdate?.Invoke();
        Coroutine = null;
    }
    public void Speen()
    {
        if (isTurn)
        {
            StartCoroutine(TurnTheWeel());
            TotalSpeen--;
            OnUpdate?.Invoke();
        }
    }

    public IEnumerator TurnTheWeel()
    {
        isTurn = false;
        ButtonExit.gameObject.SetActive(false);
        numberOfTurns = Random.Range(35, 60);

        speed = 0.01f;

        for (int i = 0; i < numberOfTurns; i++)
        {
            transform.Rotate(0, 0, 22.5f);

            if (i > Mathf.RoundToInt(numberOfTurns * 0.5f))
            {
                speed = 0.02f;
            }
            if (i > Mathf.RoundToInt(numberOfTurns * 0.7f))
            {
                speed = 0.05f;
            }
            if (i > Mathf.RoundToInt(numberOfTurns * 0.9f))
            {
                speed = 0.07f;
            }

            yield return new WaitForSeconds(speed);
        }
        if (Mathf.RoundToInt(transform.eulerAngles.z) % 45 != 0)
        {
            transform.Rotate(0, 0, 22.5f);
        }
        WhatWin = Mathf.RoundToInt(transform.eulerAngles.z);

        switch (WhatWin)
        {
            case 0:
                if(MainUI.Instance.player  != null)
                {
                    MainUI.Instance.player.Eat(5000);
                }
                if (MainUI.Instance.skin != null)
                {
                    MainUI.Instance.skin.Eat(5000);
                }
                Debug.Log("5К");
                break;
            case 45:
                MainUI.Instance.UpdateSpin(1);
                Debug.Log("+1");
                break;
            case 90:
                MainUI.Instance.MoneyKill += 10;
                Debug.Log("+10");
                break;
            case 135:
                if (MainUI.Instance.player != null)
                {
                    MainUI.Instance.player.Eat(10000);
                }
                if (MainUI.Instance.skin != null)
                {
                    MainUI.Instance.skin.Eat(10000);
                }
                Debug.Log("10К");
                break;
            case 180:
                MainUI.Instance.MoneyKill += 20;
                Debug.Log("+20");
                break;
            case 225:
                MainUI.Instance.UpdateSpin(3);
                Debug.Log("+3");
                break;
            case 270:
                MainUI.Instance.MoneyKill += 5;
                Debug.Log("+5");
                break;
            case 315:
                if (MainUI.Instance.player != null)
                {
                    MainUI.Instance.player.Eat(1000);
                }
                if (MainUI.Instance.skin != null)
                {
                    MainUI.Instance.skin.Eat(1000);
                }
                Debug.Log("1К");
                break;
        }
        if(TotalSpeen > 0)
        {
            isTurn = true;
        }       
        ButtonExit.gameObject.SetActive(true);
    }

}
