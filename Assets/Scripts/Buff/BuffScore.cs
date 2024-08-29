using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffScore : BaseBuff
{
    public int RandomTotal;
    public Player player;
    public Enemy enemy;
    public Skin skin;
    public BuffScore(int total)
    {
        RandomTotal = total;
    }

    public override float EndTime()
    {
        return 0f;
    }

    public override void OnFinish()
    {
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
            enemy.Eat(RandomTotal);           
        }
        if (player != null)
        {
            player.Eat(RandomTotal);
        }
        if (skin != null)
        {
            skin.Eat(RandomTotal);
        }
        OnFinish();
    }

    public override void OnUpdate()
    {

    }
}
