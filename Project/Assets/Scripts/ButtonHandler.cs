using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] AudioClip ac;
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        source.PlayOneShot(ac);
    }

    public void ChangeScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
