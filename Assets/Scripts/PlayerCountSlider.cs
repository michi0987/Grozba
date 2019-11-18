using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCountSlider : MonoBehaviour
{
    TextMeshProUGUI TextPro;

    void Start()
    {
        TextPro = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void SetSliderValue(float sliderValue) {
        TextPro.text = Mathf.Round(sliderValue).ToString();
        GameManager.Instance.PlayerCount = (int)sliderValue;
    }
}
