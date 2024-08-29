using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : State
{
    public EnemyController controller;
    public EnemyDeathState(StateMachine stateMachine) : base(stateMachine)
    {
        controller = stateMachine as EnemyController;
    }

    public override void OnFinish()
    {

    }

    public override void OnStart()
    {
        GameObject.Destroy(controller.Enemy.gameObject);
    }

    public override void OnUpdate()
    {

    }
}
