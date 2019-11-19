using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerConfig : MonoBehaviour
{
    private GameManager GM = GameManager.Instance;

    public List<GameObject> players = new List<GameObject>();

    void Start() {
        foreach (Transform child in transform) {
            players.Add(child.gameObject);
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
}
