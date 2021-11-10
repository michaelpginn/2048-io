using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float speed = 100f;
    private float timeToDestroy = 3f;

    [HideInInspector]
    public Vector3 target { get; set; }
    [HideInInspector]
    public bool hit { get; set; }
    [HideInInspector]
    public PlayerController originPlayerController;

    private void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(!hit && Vector3.Distance(transform.position, target) < 0.01f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Destroy bullet");
        Destroy(gameObject);
    }
}
