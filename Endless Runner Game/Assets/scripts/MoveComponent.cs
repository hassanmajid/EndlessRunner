using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float objectDistance = -40f;
    [SerializeField] private float despawnDistance = -110f;

    private healthComponent healthref;
    private GameObject player;
    private EnemyController enemy;

    private bool canSpawnGround = true;

    private Rigidbody rg;

    private void Start()
    {
        rg = GetComponent<Rigidbody>();

        if(GetComponent<healthComponent>()!=null)
            healthref = GetComponent<healthComponent>();

        player = GameObject.FindGameObjectWithTag("Player");

        if(GetComponent<EnemyController>()!=null)   
            enemy = GetComponent<EnemyController>();
    }

    private void Update()
    {
        if (enemy != null && !enemy.targetingPlayer)
            transform.position += -transform.forward * speed * Time.deltaTime;
        else if (enemy != null && enemy.targetingPlayer)
            transform.position += Vector3.zero;
        else
            transform.position += -transform.forward * speed * Time.deltaTime;

        if(transform.position.z<player.transform.position.z - 10f && enemy!=null)
        {
            healthref.resetHealth();
            gameObject.SetActive(false);
        }


        if (transform.position.z <= objectDistance && transform.tag=="ground" && canSpawnGround)
        {

            objectspawner.Instance.SpawnGround();
            canSpawnGround = false;

        }
        if(transform.position.z <= despawnDistance)
        {
            canSpawnGround = true;
            gameObject.SetActive(false);

        }

    }


}
