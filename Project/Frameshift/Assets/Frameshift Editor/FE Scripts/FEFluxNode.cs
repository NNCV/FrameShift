using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;
using TMPro;

public class FEFluxNode : FEFundamentalNode
{
    public Slider fluxModeSlider, fluxMultiplierSlider;
    public TMP_InputField fluxModeIF, fluxMultiplierIF;
    public TMP_InputField fluxMultMinIF, fluxMultMaxIF;

    public int fluxMode;

    public float fluxMultiplier;

    public override void ContinuousFunction(FEFundamentalNode output)
    {
        base.ContinuousFunction(output);
        
        switch(fluxMode)
        {
            case 0:
                output.fluxCurrent += Time.fixedDeltaTime * fluxMultiplier;
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

    public override void Update() { }

    public void SetMode()
    {
        fluxMode = (int)fluxModeSlider.value;
        fluxModeIF.text = fluxMode.ToString();
    }

    public void SetMultiplier()
    {
        fluxMultiplier = fluxMultiplierSlider.value;
        fluxMultiplierIF.text = fluxMultiplier.ToString();
    }

    public void SetModeByText()
    {
        fluxMode = int.Parse(fluxModeIF.text);
        fluxModeSlider.value = fluxMode;
        fluxModeIF.text = fluxMode.ToString();
    }

    public void SetMultiplierByText()
    {
        fluxMultiplier = float.Parse(fluxMultiplierIF.text);
        if(fluxMultiplier < float.Parse(fluxMultMinIF.text))
        {
            fluxMultiplier = float.Parse(fluxMultMinIF.text);
        }
        else if(fluxMultiplier > float.Parse(fluxMultMaxIF.text))
        {
            fluxMultiplier = float.Parse(fluxMultMaxIF.text);
        }
        fluxMultiplierSlider.value = fluxMultiplier;
        fluxMultiplierIF.text = fluxMultiplier.ToString();
    }

    public void SetMultiplierSliderMin()
    {
        fluxMultiplierSlider.minValue = float.Parse(fluxMultMinIF.text);
    }

    public void SetMultiplierSliderMax()
    {
        fluxMultiplierSlider.maxValue = float.Parse(fluxMultMaxIF.text);
    }
}
