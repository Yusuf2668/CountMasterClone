using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    [SerializeField] float magnetForce;

    public GameObject player;
    public List<Rigidbody> playerBodys;

    bool magnetOn = false;
    float magnetTime;
    int spawnCount = 0;
    private void Start()
    {
        magnetTime = 1f;
    }
    private void Update()
    {
        if (magnetOn)
        {
            RunnerMagnet();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sum"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.GetComponent<DoorController>().otherDoor.GetComponent<DoorController>().number = 0;
            for (int i = 1; i < other.gameObject.GetComponent<DoorController>().number; i++)
            {
                CreateRunner();
            }
            StartCoroutine(RunnerMagnetActive());
        }

        if (other.gameObject.CompareTag("Multiply"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.GetComponent<DoorController>().otherDoor.GetComponent<DoorController>().number = 0;
            spawnCount = (other.gameObject.GetComponent<DoorController>().number - 1) * playerBodys.Count;
            for (int i = 1; i < spawnCount; i++)
            {
                CreateRunner();
            }
            StartCoroutine(RunnerMagnetActive());
        }
    }
    void CreateRunner()
    {
        GameObject ad = Instantiate(player, transform.position, transform.rotation);
        ad.transform.SetParent(transform);
        playerBodys.Add(ad.GetComponent<Rigidbody>());
    }

    IEnumerator RunnerMagnetActive()
    {
        magnetOn = true;
        yield return new WaitForSeconds(magnetTime);
        magnetOn = false;
    }

    void RunnerMagnet()
    {
        for (int i = 0; i < playerBodys.Count; i++)
        {
            playerBodys[i].AddForce((transform.position - playerBodys[i].position) * Time.deltaTime * magnetForce);
        }
    }
}
