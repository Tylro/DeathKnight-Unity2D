using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject[] maps;

    Bench bench;
    // Start is called before the first frame update
    private void OnEnable()
    {
        bench = FindObjectOfType<Bench>();
        if(bench != null)
        {
            if(bench.interacted)
            {
                UpdateMap();
            }
        }
    }
    void UpdateMap()
    {
        Debug.Log("updated");
        var savedScenes = SaveData.Instance.sceneNames;

        for(int i = 0; i < maps.Length; i++)
        {
            if(savedScenes.Contains("Cave_" + (i + 1))) //this is i + 1 as arrays start from 0
            {
                maps[i].SetActive(true);
            }
            else
            {
                maps[i].SetActive(false);
            }
        }
    }
}
