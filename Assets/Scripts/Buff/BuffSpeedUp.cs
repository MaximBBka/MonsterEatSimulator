using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeedUp : BaseBuffUp
{
    public override BaseBuff Setup()
    {
        return new BuffSpeed();
    }
}
