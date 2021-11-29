using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject enemyPlayerPrefab;
    public int x;
    public int z;
    public int enemyCount;

    private bool droppingEnemies = false;

    static public EnemyControl instance;

    private static int MAX_ENEMIES = 30;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        enemyPlayerPrefab.GetComponent<PlayerModel>().playerType = PlayerType.CPU;
    }

    private void Update()
    {
        if (enemyCount < MAX_ENEMIES && !droppingEnemies)
        {
            droppingEnemies = true;
            StartCoroutine(EnemyDrop());
        }
    }

    IEnumerator EnemyDrop()
    {
        while(enemyCount < MAX_ENEMIES) {
            print("Dropping new enemy");
            x = Random.Range(-100, 100);
            z = Random.Range(-100, 100);
            Instantiate(enemyPlayerPrefab, new Vector3(x, 43, z), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
        droppingEnemies = false;
    }
}

