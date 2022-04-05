using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] PlayerType playerType;
    public bool attack;

    private PlayerMagnet playerMagnet;

    Animator myAnimator;

    void Start()
    {
        playerMagnet = GameObject.FindObjectOfType<PlayerMagnet>();
        myAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        if (attack)
        {
            transform.LookAt(playerMagnet.transform.position);
            transform.Translate(Vector3.right * Time.deltaTime * playerType.runSpeed, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.gameObject.SetActive(false);
        }
    }
}
