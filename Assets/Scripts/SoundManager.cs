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
//        audioSource.clip = jump;
//        audioSource.Play();
    }

    public void PlayHurryUp() {
        audioSource.PlayOneShot(hurryUp);
//        audioSource.clip = hurryUp;
//        audioSource.Play();
    }

    public void PlayLoseLife() {
        audioSource.PlayOneShot(loseLife);
//        audioSource.clip = loseLife;
//        audioSource.Play();
    }

    public void PlayOneUp() {
        audioSource.PlayOneShot(oneUp);
//        audioSource.clip = oneUp;
//        audioSource.Play();
    }

    public void PlayAirshipClear() {
        audioSource.PlayOneShot(airshipClear);
//        audioSource.clip = airshipClear;
//        audioSource.Play();
    }

    public void PlayGameOver() {
        audioSource.PlayOneShot(gameOver);
//        audioSource.clip = gameOver;
//        audioSource.Play();
    }

    public void PlayMoveShell() {
        audioSource.PlayOneShot(moveShell);
//        audioSource.clip = moveShell;
//        audioSource.Play();
    }

    public void PlayStomp() {
        audioSource.PlayOneShot(stomp);
//        audioSource.clip = stomp;
//        audioSource.Play();
    }

    public void PlayFortressClear() {
        audioSource.PlayOneShot(fortressClear);
//        audioSource.clip = fortressClear;
//        audioSource.Play();
    }
    
    
}
