using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyIdleState : State
{
    public EnemyController controller;
    public Food prefabTarget = null;
    public Collider[] colliders;
    public LayerMask layer;
    public LayerMask layerUnit;
    public bool IsRandomMove = true;
    public Vector3? RandomTarget;
    public EnemyIdleState(StateMachine stateMachine) : base(stateMachine)
    {
        controller = stateMachine as EnemyController;
    }

    public override void OnFinish()
    {

    }

    public override void OnStart()
    {
        layer = LayerMask.GetMask("Eat");
        layerUnit = LayerMask.GetMask("Unit");
        StartPosition();
    }

    public override void OnUpdate()
    {
        if (controller.Enemy.model.Health < 0)
        {
            controller.Switch(new EnemyDeathState(controller));
        }
        FindPlayerOrEnemy();
        if (FindFood())
        {
            return;
        }
        RandomMove();
    }
    public bool FindFood()
    {
        colliders = Physics.OverlapSphere(controller.Enemy.transform.position, controller.Enemy.transform.localScale.x * controller.Enemy.radius, layer);
        
        if (prefabTarget == null)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent<Food>(out Food food))
                {
                    prefabTarget = food;
                    controller.Enemy.SetTarget(food.transform);
                    IsRandomMove = false;
                    return true;
                }
            }
        }
        return prefabTarget != null;
    }
    public void FindPlayerOrEnemy()
    {
        colliders = Physics.OverlapSphere(controller.Enemy.transform.position, controller.Enemy.transform.localScale.x * controller.Enemy.model.radiusEat * 3f, layerUnit);

        if (colliders != null)
        {
            for (int i = 0;i < colliders.Length;i++)
            {
                if (colliders[i].TryGetComponent<IUnit>(out IUnit unit) && unit != controller.Enemy)
                {
                    if (unit.IsStrong(controller.Enemy.model.ScoreTotal))
                    {
                        Debug.Log(unit.GetType().Name);
                        controller.Enemy.TargetAttack = colliders[i].transform;
                        controller.Switch(new EnemyRunState(controller));                       
                    }
                    else
                    {
                        Player player = unit as Player;
                        Skin skin = unit as Skin;
                        if(player != null)
                        {
                            AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.Attack);
                        }
                        if (skin != null)
                        {
                            AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.Attack);
                        }
                        controller.Enemy.SetTarget(colliders[i].transform);
                        controller.Switch(new EnemyAttackState(controller));
                    }                    
                }
            }
        }
    }
    public void RandomMove()
    {
        Vector2 pos1 = new Vector2(controller.Enemy.transform.position.x, controller.Enemy.transform.position.z);
        Vector2 pos2 = new Vector2(RandomTarget.Value.x, RandomTarget.Value.z);

        if (RandomTarget != null && Vector2.Distance(pos1, pos2) < 1)
        {
            RandomTarget = null;
        }
        if (colliders.Length == 0 && !IsRandomMove)
        {
            IsRandomMove = true;
        }
        if (IsRandomMove && RandomTarget == null)
        {
            float x = Random.Range(-95, 95);
            float z = Random.Range(-95, 95);
            RandomTarget = new Vector3(x, controller.Enemy.transform.position.y, z);
            controller.Enemy.SetTarget(RandomTarget.Value);
            IsRandomMove = false;
        }
        else if(RandomTarget != null)
        {
            controller.Enemy.SetTarget(RandomTarget.Value);
        }
        
    }
    public void StartPosition()
    {
        float x = Random.Range(-95, 95);
        float z = Random.Range(-95, 95);
        RandomTarget = new Vector3(x, controller.Enemy.transform.position.y, z);
        controller.Enemy.SetTarget(RandomTarget.Value);
        IsRandomMove = false;
    }
}
