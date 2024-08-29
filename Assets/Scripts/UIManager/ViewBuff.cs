using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewBuff : View
{
    public Slider sliderSpeed;
    public BaseBuff buff;

    public void Init(BaseBuff _buff)
    {
        buff = _buff;
        sliderSpeed.maxValue = buff.MaxTime;
        sliderSpeed.value = buff.MaxTime;
        buff.EndCallback += Hide;
    }
    public override void Hide()
    {
        base.Hide();
        buff.EndCallback -= Hide;
        buff = null;
    }
    private void LateUpdate()
    {
        if (buff != null && buff.EndTime() > 0) 
        {
            sliderSpeed.value = buff.EndTime();
        }
    }
}
