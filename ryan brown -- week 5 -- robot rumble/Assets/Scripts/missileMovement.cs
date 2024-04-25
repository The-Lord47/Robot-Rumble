using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileMovement : MonoBehaviour
{
    //----------------------PRIVATE VARIABLES----------------------
    float moveSpeed = 20f;
    float missileStrength = 50f;


    //----------------------UPDATE----------------------
    void Update()
    {
        //----------------------FORWARD MOVEMENT----------------------
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        //----------------------DESTROY OUT OF BOUNDS----------------------
        if (Mathf.Abs(transform.position.x) > 20 || Mathf.Abs(transform.position.z) > 20)
        {
            Destroy(gameObject);
        }
    }

    //----------------------ENEMY COLLISION SYSTEM----------------------
    private void OnCollisionEnter(Collision collision)
    {
        //applies force to the enemy and increases their maximum velocity, also destroys the missile on impact
        if (collision.gameObject.CompareTag("enemy"))
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            enemyRB.maxLinearVelocity = 20f;
            enemyRB.AddForce(transform.forward * missileStrength, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}
