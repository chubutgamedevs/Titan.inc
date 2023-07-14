using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("mina"))
        {
            //other.transform.Translate(new Vector3(0, 0, 2), Space.World);
            other.gameObject.GetComponent<Bomba>().Parpadear();

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("mina"))
        {
            other.gameObject.GetComponent<Bomba>().NoParpadear();
           // other.transform.Translate(new Vector3(0, 0, -2), Space.World);
        }
    }
}
