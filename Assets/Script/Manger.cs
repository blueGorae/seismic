using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Target { NONE, T_2D, T_3D, T_GRASSLAND };
public enum Wave { NONE, W_P, W_S, W_EARTHQUAKE };
public enum Density { NONE, D_DrySands, D_WetSands, D_Shale, D_Limestone, D_Granite, D_Basalt };
public class DensityVelocity
{
    public static DensityVelocity DrySands = new DensityVelocity(800, 300, 1.6f);
    public static DensityVelocity WetSands = new DensityVelocity(2750, 800, 2.0f);
    public static DensityVelocity Shale = new DensityVelocity(2500, 1125, 2.35f);
    public static DensityVelocity Limestone = new DensityVelocity(4750, 2650, 2.55f);
    public static DensityVelocity Granite = new DensityVelocity(5250, 2900, 2.6f);
    public static DensityVelocity Basalt = new DensityVelocity(5500, 3100, 2.9f);

    public float v_p { get; }
    public float v_s { get; }
    public float density { get; }
    DensityVelocity(float v_p, float v_s, float density)
    {
        this.v_p = v_p;
        this.v_s = v_s;
        this.density = density;
    }
};

public class Manger : MonoBehaviour
{
    public ToggleGroup targetGroup;
    public Toggle target2D;
    public Toggle target3D;
    public Toggle targetGrassland;
    public ToggleGroup densityGroup;
    public Toggle densityDrySands;
    public Toggle densityWetSands;
    public Toggle densityShale;
    public Toggle densityLimestone;
    public Toggle densityGranite;
    public Toggle densityBasalt;
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

    public bool ApplyGrassland()
    {
        return CurrentTarget() == Target.T_GRASSLAND;
    }

    public bool ApplyP()
    {
        return CurrentWave() == Wave.W_P;
    }

    public bool ApplyS()
    {
        return CurrentWave() == Wave.W_S;
    }

    public bool ApplyEarthquake()
    {
        return CurrentWave() == Wave.W_EARTHQUAKE;
    }

    public void UpdateDensityGroup()
    {
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
        float magnitude;
        switch (CurrentTarget())
        {
            case Target.T_2D:
                magnitude = magnitude2D;
                break;
            case Target.T_3D:
                magnitude = magnitude3D;
                break;
            case Target.T_GRASSLAND:
                magnitude = magnitudeGrassland;
                break;
            default:
                return;
        }
        magnitudeSlider.value = magnitude;
    }

    public DensityVelocity DensityVelocityValue()
    {
        switch (CurrentDensity())
        {
            case Density.D_DrySands:
                return DensityVelocity.DrySands;
            case Density.D_WetSands:
                return DensityVelocity.WetSands;
            case Density.D_Shale:
                return DensityVelocity.Shale;
            case Density.D_Limestone:
                return DensityVelocity.Limestone;
            case Density.D_Granite:
                return DensityVelocity.Granite;
            case Density.D_Basalt:
                return DensityVelocity.Basalt;
        }
        return null;
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

    public Density CurrentDensity()
    {
        foreach (var toggle in densityGroup.ActiveToggles())
        {
            if (toggle.isOn)
            {
                if (toggle == densityDrySands)
                {
                    return Density.D_DrySands;
                }
                else if (toggle == densityWetSands)
                {
                    return Density.D_WetSands;
                }
                else if (toggle == densityShale)
                {
                    return Density.D_Shale;
                }
                else if (toggle == densityLimestone)
                {
                    return Density.D_Limestone;
                }
                else if (toggle == densityGranite)
                {
                    return Density.D_Granite;
                }
                else if (toggle == densityBasalt)
                {
                    return Density.D_Basalt;
                }
            }
        }
        return Density.NONE;
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
