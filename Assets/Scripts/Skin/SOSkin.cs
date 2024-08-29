using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Data/SkinsAllModels")]
public class SOSkin : ScriptableObject
{
    public ModelAllSkin[] ModelAllSkins;
}
[Serializable]
public struct ModelAllSkin
{
    public Skin Prefab;
    public ModelSkin[] modelSkins;
}
[Serializable]
public struct ModelSkin
{  
    public float Speed;
    public float RotationSpeed;
    public float Scale;
    public float PosY;
    public float radiusEat;
    public float CountScore;
    public float MaxScore;
    public Vector3 CameraSetup;
    public int index;
    public int SOIndex;
    public int ModelIndex;
    public float LineRender;
    public int TakeFood;
    public int TimerBuff;
}
