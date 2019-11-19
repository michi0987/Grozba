using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RGBSlider : MonoBehaviour
{
    private TextMeshProUGUI TextPro;

    void Start() {
        TextPro = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void SetSliderValue(float sliderValue) {
        TextPro.text = Mathf.Round(sliderValue).ToString();
    }
}
