using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _enemyContainer;
    //[SerializeField] GameObject _tripleShotPrefab;
    //[SerializeField] GameObject _speedBoostPrefab;
    [SerializeField] GameObject[] powerup;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawn game objects every 5 seconds
    //create a coroutine of type IEnumerator -- Yield Events
    //while loop

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
        //while loop (infinite loop if you set value to true)
        //Instantiate enemy prefab
        //yield wait for 5 seconds
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            //every 3-9 seconds  to spawn the triple shot powerup
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 10, 0);
            
            //randomize the powerup to use
            int index = Random.Range(0, 3);
            
            //create the powerup to use base on the index from above
            Instantiate(powerup[index], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5f, 15f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
