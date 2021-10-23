using UnityEngine;
using System.Collections;
using UnityEditor.UI;

public class CPUPlayerController : MonoBehaviour
{
    //variables
    private PlayerController playerController;
    
    private int delta = 200;
    private int frames = 0;
    private float scale = 0.05f;
    Vector3 direction;
    
    
    
    
    // Use this for initialization
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        direction = new Vector3((float) (Random.value * scale - scale / 2), 0,
            (float) (Random.value * scale - scale / 2));
    }

    // Update is called once per frame
    void Update()
    {
        frames++;
        frames %= delta;
        if (frames == 0)
        {
            direction = new Vector3((float) (Random.value * scale - scale / 2), 0,
                (float) (Random.value * scale - scale / 2));
        }

        transform.position += direction;

    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<BulletController>())
        {
            Explode();
        }
    }

    void Explode()
    {
        ParticleSystem explosion = GetComponent<ParticleSystem>();
        explosion.Play();
        Destroy(gameObject, explosion.main.duration);
        // GameController.enemies -= 1;
        // GameController.spawnEnemy = true;
    }

    public PlayerLevel GetLevel()
    {
        return playerController.GetLevel();
    }

    // public void SetLevel(PlayerLevel level)
    // {
    //     playerController.SetLevel(level);
    // }
}
