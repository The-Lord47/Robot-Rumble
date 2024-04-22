using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 5f;
    public float maxSpeed = 10f;
    public Transform focalPoint;
    public float animationSpeedThreshold = 1f;
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxLinearVelocity = maxSpeed;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.AddForce(focalPoint.forward * speed * forwardInput);
        rb.AddForce(focalPoint.right * speed * horizontalInput);

        if(Mathf.Abs(rb.velocity.x) > animationSpeedThreshold || Mathf.Abs(rb.velocity.z) > animationSpeedThreshold)
        {
            _animator.SetBool("Roll_Anim", true);
        }
        else
        {
            rb.velocity = Vector3.zero;
            transform.rotation = new Quaternion(0,180,0,0);
            _animator.SetBool("Roll_Anim", false);
        }


        if(transform.position.y < -2)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.rotation = new Quaternion(0, 180, 0, 0);
            transform.position = new Vector3(Random.Range(-10,10), 0.147f, Random.Range(-5,5));
        }
    }
}
