using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : State
{
    public EnemyController controller;
    public Coroutine coroutine;
    public EnemyAttackState(StateMachine stateMachine) : base(stateMachine)
    {
        controller = stateMachine as EnemyController;
    }

    public override void OnFinish()
    {

    }

    public override void OnStart()
    {
        coroutine = controller.Enemy.StartCoroutine(Timer());
    }

    public override void OnUpdate()
    {
        Attack();
    }
    public void Attack()
    {
        controller.Enemy.SetTarget(controller.Enemy.TargetAttack);
        if (controller.Enemy.TargetAttack == null)
        {
            controller.Enemy.SetTarget(null);
            controller.Switch(new EnemyIdleState(controller));
        }
    }
    public IEnumerator Timer()
    {     
        yield return new WaitForSeconds(8);
        controller.Enemy.SetTarget(null);
        controller.Switch(new EnemyIdleState(controller));
    }
}
