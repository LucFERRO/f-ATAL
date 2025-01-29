using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YEET : MonoBehaviour
{
    public float throwForce;
    public float rotateForce;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(-Vector3.up * throwForce, ForceMode.Impulse);
        rb.AddTorque(randomInitRotate() * rotateForce, ForceMode.Impulse);
    }

    Vector3 randomInitRotate()
    {
        Vector3 randomVector = Vector3.zero;
        randomVector.x = Random.Range(0f,1f);
        randomVector.y = Random.Range(0f,1f);
        randomVector.z = Random.Range(0f,1f);
        return randomVector.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
