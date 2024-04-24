using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileMovement : MonoBehaviour
{
    public float moveSpeed = 20f;
    float missileStrength = 50f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if(Mathf.Abs(transform.position.x) > 20 || Mathf.Abs(transform.position.z) > 20)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            enemyRB.maxLinearVelocity = 20f;
            enemyRB.AddForce(transform.forward * missileStrength, ForceMode.Impulse);
        }
    }
}
