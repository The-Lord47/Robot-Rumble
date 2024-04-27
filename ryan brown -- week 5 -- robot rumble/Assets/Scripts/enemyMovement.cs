using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    //----------------------PRIVATE VARIABLES----------------------
    Rigidbody rb;
    GameObject player;
    Animator _anim;
    playerMovement _playerScript;
    UIManager _UIScript;
    spawnManager _spawnScript;

    //----------------------PUBLIC VARIABLES----------------------
    public float moveForce;
    public bool moveDisabled;

    //----------------------START----------------------
    void Start()
    {
        //----------------------REFERNCES----------------------
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<playerMovement>();
        _UIScript = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        _spawnScript = GameObject.FindGameObjectWithTag("enemySpawner").GetComponent<spawnManager>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();

        //sets max velocity to 8mps
        rb.maxLinearVelocity = 8;
    }

    //----------------------UPDATE----------------------
    void Update()
    {
        //----------------------MOVEMENT----------------------
        //gets a vector from the enemy to the player
        Vector3 player_enemy_vector = player.transform.position - transform.position;

        if (!moveDisabled)
        {
            //adds force in the direction of the player
            rb.AddForce(player_enemy_vector.normalized * moveForce);
        }

        //----------------------ANIMATIONS----------------------
        _anim.SetFloat("anim_run_speed", rb.velocity.magnitude);
        _anim.SetFloat("anim_player_proximity", player_enemy_vector.magnitude);

        //----------------------ROTATION----------------------
        transform.rotation = Quaternion.LookRotation(player_enemy_vector.normalized);

        //----------------------OUT OF BOUNDS SYSTEM----------------------
        if (transform.position.y < -2 || Mathf.Abs(transform.position.x) > 20 || Mathf.Abs(transform.position.z) > 20 || transform.position.y > 10)
        {
            Destroy(gameObject);
            _UIScript.score += _spawnScript.prestigeMultiplier;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.CompareTag("Player") && _playerScript.hasShield) || collision.gameObject.CompareTag("missile"))
        {
            moveDisabled = true;
            StartCoroutine(throttleEnemyMaxVelocity());
        }
    }

    IEnumerator throttleEnemyMaxVelocity()
    {
        yield return new WaitForSeconds(1);
        rb.maxLinearVelocity = 8;
        moveDisabled = false;
    }
}
