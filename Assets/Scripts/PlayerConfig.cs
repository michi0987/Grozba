using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerConfig : MonoBehaviour
{
    private GameManager GM = GameManager.Instance;

    private RGBSliderManager rgbsl;

    public List<GameObject> players = new List<GameObject>();

    public List<Toggle> toggles = new List<Toggle>();
    public List<Image> colors = new List<Image>();

    private int lastClickedToggle = -1;

    void Start() {
        rgbsl = transform.parent.Find("RGBSliders").GetComponent<RGBSliderManager>();
        foreach (Transform child in transform) {
            players.Add(child.gameObject);
            toggles.Add(child.Find("PlayerColor").GetComponent<Toggle>());
            colors.Add(child.Find("PlayerColor/Image").GetComponent<Image>());
        }
    }

    // Robimy tak bo inaczej trzeba się bawić w mutexy
    void Update() {
        for (int i = 0; i < toggles.Count; i++) {
            ColorBlock cb = toggles[i].colors;
            if (toggles[i].isOn) {
                if (lastClickedToggle != i) {
                    rgbsl.sliders[0].value = 255 * colors[i].color.r;
                    rgbsl.sliders[1].value = 255 * colors[i].color.g;
                    rgbsl.sliders[2].value = 255 * colors[i].color.b;
                    lastClickedToggle = i;
                } else {
                    colors[i].color = new Color(rgbsl.sliders[0].value / 255, rgbsl.sliders[1].value / 255, rgbsl.sliders[2].value / 255);
                }
                cb.normalColor = new Color(0, 0, 0, 1);
                cb.highlightedColor = new Color(0, 0, 0, 1);
                cb.pressedColor = new Color(0, 0, 0, 1);
                cb.selectedColor = new Color(0, 0, 0, 1);
            } else {
                cb.normalColor = new Color(0, 0, 0, 0.549f);
                cb.highlightedColor = new Color(0, 0, 0, 0.7f);
                cb.pressedColor = new Color(0, 0, 0, 0.7f);
                cb.selectedColor = new Color(0, 0, 0, 0.7f);
            }
            toggles[i].colors = cb;
        }
    }
    public void SetSliderValue(float sliderValue) {
        int i = 1;
        foreach (var pc in players) {
            if (i <= (int)sliderValue) {
                pc.SetActive(true);
            }else{
                pc.SetActive(false);
            }
            i++;
        }
    }

    public void SetPlayerNames() {
        int i = 1;
        foreach (var pc in players) {
            string name = pc.transform.Find("PlayerName").GetComponent<TMP_InputField>().text;
            if (i <= GM.PlayerCount && name != "") {
                GM.playerNames.Add(name);
            }
            i++;
        }
    }

    public bool ValidatePlayerNames() {
        int i = 1;
        foreach (var pc in players) {
            string name = pc.transform.Find("PlayerName").GetComponent<TMP_InputField>().text;
            if (i <= GM.PlayerCount && name == "") {
                return false;
            }
            i++;
        }
        return true;
    }

    public void SetPlayerColors() {
        int i = 1;
        foreach (var pc in players) {
            Color color = pc.transform.Find("PlayerColor/Image").GetComponent<Image>().color;
            if (i <= GM.PlayerCount && name != "") {
                GM.playerColors.Add(color);
            }
            i++;
        }
    }
}
