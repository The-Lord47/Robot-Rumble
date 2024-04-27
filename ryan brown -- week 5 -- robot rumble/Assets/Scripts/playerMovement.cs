using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //----------------------PUBLIC VARIABLES----------------------
    [Header("Movement")]
    public float speed = 5f;
    public float ramImpulse = 10f;
    public float maxSpeed = 10f;

    [Header("Object References")]
    public Transform focalPoint;
    public GameObject shieldIndicator;
    public GameObject missileIndicator;
    public GameObject missilePrefab;
    public Transform missiles;

    [Header("Animation")]
    public float animationSpeedThreshold = 1f;

    [Header("SFX")]
    public GameObject musicStart;
    public GameObject musicLoop;
    public GameObject clashSFX;
    public GameObject gunSFX;
    public GameObject shieldReverb;
    public AudioClip[] clashSFXs;
    public AudioClip gunClip;
    
    [Header("Passable Variables")]
    public bool hasShield;
    public bool hasMissile;
    public float shieldStrength = 20f;
    public int lives = 3;
    public float timer;

    //----------------------PRIVATE VARIABLES----------------------
    Animator _animator;
    Rigidbody rb;
    bool canBoost = true;

    //----------------------START----------------------
    void Start()
    {
        //----------------------REFERENCES----------------------
        rb = GetComponent<Rigidbody>();
        rb.maxLinearVelocity = maxSpeed;
        _animator = GetComponent<Animator>();
    }

    //----------------------UPDATE----------------------
    void Update()
    {
        //----------------------MOVEMENT----------------------
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.AddForce(focalPoint.forward * speed * forwardInput);
        rb.AddForce(focalPoint.right * speed * horizontalInput);

        //----------------------RAM SYSTEM----------------------
        if (Input.GetMouseButtonDown(0) && canBoost == true)
        {
            rb.AddForce(focalPoint.forward * ramImpulse * forwardInput, ForceMode.Impulse);
            rb.AddForce(focalPoint.right * ramImpulse * horizontalInput, ForceMode.Impulse);
            canBoost = false;
        }
        //----------------------RAM TIMER----------------------
        if (canBoost == false)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                timer = 0;
                canBoost = true;
            }
        }

        //----------------------ANIMATION CONTROLLER----------------------
        if (Mathf.Abs(rb.velocity.x) > animationSpeedThreshold || Mathf.Abs(rb.velocity.z) > animationSpeedThreshold)
        {
            _animator.SetBool("Roll_Anim", true);
        }
        else
        {
            rb.velocity = Vector3.zero;
            transform.rotation = new Quaternion(0,180,0,0);
            _animator.SetBool("Roll_Anim", false);
        }

        //----------------------OUT OF BOUNDS RESPAWN SYSTEM----------------------
        if (transform.position.y < -2)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.rotation = new Quaternion(0, 180, 0, 0);
            transform.position = new Vector3(Random.Range(-10,10), 0.147f, Random.Range(-5,5));
            --lives;
        }
        //----------------------CEILING----------------------
        if (transform.position.y > 2)
        {
            transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        }

        //----------------------MISSILE SPAWNING SYSTEM----------------------
        if (Input.GetKeyDown(KeyCode.Space) && hasMissile == true)
        {
            gunSFX.GetComponent<AudioSource>().PlayOneShot(gunClip);
            Instantiate(missilePrefab, transform.position + new Vector3(1, 0, 1).normalized, Quaternion.Euler(0, 45, 0), missiles);
            Instantiate(missilePrefab, transform.position + new Vector3(-1, 0, 1).normalized, Quaternion.Euler(0, -45, 0), missiles);
            Instantiate(missilePrefab, transform.position + new Vector3(-1, 0, -1).normalized, Quaternion.Euler(0, -135, 0), missiles);
            Instantiate(missilePrefab, transform.position + new Vector3(1, 0, -1).normalized, Quaternion.Euler(0, 135, 0), missiles);
        }

        //----------------------POWERUP INDICATOR SYSTEM----------------------
        shieldIndicator.SetActive(hasShield);
        shieldIndicator.transform.position = transform.position;
        shieldIndicator.transform.rotation = transform.rotation;

        missileIndicator.SetActive(hasMissile);
        missileIndicator.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //----------------------SHIELD PICKUP----------------------
        if (other.CompareTag("shield"))
        {
            hasShield = true;
            clashSFX.GetComponent<AudioLowPassFilter>().enabled = true;
            musicStart.GetComponent<AudioLowPassFilter>().enabled = true;
            musicLoop.GetComponent<AudioLowPassFilter>().enabled = true;
            Destroy(other.gameObject);
            StartCoroutine(shieldCooldown());
        }
        //----------------------MISSILE PICKUP----------------------
        if (other.CompareTag("missileIcon"))
        {
            hasMissile = true;
            Destroy(other.gameObject);
            StartCoroutine(missileCooldown());
        }
        //----------------------REPAIR PICKUP----------------------
        if (other.CompareTag("repair"))
        {
            if(lives < 3)
            {
                ++lives;
            }
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //----------------------ENEMY COLLISION----------------------
        if (collision.gameObject.CompareTag("enemy"))
        {
            clashSFX.GetComponent<AudioSource>().PlayOneShot(clashSFXs[Random.Range(0, clashSFXs.Length)]);
        }
        //----------------------ENEMY COLLISION W/ SHIELD----------------------
        if (collision.gameObject.CompareTag("enemy") && hasShield)
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 player_enemy_vector = (collision.transform.position - transform.position);
            enemyRB.maxLinearVelocity = 50f;
            enemyRB.AddForce(player_enemy_vector.normalized * shieldStrength, ForceMode.Impulse);
        }
    }

    //----------------------POWERUP COOLDOWNS----------------------
    IEnumerator shieldCooldown()
    {
        yield return new WaitForSeconds(5);
        hasShield = false;
        clashSFX.GetComponent<AudioLowPassFilter>().enabled = false;
        musicStart.GetComponent<AudioLowPassFilter>().enabled = false;
        musicLoop.GetComponent<AudioLowPassFilter>().enabled = false;
        //shieldReverb.SetActive(false);
    }

    IEnumerator missileCooldown()
    {
        yield return new WaitForSeconds(5);
        hasMissile = false;
    }
}
