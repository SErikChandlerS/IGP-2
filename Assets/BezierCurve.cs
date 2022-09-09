using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    // The task asks to implement quadratic Bezier Curve.
    // However, there is support for higher order Bezier Curves.

    public Vector3[] Points;
    public float Speed;
    private int Degree;
    private float Position = 0.0f;
    private Rigidbody _rigidbody; 
    private Collider _collider; 

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        Degree = Points.Length - 1;
    }


    void Update()
    {
        if (_rigidbody.isKinematic) {
            Vector3 x = transform.position;
            transform.position = CalculatePosition(Position);
            Position += Speed / Vector3.Magnitude(CalculateVelocity(Position)); // Normalize the velocity
            if (Position >= 1.0f) // Loop the curve
                Position -= 1.0f;
        }
    }
    
    public void OnTriggerEnter(Collider other) {
        _collider.isTrigger = false;
        _rigidbody.isKinematic = false;
    }

    float Factorial(int n) {
        float Result = 1.0f;
        for (int i = 2; i <= n; i++) {
            Result *= i;
        }
        return Result;
    }

    float Combinations(int n, int k) {
        float Result = Factorial(n) / (Factorial(k) * Factorial(n - k));
        return Result;
    }

    // B_n(t) = sum [i = 0..n] {C(n, i) * t^i * (1 - t)^(n - i) * P_i}
    Vector3 CalculatePosition(float t) {
        Vector3 Result = new Vector3(0.0f, 0.0f, 0.0f);
        for (int i = 0; i <= Degree; i++) {
            float Coefficient = Combinations(Degree, i) * Mathf.Pow(t, i) * Mathf.Pow(1 - t, Degree - i);
            Result += Coefficient * Points[i];
        }
        return Result;
    }

    // d(B_n(t)) / dt = sum [i = 0..n] {C(n, i) * (- (n - i) * t^i * (1 - t)^(n - i - 1) + i * t^(i - 1) * (1 - t)^(n - 1)) * P_i}
    Vector3 CalculateVelocity(float t) {
        Vector3 Result = new Vector3(0.0f, 0.0f, 0.0f);
        for (int i = 0; i <= Degree; i++) {
            float Coefficient = Combinations(Degree, i) * (
                - (Degree - i) * Mathf.Pow(t, i) * (Degree - i - 1 >= 0 ? Mathf.Pow(1 - t, Degree - i - 1) : 0) + 
                i * (i - 1 >= 0 ? Mathf.Pow(t, i - 1) : 0) * Mathf.Pow(1 - t, Degree - i)
            );
            Result += Coefficient * Points[i];
        }
        return Result;
    }
}
