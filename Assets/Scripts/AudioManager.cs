using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    private static AudioManager _instance;
    public static AudioManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    #endregion

    [SerializeField]
    private AudioClip[] audioList;
    [SerializeField]
    private AudioClip brickBreakSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        PlayRandomMusic();
    }

    public void BreakBrick()
    {
        audioSource.PlayOneShot(brickBreakSound, 1f);
    }

    public void PlayRandomMusic()
    {
        int index = Random.Range(0, audioList.Length);
        audioSource.clip = audioList[index];
        audioSource.loop = true;
        audioSource.Play();
    }
}
