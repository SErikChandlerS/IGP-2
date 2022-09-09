using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public List<GameObject> Targets;
    public GameObject ProjectilePrefab;
    public float ProjectileVelocity;
    public float ReloadTime;
    public Vector3 ShootDirection;
    public float ShootAngle;
    private float LastShootTime = 0.0f;
    
    void Start()
    {
        LastShootTime = (float)Time.timeAsDouble;
    }

    void Update()
    {
        if ((float)Time.timeAsDouble >= LastShootTime + ReloadTime) {
            LastShootTime = (float)Time.timeAsDouble;
            GameObject CurrentTarget = null;
            foreach (GameObject GO in Targets) {
                if (GO && 
                    Vector3.Dot(ShootDirection, 
                                Vector3.Normalize(GO.transform.position - transform.position)
                    ) > Mathf.Cos(Mathf.Deg2Rad * ShootAngle)) {
                    if (GO.GetComponent<RagDoll>() && 
                        GO.GetComponent<RagDoll>().Damaged)
                        continue; // Do not shoot at damaged ragdoll
                    CurrentTarget = GO;
                    break;
                }
            }
            if (!CurrentTarget) {
                Debug.Log("No more targets");
            }
            else {
                Vector3 TargetPosition = CurrentTarget.transform.position;
                Vector3 Rotation = Vector3.Normalize(TargetPosition - transform.position);
                Vector3 ModelRotation = new Vector3(90.0f, 0.0f, 0.0f);
                transform.rotation = Quaternion.LookRotation(Rotation, Vector3.up) * Quaternion.Euler(ModelRotation);
                GameObject i = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
                Rigidbody _rigidbody = i.GetComponent<Rigidbody>();
                _rigidbody.velocity = Vector3.Normalize(TargetPosition - transform.position) * ProjectileVelocity;
            }
        }
    }
}
