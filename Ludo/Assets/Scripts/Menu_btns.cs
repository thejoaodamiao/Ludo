using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Menu_btns : MonoBehaviour
{
   
    public static bool Musica_ParouDeTocar = false;
    public AudioClip som_Botao;
    public float volume = 0.5f;
   

    ////////////Atributos da interface da tela de ranking////////////
   

    public void btn_play()
    {
        AudioSource.PlayClipAtPoint(som_Botao, Camera.main.transform.position, volume);
        SceneManager.LoadScene("Main");
    }
    public void btn_quit()
    {
        Application.Quit();
    }
    

    ////////////////////////////////////////////////////////////////////
}
