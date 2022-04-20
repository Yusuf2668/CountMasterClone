using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerType playerData;
    public bool rush;

    float minZBound;
    float maxZBound;

    [SerializeField] LayerMask ground;
    [SerializeField] Vector3 offset;

    private PlayerMagnet playerMagnet;
    private Vector3 currentTouchDeltaPosition;

    [HideInInspector] public List<GameObject> enemyList;
    GameObject[] enemies;
    void Start()
    {
        rush = true;
        playerMagnet = GameObject.FindObjectOfType<PlayerMagnet>();
        Cursor.lockState = CursorLockMode.Locked;
        maxZBound = 4.53f;
        minZBound = -4.53f;
    }

    void Update()
    {

        if (rush)
        {
            transform.Translate(Vector3.left * Time.deltaTime * playerData.runSpeed, Space.World);
            MovePlayer();
        }
        else
        {
            CheckEnemiesLive();
        }
        CheckZBound();
    }

    void MovePlayer()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z + Input.GetAxis("Mouse X") * Time.deltaTime * playerData.moveSpeed, minZBound, maxZBound));
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                currentTouchDeltaPosition = Input.GetTouch(0).deltaPosition;
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z + currentTouchDeltaPosition.x * Time.deltaTime * playerData.moveSpeed, minZBound, maxZBound));
            }
        }
    }
    void CheckEnemiesLive()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            rush = true;
        }
    }
    void CheckZBound()
    {
        minZBound = transform.position.z;
        maxZBound = transform.position.z;

        for (int i = 0; i < playerMagnet.playerBodys.Count; i++)
        {
            if (transform.GetChild(i).transform.position.z < minZBound)
            {
                minZBound = transform.GetChild(i).transform.position.z;
            }
            if (transform.GetChild(i).transform.position.z > maxZBound)
            {
                maxZBound = transform.GetChild(i).transform.position.z;
            }
        }

        if (!Physics.Raycast(new Vector3(transform.position.x, 3f, minZBound - 1f), new Vector3(0f, -1f, 0f), 10f, ground))
        {
            minZBound = transform.position.z;
        }
        else
        {
            minZBound = -4.53f;
        }
        if (!Physics.Raycast(new Vector3(transform.position.x, 3f, maxZBound + 1f), new Vector3(0f, -1f, 0f), 10f, ground))
        {
            maxZBound = transform.position.z;
        }
        else
        {
            maxZBound = 4.53f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x, 3f, minZBound - 1f), new Vector3(transform.position.x, -2f, minZBound - 0.5f));
        Gizmos.DrawLine(new Vector3(transform.position.x, 3f, maxZBound + 1f), new Vector3(transform.position.x, -2f, maxZBound + 0.5f));
    }

}
