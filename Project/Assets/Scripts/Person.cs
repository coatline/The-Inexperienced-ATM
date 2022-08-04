using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Person : MonoBehaviour
{
    [SerializeField] float chanceToGiveMoney;
    [SerializeField] TMP_Text LostGainTxt;
    [SerializeField] AudioClip pickup_1;
    [SerializeField] AudioClip pickup_2;
    [SerializeField] float moveSpeed;
    [SerializeField] int money;
    GameObject moneyCarrying;
    float chanceToTakeMoney;
    Vector3 startingPos;
    AudioSource source;
    public string type;
    SpriteRenderer sr;
    bool satisfied;
    ATM atm;

    void Start()
    {
        source = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        startingPos = transform.position;
        atm = FindObjectOfType<ATM>();
        chanceToTakeMoney = 100 - chanceToGiveMoney;
        moveSpeed += Random.Range(-.025f, .025f);
    }

    void Update()
    {
        FaceDir();

        if (atm.broken)
        {
            satisfied = true;
        }

        if (moneyCarrying)
        {
            Carry();
        }
    }

    private void FixedUpdate()
    {
        if (!satisfied)
        {
            GoToATM();
        }
        else if (satisfied)
        {
            LeaveATM();
        }
    }

    void FaceDir()
    {
        if (satisfied)
        {
            if (transform.position.x > startingPos.x)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }
        else
        {
            if (transform.position.x > startingPos.x)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }
    }

    void LeaveATM()
    {
        transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, startingPos.x, moveSpeed), transform.position.y);
    }

    void GoToATM()
    {
        transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, atm.transform.position.x, moveSpeed), transform.position.y);
    }

    void Carry()
    {
        moneyCarrying.transform.position = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MoneyProjectile") && !moneyCarrying)
        {
            moneyCarrying = collision.gameObject;
            moneyCarrying.GetComponent<BoxCollider2D>().enabled = false;
            satisfied = true;

            var rand = Random.Range(0, 2);

            if (rand == 0)
            {
                source.PlayOneShot(pickup_1);
            }
            else
            {
                source.PlayOneShot(pickup_2);
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (satisfied || !collision.gameObject.CompareTag("Player")) { return; }

        if (Random.Range(0, 100) < chanceToGiveMoney)
        {
            var atmScript = collision.gameObject.GetComponentInParent<ATM>();
            atmScript.Money += money;
            atmScript.moneyText.GetComponent<Animator>().Play("GainMoney");

            NewLoseGainMoneyText(true);
        }
        else
        {
            var atmScript = collision.gameObject.GetComponentInParent<ATM>();
            atmScript.Money -= money;
            atmScript.moneyText.GetComponent<Animator>().Play("LoseMoney");

            NewLoseGainMoneyText(false);
        }

        if (type == "Vandal")
        {
            var ATMscript = collision.gameObject.GetComponentInParent<ATM>();
            ATMscript.GetComponent<SpriteRenderer>().sprite = ATMscript.vandalizedSprite;
            ATMscript.broken = true;
        }

        satisfied = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (satisfied && collision.gameObject.CompareTag("Finish"))
        {
            Destroy(this.gameObject);
        }
    }

    void NewLoseGainMoneyText(bool adding)
    {
        var newText = Instantiate(LostGainTxt, transform.position, Quaternion.identity);
        if (adding)
        {
            newText.GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            newText.GetComponent<TMP_Text>().color = Color.red;
        }

        var amount = 0;

        if (adding)
        {
            amount = money;
        }
        else
        {
            amount = -money;
        }

        newText.gameObject.GetComponent<GainLoseText>().amount = amount;
    }
}
