using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTagController : MonoBehaviour
{
    TMPro.TextMeshPro mText;

    private void Start()
    {
        mText = this.gameObject.GetComponentInChildren<TMPro.TextMeshPro>();
    }

    public void setNumber(int number)
    {
        string text = number.ToString();
        mText.SetText(text);
    }

    public int getNumber()
    {
        return int.Parse(mText.text);
    }
}
