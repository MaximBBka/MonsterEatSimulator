using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Game/Data/PlayerModels")]
public class SOPlayers : ScriptableObject
{
    public ModelPlayer[] _modelPlayer;
}

[Serializable]
public struct ModelPlayer
{
    public Player Prefab;
    public float Health;
    public float Speed;
    public float RotationSpeed;
    public float Scale;
    public float PosY;
    public float radiusEat;
    public float CountScore;
    public float MaxScore;
    public Vector3 CameraSetup;
    public int index;
    public int indexForLineRender;
    public int TakeFood;
    public int TimerBuff;
}
