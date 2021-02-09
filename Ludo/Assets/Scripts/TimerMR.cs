using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerMR : MonoBehaviour
{
    public Image barrinha;
    public float tempo = 20.0f;
    public static float ContadorTempo;

    public LudoModoRapido gm2;
    public AudioClip Contagem_Som;
    public bool Tempo_Esgotando = false;
    public float volume = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        ContadorTempo = 0;
        gm2 = GameObject.FindObjectOfType<LudoModoRapido>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ContadorTempo <= tempo)
        {
            if (ContadorTempo > 11 && Tempo_Esgotando == false)
            {
                AudioSource.PlayClipAtPoint(Contagem_Som, Camera.main.transform.position, volume);
                Tempo_Esgotando = true;
            }
            ContadorTempo = ContadorTempo + Time.deltaTime;
            barrinha.fillAmount = ContadorTempo / tempo;
        }
        if (barrinha.fillAmount == 1 && gm2.playerTurn == "YELLOW")
        {
            gm2.MovimentaYellowPlayer();
            barrinha.fillAmount = 0;
            ContadorTempo = 0;
        }
        if (barrinha.fillAmount == 1 && gm2.playerTurn == "RED")
        { 
       gm2.MovimentaRedPlayer();
        barrinha.fillAmount = 0;
        ContadorTempo = 0;
    }
}
}

