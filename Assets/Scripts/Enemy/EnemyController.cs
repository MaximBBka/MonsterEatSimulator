using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : StateMachine
{
    public Enemy Enemy;

    public EnemyController(Enemy enemy)
    {
        Enemy = enemy;
    }
    public override void OnUpdate()
    {
        Current?.OnUpdate();
    }

    public override void Switch(State state)
    {
        Current?.OnFinish();
        Current = state;
        Current?.OnStart();
    }
}
