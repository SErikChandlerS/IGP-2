using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
