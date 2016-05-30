using UnityEngine;
using System.Collections;

public class grav : MonoBehaviour
{
    Rigidbody rg;
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        rg.AddForce(Vector3.down*9.81f, ForceMode.Force);
    }
}
