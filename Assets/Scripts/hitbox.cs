using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitbox : MonoBehaviour
{
    [SerializeField] Playercontroller playercontroller;


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name + "Hitbox");
        playercontroller.Colliders(other);
        
    }

}
