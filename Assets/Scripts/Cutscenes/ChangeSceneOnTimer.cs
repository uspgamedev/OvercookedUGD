using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeSceneOnTimer : MonoBehaviour
{
    [SerializeField] private float changeTime;
    [SerializeField] private string sceneName;

    void Update(){
        changeTime -= Time.deltaTime;
        if(changeTime <= 0) SceneManager.LoadScene(sceneName);
    }


}
