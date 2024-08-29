using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;
using static UnityEngine.SortingLayer;

public class Skin : MonoBehaviour, ITryEat, IUnit
{
    public Animator animator;
    public ModelSkin skin;
    public ModelAllSkin[] allSkin;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private Transform Canvas;
    public CameraController CameraController;
    public int segments = 50; // Количество сегментов для круга
    public float lineWidth = 0.1f; // Толщина линии круга   
    int index = 1;

    public List<BaseBuff> Buffs { get; private set; } = new List<BaseBuff>();

    public void Init(ModelSkin model, ModelAllSkin[] _allSkin, CameraController controller)
    {
        skin = model;
        allSkin = _allSkin;
        CameraController = controller;
    }
    private void Start()
    {
        SetupCircle();
        if (skin.CountScore > 1000)
        {
            int thousands = (int)skin.CountScore / 1000;
            int hundreds = ((int)skin.CountScore % 1000) / 100;
            textScore.text = $"{thousands}.{hundreds}K";
        }
        else
        {
            textScore.text = $"{skin.CountScore}";
        }
        textName.text = $"{YandexGame.savesData.newPlayerName}";
        StartCoroutine(ScaleAndMove(allSkin[skin.SOIndex].modelSkins[skin.index - 1].Scale, allSkin[skin.SOIndex].modelSkins[skin.index - 1].PosY, 4f));
    }
    private void Update()
    {
        CinemachineBrain cinemachineBrain = FindObjectOfType<CinemachineBrain>();

        // Проверяем, найден ли CinemachineBrain
        if (cinemachineBrain != null)
        {
            // Получаем первую активную виртуальную камеру
            CinemachineVirtualCamera activeVirtualCamera = (CinemachineVirtualCamera)cinemachineBrain.ActiveVirtualCamera;
            Canvas.transform.LookAt(activeVirtualCamera.VirtualCameraGameObject.transform.position);
        }
        CircleEaten();
        for (int i = Buffs.Count - 1; i >= 0; i--)
        {
            Buffs[i].OnUpdate();
        }
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

    public void Eat(int total)
    {
        skin.CountScore += total * skin.TakeFood;       
        if (skin.CountScore > 1000)
        {
            int thousands = (int)skin.CountScore / 1000;
            int hundreds = ((int)skin.CountScore % 1000) / 100;
            textScore.text = $"{thousands}.{hundreds}K";
        }
        else
        {
            textScore.text = $"{skin.CountScore}";
        }
        if (skin.CountScore >= skin.MaxScore && skin.index < 6)
        {
            UpSkin();
            AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.Up);
        }
    }

    public void UpSkin()
    {
        if (skin.index == 6)
        {
            return;
        }
        
        index = skin.index;
        if (skin.index == 5)
        {
            index = 5;
        }       
        StartCoroutine(ScaleAndMove(allSkin[skin.SOIndex].modelSkins[index].Scale, allSkin[skin.SOIndex].modelSkins[index].PosY, 4f));       
        float temp = skin.CountScore;
        skin = allSkin[skin.SOIndex].modelSkins[skin.index];
        skin.CountScore  = temp;
        CameraController.cinemachine.m_TrackedObjectOffset = skin.CameraSetup;
        SetupCircle();
        Eat(0);
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
            if (skin.index == 1)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * skin.LineRender;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * skin.LineRender;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }
            if (skin.index == 2)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * skin.LineRender;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * skin.LineRender;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }
            if (skin.index == 3)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * skin.LineRender;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * skin.LineRender;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }
            if (skin.index == 4)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * skin.LineRender;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * skin.LineRender;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }
            if (skin.index == 5)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * skin.LineRender;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * skin.LineRender;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }
            if (skin.index == 6)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * skin.LineRender;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * skin.LineRender;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }
        }
        lineRenderer.SetPositions(positions);
    }
    public void CircleEaten()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, skin.radiusEat);

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
            if (Buffs[i].GetType().Equals(buff.GetType()))
            {
                Buffs[i].OnFinish();
                break;
            }
        }
        AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.TakeBuff);
        buff.OnStart(this);
        Buffs.Add(buff);
    }

    public bool IsStrong(float score)
    {
        return skin.CountScore > score;
    }

    public float Eaten()
    {
        return skin.CountScore;
    }

    public void Eat(float Score)
    {
        skin.CountScore += Score;
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
