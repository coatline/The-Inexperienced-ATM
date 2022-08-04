using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ATM : MonoBehaviour
{
    [SerializeField] GameObject moneyProjectilePrefab;
    [SerializeField] TMP_Text vandalizedText;
    [SerializeField] float timeBetweenShots;
    [SerializeField] Vector2 dispenseForce;
    [SerializeField] AudioClip dispense_1;
    [SerializeField] AudioClip dispense_2;
    [SerializeField] float jumpforce;
    [SerializeField] float moveSpeed;
    public Sprite vandalizedSprite;
    public TMP_Text moneyText;
    public TMP_Text debtText;
    AudioSource aSource;
    public bool broken;
    Rigidbody2D rb;
    bool canJump;
    int money;

    public int Money
    {
        set
        {
            money = value;
            moneyText.text = $"Money: {value}$";

            if(money < 0)
            {
                debtText.gameObject.SetActive(true);
            }
            else
            {
                debtText.gameObject.SetActive(false);
            }
        }
        get
        {
            return money;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        aSource = GetComponent<AudioSource>();
    }

    void EnableVandalizedTxt()
    {
        vandalizedText.gameObject.SetActive(true);
    }

    bool done;

    void Update()
    {
        if (broken)
        {
            if (!done)
            {
                done = true;
                Invoke("EnableVandalizedTxt", 3);
            }
            return;
        }

        HandleInputs();
    }

    void HandleInputs()
    {
        var inputX = Input.GetAxis("Horizontal");
        var rotation = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);

        shootTimer += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            DoShooting();
        }

        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            canJump = false;
            rb.AddForce(new Vector2(0, jumpforce));
        }

        if (canJump)
        {
            return;
        }

        transform.Rotate(new Vector3(0, 0, 2 * rotation));
    }

    float shootTimer;

    void DoShooting()
    {
        if (shootTimer > timeBetweenShots)
        {
            shootTimer = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        var force = dispenseForce;

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            force.x = -force.x;
        }

        var newProjectile = Instantiate(moneyProjectilePrefab, transform.GetChild(0).transform.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().AddForce(force + new Vector2(Random.Range(-force.x / 3, force.x / 3), Random.Range(-force.y / 3, force.y)));

        Money -= 1;

        var rand = Random.Range(0, 2);

        if(rand == 0)
        {
            aSource.PlayOneShot(dispense_1);
        }
        else
        {
            aSource.PlayOneShot(dispense_2);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canJump = true;
        }
    }
}
