using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRespawnPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("Player"))
        {
            GameManager.Instance.platformingRespawnPoint = transform.position;
        }
    }
}
