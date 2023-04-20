using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private GameObject ObjectToBeSpawned;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == true)
        {
            Instantiate(ObjectToBeSpawned, SpawnPoint.position, Quaternion.identity);
        }
        Debug.Log(collision);
    }
}
