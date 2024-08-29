using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffScoreX2UP : BaseBuffUp
{
    public override BaseBuff Setup()
    {
        return new BuffScoreX2();
    }
}
