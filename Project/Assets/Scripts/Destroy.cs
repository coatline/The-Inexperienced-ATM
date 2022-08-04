using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] float timeBeforeDestroy;

    void Start()
    {
        Invoke("Kill", timeBeforeDestroy);
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
