using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour {

    public AudioClip _backgroundMusic;
    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = this.gameObject.AddComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        if (_backgroundMusic != null)
        {
            _audioSource.clip = _backgroundMusic;
            _audioSource.loop = true;
            _audioSource.Play();
        }
	}
}
