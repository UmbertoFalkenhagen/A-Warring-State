using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisibilityBarScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxDuration(float duration)
    {
        slider.maxValue = duration;
        slider.value = 0;

        fill.color = gradient.Evaluate(0f);
    }
    public void SetVisibility(float visibility)
    {
        slider.value = visibility;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
