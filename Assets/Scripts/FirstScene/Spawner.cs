using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private GameObject ObjectToBeSpawned;
    private bool IsInsideTrigger = false;

    private void Update()
    {
        Spawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            IsInsideTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            IsInsideTrigger = false;
        }
    }
    private void Spawn()
    {
        if (GameManager.Instance.player.GetComponent<GrabObjects>().GrabbedObject == null && IsInsideTrigger == true && (Input.GetKeyDown(KeyCode.E)))
        {
            Instantiate(ObjectToBeSpawned, SpawnPoint.position, Quaternion.identity);
        }
    }
}
