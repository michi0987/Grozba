using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGBSliderManager : MonoBehaviour
{

    private PlayerConfig pc;
    public List<Slider> sliders = new List<Slider>();

    void Start() {
        pc = transform.parent.Find("PlayerConfig").GetComponent<PlayerConfig>();
        foreach (Transform child in transform) {
            sliders.Add(child.gameObject.GetComponent<Slider>());
        }
    }
}
