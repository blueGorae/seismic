using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Target { NONE, T_2D, T_3D, T_GRASSLAND };
public enum Wave { NONE, W_P, W_S, W_EARTHQUAKE };

public class Manger : MonoBehaviour
{


    public ToggleGroup targetGroup;
    public Toggle target2D;
    public Toggle target3D;
    public Toggle targetGrassland;
    public Slider densitySlider;
    public Text densityValue;
    public Slider magnitudeSlider;
    public Text magnitudeValue;
    public ToggleGroup waveGroup;
    public Toggle waveP;
    public Toggle waveS;
    public Toggle waveEarthquake;

    // Start is called before the first frame update
    void Start()
    {
        densityValue.text = densitySlider.value.ToString();
        magnitudeValue.text = magnitudeSlider.value.ToString();
        target2D.Select();
        waveP.Select();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateDensitySlider(float value)
    {
        densityValue.text = value.ToString();
    }

    public void UpdateMagnitudeSlider(float value)
    {
        magnitudeValue.text = value.ToString();
    }

    public Target CurrentTarget()
    {
        foreach (var toggle in targetGroup.ActiveToggles())
        {
            if (toggle.isOn)
            {
                if (toggle == target2D)
                {
                    return Target.T_2D;
                }
                else if (toggle == target3D)
                {
                    return Target.T_3D;
                }
                else if (toggle == targetGrassland)
                {
                    return Target.T_GRASSLAND;
                }
            }
        }
        return Target.NONE;
    }

    public Wave CurrentWave()
    {
        foreach (var toggle in waveGroup.ActiveToggles())
        {
            if (toggle.isOn)
            {
                if (toggle == waveP)
                {
                    return Wave.W_P;
                }
                else if (toggle == waveS)
                {
                    return Wave.W_S;
                }
                else if (toggle == waveEarthquake)
                {
                    return Wave.W_EARTHQUAKE;
                }
            }
        }
        return Wave.NONE;
    }
}
