using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeed : BaseBuff
{
    public float Speed = 1f;
    public float startBuff;
    public Player player;
    public Enemy enemy;
    public Skin skin;
    public override event EndTimeDelegate EndCallback;
    public override float EndTime()
    {
        if (player != null)
        {
            return player.player.TimerBuff - startBuff;
        }
        if (skin != null)
        {
            return skin.skin.TimerBuff - startBuff;
        }
        return 0f;
    }
    public override void OnFinish()
    {
        if (enemy != null)
        {
            enemy.model.Speed -= Speed;
        }
        if (player != null)
        {
            player.player.Speed -= Speed;
        }
        if (skin != null)
        {
            skin.skin.Speed -= Speed;
        }
        EndCallback?.Invoke();
        Owner.Buffs.Remove(this);
    }

    public override void OnStart(IUnit owner)
    {
        Owner = owner;
        player = owner as Player;
        enemy = owner as Enemy;
        skin = owner as Skin;
        if (enemy != null)
        {
            enemy.model.Speed += Speed;
        }
        if (player != null)
        {
            player.player.Speed += Speed;
            MaxTime = player.player.TimerBuff;
        }
        if (skin != null)
        {
            skin.skin.Speed += Speed;
            MaxTime = skin.skin.TimerBuff;
        }
    }

    public override void OnUpdate()
    {
        startBuff += Time.deltaTime;
        if(player != null && player.player.TimerBuff <= startBuff)
        {
            OnFinish();
        }
        if (enemy != null && enemy.model.TimerBuff <= startBuff)
        {
            OnFinish();
        }
        if (skin != null && skin.skin.TimerBuff <= startBuff)
        {
            OnFinish();
        }
    }
}
