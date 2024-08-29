using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using YG;

public class Enemy : MonoBehaviour, ITryEat, IUnit
{
    [SerializeField] private EnemyController Controller;
    public ModelEnemy model;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SpawnEnemy spawnEnemy;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] public TextMeshProUGUI textName;
    [SerializeField] private Transform Canvas;
    public string[] Nickname;
    public Transform TargetAttack;
    public delegate void EnemyDelegate();
    public event EnemyDelegate EnemyCallback;
    public int index = 0;
    public int indexBirth = 0;
    public int segments = 50; // Количество сегментов для круга
    public float radius = 0f; // Радиус круга
    public float lineWidth = 0.1f; // Толщина линии круга

    public List<BaseBuff> Buffs { get; private set; } = new List<BaseBuff>();

    public void Init(ModelEnemy modelEnemy, SpawnEnemy spawn)
    {
        model = modelEnemy;
        spawnEnemy = spawn;
        index = model.index;
    }
    public void Eat(int total)
    {
        model.ScoreTotal += total * model.TakeFood;
        if (model.ScoreTotal > 1000)
        {
            int thousands = (int)model.ScoreTotal / 1000;
            int hundreds = ((int)model.ScoreTotal % 1000) / 100;
            textScore.text = $"{thousands}.{hundreds}K";
        }
        else
        {
            textScore.text = $"{model.ScoreTotal}";
        }
        if (model.ScoreTotal >= model.MaxTotal)
        {
            EnemyCallback?.Invoke();
        }
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
        Controller?.OnUpdate();
        CircleEaten();
        for (int i = Buffs.Count - 1; i >= 0; i--)
        {
            Buffs[i].OnUpdate();
        }
    }
    private void Start()
    {
        Controller = new EnemyController(this);
        Controller.Switch(new EnemyIdleState(Controller));
        EnemyCallback += EnemyUP;
        SetupCircle();
        StartCoroutine(ScaleAndMove(model.Scale, model.PosY, 2f));
        if (model.ScoreTotal > 1000)
        {
            int thousands = (int)model.ScoreTotal / 1000;
            int hundreds = ((int)model.ScoreTotal % 1000) / 100;
            textScore.text = $"{thousands}.{hundreds}K";
        }
        else
        {
            textScore.text = $"{model.ScoreTotal}";
        }
              
    }
    public void SetTarget(Transform target)
    {
        TargetAttack = target;
        if (target == null)
        {
            agent.speed = model.Speed;
            SetTarget(transform.position);
            return;
        }
        SetTarget(target.position);
    }
    public void SetTarget(Vector3 target)
    {
        agent.speed = model.Speed;
        agent.SetDestination(target);
        if( animator != null )
        {
            animator.SetBool("IsWalk", true);
        }
        
    }
    public void EnemyUP()
    {
        EnemyCallback -= EnemyUP;
        spawnEnemy.DeSpawner(this);
        Enemy enemy = spawnEnemy.RespawnEnemy(this);
        EnemyCallback += EnemyUP;     
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
            if (model.indexForLineRender == 1)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * transform.localScale.x;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * transform.localScale.x;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }
            if (model.indexForLineRender == 2)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * model.Scale / 3f;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * model.Scale / 3f;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }
            if (model.indexForLineRender == 3)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * model.Scale / 2f;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * model.Scale / 2f;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }
            if (model.indexForLineRender == 4)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * model.Scale / 5f;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * model.Scale / 5f;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }
            if (model.indexForLineRender == 5)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * model.Scale;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * model.Scale;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }
            if (model.indexForLineRender == 6)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * model.Scale * 1.3f;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * model.Scale * 1.3f;
                positions[i] = new Vector3(x, 0.05f, y);
                angle += angleStep;
            }
        }
        lineRenderer.SetPositions(positions);
    }

    public bool IsStrong(float score)
    {
        return model.ScoreTotal > score;
    }

    public float Eaten()
    {
        return model.ScoreTotal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IUnit>(out IUnit unit))
        {
            ITryEat tryEat = unit as ITryEat;
            if (unit.IsStrong(model.ScoreTotal))
            {
                Player player = unit as Player;
                Skin skin = unit as Skin;
                if (player != null)
                {
                    MainUI.Instance.MoneyKill += 1;
                }
                if (skin != null)
                {
                    MainUI.Instance.MoneyKill += 1;
                }          
                tryEat?.Eat((int)Eaten());
                if (MainUI.Instance.units.Contains(this))
                {
                    MainUI.Instance.units.Remove(this);
                }
                /*unit.Eat(Eaten());*/ // Когда противники съедают они не растут, только после еды. Пробовал починить, в итоге ошибки на SetDistination !!!!!!!!
                spawnEnemy.AllCountEnemy--;
                if (indexBirth == 0)
                {
                    spawnEnemy.CountEnemy1--;
                }
                if (indexBirth == 1)
                {
                    spawnEnemy.CountEnemy2--;
                }
                if (indexBirth == 2)
                {
                    spawnEnemy.CountEnemy3--;
                }
                if (indexBirth == 3)
                {
                    spawnEnemy.CountEnemy4--;
                }
                if (indexBirth == 4)
                {
                    spawnEnemy.CountEnemy5--;
                }
                if (indexBirth == 5)
                {
                    spawnEnemy.CountEnemy6--;
                }
                Destroy(gameObject);
                return;
            }
            else
            {

                if (unit.GetType().Equals(typeof(Player))) // Когда умирает гг
                {
                    if(other.gameObject.layer == 0)
                    {
                        return;
                    }
                    AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.Death);
                    SetTarget(null);
                    Controller.Switch(new EnemyIdleState(Controller));
                    other.gameObject.SetActive(false);
                    MainUI.Instance.WindowDeath.gameObject.SetActive(true);
                    return;
                }
                if (unit.GetType().Equals(typeof(Skin))) // Когда умирает гг
                {
                    if (other.gameObject.layer == 0)
                    {
                        return;
                    }
                    AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.Death);
                    SetTarget(null);
                    Controller.Switch(new EnemyIdleState(Controller));
                    other.gameObject.SetActive(false);
                    MainUI.Instance.WindowDeath.gameObject.SetActive(true);
                    return;
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x * model.radiusEat * 3f);
    }
    public void Eat(float Score)
    {
        model.ScoreTotal += Score;
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x * model.radiusEat);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent<Food>(out Food food))
            {
                Eat(food.modelFood.totalCount);
                food.Eaten();
            }
        }
    }

    public void TakeBuff(BaseBuff buff)
    {
        buff.OnStart(this);
        for (int i = 0; i < Buffs.Count; i++)
        {
            if (Buffs[i].GetType().Equals(buff.GetType()))
            {
                return;
            }
        }
        Buffs.Add(buff);
    }
    public string Name()
    {
        return textName.text;
    }
}
