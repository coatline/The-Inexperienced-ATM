using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{
    [SerializeField] float timeBetweenSpawnsVariation;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] Person vandalPrefab;
    [SerializeField] Person averageGuyPrefab;
    [SerializeField] Person robberPrefab;
    [SerializeField] Person poorGuyPrefab;
    [SerializeField] Person richKidPrefab;
    float currentTimeBetweenSpawn;
    float timer;

    void RandomizeTimeBetweenSpawns()
    {
        currentTimeBetweenSpawn = timeBetweenSpawns + Random.Range(-timeBetweenSpawnsVariation, timeBetweenSpawnsVariation);
    }

    void Start()
    {
        RandomizeTimeBetweenSpawns();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > currentTimeBetweenSpawn)
        {
            RandomizeTimeBetweenSpawns();
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        var chance = Random.Range(0, 100);

        //25% chance to spawn a robber
        if (chance <= 25)
        {
            Instantiate(robberPrefab, transform.position, Quaternion.identity);
        }
        //10% chance to spawn a rich dude
        else if (chance > 25 && chance <= 35)
        {
            Instantiate(richKidPrefab, transform.position, Quaternion.identity);
        }
        //20% chance to spawn a poor guy
        else if (chance > 35 && chance <= 55)
        {
            Instantiate(poorGuyPrefab, transform.position, Quaternion.identity);
        }
        //40% chance to spawn an average guy
        else if(chance > 55 && chance <= 95)
        {
            Instantiate(averageGuyPrefab, transform.position, Quaternion.identity);
        }
        //5% chance to spawn a vandal
        else
        {
            Instantiate(vandalPrefab, transform.position, Quaternion.identity);
        }
    }
}
