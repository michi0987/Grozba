using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]

public class Territory : MonoBehaviour
{

    public Color32 hoverColor = new Color32(255, 0, 0, 255);
    public Territory[] nodes;
    public string objectName;
    TextMesh textHelper;
    protected Transform objTransform;



    void Start()
    {
        objectName = gameObject.name;
        Debug.Log(objectName);
        objTransform = GetComponent<SpriteRenderer>().transform;

    }

    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = hoverColor;
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
    }
    void OnGui()
    {
        GUI.Label(new Rect(objTransform.position.x+1, objTransform.position.y, 100, 20), "Hello WorlDDDDDDDDDDDDDd!");
    }
}
