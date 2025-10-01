using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVoiceOnPlayer : MonoBehaviour
{
    AudioSource audioSource;
    public GameObject playerRef;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = playerRef.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
        Debug.Log("Play");
    }
}
