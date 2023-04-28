using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu1 : MonoBehaviour
{
   public void PlayGame ()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 4);
    }

    public void QuitGame ()
    {
        Debug.Log ("se n der erro foi milagre");
        Application.Quit();
    }

   

}
