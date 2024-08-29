using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : State
{
    public EnemyController controller;
    public Coroutine coroutine;
    public EnemyRunState(StateMachine stateMachine) : base(stateMachine)
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
        Run();
    }

    public void Run()
    {
        if(controller.Enemy.TargetAttack == null)
        {
            controller.Switch(new EnemyIdleState(controller));
            return;
        }
        Vector3 directionToEnemy = (controller.Enemy.transform.position - controller.Enemy.TargetAttack.position);
        controller.Enemy.SetTarget(controller.Enemy.transform.position + directionToEnemy);    
    }


    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(10);
        controller.Switch(new EnemyIdleState(controller));
    }
}
