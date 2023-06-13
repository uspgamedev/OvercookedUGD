using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SecretRecipe : MonoBehaviour
{

    public event EventHandler foundPage;
    public static SecretRecipe Instance { get; private set; }

    void Start(){
        Instance = this;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        foundPage?.Invoke(this, EventArgs.Empty);
        Destroy(this.gameObject);
    }
}
