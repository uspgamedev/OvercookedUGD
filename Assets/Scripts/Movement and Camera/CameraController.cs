using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private GameObject player;


    void Update(){

        transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, -5f, 0f), Mathf.Clamp(player.transform.position.y, 0f, 0f), transform.position.z);
        
    }
        
}
