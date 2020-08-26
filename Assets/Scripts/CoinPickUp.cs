using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    private void Awake()
    {
        WWW audio = new WWW("D:/original.ogg");
        audioClip = audio.GetAudioClip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(audioClip,Camera.main.transform.position);
        Destroy(gameObject);
    }
}
