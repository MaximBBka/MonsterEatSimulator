using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using YG;
using Random = UnityEngine.Random;

public interface ITryEat // Удобно использоваться, потому что можно унаследоваться для всех объектов которым присуща эта возможность
{
    public void Eat(int total);
}
public interface IUnit
{
    public List<BaseBuff> Buffs { get; }
    public void TakeBuff(BaseBuff buff);
    public bool IsStrong(float score);
    public float Eaten();
    public string Name();
    public void Eat(float Score);
}
public class Player : MonoBehaviour, ITryEat, IUnit
{
    public Animator animator;
    public ModelPlayer player;
    public delegate void PlayerDelegate(); // Что-то типо Observera. Я подписываю на ивент логику. И в нужных скриптах вызываю ивент (получаю уведомления)
    public event PlayerDelegate PlayerCallback;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private Transform Canvas;
    public int segments = 50; // Количество сегментов для круга
    public float lineWidth = 0.1f; // Толщина линии круга

    public List<BaseBuff> Buffs { get; private set; } = new List<BaseBuff>();

    private void Start()
    {
        SetupCircle();
        //StartCoroutine(UpScale());
        StartCoroutine(ScaleAndMove(player.Scale, player.PosY, 4f));
        if (player.CountScore > 1000)
        {
            int thousands = (int)player.CountScore / 1000;
            int hundreds = ((int)player.CountScore % 1000) / 100;
            textScore.text = $"{thousands}.{hundreds}K";
        }
        else
        {
            textScore.text = $"{player.CountScore}";
        }     
        textName.text = $"{YandexGame.savesData.newPlayerName}";
    }
    public void Init(ModelPlayer model)
    {
        player = model;
    }
    private void Update()
    {
        CinemachineBrain cinemachineBrain = FindObjectOfType<CinemachineBrain>();

        // Проверяем, найден ли CinemachineBrain
        if (cinemachineBrain != null)
        {
            // Получаем первую активную виртуальную камеру
            CinemachineVirtualCamera activeVirtualCamera = (CinemachineVirtualCamera)cinemachineBrain.ActiveVirtualCamera;
            if (activeVirtualCamera != null)
            {
                Canvas.transform.LookAt(activeVirtualCamera.VirtualCameraGameObject.transform.position);
            }           
        }
        CircleEaten();
        for (int i = Buffs.Count - 1; i >= 0; i--)
        {
            Buffs[i].OnUpdate();
        }
    }
    //public void BonusUP()
    //{
    //    if (player.CountScore >= player.MaxScore)
    //    {
    //        PlayerCallback?.Invoke();
    //    }
    //}
    public void Eat(int total)
    {
        player.CountScore += total * player.TakeFood;      
        if (player.CountScore > 1000)
        {
            int thousands = (int)player.CountScore / 1000;
            int hundreds = ((int)player.CountScore % 1000) / 100;
            textScore.text = $"{thousands}.{hundreds}K";
        }
        else
        {
            textScore.text = $"{player.CountScore}";
        }
        if (player.CountScore >= player.MaxScore && player.index < 6)
        {
            PlayerCallback?.Invoke();
            AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.Up);
        }
    }
    public bool IsStrong(float score)
    {
        return player.CountScore > score;
    }

    public float Eaten()
    {
        return player.CountScore;
    }

    public void Eat(float Score)
    {
        player.CountScore += Score;
    }
    private void SetupCircle()
    {
        lineRenderer.widthMultiplier = lineWidth;
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = segments + 1;

        float angleStep = 360f / segments;
        var positions = new Vector3[segments + 1];
        float angle = 0f;

        for (int i = 0; i <= segments; i++)
        {
            if (player.indexForLineRender == 1)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * transform.localScale.x;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * transform.localScale.x;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }
            if (player.indexForLineRender == 2)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * player.Scale / 3f;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * player.Scale / 3f;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }
            if (player.indexForLineRender == 3)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * player.Scale / 2f;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * player.Scale / 2f;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }
            if (player.indexForLineRender == 4)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * player.Scale / 5f;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * player.Scale / 5f;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }
            if (player.indexForLineRender == 5)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * player.Scale;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * player.Scale;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }
            if (player.indexForLineRender == 6)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * player.Scale * 1.3f;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * player.Scale * 1.3f;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }            
        }
        lineRenderer.SetPositions(positions);
    }
    public IEnumerator ScaleAndMove(float targetScale, float targetPosY, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float lerpProgress = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScale, targetScale, targetScale), lerpProgress); // Изменяем масштаб по всем осям
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, targetPosY, lerpProgress), transform.position.z); // Изменяем позицию по оси Y
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = new Vector3(targetScale, targetScale, targetScale); // Фиксируем конечный масштаб
        transform.position = new Vector3(transform.position.x, targetPosY, transform.position.z); // Фиксируем конечную позицию по оси Y
    }
    public void CircleEaten()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x * player.radiusEat);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent<Food>(out Food food))
            {
                Eat(food.modelFood.totalCount);
                food.Eaten();
                AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.TakeFood[Random.Range(0, AudioManager.Instance.TakeFood.Length)]);
            }
        }
    }

    public void TakeBuff(BaseBuff buff)
    {
        for (int i = Buffs.Count - 1; i >= 0; i--)
        {
            if(Buffs[i].GetType().Equals(buff.GetType()))
            {
                Buffs[i].OnFinish();
                break;
            }
        }
        AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.TakeBuff);
        buff.OnStart(this);
        Buffs.Add(buff);
    }
    private void OnDestroy()
    {
        for (int i = Buffs.Count - 1; i >= 0; i--)
        {
            Buffs[i].OnFinish();
        }
    }

    public string Name()
    {
        return textName.text;
    }
}
