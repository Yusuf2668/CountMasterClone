using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] PlayerType playerType;

    GameObject[] playerList;
    GameObject closestPlayer;
    float distanceToClosestPlayer;
    float distanceToPlayer;

    public bool attack;

    Animator myAnimator;

    void Start()
    {
        closestPlayer = null;
        attack = false;
        myAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        if (attack)
        {
            FindTheClosestPlayer();
            transform.LookAt(closestPlayer.transform.position);
            transform.Translate(Vector3.forward * Time.deltaTime * playerType.runSpeed/3);
        }
    }

    void FindTheClosestPlayer()
    {
        distanceToClosestPlayer = Mathf.Infinity;
        closestPlayer = null;
        playerList = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playerList.Length; i++)
        {
            distanceToPlayer = (playerList[i].transform.position - transform.position).sqrMagnitude;
            if (distanceToPlayer < distanceToClosestPlayer)
            {
                distanceToClosestPlayer = distanceToPlayer;
                closestPlayer = playerList[i];
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
    }
}
