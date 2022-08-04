using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyRainer : MonoBehaviour
{
    [SerializeField] float timeBetweenNewMoney;
    [SerializeField] GameObject money;

    void Start()
    {
        
    }

    float timer;

    void Update()
    {
        timer += Time.deltaTime / 3;

        if(timer > timeBetweenNewMoney)
        {
            var newMoney = Instantiate(money, new Vector3(Random.Range(-9, 9), Random.Range(5, 5.5f)), Quaternion.identity);
            newMoney.transform.localScale = new Vector3(2, 2, 0);
            newMoney.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            newMoney.GetComponent<SpriteRenderer>().sortingOrder = -1;
            timer = 0;
        }
    }
}
