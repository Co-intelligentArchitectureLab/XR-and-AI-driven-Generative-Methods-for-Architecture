using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class colliderDetector : MonoBehaviour
{

    public bool isCollided = false;
    // Start is called before the first frame update

    public void OnTriggerEnter(Collider other)
    {

    if (other.CompareTag("target"))
        {
            isCollided = true;
        }
    }
}