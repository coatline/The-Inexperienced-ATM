using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GainLoseText : MonoBehaviour
{

    GameObject canvas;
    public int amount;
    TMP_Text text;
    float x;
    float y;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        transform.SetParent(canvas.transform);
        x = Random.Range(-.04f, .04f);
        y = Random.Range(-.02f, .04f);
        text.text = $"${amount}";
    }

    float a = 1f;

    void Update()
    {
        a -= Time.deltaTime;

        text.color = new Color(text.color.r, text.color.g, text.color.b, a);

        if (a > 1)
        {
            Destroy(this.gameObject);
        }

        transform.Translate(new Vector3(x, y));
    }
}
