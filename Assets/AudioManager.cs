using UnityEngine;
using System.Collections;

public enum Sound { Shoot, Hit, Pickup }

public class AudioManager : MonoBehaviour {

	[SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip pickUp;

    [SerializeField] AudioClip magicForest;
    [SerializeField] AudioClip boss;

    void Start() {
        ChangeMusic(magicForest);
    }

    void PlaySound(AudioClip c) {
        audioSource.PlayOneShot(c, 1);
    }

    public void PlaySound(Sound s) {
        switch (s){
            case (Sound.Shoot):
                PlaySound(shoot);
                break;
            case (Sound.Hit):
                PlaySound(hit);
                break;
            case (Sound.Pickup):
                PlaySound(pickUp);
                break;

        }
    }

    void ChangeMusic(AudioClip c) {
        audioSource.clip = c;
    }

    public void PlayBossMusic() {
        ChangeMusic(boss);
        audioSource.Play();
        audioSource.volume = 1;
    }

}
