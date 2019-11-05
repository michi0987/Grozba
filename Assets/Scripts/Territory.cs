using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Territory : MonoBehaviour
{

    Color32 hoverColor = new Color32(255, 0, 0, 255);

    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = hoverColor;
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
    }
}
