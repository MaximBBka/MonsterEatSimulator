using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseBuff
{
    protected IUnit Owner;
    public float MaxTime;
    public delegate void EndTimeDelegate();
    public virtual event EndTimeDelegate EndCallback;
    public abstract void OnStart(IUnit owner);
    public abstract void OnFinish();
    public abstract void OnUpdate();
    public abstract float EndTime();

}
