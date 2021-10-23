using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject theEnemy;
    public int x;
    public int z;
    public int countenemies;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (countenemies < 20)
        {
            x = Random.Range(-100, 100);
            z = Random.Range(-100, 100);
            Instantiate(theEnemy, new Vector3(x, 43, z), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            countenemies += 1;
        }
    }
}

