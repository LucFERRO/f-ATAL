using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabbedBehavior : MonoBehaviour
{

    public GameObject target;
    private Rigidbody rb;

    private Vector3 direction;

    private Vector3 Rotation;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectsWithTag("Cursor")[0]; // récupère le curseur
        GetComponent<GrabbedBehavior>().enabled = false;
    }

    private void OnEnable()
    {

        Rotation = Random.insideUnitSphere.normalized * 5;
        rb.useGravity = false;
        rb.AddTorque(Rotation, ForceMode.Impulse);
        
    }

    private void OnDisable()
    {
        rb.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        direction =  target.transform.position - transform.position;

        if (Vector3.Distance(transform.position,target.transform.position) >= 10) //rb.velocity = rb.velocity.magnitude * direction.normalized
        {
            rb.velocity = direction * 5;
        }
        else
        {
            rb.AddForce(direction.normalized * 6, ForceMode.Force);
        }

        rb.AddTorque(Rotation, ForceMode.Impulse);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Dice")
        {
            rb.velocity = ( transform.position - collision.transform.position).normalized * 10;
            
        }
    }
}
