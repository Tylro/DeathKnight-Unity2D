using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    public GameObject textDesc;
    // Start is called before the first frame update
    void Start()
    {
        textDesc.SetActive(false);
    }

    public void Show()
    {
        textDesc.SetActive(true);
    }
    public void Hide()
    {
        textDesc.SetActive(false);
    }
}
