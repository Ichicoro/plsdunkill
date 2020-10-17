using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Jukebox : MonoBehaviour {
    public bool keepPlaying = true;
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private Animation fadeinAnim;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        fadeinAnim = GetComponent<Animation>();
        InvokeRepeating(nameof(CheckPlayingAndStartNewIfNot), 0f, 1f);
    }

    // void Update() {
        
    // }

    void CheckPlayingAndStartNewIfNot() {
        if (!audioSource.isPlaying && keepPlaying) {
            audioSource.clip = getRandomAudioClip();
            audioSource.time = 0;
            audioSource.loop = false;
            if (fadeinAnim) {
                fadeinAnim.Play();
            }
            audioSource.Play();
        }
    }

    AudioClip getRandomAudioClip() {
        return audioClips[(int) Mathf.Floor(Random.Range(0, audioClips.Length))];
    }
}
