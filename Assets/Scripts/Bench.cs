using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bench : MonoBehaviour
{
    bool inRange = false;
    public bool interacted;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && inRange)
        {
            interacted = true;

            SaveData.Instance.benchSceneName = SceneManager.GetActiveScene().name;
            SaveData.Instance.benchPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            SaveData.Instance.SaveBench();
            SaveData.Instance.SavePlayerData();

            Debug.Log("benched");
        }
    }

    void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player")) inRange = true;
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
