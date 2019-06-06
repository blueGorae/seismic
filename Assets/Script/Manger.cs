using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manger : MonoBehaviour
{
    public Slider densitySlider;
    public Text densityValue;
    public Slider magnitudeSlider;
    public Text magnitudeValue;

    // Start is called before the first frame update
    void Start()
    {
        densityValue.text = densitySlider.value.ToString();
        magnitudeValue.text = magnitudeSlider.value.ToString();
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
}
