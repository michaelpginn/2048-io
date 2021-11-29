using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject enemyPlayerPrefab;
    public int x;
    public int z;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        enemyPlayerPrefab.GetComponent<PlayerModel>().playerType = PlayerType.CPU;
    }

    private void Update()
    {
        if (enemyCount < 20)
        {
            StartCoroutine(EnemyDrop());
        }
    }

    IEnumerator EnemyDrop()
    {
        while(enemyCount < 20) {
            print("Dropping new enemy");
            x = Random.Range(-100, 100);
            z = Random.Range(-100, 100);
            Instantiate(enemyPlayerPrefab, new Vector3(x, 43, z), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }
}

