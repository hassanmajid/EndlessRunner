using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform Player;
    [SerializeField] private float movespeed = 5f;
    [SerializeField] private float range = 7.5f;
    [SerializeField] private float attackRange = 0.5f;
    private bool isAttacking = false;

    public bool targetingPlayer = false;
    private healthComponent healthref;


    // Start is called before the first frame update    
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        healthref = GetComponent<healthComponent>();
    }
   
    // Update is called once per frame
    void Update()
    {
        if (!GameController.GamePaused)
        {
            if (Vector3.Distance(transform.position, Player.position) <= range)
            {

                transform.position = Vector3.MoveTowards(transform.position, Player.position, movespeed * Time.deltaTime);
                transform.LookAt(Player);
                targetingPlayer = true;


            }
            else
                targetingPlayer = false;

            if(targetingPlayer && Vector3.Distance(transform.position,Player.position)<=attackRange && !isAttacking)
            {
                isAttacking = true;
                StartCoroutine(attack());
            }
        }
    }
    private IEnumerator attack()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        Player.GetComponent<healthComponent>().updateHealth(-5f);
        isAttacking = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Weapon")
            {
            healthref.updateHealth(-10f);
        }
    }


}
