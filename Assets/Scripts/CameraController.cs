using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private GameObject player;


    void Update(){

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(player.transform.position.y, 0f, 12f), transform.position.z);
        
    }
        
}
