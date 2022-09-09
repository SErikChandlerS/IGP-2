using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDoll : MonoBehaviour
{
    public bool Damaged = false;
    private Rigidbody[] rigidbodies;

    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies){
            rb.isKinematic = true;
        }
    }
    
    public void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
        foreach (Rigidbody rb in rigidbodies){
            rb.isKinematic = false;
        }
        Damaged = true;
    }
}
