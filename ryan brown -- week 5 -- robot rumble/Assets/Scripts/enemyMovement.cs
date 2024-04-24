using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public float moveForce;
    private Rigidbody rb;
    private GameObject player;
    Animator _anim;

    private spawnManager _enemySpawnerScript;
    private playerMovement _playerScript;

    // Start is called before the first frame update
    void Start()
    {
        _enemySpawnerScript = GameObject.FindGameObjectWithTag("enemySpawner").GetComponent<spawnManager>();
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<playerMovement>();

        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
        rb.maxLinearVelocity = 8;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player_enemy_vector = player.transform.position - transform.position;

        rb.AddForce(player_enemy_vector.normalized * moveForce);

        _anim.SetFloat("anim_run_speed", rb.velocity.magnitude);
        _anim.SetFloat("anim_player_proximity", player_enemy_vector.magnitude);

        transform.rotation = Quaternion.LookRotation(player_enemy_vector.normalized);

        if (transform.position.y < -2 || Mathf.Abs(transform.position.x) > 20 || Mathf.Abs(transform.position.z) > 20)
        {
            _enemySpawnerScript.spawnCount--;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.CompareTag("Player") && _playerScript.hasShield) || collision.gameObject.CompareTag("missile"))
        {
            StartCoroutine(throttleEnemyMaxVelocity());
        }
    }

    IEnumerator throttleEnemyMaxVelocity()
    {
        yield return new WaitForSeconds(2);
        rb.maxLinearVelocity = 8;
    }
}
