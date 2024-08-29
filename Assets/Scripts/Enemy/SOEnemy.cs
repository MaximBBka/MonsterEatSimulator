using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(menuName = "Game/Data/Enemies")]
public class SOEnemy : ScriptableObject
{
    public ModelEnemy[] modelEnemies;
}
[Serializable]
public struct ModelEnemy
{
    public Enemy Prefab;
    public float Health;
    public float Speed;
    public float Scale;
    public float PosY;
    public float radiusEat;
    public float ScoreTotal;
    public int MaxTotal;
    public int index;
    public int indexForLineRender;
    public int TakeFood;
    public int TimerBuff;
}