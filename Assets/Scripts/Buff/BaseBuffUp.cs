using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseBuffUp : MonoBehaviour
{
    public float Scale;
    public delegate void BuffUPDelegate();
    private BuffUPDelegate CallBack;
    public void Init(BuffUPDelegate buffUP)
    {
        CallBack = buffUP;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IUnit>(out IUnit unit))
        {
            unit.TakeBuff(Setup());
            Destroy(gameObject);
        }
    }
    public virtual void Start()
    {
        Destroy(gameObject, 40f);
    }
    public virtual void Update()
    {
        
    }
    public virtual BaseBuff Setup()
    {
        return null;
    }
    private void OnDestroy()
    {
        CallBack?.Invoke();
    }
}
