using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 5f;
    public float ramImpulse = 10f;
    public float maxSpeed = 10f;
    float timer;
    bool canBoost = true;
    public Transform focalPoint;
    public float animationSpeedThreshold = 1f;
    public bool hasShield;
    public bool hasMissile;
    public float shieldStrength = 20f;
    public GameObject shieldIndicator;
    public GameObject missilePrefab;
    public Transform missiles;
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

        if (Input.GetMouseButtonDown(0) && canBoost == true)
        {
            rb.AddForce(focalPoint.forward * ramImpulse * forwardInput, ForceMode.Impulse);
            rb.AddForce(focalPoint.right * ramImpulse * horizontalInput, ForceMode.Impulse);
            canBoost = false;
        }
        
        if(canBoost == false)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                timer = 0;
                canBoost = true;
            }
        }


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


        if (Input.GetKeyDown(KeyCode.Space) && hasMissile == true)
        {
            Instantiate(missilePrefab, transform.position + new Vector3(1, 0, 1).normalized, Quaternion.Euler(0, 45, 0), missiles);
            Instantiate(missilePrefab, transform.position + new Vector3(-1, 0, 1).normalized, Quaternion.Euler(0, -45, 0), missiles);
            Instantiate(missilePrefab, transform.position + new Vector3(-1, 0, -1).normalized, Quaternion.Euler(0, -135, 0), missiles);
            Instantiate(missilePrefab, transform.position + new Vector3(1, 0, -1).normalized, Quaternion.Euler(0, 135, 0), missiles);
        }


        shieldIndicator.SetActive(hasShield);
        shieldIndicator.transform.position = transform.position;
        shieldIndicator.transform.rotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("shield"))
        {
            hasShield = true;
            Destroy(other.gameObject);
            StartCoroutine(ramBoostCooldown());
        }
        if (other.CompareTag("missileIcon"))
        {
            hasMissile = true;
            Destroy(other.gameObject);
            StartCoroutine(missileCooldown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("enemy") && hasShield)
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 player_enemy_vector = (collision.transform.position - transform.position);
            enemyRB.maxLinearVelocity = 50f;
            enemyRB.AddForce(player_enemy_vector.normalized * shieldStrength, ForceMode.Impulse);
        }
    }

    IEnumerator ramBoostCooldown()
    {
        yield return new WaitForSeconds(5);
        hasShield = false;
    }

    IEnumerator missileCooldown()
    {
        yield return new WaitForSeconds(5);
        hasMissile = false;
    }
}
