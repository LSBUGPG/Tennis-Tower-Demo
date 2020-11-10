using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAnimate : MonoBehaviour
{

    public Slider slider;
    private float minValue = 0;
    private float maxValue = 1f;
    private float sliderValue = 0;
    public float sliderIncrementValue = 0.01f;
    private int direction = 1;
    bool sliderCanAnimate = false;

    // Start is called before the first frame update
    void Start()
    {
        sliderCanAnimate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (sliderCanAnimate)
        {
            GoSlider();
        }
    }

    public void Animateslider()
    {
        sliderCanAnimate = true;
        slider.value = 0;
        sliderValue = 0;
    }

    public void StopSlider()
    {
        sliderCanAnimate = false;
        slider.value = 0;
        sliderValue = 0;
    }

    public float GetValue()
    {
        return slider.value;
    }

    void GoSlider()
    {
        sliderValue += sliderIncrementValue * direction;
        if (sliderValue > maxValue || sliderValue < minValue)
        {
            direction *= -1;
        }
        slider.value = sliderValue;
    }
}
