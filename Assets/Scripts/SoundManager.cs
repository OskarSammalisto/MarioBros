using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    private AudioSource audioSource;
    public AudioClip jump;
    public AudioClip hurryUp;
    public AudioClip loseLife;
    public AudioClip oneUp;
    public AudioClip airshipClear;
    public AudioClip gameOver;
    public AudioClip moveShell;
    public AudioClip stomp;
    public AudioClip fortressClear;
    
    
    void Start() {

        audioSource = GetComponent<AudioSource>();

    }

    public void PlayJump() {
        
        audioSource.PlayOneShot(jump);

    }

    public void PlayHurryUp() {
        audioSource.PlayOneShot(hurryUp);

    }

    public void PlayLoseLife() {
        audioSource.PlayOneShot(loseLife);

    }

    public void PlayOneUp() {
        audioSource.PlayOneShot(oneUp);

    }

    public void PlayAirshipClear() {
        audioSource.PlayOneShot(airshipClear);

    }

    public void PlayGameOver() {
        audioSource.PlayOneShot(gameOver);

    }

    public void PlayMoveShell() {
        audioSource.PlayOneShot(moveShell);

    }

    public void PlayStomp() {
        audioSource.PlayOneShot(stomp);

    }

    public void PlayFortressClear() {
        audioSource.PlayOneShot(fortressClear);

    }
    
    
}
