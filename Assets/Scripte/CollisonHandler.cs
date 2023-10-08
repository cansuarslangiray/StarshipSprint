using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisonHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 1f;
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.transform.tag)
        {
            case "Friendly":
                Debug.Log("start point");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        GetComponent<Movemant>().enabled = false;
        Invoke("NextLevel", levelLoadDelay);
        if (!_audio.isPlaying)
        {
            _audio.PlayOneShot(audioClips[0]);
        }
        else
        {
            _audio.Stop();
        }
    }

    void StartCrashSequence()
    {
        GetComponent<Movemant>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        if (!_audio.isPlaying)
        {
            _audio.PlayOneShot(audioClips[1]);
        }
        else
        {
            _audio.Stop();
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}