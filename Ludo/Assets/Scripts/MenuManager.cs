using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Video;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public static int quantidadePlayers;
    
    //intro
    public VideoPlayer vd;
    public AudioSource music;
    public AudioSource tomada;
    public Button btn_play;
    public GameObject dark;

    //define modo de jogo
    private void Start()
    {
        vd = GameObject.Find("Canvas").GetComponent<VideoPlayer>();
        music = GameObject.Find("Music").GetComponent<AudioSource>();
        StartCoroutine("intro");
        StartCoroutine("musicMenu");
        StartCoroutine("tomadaSfx");


    }
    //Sfx do menu
    IEnumerator intro()
    {

        yield return new WaitForSeconds(19f);
        vd.Stop();
        
    }



    IEnumerator tomadaSfx()
    {
        yield return new WaitForSeconds(20f);
        dark.SetActive(false);
        tomada.Play();
    }

    IEnumerator musicMenu()
    {
        yield return new WaitForSeconds(21f);

        btn_play.animator.enabled = true;
        music.Play();
    }

    public void DoisPlayers()
    {
        soundManager.buttonAudioSource.Play();
        quantidadePlayers = 2;
        SceneManager.LoadScene("Ludo");
    }

    public void TrêsPlayers()
    {
        soundManager.buttonAudioSource.Play();
        quantidadePlayers = 3;
        SceneManager.LoadScene("Ludo");
    }

    public void QuatroPlayers()
    {
        soundManager.buttonAudioSource.Play();
        quantidadePlayers = 4;
        SceneManager.LoadScene("Ludo(Modo Rapido)");
    }

    //será adpatado futuramente
    public void Quit()
    {
        soundManager.buttonAudioSource.Play();
        if (Application.isPlaying == true)
        {
            Application.Quit();
        }
    }

}
