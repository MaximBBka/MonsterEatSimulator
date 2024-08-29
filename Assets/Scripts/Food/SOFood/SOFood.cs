using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(menuName = "Game/Data/Foods")]
public class SOFood : ScriptableObject
{
    public ModelFood[] ModelFoods;
}
[Serializable]
public struct ModelFood
{
    public Food prefab;
    public int totalCount;
}

