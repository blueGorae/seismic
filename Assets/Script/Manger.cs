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
    private float density2D;
    private float density3D;
    private float densityGrassland;
    public Slider magnitudeSlider;
    public Text magnitudeValue;
    private float magnitude2D;
    private float magnitude3D;
    private float magnitudeGrassland;
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

    public bool Apply2D()
    {
        return CurrentTarget() == Target.T_2D;
    }

    public bool Apply3D()
    {
        return CurrentTarget() == Target.T_3D;
    }

    public bool ApplyP()
    {
        return CurrentWave() == Wave.W_P;
    }

    public bool ApplyS()
    {
        return CurrentWave() == Wave.W_S;
    }

    public bool ApplyP2D()
    {
        return (CurrentTarget() == Target.T_2D && CurrentWave() == Wave.W_P);
    }

    public bool ApplyP3D()
    {
        return (CurrentTarget() == Target.T_3D && CurrentWave() == Wave.W_P);
    }

    public bool ApplyS2D()
    {
        return (CurrentTarget() == Target.T_2D && CurrentWave() == Wave.W_S);
    }

    public bool ApplyS3D()
    {
        return (CurrentTarget() == Target.T_3D && CurrentWave() == Wave.W_S);
    }

    public bool ApplyPGrassland()
    {
        return (CurrentTarget() == Target.T_GRASSLAND && CurrentWave() == Wave.W_P);
    }

    public bool ApplySGrassland()
    {
        return (CurrentTarget() == Target.T_GRASSLAND && CurrentWave() == Wave.W_S);
    }

    public void UpdateDensitySlider(float value)
    {
        switch (CurrentTarget())
        {
            case Target.NONE:
                break;
            case Target.T_2D:
                density2D = value;
                break;
            case Target.T_3D:
                density3D = value;
                break;
            case Target.T_GRASSLAND:
                densityGrassland = value;
                break;
        }
        densityValue.text = value.ToString();
    }

    public void UpdateMagnitudeSlider(float value)
    {
        switch (CurrentTarget())
        {
            case Target.T_2D:
                magnitude2D = value;
                break;
            case Target.T_3D:
                magnitude3D = value;
                break;
            case Target.T_GRASSLAND:
                magnitudeGrassland = value;
                break;
        }
        magnitudeValue.text = value.ToString();
    }

    public void UpdateTargetGroup()
    {
        float density;
        float magnitude;
        switch (CurrentTarget())
        {
            case Target.T_2D:
                density = density2D;
                magnitude = magnitude2D;
                break;
            case Target.T_3D:
                density = density3D;
                magnitude = magnitude3D;
                break;
            case Target.T_GRASSLAND:
                density = densityGrassland;
                magnitude = magnitudeGrassland;
                break;
            default:
                return;
        }
        densitySlider.value = density;
        magnitudeSlider.value = magnitude;
    }

    public float Density(Target target)
    {
        switch (target)
        {
            case Target.T_2D:
                return density2D;
            case Target.T_3D:
                return density3D;
            case Target.T_GRASSLAND:
                return densityGrassland;
        }
        return 0f;
    }

    public float Magnitude(Target target)
    {
        switch (target)
        {
            case Target.NONE:
                break;
            case Target.T_2D:
                return magnitude2D;
            case Target.T_3D:
                return magnitude3D;
            case Target.T_GRASSLAND:
                return magnitudeGrassland;
        }
        return 0f;
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
