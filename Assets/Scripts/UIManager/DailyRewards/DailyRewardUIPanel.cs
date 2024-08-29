using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using YG;

public enum RewardType
{
    None,
    Empty,
    Filled
}

[Serializable]
public struct RewardData
{
    public string LastTime;
    public DateTime Time;
    public int AllRewards;
    public int TakeCount;
    public int NotakeCount;
    public RewardType[] Rewards;

    public RewardData(string lastTime, DateTime date, int allRewards, int takeCount, int notakeCount, RewardType[] rewards)
    {
        LastTime = lastTime;
        Time = date;
        AllRewards = allRewards;
        TakeCount = takeCount;
        NotakeCount = notakeCount;
        Rewards = rewards;
    }
}

public class DailyRewardUIPanel : MonoBehaviour
{
    [SerializeField] private List<Button> ButtonDailyRewards;

    public delegate void RewardDelegate();
    public event RewardDelegate OnUpdateReward;

    public void OnEnable()
    {
        Init();

        for (int i = 0; i < ButtonDailyRewards.Count; i++)
        {
            ButtonDailyRewards[i].gameObject.SetActive(YandexGame.savesData.RewardData.Rewards[i] == RewardType.Filled);
            int index = i;
            ButtonDailyRewards[i].onClick.AddListener(() => TakeReward(index));
        }

        OnUpdateReward?.Invoke();
    }

    public void Init()
    {
        ChekcDate();
        CheckRewards();
    }

    public void TakeReward(int index)
    {
        YandexGame.savesData.RewardData.Rewards[index] = RewardType.Empty;
        YandexGame.savesData.RewardData.NotakeCount--;
        YandexGame.savesData.RewardData.TakeCount++;
        ButtonDailyRewards[index].gameObject.SetActive(false);

        switch (index)
        {
            case 0:
                if (MainUI.Instance.player != null)
                {
                    MainUI.Instance.player.Eat(5000);
                }
                if (MainUI.Instance.skin != null)
                {
                    MainUI.Instance.skin.Eat(5000);
                }
                break;
            case 1:
                MainUI.Instance.UpdateSpin(8);
                break;
            case 2:
                if (MainUI.Instance.player != null)
                {
                    MainUI.Instance.player.Eat(20000);
                }
                if (MainUI.Instance.skin != null)
                {
                    MainUI.Instance.skin.Eat(20000);
                }
                break;
            case 3:
                MainUI.Instance.UpdateSpin(20);
                break;
            case 4:
                if (MainUI.Instance.player != null)
                {
                    MainUI.Instance.player.Eat(30000);
                }
                if (MainUI.Instance.skin != null)
                {
                    MainUI.Instance.skin.Eat(30000);
                }
                break;
            case 5:
                MainUI.Instance.UpdateSpin(50);
                break;
            case 6:
                if (MainUI.Instance.player != null)
                {
                    MainUI.Instance.player.Eat(50000);
                }
                if (MainUI.Instance.skin != null)
                {
                    MainUI.Instance.skin.Eat(50000);
                }
                break;
        }

        if (ResetRewards())
        {
            ChekcDate();
            return;
        }

        YandexGame.SaveProgress();
        OnUpdateReward?.Invoke();
    }

    public void OnDisable()
    {
        for (int i = 0; i < ButtonDailyRewards.Count; i++)
        {
            ButtonDailyRewards[i].gameObject.SetActive(false);
            ButtonDailyRewards[i].onClick.RemoveAllListeners();
        }
    }

    public void ChekcDate()
    {
        YandexGame.LoadProgress();
        DateTime.TryParse(YandexGame.savesData.RewardData.LastTime, out YandexGame.savesData.RewardData.Time);

        if (ResetRewards())
        {
            ChekcDate();
            return;
        }

        if (string.IsNullOrEmpty(YandexGame.savesData.RewardData.LastTime))
        {
            YandexGame.savesData.RewardData.Time = DateTime.Today.AddDays(-1);
            YandexGame.savesData.RewardData.LastTime = YandexGame.savesData.RewardData.Time.ToString();
            YandexGame.SaveProgress();
        }
    }

    public void CheckRewards()
    {
        for (int i = YandexGame.savesData.RewardData.Rewards.Length - 1; i >= 0; i--)
        {
            if (DateTime.Today.AddDays(-i) > YandexGame.savesData.RewardData.Time && YandexGame.savesData.RewardData.Rewards[i] == RewardType.None)
            {
                YandexGame.savesData.RewardData.Rewards[i] = RewardType.Filled;
                YandexGame.savesData.RewardData.NotakeCount++;
            }
        }

        YandexGame.SaveProgress();
        OnUpdateReward?.Invoke();
    }

    public bool ResetRewards()
    {
        if (YandexGame.savesData.RewardData.TakeCount == YandexGame.savesData.RewardData.AllRewards)
        {
            YandexGame.savesData.RewardData = new() { Rewards = new RewardType[YandexGame.savesData.RewardData.AllRewards], AllRewards = YandexGame.savesData.RewardData.AllRewards };
            YandexGame.SaveProgress();
            OnDisable();
            OnEnable();
            return true;
        }

        return false;
    }

}
