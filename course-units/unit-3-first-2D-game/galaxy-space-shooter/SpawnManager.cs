using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerup;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        //Instantiate enemy prefab
        //yield wait for 5 seconds
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9f, 9f), 12f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
        
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9f, 9f), 12f, 0);
            int index = Random.Range(0, 3);
            Instantiate(_powerup[index], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5f, 12f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
