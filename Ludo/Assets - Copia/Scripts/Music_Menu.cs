using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Menu : MonoBehaviour
{
    public GameObject Music;
    public static bool Musica_tocando = false;
    public static bool Musica_ParouDeTocar = false;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(Music);
        Musica_tocando = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Musica_ParouDeTocar == true)
        {
            Object.DestroyImmediate(this.gameObject);
        }
    }
}
