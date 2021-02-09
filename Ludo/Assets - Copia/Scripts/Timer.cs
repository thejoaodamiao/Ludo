using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public  Image barrinha;
    public float tempo=20.0f;
    public static float ContadorTempo;

    public GameManager gm;
    public AudioClip Contagem_Som;
    public bool Tempo_Esgotando=false;
    public float volume = 0.5f;

    void Start()
    {
        ContadorTempo = 0;
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ContadorTempo <= tempo)
        {
            if (ContadorTempo > 11 &&Tempo_Esgotando==false)
            {
                AudioSource.PlayClipAtPoint(Contagem_Som, Camera.main.transform.position, volume);
                Tempo_Esgotando = true;
            }
            ContadorTempo = ContadorTempo + Time.deltaTime;
            barrinha.fillAmount = ContadorTempo / tempo;
        }
        if (barrinha.fillAmount == 1&&gm.playerTurn=="YELLOW")
        {
          
            gm.MovimentaYellowPlayer();
            barrinha.fillAmount = 0;
            ContadorTempo = 0;
        }
    }
}
