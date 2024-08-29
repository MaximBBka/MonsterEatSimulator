using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class MainUI : MonoBehaviour
{
    [SerializeField] private Transform WindowDailyRewards;
    [SerializeField] public LackySpin speen;
    [SerializeField] private DailyRewardUIPanel _panel;
    [SerializeField] private WindowGifts _windowGifts;
    [SerializeField] private InfoText[] _infos;
    [SerializeField] private TextMeshProUGUI totalScore;
    [SerializeField] private TextMeshProUGUI totalScoreDeath;
    [SerializeField] private TextMeshProUGUI totalMoney;
    [SerializeField] public Transform[] ButtonSkinsBuy;
    [SerializeField] public Transform[] ButtonSkinsSelect;
    [SerializeField] public Transform WindowDeath;
    [SerializeField] private ViewBuff[] ViewBuff;
    [SerializeField] private TextMeshProUGUI[] textLeaderBoard;
    public List<IUnit> units = new List<IUnit>();
    public Player player;
    public Skin skin;
    public int MoneyKill;

    [field: SerializeField] public static MainUI Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Update()
    {
        if (!speen.isTurn && speen.Coroutine == null)
        {
            speen.Coroutine = StartCoroutine(speen.Timer(speen.timer, speen.timerText));
        }
        if (speen.isTurn)
        {
            speen.timerText.text = "";
        }
        UpdateInfo();
        CheckBufff();
        SortListLeaderBoard();
        UpdateLeaderBoard();
    }
    public void ButtonPlay()
    {
        AudioManager.Instance.ButtonSound();
    }
    public void CheckBufff()
    {
        if (player != null && player.Buffs.Count > 0)
        {
            for (int i = player.Buffs.Count - 1; i >= 0; i--)
            {
                if (player.Buffs[i].GetType() == typeof(BuffSpeed) && !ViewBuff[0].gameObject.activeSelf)
                {
                    ViewBuff[0].Init(player.Buffs[i]);
                    ViewBuff[0].Show();
                }
                if (player.Buffs[i].GetType() == typeof(BuffScoreX2) && !ViewBuff[1].gameObject.activeSelf)
                {
                    ViewBuff[1].Init(player.Buffs[i]);
                    ViewBuff[1].Show();
                }
            }
        }
        if (skin != null && skin.Buffs.Count > 0)
        {
            for (int i = skin.Buffs.Count - 1; i >= 0; i--)
            {
                if (skin.Buffs[i].GetType() == typeof(BuffSpeed) && !ViewBuff[0].gameObject.activeSelf)
                {
                    ViewBuff[0].Init(skin.Buffs[i]);
                    ViewBuff[0].Show();
                }
                if (skin.Buffs[i].GetType() == typeof(BuffScoreX2) && !ViewBuff[1].gameObject.activeSelf)
                {
                    ViewBuff[1].Init(skin.Buffs[i]);
                    ViewBuff[1].Show();
                }
            }
        }
    }
    private void Start()
    {

        CheckReward();
        _windowGifts.OnUpdate += UpdateGifts;
        speen.OnUpdate += UpdateSpeen;
        //UpdateSpeen();
    }

    public void UpdateSpin(int speens)
    {
        if (speen.Coroutine != null)
        {
            StopCoroutine(speen.Coroutine);
            speen.Coroutine = null;
        }
        speen.TotalSpeen += speens;
        UpdateSpeen();
    }
    private void OnDestroy()
    {
        _panel.OnUpdateReward -= UpdateReward;
        _windowGifts.OnUpdate -= UpdateGifts;
        speen.OnUpdate -= UpdateSpeen;
    }

    private void CheckReward()
    {
        _panel.Init();
        _panel.OnUpdateReward += UpdateReward;
        UpdateReward();
    }

    private void UpdateReward()
    {
        if (YandexGame.savesData.RewardData.NotakeCount > 0)
        {
            _infos[1].Set(YandexGame.savesData.RewardData.NotakeCount.ToString());
            _infos[1].Show();
            AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.Notificationton);
            return;
        }

        _infos[1].Hide();
    }

    private void UpdateGifts()
    {
        if (_windowGifts.NoTake > 0)
        {
            _infos[3].Set(_windowGifts.NoTake.ToString());
            _infos[3].Show();
            AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.Notificationton);
            return;
        }

        _infos[3].Hide();
    }
    private void UpdateSpeen()
    {
        if (speen.TotalSpeen > 0)
        {
            _infos[2].Set(speen.TotalSpeen.ToString());
            _infos[2].Show();
            AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.Notificationton);
            return;
        }

        _infos[2].Hide();
    }
    public void UpdateInfo()
    {
        if (player != null)
        {
            if (player.player.CountScore > 1000)
            {
                int thousands = (int)player.player.CountScore / 1000;
                int hundreds = ((int)player.player.CountScore % 1000) / 100;
                totalScore.text = $"{thousands}.{hundreds}K";
            }
            else
            {
                totalScore.text = $"{player.player.CountScore}";
            }
            totalScoreDeath.text = $"{player.player.CountScore}";
        }
        if (skin != null)
        {
            if (skin.skin.CountScore > 1000)
            {
                int thousands = (int)skin.skin.CountScore / 1000;
                int hundreds = ((int)skin.skin.CountScore % 1000) / 100;
                totalScore.text = $"{thousands}.{hundreds}K";
            }
            else
            {
                totalScore.text = $"{skin.skin.CountScore}";
            }
            totalScoreDeath.text = $"{skin.skin.CountScore}";
        }
        totalMoney.text = $"{MoneyKill}";
    }

    public void BuySkin(int index)
    {
        if (MoneyKill >= 10)
        {
            ButtonSkinsBuy[index].gameObject.SetActive(false);
            ButtonSkinsSelect[index].gameObject.SetActive(true);
            MoneyKill -= 10;
        }
    }

    public void SortListLeaderBoard()
    {
        for (int i = units.Count - 1; i >= 0; i--)
        {
            for (int j = units.Count - 1; j >= 0; j--)
            {
                if (units[i].Eaten() < units[j].Eaten())
                {
                    IUnit temp = units[i];
                    units[i] = units[j];
                    units[j] = temp;
                }
            }
        }
    }

    public void UpdateLeaderBoard()
    {
        int playerIndex = 0;
        for (int i = 0; i < units.Count; i++)
        {           
            if (units[i] as Player != null)
            {
                
                Player player = units[i] as Player;
                if (player != null)
                {
                    playerIndex = i;
                    textLeaderBoard[2].text = $"{playerIndex}. {player.Name()}";
                }
            }

            if (units[i] as Skin != null)
            {

                Skin skin = units[i] as Skin;
                if (skin != null)
                {
                    playerIndex = i;
                    textLeaderBoard[2].text = $"{playerIndex}. {skin.Name()}";
                }
            }

            if (playerIndex + 1 == units.Count)
            {
                textLeaderBoard[3].text = $"0.";
            }
            else
            {
                if (units[playerIndex + 1] as Enemy != null)
                {
                    Enemy enemy = units[playerIndex + 1] as Enemy;
                    if (enemy != null)
                    {
                        textLeaderBoard[3].text = $"{playerIndex + 1}. {enemy.Name()}";
                    }
                }
            }
            if (playerIndex + 2 == units.Count || playerIndex + 2 == units.Count + 1)
            {
                textLeaderBoard[4].text = $"0.";
            }
            else
            {
                if (units[playerIndex + 2] as Enemy != null)
                {
                    Enemy enemy = units[playerIndex + 2] as Enemy;
                    if (enemy != null)
                    {
                        textLeaderBoard[4].text = $"{playerIndex + 2}. {enemy.Name()}";
                    }
                }
            }

            if (playerIndex - 1 == -1)
            {
                textLeaderBoard[1].text = $"0.";
            }
            else
            {
                if (units[playerIndex - 1] as Enemy != null)
                {
                    Enemy enemy = units[playerIndex - 1] as Enemy;
                    if (enemy != null)
                    {
                        textLeaderBoard[1].text = $"{playerIndex - 1}. {enemy.Name()}";
                    }
                }
            }

            if (playerIndex - 2 == -1 || playerIndex - 2 == -2)
            {
                textLeaderBoard[0].text = $"0.";
            }
            else
            {
                if (units[playerIndex - 2] as Enemy != null)
                {
                    Enemy enemy = units[playerIndex - 2] as Enemy;
                    if (enemy != null)
                    {
                        textLeaderBoard[0].text = $"{playerIndex - 2}. {enemy.Name()}";
                    }
                }
            }

            if (units[i] as Skin != null)
            {
                Skin skin = units[i] as Skin;
                textLeaderBoard[2].text = $"{i}. {skin.Name()}";
            }

        }
    }
}
