using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameMenu : MonoBehaviour
{
    public void StartNewGame() {
        PlayerConfig pc = transform.Find("PlayerCountSlider/PlayerConfig").GetComponent<PlayerConfig>();
        if (pc.ValidatePlayerNames()) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            pc.SetPlayerNames();
        } 
    }

    public void SetTerritoryAssigment(int index) {
        GameManager.Instance.TerritoryAssignment = (GameManager.AssignmentType)index;
    }
}
