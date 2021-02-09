using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

public class LudoModoRapido : MonoBehaviour
{
    private int totalRedInHouse, totalGreenInHouse, totalBlueInHouse, totalYellowInHouse;

    public GameObject frameRed, frameBlue, frameGreen, frameYellow;

    public GameObject redPlayer1Border, redPlayer2Border, redPlayer3Border, redPlayer4Border;
    public GameObject YellowPlayer1Border, YellowPlayer2Border, YellowPlayer3Border, YellowPlayer4Border;
    public GameObject GreenPlayer1Border, GreenPlayer2Border, GreenPlayer3Border, GreenPlayer4Border;
    public GameObject BluePlayer1Border, BluePlayer2Border, BluePlayer3Border, BluePlayer4Border;


    public Vector3 redPlayer1Pos, redPlayer2Pos, redPlayer3Pos, redPlayer4pos;
    public Vector3 YellowPlayer1Pos, YellowPlayer2Pos, YellowPlayer3Pos, YellowPlayer4pos;
    public Vector3 GreenPlayer1Pos, GreenPlayer2Pos, GreenPlayer3Pos, GreenPlayer4pos;
    public Vector3 BluePlayer1Pos, BluePlayer2Pos, BluePlayer3Pos, BluePlayer4pos;

    public Transform diceRoll;
    public Transform redDiceRoll, blueDiceRoll, greenDiceRoll, yellowDiceRoll;

    public Text diceNumber;
    public Button diceRollButton;
    public Button redPlayer1Button, redPlayer2Button, redPlayer3Button, redPlayer4Button;
    public Button bluePlayer1Button, bluePlayer2Button, bluePlayer3Button, bluePlayer4Button;
    public Button greenPlayer1Button, greenPlayer2Button, greenPlayer3Button, greenPlayer4Button;
    public Button yellowPlayer1Button, yellowPlayer2Button, yellowPlayer3Button, yellowPlayer4Button;

    public GameObject redScreen, blueScreen, greenScreen, yellowScreen;

    
    private string currentPlayer = "none";
    private string currentPlayerName = "none";

    //Player controller movement
    public GameObject redPlayer1, redPlayer2, redPlayer3, redPlayer4;
    public GameObject bluePlayer1, bluePlayer2, bluePlayer3, bluePlayer4;
    public GameObject greenPlayer1, greenPlayer2, greenPlayer3, greenPlayer4;
    public GameObject yellowPlayer1, yellowPlayer2, yellowPlayer3, yellowPlayer4;

    private int redPlayer1Steps = 0, redPlayer2Steps, redPlayer3Steps, redPlayer4Steps;
    private int bluePlayer1Steps, bluePlayer2Steps, bluePlayer3Steps, bluePlayer4Steps;
    private int greenPlayer1Steps, greenPlayer2Steps, greenPlayer3Steps, greenPlayer4Steps;
    private int yellowPlayer1Steps=0, yellowPlayer2Steps=0, yellowPlayer3Steps=0, yellowPlayer4Steps=0;

   

    public GameObject diceRollAnimation, dice2RollAnimation, dice3RollAnimation,
        dice4RollAnimation, dice5RollAnimation, dice6RollAnimation;

    public List<GameObject> redMovementBlock = new List<GameObject>();
    public List<GameObject> yellowMovementBlock = new List<GameObject>();
    public List<GameObject> blueMovementBlock = new List<GameObject>();
    public List<GameObject> greenMovementBlock = new List<GameObject>();

    public GameObject confirmScreen;
    public GameObject gameCompleteScreen;

    private System.Random randNo;
    private System.Random computerPlayer;
    private int playerGreen,playerBlue;

    ///AUXILIAR DOS PLAYERS RED 
    int aux_casasTablueiro = 0, aux_casasTabuleiro2 = 0, aux_casasTabuleiro3 = 0, aux_casasTabuleiro4 = 0;

    ///CASAS DO TABULEIRO OCUPADAS PLAYERS RED
    Vector3 redPlayer1_CasaAtual, redPlayer2_CasaAtual, redPlayer3_CasaAtual, redPlayer4_CasaAtual;

    ///AUXILIAR DOS PLAYERS GREEN
    int aux_green1 = 0, aux_green2 = 0, aux_green3 = 0, aux_green4 = 0;

    ///CASAS DO TABULEIRO OCUPADAS PLAYERS GREEN
    Vector3 greenPlayer1_CasaAtual, greenPlayer2_CasaAtual, greenPlayer3_CasaAtual, green_Player4_CasaAtual;

    ///AUXILIAR DOS PLAYERS AMARELOS
    int aux_yellow1 = 0, aux_yellow2 = 0, aux_yellow3 = 0, aux_yellow4 = 0;

    ///CASAS DO TABULEIRO OCUPADAS PLAYERS YELLOW
    Vector3 yellowPlayer1_CasaAtual, yellowPlayer2_CasaAtual, yellowPlayer3_CasaAtual, yellowPlayer4_CasaAtual;

    ///AUXILIAR DOS PLAYERS AZUIS
    int aux_blue1 = 0, aux_blue2 = 0, aux_blue3 = 0, aux_blue4 = 0;

    ///CASAS DO TABULEIRO OCUPADAS PLAYERS AZUIS
    Vector3 bluePlayer1_CasaAtual, bluePlayer2_CasaAtual, bluePlayer3_CasaAtual, bluePlayer4_CasaAtual;

    //AVANCOS
    public int SelectDiceNumAnimation;
    public string playerTurn = "YELLOW";
    public bool avancoRed = false, avancoYellow = false, avancoGreen = false, avancoBlue = false;
    public string statusRed = "NAO AVANCAR", statusYellow = "NAO AVANCAR", statusBlue = "NAO AVANCAR",
   statusGreen = "NAO AVANCAR";
    //TIMERS
    public GameObject BarraContador;
    public TimerMR timer;
    private float tempoPartida_aux=0f;
    public float tempoPartida = 0f;
    public bool PartidaFim;
    //Capturar hora
    public string hora;
    //Capturar data
    public string data;
    ///Coleta de dados
    public static string Teste;
    public GameObject TelaNext;
    public GameObject TelaDerrota;

    //Sons
    public AudioClip Som_Dado;
    public float volume = 0.5f;

    public AudioClip Som_Captura;

    public AudioClip Som_Movement;

    ///Tela avanco/////////
    public GameObject Tela_Avanco;
    public Text cor_player;

    /////////////////////SAIR DO GAME(CHAMA TELA DE SAÍDA)//////////////////////////
    public void Exit()
    {

        confirmScreen.SetActive(true);

    }

    public void No()
    {
        //soundManager.playerAudioSource.Play();
        confirmScreen.SetActive(false);
    }

    public void yes()
    {
        Music_Menu.Musica_ParouDeTocar = false;
        Music_Menu.Musica_tocando = true;
        SceneManager.LoadScene("Menu");
        
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 30;
        if (Music_Menu.Musica_tocando == true)
        {
            Music_Menu.Musica_ParouDeTocar = true;
        }
        randNo = new System.Random();
        diceRollAnimation.SetActive(false);
        dice2RollAnimation.SetActive(false);
        dice3RollAnimation.SetActive(false);
        dice4RollAnimation.SetActive(false);
        dice5RollAnimation.SetActive(false);
        dice6RollAnimation.SetActive(false);

        //Players Initial position

        redPlayer1Pos = redPlayer1.transform.position;
        redPlayer2Pos = redPlayer2.transform.position;
        redPlayer3Pos = redPlayer3.transform.position;
        redPlayer4pos = redPlayer4.transform.position;

        GreenPlayer1Pos = greenPlayer1.transform.position;
        GreenPlayer2Pos = greenPlayer2.transform.position;
        GreenPlayer3Pos = greenPlayer3.transform.position;
        GreenPlayer4pos = greenPlayer4.transform.position;

        YellowPlayer1Pos = yellowPlayer1.transform.position;
        YellowPlayer2Pos = yellowPlayer2.transform.position;
        YellowPlayer3Pos = yellowPlayer3.transform.position;
        YellowPlayer4pos = yellowPlayer4.transform.position;

        BluePlayer1Pos = bluePlayer1.transform.position;
        BluePlayer2Pos = bluePlayer2.transform.position;
        BluePlayer3Pos = bluePlayer3.transform.position;
        BluePlayer4pos = bluePlayer4.transform.position;


        //desativando bordas
        redPlayer1Border.SetActive(false);
        redPlayer2Border.SetActive(false);
        redPlayer3Border.SetActive(false);
        redPlayer4Border.SetActive(false);

        GreenPlayer1Border.SetActive(false);
        GreenPlayer2Border.SetActive(false);
        GreenPlayer3Border.SetActive(false);
        GreenPlayer4Border.SetActive(false);

        YellowPlayer1Border.SetActive(false);
        YellowPlayer2Border.SetActive(false);
        YellowPlayer3Border.SetActive(false);
        YellowPlayer4Border.SetActive(false);

        BluePlayer1Border.SetActive(false);
        BluePlayer2Border.SetActive(false);
        BluePlayer3Border.SetActive(false);
        BluePlayer4Border.SetActive(false);

        redScreen.SetActive(false);
        blueScreen.SetActive(false);
        greenScreen.SetActive(false);
        yellowScreen.SetActive(false);

        ///Iniciando com players desailitados
        redPlayer1Button.interactable = false;
        redPlayer2Button.interactable = false;
        redPlayer3Button.interactable = false;
        redPlayer4Button.interactable = false;

        bluePlayer1Button.interactable = false;
        bluePlayer2Button.interactable = false;
        bluePlayer3Button.interactable = false;
        bluePlayer4Button.interactable = false;

        yellowPlayer1Button.interactable = false;
        yellowPlayer2Button.interactable = false;
        yellowPlayer3Button.interactable = false;
        yellowPlayer4Button.interactable = false;

        greenPlayer1Button.interactable = false;
        greenPlayer2Button.interactable = false;
        greenPlayer3Button.interactable = false;
        greenPlayer4Button.interactable = false;
        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////YELLOW PLAYER///////////////////////////////////////////
        yellowPlayer1.transform.position = yellowMovementBlock[yellowPlayer1Steps].transform.position;
        yellowPlayer1_CasaAtual = yellowMovementBlock[yellowPlayer1Steps].transform.position;
        yellowPlayer1Steps++;
        ////////////////////////////RED PLAYER/////////////////////////////////////////////////
        redPlayer1.transform.position = redMovementBlock[redPlayer1Steps].transform.position;
        redPlayer1_CasaAtual = redMovementBlock[redPlayer1Steps].transform.position;
        redPlayer1Steps++;
        ///////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////GREEN PLAYER///////////////////////////////////////
        greenPlayer1.transform.position = greenMovementBlock[greenPlayer1Steps].transform.position;
        greenPlayer1_CasaAtual = greenMovementBlock[greenPlayer1Steps].transform.position;
        greenPlayer1Steps++;
        ////////////////////////////////////////BLUE PLAYER/////////////////////////////////////
        bluePlayer1.transform.position = blueMovementBlock[bluePlayer1Steps].transform.position;
        bluePlayer1_CasaAtual = blueMovementBlock[bluePlayer1Steps].transform.position;
        bluePlayer1Steps++;
        /////////////////////////////////////////////////////////////////////////////////////////
        switch (MenuManager.quantidadePlayers)
        {
            case 4:
                playerTurn = "YELLOW";
                diceRoll.position = yellowDiceRoll.position;
                frameRed.SetActive(false);
                frameGreen.SetActive(false);
                frameBlue.SetActive(false);
                frameYellow.SetActive(true);
                break;
        }

    }

    /////////////ROLA O DADO//////////////////////////////////
    #region Rolagem de dados
    public void DiceRoll()
    {
        //diceRoll.GetComponent<Animator>().Play("diceButton");
        StartCoroutine("Sound_dice");
        SelectDiceNumAnimation = Random.Range(0, 9);
        diceNumber.gameObject.SetActive(false);

        if (playerTurn == "YELLOW")
        {
            TimerMR.ContadorTempo = 0;
            BarraContador.transform.position=new Vector3(diceRollButton.transform.position.x-35,
                diceRollButton.transform.position.y-150, 0);
            BarraContador.SetActive(true);

        }
        if (playerTurn == "RED")
        {
            TimerMR.ContadorTempo = 0;
            BarraContador.transform.position = new Vector3(diceRollButton.transform.position.x + 25,
                diceRollButton.transform.position.y - 150, 0);
            BarraContador.SetActive(true);

        }
        StartCoroutine("legendary");

        ////DEPOIS DE ROLAR OS DADOS CHAMA A ROTINA PARA DAR SEQUENCIA A JOGADA E A MOVIMENTACAO DO PLAYER
        StartCoroutine("PlayerNotInitialed");

    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////STARTA A JOGADA DO PLAYER//////////////////////////////////////////////////////

    IEnumerator legendary()
    {
        yield return new WaitForSeconds(0.5f);
        diceNumber.gameObject.SetActive(true);
        diceNumber.text = SelectDiceNumAnimation.ToString();
        if (playerTurn == "BLUE"||playerTurn=="GREEN")
        {
            TimerMR.ContadorTempo = 0;
        }
    }
    IEnumerator Sound_dice()
    {
        AudioSource.PlayClipAtPoint(Som_Dado, Camera.main.transform.position, volume);
        yield return new WaitForSeconds(1.5f);

    }
    #endregion
    ///////////////////////////////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////STARTA A JOGADA DO PLAYER/////////////////////////////////////
    #region Inicia a jogada do Player da vez
    IEnumerator PlayerNotInitialed()
    {
        yield return new WaitForSeconds(1f);

        switch (playerTurn)
        {
            case "RED":
                if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && redPlayer1Steps == 0)
                {
                    redPlayer1Border.SetActive(true);
                    redPlayer1Button.interactable = true;
                }
                if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && redPlayer2Steps == 0)
                {
                    redPlayer2Border.SetActive(true);
                    redPlayer2Button.interactable = true;
                }
                if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && redPlayer3Steps == 0)
                {
                    redPlayer3Border.SetActive(true);
                    redPlayer3Button.interactable = true;
                }
                if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && redPlayer4Steps == 0)
                {
                    redPlayer4Border.SetActive(true);
                    redPlayer4Button.interactable = true;
                }
                //////VERIFICA SE OS PLAYERS TÃO NO TABULEIRO////////////////
                if (aux_casasTablueiro > 0 || redPlayer1Steps > 0)
                {
                    redPlayer1Border.SetActive(true);
                    redPlayer1Button.interactable = true;
                }

                if (aux_casasTabuleiro2 > 0 || redPlayer2Steps > 0)
                {
                    redPlayer2Border.SetActive(true);
                    redPlayer2Button.interactable = true;
                }
                if (aux_casasTabuleiro3 > 0 || redPlayer3Steps > 0)
                {
                    redPlayer3Border.SetActive(true);
                    redPlayer3Button.interactable = true;
                }
                if (aux_casasTabuleiro4 > 0 || redPlayer4Steps > 0)
                {
                    redPlayer4Border.SetActive(true);
                    redPlayer4Button.interactable = true;
                }
                ///////////////////////////////////////////////////////////////////////////////////////

                /////////////////Desativa players Vencedores incapacitados//////////////////////////////////////////
                if ((redPlayer1Steps == 76 || 
                    redMovementBlock.Count - (redPlayer1Steps) < SelectDiceNumAnimation)
                    ||  (redPlayer1Steps > 68 &&
              redMovementBlock.Count - (redPlayer1Steps - 1) != SelectDiceNumAnimation + 1))
                {
                    redPlayer1Button.interactable = false;
                    redPlayer1Border.SetActive(false);
                }

                if ((redPlayer2Steps == 76 || 
                    redMovementBlock.Count - (redPlayer2Steps) < SelectDiceNumAnimation)
                    ||(redPlayer2Steps > 68 &&
              redMovementBlock.Count - (redPlayer2Steps - 1) != SelectDiceNumAnimation + 1 && avancoRed))
                {
                    redPlayer2Button.interactable = false;
                    redPlayer2Border.SetActive(false);
                }
                if ((redPlayer3Steps == 76 || 
                    redMovementBlock.Count - (redPlayer3Steps) < SelectDiceNumAnimation)
                    || (redPlayer3Steps > 68 &&
              redMovementBlock.Count - (redPlayer3Steps - 1) != SelectDiceNumAnimation + 1))
                {
                    redPlayer3Button.interactable = false;
                    redPlayer3Border.SetActive(false);
                }
                if ((redPlayer4Steps == 76 || 
                    redMovementBlock.Count - (redPlayer4Steps) < SelectDiceNumAnimation)
                    ||  (redPlayer4Steps > 68 &&
              redMovementBlock.Count - (redPlayer4Steps - 1) != SelectDiceNumAnimation + 1))
                {
                    redPlayer4Button.interactable = false;
                    redPlayer4Border.SetActive(false);
                }
                ///////////////////////////////////////////////////////////////////////////////
                //////////////////////Se o Numero for 0///////////////////////////////////////
                if (SelectDiceNumAnimation == 0)
                {
                    if (redPlayer1Steps != 0 || aux_casasTablueiro != 0)
                    {
                        redPlayer1Border.SetActive(false);
                        redPlayer1Button.interactable = false;
                    }
                    if (redPlayer2Steps != 0 || aux_casasTabuleiro2 != 0)
                    {
                        redPlayer2Border.SetActive(false);
                        redPlayer2Button.interactable = false;
                    }
                    if (redPlayer3Steps != 0 || aux_casasTabuleiro3 != 0)
                    {
                        redPlayer3Border.SetActive(false);
                        redPlayer3Button.interactable = false;
                    }
                    if (redPlayer4Steps != 0 || aux_casasTabuleiro4 != 0)
                    {
                        redPlayer4Border.SetActive(false);
                        redPlayer4Button.interactable = false;
                    }
                }
                ///////////////////////////////////////////////////////////////////////
                if (!redPlayer1Border.activeInHierarchy && !redPlayer2Border.activeInHierarchy
                    && !redPlayer3Border.activeInHierarchy &&
                    !redPlayer4Border.activeInHierarchy)
                {
                    redPlayer1Button.interactable = false;
                    redPlayer2Button.interactable = false;
                    redPlayer3Button.interactable = false;
                    redPlayer4Button.interactable = false;

                    switch (MenuManager.quantidadePlayers)
                    {
                        case 4:
                            playerTurn = "BLUE";
                            initializeDice();
                            break;
                    }
                }
                break;
            case "GREEN":

                //brilho nas bordas dos players disponiveis
                if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && greenPlayer1Steps == 0)
                {
                    GreenPlayer1Border.SetActive(true);
                    greenPlayer1Button.interactable = true;


                }

                if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && greenPlayer2Steps == 0)
                {
                    GreenPlayer2Border.SetActive(true);
                    greenPlayer2Button.interactable = true;
                }

                if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && greenPlayer3Steps == 0)
                {
                    GreenPlayer3Border.SetActive(true);
                    greenPlayer3Button.interactable = true;
                }

                if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 &&  greenPlayer4Steps == 0)
                {
                    GreenPlayer4Border.SetActive(true);
                    greenPlayer4Button.interactable = true;
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////
                //////Fazer para todos os players verdes, pois verificam se eles estão no tabuleiro
                if (aux_green1 > 0 || greenPlayer1Steps > 0)
                {
                    GreenPlayer1Border.SetActive(true);
                    greenPlayer1Button.interactable = true;

                }
                if (aux_green2 > 0 || greenPlayer2Steps > 0)
                {
                    GreenPlayer2Border.SetActive(true);
                    greenPlayer2Button.interactable = true;

                }
                if (aux_green3 > 0 || greenPlayer3Steps > 0)
                {
                    GreenPlayer3Border.SetActive(true);
                    greenPlayer3Button.interactable = true;

                }
                if (aux_green4 > 0 || greenPlayer4Steps > 0)
                {
                    GreenPlayer4Border.SetActive(true);
                    greenPlayer4Button.interactable = true;

                }
                ///////////////////////////////////////////////////////////////////////////////////////


                /////////////////Desativa players Vencedores e Players na meta///////////////////
                if (greenPlayer1Steps == 76 || 
                    greenMovementBlock.Count - (greenPlayer1Steps) < SelectDiceNumAnimation
                      || (greenPlayer1Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer1Steps - 1) != SelectDiceNumAnimation + 1))
                {
                    greenPlayer1Button.interactable = false;
                    GreenPlayer1Border.SetActive(false);
                }
                if (greenPlayer2Steps == 76 || 
                    greenMovementBlock.Count - (greenPlayer2Steps) < SelectDiceNumAnimation
                    || (greenPlayer2Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer2Steps - 1) != SelectDiceNumAnimation + 1))
                {
                    greenPlayer2Button.interactable = false;
                    GreenPlayer2Border.SetActive(false);
                }
                if (greenPlayer3Steps == 76 ||
                    greenMovementBlock.Count - (greenPlayer3Steps) < SelectDiceNumAnimation
                    || (greenPlayer3Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer3Steps - 1) != SelectDiceNumAnimation + 1))
                {
                    greenPlayer3Button.interactable = false;
                    GreenPlayer3Border.SetActive(false);
                }
                if (greenPlayer4Steps == 76 || 
                    greenMovementBlock.Count - (greenPlayer4Steps) < SelectDiceNumAnimation
                    || (greenPlayer4Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer4Steps - 1) != SelectDiceNumAnimation + 1))
                {
                    greenPlayer4Button.interactable = false;
                    GreenPlayer4Border.SetActive(false);
                }
                ///////////////////////////////////////////////////////////////////////////////////////
                //////////////////////Se o Numero for 0///////////////////////////////////////
                if (SelectDiceNumAnimation == 0)
                {
                    if (greenPlayer1Steps != 0 || aux_green1 != 0)
                    {
                        GreenPlayer1Border.SetActive(false);
                        greenPlayer1Button.interactable = false;
                    }
                    if (greenPlayer2Steps != 0 || aux_green2 != 0)
                    {
                        GreenPlayer2Border.SetActive(false);
                        greenPlayer2Button.interactable = false;
                    }
                    if (greenPlayer3Steps != 0 || aux_green3 != 0)
                    {
                        GreenPlayer3Border.SetActive(false);
                        greenPlayer3Button.interactable = false;
                    }
                    if (greenPlayer4Steps != 0 || aux_green4 != 0)
                    {
                        GreenPlayer4Border.SetActive(false);
                        greenPlayer4Button.interactable = false;
                    }
                }
                /////////////////////////////////////////////////////////////////////////////////
                StartCoroutine("MovimentacaoPlayerGreen");

                if (!GreenPlayer1Border.activeInHierarchy && !GreenPlayer2Border.activeInHierarchy && !GreenPlayer3Border.activeInHierarchy
                        && !GreenPlayer4Border.activeInHierarchy)
                {
                    greenPlayer1Button.interactable = false;
                    greenPlayer2Button.interactable = false;
                    greenPlayer3Button.interactable = false;
                    greenPlayer4Button.interactable = false;

                    switch (MenuManager.quantidadePlayers)
                    {
                      
                        case 4:
                            playerTurn = "RED";
                            initializeDice();
                            break;
                    }

                }
                break;
            case "BLUE":

                if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 &&  bluePlayer1Steps == 0)
                {
                    BluePlayer1Border.SetActive(true);
                    bluePlayer1Button.interactable = true;
                }
                if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0  && bluePlayer2Steps == 0)
                {
                    BluePlayer2Border.SetActive(true);
                    bluePlayer2Button.interactable = true;
                }
                if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && bluePlayer3Steps == 0)
                {
                    BluePlayer3Border.SetActive(true);
                    bluePlayer3Button.interactable = true;
                }
                if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && bluePlayer4Steps == 0)
                {
                    BluePlayer4Border.SetActive(true);
                    bluePlayer4Button.interactable = true;
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////
                //////Fazer para todos os players verdes, pois verificam se eles estão no tabuleiro
                if (aux_blue1 > 0 || bluePlayer1Steps > 0)
                {
                    BluePlayer1Border.SetActive(true);
                    bluePlayer1Button.interactable = true;

                }
                if (aux_blue2 > 0 || bluePlayer2Steps > 0)
                {
                    BluePlayer2Border.SetActive(true);
                    bluePlayer2Button.interactable = true;

                }
                if (aux_blue3 > 0 || bluePlayer3Steps > 0)
                {
                    BluePlayer3Border.SetActive(true);
                    bluePlayer3Button.interactable = true;

                }
                if (aux_blue4 > 0 || bluePlayer4Steps > 0)
                {
                    BluePlayer4Border.SetActive(true);
                    bluePlayer4Button.interactable = true;

                }
                ///////////////////////////////////////////////////////////////////////////////////////


                /////////////////Desativa players Vencedores e Players na meta///////////////////
                if (bluePlayer1Steps == 76 ||
                    blueMovementBlock.Count - (bluePlayer1Steps) < SelectDiceNumAnimation
                    || (bluePlayer1Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer1Steps - 1) != SelectDiceNumAnimation + 1))
                {
                    bluePlayer1Button.interactable = false;
                    BluePlayer1Border.SetActive(false);
                }
                if (bluePlayer2Steps == 76 ||
                    blueMovementBlock.Count - (bluePlayer2Steps) < SelectDiceNumAnimation
                    || (bluePlayer2Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer2Steps - 1) != SelectDiceNumAnimation + 1))
                {
                    bluePlayer2Button.interactable = false;
                    BluePlayer2Border.SetActive(false);
                }
                if (bluePlayer3Steps == 76 || blueMovementBlock.Count - (bluePlayer3Steps) < SelectDiceNumAnimation
                    || (bluePlayer3Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer3Steps - 1) != SelectDiceNumAnimation + 1))
                {
                    bluePlayer3Button.interactable = false;
                    BluePlayer3Border.SetActive(false);
                }
                if (bluePlayer4Steps == 76 || blueMovementBlock.Count - (bluePlayer4Steps) < SelectDiceNumAnimation
                    || (bluePlayer4Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer4Steps - 1) != SelectDiceNumAnimation + 1))
                {
                    bluePlayer4Button.interactable = false;
                    BluePlayer4Border.SetActive(false);
                }
                //////////////////////Se o Numero for 0///////////////////////////////////////
                if (SelectDiceNumAnimation == 0)
                {
                    if (bluePlayer1Steps != 0 || aux_blue1 != 0)
                    {
                        BluePlayer1Border.SetActive(false);
                        bluePlayer1Button.interactable = false;
                    }
                    if (bluePlayer2Steps != 0 || aux_blue2 != 0)
                    {
                        BluePlayer2Border.SetActive(false);
                        bluePlayer2Button.interactable = false;
                    }
                    if (bluePlayer3Steps != 0 || aux_blue3 != 0)
                    {
                        BluePlayer3Border.SetActive(false);
                        bluePlayer3Button.interactable = false;
                    }
                    if (bluePlayer4Steps != 0 || aux_blue4 != 0)
                    {
                        BluePlayer4Border.SetActive(false);
                        bluePlayer4Button.interactable = false;
                    }
                }
                /////////////////////////////////////////////////////////////////////////////////
                StartCoroutine("MovimentacaoPlayerBlue");

                ///////////////////////////////////////////////////////////////////////////////////////////
                if (!BluePlayer1Border.activeInHierarchy && !BluePlayer2Border.activeInHierarchy && !BluePlayer3Border.activeInHierarchy
                        && !BluePlayer4Border.activeInHierarchy)
                {
                    bluePlayer1Button.interactable = false;
                    bluePlayer2Button.interactable = false;
                    bluePlayer3Button.interactable = false;
                    bluePlayer4Button.interactable = false;

                    switch (MenuManager.quantidadePlayers)
                    {
                        case 4:
                            playerTurn = "YELLOW";
                            initializeDice();
                            break;
                    }
                }
                break;

            case "YELLOW":
                if ((SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0) && yellowPlayer1Steps == 0)
                {
                    YellowPlayer1Border.SetActive(true);
                    yellowPlayer1Button.interactable = true;
                }
                if ((SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0) && yellowPlayer2Steps == 0)
                {
                    YellowPlayer2Border.SetActive(true);
                    yellowPlayer2Button.interactable = true;
                }
                if ((SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0) && yellowPlayer3Steps == 0)
                {
                    YellowPlayer3Border.SetActive(true);
                    yellowPlayer3Button.interactable = true;
                }
                if ((SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0) && yellowPlayer4Steps == 0)
                {
                    YellowPlayer4Border.SetActive(true);
                    yellowPlayer4Button.interactable = true;
                }
                //////VERIFICA SE OS PLAYERS TÃO NO TABULEIRO////////////////
                if (aux_yellow1 > 0 || yellowPlayer1Steps > 0)
                {
                    YellowPlayer1Border.SetActive(true);
                    yellowPlayer1Button.interactable = true;
                }

                if (aux_yellow2 > 0 || yellowPlayer2Steps > 0)
                {
                    YellowPlayer2Border.SetActive(true);
                    yellowPlayer2Button.interactable = true;
                }
                if (aux_yellow3 > 0 || yellowPlayer3Steps > 0)
                {
                    YellowPlayer3Border.SetActive(true);
                    yellowPlayer3Button.interactable = true;
                }
                if (aux_yellow4 > 0 || yellowPlayer4Steps > 0)
                {
                    YellowPlayer4Border.SetActive(true);
                    yellowPlayer4Button.interactable = true;
                }
                ///////////////////////////////////////////////////////////////////////////////////////

                /////////////////Desativa players Vencedores ou incapacitados//////////////////////////////////////////
                if ((yellowPlayer1Steps == 76 || 
                    yellowMovementBlock.Count - (yellowPlayer1Steps) < SelectDiceNumAnimation)||
                    (yellowPlayer1Steps > 68 &&
                     yellowMovementBlock.Count - (yellowPlayer1Steps - 1) != SelectDiceNumAnimation + 1 ))
                {
                    yellowPlayer1Button.interactable = false;
                    YellowPlayer1Border.SetActive(false);
                }

                if ((yellowPlayer2Steps == 76 ||
                    yellowMovementBlock.Count - (yellowPlayer2Steps) < SelectDiceNumAnimation)
                    ||( yellowPlayer2Steps > 68 &&
            yellowMovementBlock.Count - (yellowPlayer2Steps - 1) != SelectDiceNumAnimation + 1 ))
                {
                    yellowPlayer2Button.interactable = false;
                    YellowPlayer2Border.SetActive(false);
                }
                if (yellowPlayer3Steps == 76 || 
                    yellowMovementBlock.Count - (yellowPlayer3Steps) < SelectDiceNumAnimation
                    ||  (yellowPlayer3Steps > 68 &&
            yellowMovementBlock.Count - (yellowPlayer3Steps - 1) != SelectDiceNumAnimation + 1))
                {
                    yellowPlayer3Button.interactable = false;
                    YellowPlayer3Border.SetActive(false);
                }
                if ((yellowPlayer4Steps == 76 ||
                    yellowMovementBlock.Count - (yellowPlayer4Steps) < SelectDiceNumAnimation)
                    || (yellowPlayer4Steps > 68 &&
            yellowMovementBlock.Count - (yellowPlayer4Steps - 1) == SelectDiceNumAnimation + 1))
                {
                    yellowPlayer4Button.interactable = false;
                    YellowPlayer4Border.SetActive(false);
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////Se o Numero for 0///////////////////////////////////////
                if (SelectDiceNumAnimation == 0)
                {
                    if (yellowPlayer1Steps != 0 || aux_yellow1 != 0)
                    {
                        YellowPlayer1Border.SetActive(false);
                        yellowPlayer1Button.interactable = false;
                    }
                    if (yellowPlayer2Steps != 0 || aux_yellow2 != 0)
                    {
                        YellowPlayer2Border.SetActive(false);
                        yellowPlayer2Button.interactable = false;
                    }
                    if (yellowPlayer3Steps != 0 || aux_yellow3 != 0)
                    {
                        YellowPlayer3Border.SetActive(false);
                        yellowPlayer3Button.interactable = false;
                    }
                    if (yellowPlayer4Steps != 0 || aux_yellow4 != 0)
                    {
                        YellowPlayer4Border.SetActive(false);
                        yellowPlayer4Button.interactable = false;
                    }
                }
                /////////////////////////////////////////////////////////////////////////////////
                if (!YellowPlayer1Border.activeInHierarchy && !YellowPlayer2Border.activeInHierarchy && !YellowPlayer3Border.activeInHierarchy
                      && !YellowPlayer4Border.activeInHierarchy)
                {
                    yellowPlayer1Button.interactable = false;
                    yellowPlayer2Button.interactable = false;
                    yellowPlayer3Button.interactable = false;
                    yellowPlayer4Button.interactable = false;

                    switch (MenuManager.quantidadePlayers)
                    {
                        case 4:
                            playerTurn = "GREEN";
                            initializeDice();
                            break;
                    }
                }
                break;
        }
    }
    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////////////////

    ///INICIALIZA O DADO,VERIFICANDO DE QUEM É A VEZ DE JOGAR E SETA A POSIÇÃO DO DADO DE ACORDO COM A VEZ DO JOGADOR
    
    #region Posicao do dado 
    private void initializeDice()
    {
        diceRollButton.interactable = true;
        diceRollAnimation.SetActive(false);
        dice2RollAnimation.SetActive(false);
        dice3RollAnimation.SetActive(false);
        dice4RollAnimation.SetActive(false);
        dice5RollAnimation.SetActive(false);
        dice6RollAnimation.SetActive(false);

        switch (MenuManager.quantidadePlayers)
        {
            case 4:
                if (avancoYellow == true)
                {
                    Debug.Log("AVANCO PERMITIDO");
                    frameGreen.SetActive(false);
                    playerTurn = "YELLOW";
                    diceNumber.text = "Roll";
                    diceRoll.position = yellowDiceRoll.position;
                    avancoYellow = false;

                    if (yellowPlayer1Steps > 0)
                    {
                        yellowPlayer1Button.interactable = true;
                        YellowPlayer1Border.SetActive(true);
                    }
                    if (yellowPlayer2Steps > 0)
                    {
                        yellowPlayer2Button.interactable = true;
                        YellowPlayer1Border.SetActive(true);
                    }
                    if (yellowPlayer3Steps > 0)
                    {
                        yellowPlayer3Button.interactable = true;
                        YellowPlayer1Border.SetActive(true);
                    }
                    if (yellowPlayer4Steps > 0)
                    {
                        yellowPlayer4Button.interactable = true;
                        YellowPlayer1Border.SetActive(true);
                    }

                }
                if (avancoRed == true)
                {
                    Debug.Log("AVANCO PERMITIDO");
                    frameBlue.SetActive(false);
                    playerTurn = "RED";
                    diceNumber.text = "Roll";
                    diceRoll.position = redDiceRoll.position;
                    avancoRed = false;

                    if (redPlayer1Steps > 0)
                    {
                        redPlayer1Button.interactable = true;
                        redPlayer1Border.SetActive(true);
                    }
                    if (redPlayer2Steps > 0)
                    {
                        redPlayer2Button.interactable = true;
                        redPlayer1Border.SetActive(true);
                    }
                    if (redPlayer3Steps > 0)
                    {
                        redPlayer3Button.interactable = true;
                        redPlayer1Border.SetActive(true);
                    }
                    if (redPlayer4Steps > 0)
                    {
                        redPlayer4Button.interactable = true;
                        redPlayer1Border.SetActive(true);
                    }
                    //StartCoroutine("MovimentacaoPlayerRedCatch");
                }
                if (avancoBlue == true)
                {
                    Debug.Log("AVANCO PERMITIDO");
                    frameYellow.SetActive(false);
                    playerTurn = "BLUE";
                    diceNumber.text = "Roll";
                    diceRoll.position = blueDiceRoll.position;
                    avancoBlue = false;

                    if (bluePlayer1Steps > 0)
                    {
                        bluePlayer1Button.interactable = true;
                        BluePlayer1Border.SetActive(true);
                    }
                    if (bluePlayer2Steps > 0)
                    {
                        bluePlayer2Button.interactable = true;
                        BluePlayer1Border.SetActive(true);
                    }
                    if (bluePlayer3Steps > 0)
                    {
                        bluePlayer3Button.interactable = true;
                        BluePlayer1Border.SetActive(true);
                    }
                    if (bluePlayer4Steps > 0)
                    {
                        bluePlayer4Button.interactable = true;
                        BluePlayer1Border.SetActive(true);
                    }
                    StartCoroutine("MovimentacaoPlayerBlueCatch");
                }
                if (avancoGreen == true)
                {
                    Debug.Log("AVANCO PERMITIDO");
                    frameRed.SetActive(false);
                    playerTurn = "GREEN";
                    diceNumber.text = "Roll";
                    diceRoll.position = greenDiceRoll.position;
                    avancoGreen = false;

                    if (greenPlayer1Steps > 0)
                    {
                        greenPlayer1Button.interactable = true;
                        GreenPlayer1Border.SetActive(true);
                    }
                    if (greenPlayer2Steps > 0)
                    {
                        greenPlayer2Button.interactable = true;
                        GreenPlayer1Border.SetActive(true);
                    }
                    if (greenPlayer3Steps > 0)
                    {
                        greenPlayer3Button.interactable = true;
                        GreenPlayer1Border.SetActive(true);
                    }
                    if (greenPlayer4Steps > 0)
                    {
                        greenPlayer4Button.interactable = true;
                        GreenPlayer1Border.SetActive(true);
                    }
                    StartCoroutine("MovimentacaoPlayerGreenCatch");
                }
                if (playerTurn == "RED" && SelectDiceNumAnimation != 10   && statusRed != "AVANCAR")
                {
                    ///////Ativando e dsativando peoes/////////////////////////////////////
                    redPlayer1Button.interactable = true;
                    redPlayer2Button.interactable = true;
                    redPlayer3Button.interactable = true;
                    redPlayer4Button.interactable = true;

                    bluePlayer1Button.interactable = false;
                    bluePlayer2Button.interactable = false;
                    bluePlayer3Button.interactable = false;
                    bluePlayer4Button.interactable = false;

                    yellowPlayer1Button.interactable = false;
                    yellowPlayer2Button.interactable = false;
                    yellowPlayer3Button.interactable = false;
                    yellowPlayer4Button.interactable = false;

                    greenPlayer1Button.interactable = false;
                    greenPlayer2Button.interactable = false;
                    greenPlayer3Button.interactable = false;
                    greenPlayer4Button.interactable = false;
                    //////////////////////////////////////////////////////////////////////
                    diceNumber.text = "Roll";
                    diceRoll.position = redDiceRoll.position;
                    frameRed.SetActive(true);
                    frameYellow.SetActive(false);
                    frameBlue.SetActive(false);
                    frameGreen.SetActive(false);
                }
                if (playerTurn == "YELLOW" && SelectDiceNumAnimation != 10 && statusYellow != "AVANCAR")
                {
                    ///////////Ativando e desativando Peoes///////////////////////////////////
                    redPlayer1Button.interactable = false;
                    redPlayer2Button.interactable = false;
                    redPlayer3Button.interactable = false;
                    redPlayer4Button.interactable = false;

                    bluePlayer1Button.interactable = false;
                    bluePlayer2Button.interactable = false;
                    bluePlayer3Button.interactable = false;
                    bluePlayer4Button.interactable = false;

                    yellowPlayer1Button.interactable = true;
                    yellowPlayer2Button.interactable = true;
                    yellowPlayer3Button.interactable = true;
                    yellowPlayer4Button.interactable = true;

                    greenPlayer1Button.interactable = false;
                    greenPlayer2Button.interactable = false;
                    greenPlayer3Button.interactable = false;
                    greenPlayer4Button.interactable = false;
                    /////////////////////////////////////////////////////////////////////////////
                    diceNumber.text = "Roll";
                    diceRoll.position = yellowDiceRoll.position;
                    frameYellow.SetActive(true);
                    frameRed.SetActive(false);
                    frameBlue.SetActive(false);
                    frameGreen.SetActive(false);
                }
                if (playerTurn == "BLUE" && SelectDiceNumAnimation != 10  && statusBlue != "AVANCAR")
                {
                    BarraContador.SetActive(false);
                    ////Ativando e Desativando Peoes/////////////////////////
                    redPlayer1Button.interactable = false;
                    redPlayer2Button.interactable = false;
                    redPlayer3Button.interactable = false;
                    redPlayer4Button.interactable = false;

                    bluePlayer1Button.interactable = true;
                    bluePlayer2Button.interactable = true;
                    bluePlayer3Button.interactable = true;
                    bluePlayer4Button.interactable = true;

                    yellowPlayer1Button.interactable = false;
                    yellowPlayer2Button.interactable = false;
                    yellowPlayer3Button.interactable = false;
                    yellowPlayer4Button.interactable = false;

                    greenPlayer1Button.interactable = false;
                    greenPlayer2Button.interactable = false;
                    greenPlayer3Button.interactable = false;
                    greenPlayer4Button.interactable = false;
                    //////////////////////////////////////////////
                    diceNumber.text = "Roll";
                    diceRoll.position = blueDiceRoll.position;
                    StartCoroutine("RolagemDeDadosIA");
                    frameBlue.SetActive(true);
                    frameYellow.SetActive(false);
                    frameRed.SetActive(false);
                    frameGreen.SetActive(false);
                }
                if (playerTurn == "GREEN" && SelectDiceNumAnimation != 10 && statusGreen != "AVANCAR")
                {
                    BarraContador.SetActive(false);
                    /////Ativando e desativando Peoes//////////////////////
                    redPlayer1Button.interactable = false;
                    redPlayer2Button.interactable = false;
                    redPlayer3Button.interactable = false;
                    redPlayer4Button.interactable = false;

                    bluePlayer1Button.interactable = false;
                    bluePlayer2Button.interactable = false;
                    bluePlayer3Button.interactable = false;
                    bluePlayer4Button.interactable = false;

                    yellowPlayer1Button.interactable = false;
                    yellowPlayer2Button.interactable = false;
                    yellowPlayer3Button.interactable = false;
                    yellowPlayer4Button.interactable = false;

                    greenPlayer1Button.interactable = true;
                    greenPlayer2Button.interactable = true;
                    greenPlayer3Button.interactable = true;
                    greenPlayer4Button.interactable = true;
                    //////////////////////////////////////////////////////
                    diceNumber.text = "Roll";
                    diceRoll.position = greenDiceRoll.position;
                    StartCoroutine("RolagemDeDadosIA");
                    frameGreen.SetActive(true);
                    frameBlue.SetActive(false);
                    frameYellow.SetActive(false);
                    frameRed.SetActive(false);

                }
                redPlayer1Border.SetActive(false);
                redPlayer2Border.SetActive(false);
                redPlayer3Border.SetActive(false);
                redPlayer4Border.SetActive(false);

                BluePlayer1Border.SetActive(false);
                BluePlayer2Border.SetActive(false);
                BluePlayer3Border.SetActive(false);
                BluePlayer4Border.SetActive(false);

                YellowPlayer1Border.SetActive(false);
                YellowPlayer2Border.SetActive(false);
                YellowPlayer3Border.SetActive(false);
                YellowPlayer4Border.SetActive(false);

                GreenPlayer1Border.SetActive(false);
                GreenPlayer2Border.SetActive(false);
                GreenPlayer3Border.SetActive(false);
                GreenPlayer4Border.SetActive(false);
                break;
        }
    }
    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////////////

    //////////////////////////////MOVIMENTO DOS PLAYERS VERMELHOS/////////////////////////////////
    #region Movimento dos Players Vermelhos 
    ///////////////////////////////////////////RED PLAYER1////////////////////////////////////////
    #region Movimentacao Player red 1
    public void redPlayer1Movement()
    {
        redPlayer1Border.SetActive(false);
        redPlayer2Border.SetActive(false);
        redPlayer3Border.SetActive(false);
        redPlayer4Border.SetActive(false);

        redPlayer1Button.interactable = false;
        redPlayer2Button.interactable = false;
        redPlayer3Button.interactable = false;
        redPlayer4Button.interactable = false;
      

        if (playerTurn == "RED" && redPlayer1Steps > 67 &&
              redMovementBlock.Count - (redPlayer1Steps - 1) == SelectDiceNumAnimation + 1 && avancoRed == false)
        {
            StartCoroutine("PeaoRed1");
        }

        if (playerTurn == "RED" && (redMovementBlock.Count - (redPlayer1Steps - 1)) > SelectDiceNumAnimation
            && redPlayer1Steps < 69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "RED";
               
            }
            /*
            if (redPlayer1Steps == 76)
            {
              redPlayer1.transform.position = redMovementBlock[redPlayer1Steps - 1].transform.position;
                
            }
           */
            if (aux_casasTablueiro == 1)
            {
                redPlayer1.transform.position = redMovementBlock[aux_casasTablueiro].transform.position;
                redPlayer1_CasaAtual = redMovementBlock[aux_casasTablueiro].transform.position;
                aux_casasTablueiro = 0;
                redPlayer1Steps++;
            }

            if (redPlayer1Steps > 0 && avancoRed == false)
            {
                StartCoroutine("PeaoRed1");
            }
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && redPlayer1Steps == 0)
            {
                BarraContador.SetActive(false);
                redPlayer1.transform.position = redMovementBlock[redPlayer1Steps].transform.position;
                redPlayer1_CasaAtual = redMovementBlock[redPlayer1Steps].transform.position;
                aux_casasTablueiro += 1;
                playerTurn = "RED";
                currentPlayerName = "RED PLAYER 1";
                initializeDice();
            }

            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 && SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoBlue");
                        break;
                }

            }
        }
        else
        {
            if (SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoBlue");
                        break;
                }
            }
        }



    }
    #endregion
    ///////////////////////////////////////////RED PLAYER2/////////////////////////////////////////
    #region Movimentacao Player red 2
    public void redPlayer2Movement()
    {
        redPlayer1Border.SetActive(false);
        redPlayer2Border.SetActive(false);
        redPlayer3Border.SetActive(false);
        redPlayer4Border.SetActive(false);

        redPlayer1Button.interactable = false;
        redPlayer2Button.interactable = false;
        redPlayer3Button.interactable = false;
        redPlayer4Button.interactable = false;

        if (playerTurn == "RED" && redPlayer2Steps > 67 &&
          redMovementBlock.Count - (redPlayer2Steps - 1) == SelectDiceNumAnimation + 1 && avancoRed == false)
        {
            StartCoroutine("PeaoRed2");
        }


        if (playerTurn == "RED" && (redMovementBlock.Count - (redPlayer2Steps - 1)) > SelectDiceNumAnimation
            && redPlayer2Steps < 69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "RED";
               
            }
            /*
            if (redPlayer2Steps == 76)
            { 
                redPlayer2.transform.position = redMovementBlock[redPlayer2Steps - 1].transform.position;
            }
            */
          
            if (aux_casasTabuleiro2 == 1)
            {
                redPlayer2.transform.position = redMovementBlock[aux_casasTabuleiro2].transform.position;
                redPlayer2_CasaAtual = redMovementBlock[aux_casasTabuleiro2].transform.position;
                aux_casasTabuleiro2 = 0;
                redPlayer2Steps++;
            }
            if (redPlayer2Steps > 0 && avancoRed == false)
            {
                StartCoroutine("PeaoRed2");
            }


            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && redPlayer2Steps == 0)
            {
                BarraContador.SetActive(false);
                redPlayer2.transform.position = redMovementBlock[redPlayer2Steps].transform.position;
                redPlayer2_CasaAtual = redMovementBlock[redPlayer2Steps].transform.position;
                aux_casasTabuleiro2 += 1;
                playerTurn = "RED";
                currentPlayerName = "RED PLAYER 2";
                initializeDice();
            }

            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 && SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoBlue");
                        break;
                }
            }
        }
        else
        {
            if (SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoBlue");
                        break;
                }

            }
        }

    }
    #endregion
    ///////////////////////////////////////////RED PLAYER3//////////////////////////////////////////
    #region Movimentacao Player red 3
    public void redPlayer3Movement()
    {
        redPlayer1Border.SetActive(false);
        redPlayer2Border.SetActive(false);
        redPlayer3Border.SetActive(false);
        redPlayer4Border.SetActive(false);

        redPlayer1Button.interactable = false;
        redPlayer2Button.interactable = false;
        redPlayer3Button.interactable = false;
        redPlayer4Button.interactable = false;

        if (playerTurn == "RED" && redPlayer3Steps > 67 &&
          redMovementBlock.Count - (redPlayer3Steps - 1) == SelectDiceNumAnimation + 1 && avancoRed == false)
        {
            StartCoroutine("PeaoRed3");
        }

        if (playerTurn == "RED" && (redMovementBlock.Count - (redPlayer3Steps - 1)) > SelectDiceNumAnimation &&
            redPlayer3Steps < 69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "RED";
                
            }
            /*
            if (redPlayer3Steps == 76)
            { 
              redPlayer3.transform.position = redMovementBlock[redPlayer3Steps - 1].transform.position;
            }
           */

            if (aux_casasTabuleiro3 == 1)
            {
                redPlayer3.transform.position = redMovementBlock[aux_casasTabuleiro3].transform.position;
                redPlayer3_CasaAtual = redMovementBlock[aux_casasTabuleiro3].transform.position;
                aux_casasTabuleiro3 = 0;
                redPlayer3Steps++;
            }

            if (redPlayer3Steps > 0 && avancoRed == false)
            {
                StartCoroutine("PeaoRed3");
            }


            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && redPlayer3Steps == 0)
            {
                BarraContador.SetActive(false);
                redPlayer3.transform.position = redMovementBlock[redPlayer3Steps].transform.position;
                redPlayer3_CasaAtual = redMovementBlock[redPlayer3Steps].transform.position;
                aux_casasTabuleiro3 += 1;
                playerTurn = "RED";
                currentPlayerName = "RED PLAYER 3";
                initializeDice();
            }

            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 && SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoBlue");
                        break;
                    
                }

            }
        }
        else
        {
            if (SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoBlue");
                        break;
                  
                }
            }
        }


    }
    #endregion
    /////////////////////////////////////RED PLAYER4////////////////////////////////////////////////
    #region Movimentacao Player red 4
    public void redPlayer4Movement()
    {
        redPlayer1Border.SetActive(false);
        redPlayer2Border.SetActive(false);
        redPlayer3Border.SetActive(false);
        redPlayer4Border.SetActive(false);

        redPlayer1Button.interactable = false;
        redPlayer2Button.interactable = false;
        redPlayer3Button.interactable = false;
        redPlayer4Button.interactable = false;

        if (playerTurn == "RED" && redPlayer4Steps > 67 &&
        redMovementBlock.Count - (redPlayer4Steps - 1) == SelectDiceNumAnimation + 1 && avancoRed == false)
        {
            StartCoroutine("PeaoRed4");
        }

        if (playerTurn == "RED" && (redMovementBlock.Count - (redPlayer4Steps - 1)) > SelectDiceNumAnimation &&
            redPlayer4Steps < 69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "RED";
                
            }
            /*
            if (redPlayer4Steps == 76)
            {
                
            redPlayer4.transform.position = redMovementBlock[redPlayer4Steps - 1].transform.position;
            }
            */

            if (aux_casasTabuleiro4 == 1)
            {
                redPlayer4.transform.position = redMovementBlock[aux_casasTabuleiro4].transform.position;
                redPlayer4_CasaAtual = redMovementBlock[aux_casasTabuleiro4].transform.position;
                aux_casasTabuleiro4 = 0;
                redPlayer4Steps++;
            }

            if (redPlayer4Steps > 0 && avancoRed == false)
            {
                StartCoroutine("PeaoRed4");
            }



            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && redPlayer4Steps == 0)
            {
                BarraContador.SetActive(false);
                redPlayer4.transform.position = redMovementBlock[redPlayer4Steps].transform.position;
                redPlayer4_CasaAtual = redMovementBlock[redPlayer4Steps].transform.position;
                aux_casasTabuleiro4 += 1;
                playerTurn = "RED";
                currentPlayerName = "RED PLAYER 4";
                initializeDice();
            }

            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 && SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoBlue");
                        break;
                }

            }

        }
        else
        {
            if (SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:
                        StartCoroutine("TrocaDeTurnoBlue");
                        break;
                }
            }
        }
    }
    #endregion
    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////////
   
    //////////////////////////////////////////MOVIMENTACAO PLAYERS AMARELOS///////////////////////
    #region Movimento dos Players Amarelos
    //////////////////////////////////////////////YELLOW PLAYER1///////////////////////////////////
    #region Movimento do Player Amarelo 1
    public void yellowPlayer1Movement()
    {
        YellowPlayer1Border.SetActive(false);
        YellowPlayer2Border.SetActive(false);
        YellowPlayer3Border.SetActive(false);
        YellowPlayer4Border.SetActive(false);

        yellowPlayer1Button.interactable = false;
        yellowPlayer2Button.interactable = false;
        yellowPlayer3Button.interactable = false;
        yellowPlayer4Button.interactable = false;

        if (playerTurn == "YELLOW" && yellowPlayer1Steps > 67 &&
            yellowMovementBlock.Count - (yellowPlayer1Steps - 1) == SelectDiceNumAnimation + 1 && avancoYellow == false)
        {
            StartCoroutine("PeaoYellow1");
        }
        ///////////VERIFICA SE O PLAYER TA APTO PARA SE MOVIMENTAR DE ACORDO COM O DADO TIRADO////////////////////
        if (playerTurn == "YELLOW" && (yellowMovementBlock.Count - (yellowPlayer1Steps - 1)) > SelectDiceNumAnimation &&
           yellowPlayer1Steps < 69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "YELLOW";
            }
            /*
             if (yellowPlayer1Steps == 76)
             {
               yellowPlayer1.transform.position = yellowMovementBlock[yellowPlayer1Steps-1].transform.position;
             }
         */
            if (aux_yellow1 == 1)
            {
                yellowPlayer1.transform.position = yellowMovementBlock[aux_yellow1].transform.position;
                yellowPlayer1_CasaAtual = yellowMovementBlock[aux_yellow1].transform.position;
                aux_yellow1 = 0;
                yellowPlayer1Steps++;
            }
            ///CASO OS PASSOS DO PLAYER SEJAM MAIORES QUE 0, O PLAYER SE MOVIMENTA ATRAVÉS DE ROTINA QUE ESTÁ SENDO CHAMADA/////
            if (yellowPlayer1Steps > 0 && avancoYellow == false)
            {
                StartCoroutine("PeaoYellow1");
                Debug.Log("Passei por aqui primeiro");
            }
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && yellowPlayer1Steps == 0)
            {
                BarraContador.SetActive(false);
                yellowPlayer1.transform.position = yellowMovementBlock[yellowPlayer1Steps].transform.position;
                yellowPlayer1_CasaAtual = yellowMovementBlock[yellowPlayer1Steps].transform.position;
                aux_yellow1 += 1;
                playerTurn = "YELLOW";
                currentPlayerName = "YELLOW PLAYER 1";
                initializeDice();
            }
            ////////////SE O DADO FOR DIFERENTE DE 0 ou 9 CHAMA A ROTINA PRA TROCAR DE TURNO/////////////////////
            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 &&
                SelectDiceNumAnimation != 10 )
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:
                       // Debug.Log("Essa rotina foi chamada");
                        StartCoroutine("TrocaDeTurnoGreen");
                        break;
                }
                initializeDice();
            }


        }
        else
        {
            if (SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoGreen");
                        break;
                }
                initializeDice();
            }
        }


    }
    #endregion
    //////////////////////////////////////////////YELLOW PLAYER2///////////////////////////////////////
    #region Movimento do Player Amarelo 2
    public void yellowPlayer2Movement()
    {
        YellowPlayer1Border.SetActive(false);
        YellowPlayer2Border.SetActive(false);
        YellowPlayer3Border.SetActive(false);
        YellowPlayer4Border.SetActive(false);

        yellowPlayer1Button.interactable = false;
        yellowPlayer2Button.interactable = false;
        yellowPlayer3Button.interactable = false;
        yellowPlayer4Button.interactable = false;

        if (playerTurn == "YELLOW" && yellowPlayer2Steps > 67 &&
            yellowMovementBlock.Count - (yellowPlayer2Steps - 1) == SelectDiceNumAnimation + 1 && avancoYellow == false)
        {
            StartCoroutine("PeaoYellow2");
        }

        if (playerTurn == "YELLOW" && (yellowMovementBlock.Count - (yellowPlayer2Steps - 1)) > SelectDiceNumAnimation
            && yellowPlayer2Steps < 69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "YELLOW";

            }
            /*
            if (yellowPlayer2Steps == 76)
            {
                yellowPlayer2.transform.position = yellowMovementBlock[yellowPlayer2Steps - 1].transform.position;
                avancoRed = true;
                playerTurn = "RED";
                statusRed = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }
            */
            if (aux_yellow2 == 1)
            {
                yellowPlayer2.transform.position = yellowMovementBlock[aux_yellow2].transform.position;
                yellowPlayer2_CasaAtual = yellowMovementBlock[aux_yellow2].transform.position;
                aux_yellow2 = 0;
                yellowPlayer2Steps++;
            }

            if (yellowPlayer2Steps > 0 && avancoYellow == false)
            {

                StartCoroutine("PeaoYellow2");
            }

            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && yellowPlayer2Steps == 0)
            {
                BarraContador.SetActive(false);
                yellowPlayer2.transform.position = yellowMovementBlock[yellowPlayer2Steps].transform.position;
                yellowPlayer2_CasaAtual = yellowMovementBlock[yellowPlayer2Steps].transform.position;
                aux_yellow2 += 1;
                playerTurn = "YELLOW";
                currentPlayerName = "YELLOW PLAYER 2";
                initializeDice();
            }
            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 &&
                SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoGreen");
                        break;
                }
                initializeDice();
            }


        }
        else
        {
            if (SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoGreen");
                        break;
                    
                }
                initializeDice();
            }
        }

    }
    #endregion
    //////////////////////////////////////////////YELLOW PLAYER3///////////////////////////////////////
    #region Movimento Player Amarelo 3
    public void yellowPlayer3Movement()
    {
        YellowPlayer1Border.SetActive(false);
        YellowPlayer2Border.SetActive(false);
        YellowPlayer3Border.SetActive(false);
        YellowPlayer4Border.SetActive(false);

        yellowPlayer1Button.interactable = false;
        yellowPlayer2Button.interactable = false;
        yellowPlayer3Button.interactable = false;
        yellowPlayer4Button.interactable = false;

        if (playerTurn == "YELLOW" && yellowPlayer3Steps > 67 &&
              yellowMovementBlock.Count - (yellowPlayer3Steps - 1) == SelectDiceNumAnimation + 1 && avancoYellow == false)
        {
            StartCoroutine("PeaoYellow3");
        }

        if (playerTurn == "YELLOW" && (yellowMovementBlock.Count - (yellowPlayer3Steps - 1)) > SelectDiceNumAnimation
            && yellowPlayer3Steps < 69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "YELLOW";

            }
            /*
            if (yellowPlayer3Steps == 76)
            {
                yellowPlayer3.transform.position = yellowMovementBlock[yellowPlayer3Steps - 1].transform.position;
                avancoRed = true;
                playerTurn = "RED";
                statusRed = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }
            */

            if (aux_yellow3 == 1)
            {
                yellowPlayer3.transform.position = yellowMovementBlock[aux_yellow3].transform.position;
                yellowPlayer3_CasaAtual = yellowMovementBlock[aux_yellow3].transform.position;
                aux_yellow3 = 0;
                yellowPlayer3Steps++;
            }

            if (yellowPlayer3Steps > 0 && avancoYellow == false)
            {

                StartCoroutine("PeaoYellow3");
            }



            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && yellowPlayer3Steps == 0)
            {
                BarraContador.SetActive(false);
                yellowPlayer3.transform.position = yellowMovementBlock[yellowPlayer3Steps].transform.position;
                yellowPlayer3_CasaAtual = yellowMovementBlock[yellowPlayer3Steps].transform.position;
                aux_yellow3 += 1;
                playerTurn = "YELLOW";
                currentPlayerName = "YELLOW PLAYER 3";
                initializeDice();
            }
            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 &&
                SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoGreen");
                        break;
                }
                initializeDice();
            }



        }
        else
        {
            if (SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:
                        StartCoroutine("TrocaDeTurnoGreen");
                        break;
                }
                initializeDice();
            }
        }


    }
    #endregion
    ////////////////////////////////////////////////////YELLOW PLAYER4////////////////////////////////
    #region Movimento Player Amarelo 4
    public void yellowPlayer4Movement()
    {
        YellowPlayer1Border.SetActive(false);
        YellowPlayer2Border.SetActive(false);
        YellowPlayer3Border.SetActive(false);
        YellowPlayer4Border.SetActive(false);

        yellowPlayer1Button.interactable = false;
        yellowPlayer2Button.interactable = false;
        yellowPlayer3Button.interactable = false;
        yellowPlayer4Button.interactable = false;

        if (playerTurn == "YELLOW" && yellowPlayer4Steps > 67 &&
           yellowMovementBlock.Count - (yellowPlayer4Steps - 1) == SelectDiceNumAnimation + 1 && avancoYellow == false)
        {
            StartCoroutine("PeaoYellow4");
        }

        if (playerTurn == "YELLOW" && (yellowMovementBlock.Count - (yellowPlayer4Steps - 1)) > SelectDiceNumAnimation
            && avancoYellow == false && yellowPlayer4Steps < 69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "YELLOW";

            }
            /*
            if (yellowPlayer4Steps == 76)
            {

                yellowPlayer4.transform.position = yellowMovementBlock[yellowPlayer4Steps - 1].transform.position;
                avancoRed = true;
                playerTurn = "RED";
                statusRed = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }
            */
            //Estava pulando uma casa por algum motivo

            if (aux_yellow4 == 1)
            {

                yellowPlayer4.transform.position = yellowMovementBlock[aux_yellow4].transform.position;
                yellowPlayer4_CasaAtual = yellowMovementBlock[aux_yellow4].transform.position;
                aux_yellow4 = 0;
                yellowPlayer4Steps++;
            }

            if (yellowPlayer4Steps > 0 && avancoYellow == false)
            {
                StartCoroutine("PeaoYellow4");
            }

            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && yellowPlayer4Steps == 0)
            {

                BarraContador.SetActive(false);
                yellowPlayer4.transform.position = yellowMovementBlock[yellowPlayer4Steps].transform.position;
                yellowPlayer4_CasaAtual = yellowMovementBlock[yellowPlayer4Steps].transform.position;
                aux_yellow4 += 1;
                playerTurn = "YELLOW";
                currentPlayerName = "YELLOW PLAYER 4";
                initializeDice();
            }
            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 &&
                SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoGreen");
                        break;
                 
                }
                initializeDice();
            }



        }
        else
        {
            if (SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:
                        //tem que ser green
                        StartCoroutine("TrocaDeTurnoGreen");
                        break;
                   
                }
                initializeDice();
            }
        }

    }
    #endregion

    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////Movimento dos Players Verdes////////////////////////
    #region Movimento dos Players Verdes
    ////////////////////////////////////////////////////PLAYER GREEN 1///////////////////////////////////////
    #region Movimento do Player Green 1
    public void greenPlayer1Movement()
    {
        GreenPlayer1Border.SetActive(false);
        GreenPlayer2Border.SetActive(false);
        GreenPlayer3Border.SetActive(false);
        GreenPlayer4Border.SetActive(false);

        greenPlayer1Button.interactable = false;
        greenPlayer2Button.interactable = false;
        greenPlayer3Button.interactable = false;
        greenPlayer4Button.interactable = false;

        if (playerTurn == "GREEN" && greenPlayer1Steps > 67 &&
            greenMovementBlock.Count - (greenPlayer1Steps - 1) == SelectDiceNumAnimation + 1 && avancoGreen == false)
        {
            StartCoroutine("PeaoGreen1");
        }
        if (playerTurn == "GREEN" && (greenMovementBlock.Count - (greenPlayer1Steps - 1)) > SelectDiceNumAnimation &&
            greenPlayer1Steps < 69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "GREEN";
            }
            /*
            if (greenPlayer1Steps == 56)
            {
                greenPlayer1.transform.position = greenMovementBlock[greenPlayer1Steps - 1].transform.position;
            }
            */


            if (aux_green1 == 1)
            {

                greenPlayer1.transform.position = greenMovementBlock[aux_green1].transform.position;
                greenPlayer1_CasaAtual = greenMovementBlock[aux_green1].transform.position;
                aux_green1 = 0;
                greenPlayer1Steps++;
            }
            if (greenPlayer1Steps > 0 && avancoGreen == false)
            {

                StartCoroutine("PeaoGreen1");
            }

            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && greenPlayer1Steps == 0)
            {

                greenPlayer1.transform.position = greenMovementBlock[greenPlayer1Steps].transform.position;
                greenPlayer1_CasaAtual = greenMovementBlock[greenPlayer1Steps].transform.position;
                aux_green1 += 1;
                playerTurn = "GREEN";
                currentPlayerName = "GREEN PLAYER 1";
                initializeDice();
            }
            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 &&
               SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoRed");
                        break;
                }

            }
        }
        else
        {
            if (SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoRed");
                        break;
                }
            }

        }
    }
    #endregion
    //////////////////////////////////////////////PLAYER GREEN 2/////////////////////////////////////////////
    #region Movimento do Player Green 2
    public void greenPlayer2Movement()
    {
        GreenPlayer1Border.SetActive(false);
        GreenPlayer2Border.SetActive(false);
        GreenPlayer3Border.SetActive(false);
        GreenPlayer4Border.SetActive(false);

        greenPlayer1Button.interactable = false;
        greenPlayer2Button.interactable = false;
        greenPlayer3Button.interactable = false;
        greenPlayer4Button.interactable = false;

        if (playerTurn == "GREEN" && greenPlayer2Steps > 67 &&
            greenMovementBlock.Count - (greenPlayer2Steps - 1) == SelectDiceNumAnimation + 1 && avancoGreen == false)
        {
            StartCoroutine("PeaoGreen2");
        }
        if (playerTurn == "GREEN" && (greenMovementBlock.Count - (greenPlayer2Steps - 1)) > SelectDiceNumAnimation &&
            greenPlayer2Steps < 69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "GREEN";
            }
            /*
            if (greenPlayer1Steps == 56)
            {
                greenPlayer1.transform.position = greenMovementBlock[greenPlayer1Steps - 1].transform.position;
            }
            */

            if (aux_green2 == 1)
            {

                greenPlayer2.transform.position = greenMovementBlock[aux_green2].transform.position;
                greenPlayer2_CasaAtual = greenMovementBlock[aux_green2].transform.position;
                aux_green2 = 0;
                greenPlayer2Steps++;
            }
            if (greenPlayer2Steps > 0 && avancoGreen == false)
            {

                StartCoroutine("PeaoGreen2");
            }

            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && greenPlayer2Steps == 0)
            {

                greenPlayer2.transform.position = greenMovementBlock[greenPlayer2Steps].transform.position;
                greenPlayer2_CasaAtual = greenMovementBlock[greenPlayer2Steps].transform.position;
                aux_green2 += 1;
                playerTurn = "GREEN";
                currentPlayerName = "GREEN PLAYER 2";
                initializeDice();
            }

            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 &&
               SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoRed");
                        break;
                }

            }
        }
        else
        {
            if (SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoRed");
                        break;
                }
            }

        }

    }
    #endregion
    //////////////////////////PLAYER GREEN 3/////////////////////////////////////////////////////////////////
    #region Moviemento do Player Green 3
    public void greenPlayer3Movement()
    {
        GreenPlayer1Border.SetActive(false);
        GreenPlayer2Border.SetActive(false);
        GreenPlayer3Border.SetActive(false);
        GreenPlayer4Border.SetActive(false);

        greenPlayer1Button.interactable = false;
        greenPlayer2Button.interactable = false;
        greenPlayer3Button.interactable = false;
        greenPlayer4Button.interactable = false;

        if (playerTurn == "GREEN" && greenPlayer3Steps > 67 &&
            greenMovementBlock.Count - (greenPlayer3Steps - 1) == SelectDiceNumAnimation + 1 && avancoGreen == false)
        {
            StartCoroutine("PeaoGreen3");
        }
        if (playerTurn == "GREEN" && (greenMovementBlock.Count - (greenPlayer3Steps - 1)) > SelectDiceNumAnimation &&
            greenPlayer3Steps < 69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "GREEN";
            }
            /*
            if (greenPlayer1Steps == 56)
            {
                greenPlayer1.transform.position = greenMovementBlock[greenPlayer1Steps - 1].transform.position;
            }
            */

            if (aux_green3 == 1)
            {

                greenPlayer3.transform.position = greenMovementBlock[aux_green3].transform.position;
                greenPlayer3_CasaAtual = greenMovementBlock[aux_green3].transform.position;
                aux_green3 = 0;
                greenPlayer3Steps++;
            }
            if (greenPlayer3Steps > 0 && avancoGreen == false)
            {

                StartCoroutine("PeaoGreen3");
            }

            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && greenPlayer3Steps == 0)
            {

                greenPlayer3.transform.position = greenMovementBlock[greenPlayer3Steps].transform.position;
                greenPlayer3_CasaAtual = greenMovementBlock[greenPlayer3Steps].transform.position;
                aux_green3 += 1;
                playerTurn = "GREEN";
                currentPlayerName = "GREEN PLAYER 3";
                initializeDice();
            }

            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 &&
               SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoRed");
                        break;
                }

            }
        }
        else
        {
            if (SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoRed");
                        break;
                }
            }

        }
    }
    #endregion
    //////////////////////////////////////////////PLAYER GREEN 4//////////////////////////////////////////////
    #region Movimento do Player Green 4
    public void greenPlayer4Movement()
    {
        GreenPlayer1Border.SetActive(false);
        GreenPlayer2Border.SetActive(false);
        GreenPlayer3Border.SetActive(false);
        GreenPlayer4Border.SetActive(false);

        greenPlayer1Button.interactable = false;
        greenPlayer2Button.interactable = false;
        greenPlayer3Button.interactable = false;
        greenPlayer4Button.interactable = false;

        if (playerTurn == "GREEN" && greenPlayer4Steps > 67 &&
            greenMovementBlock.Count - (greenPlayer4Steps - 1) == SelectDiceNumAnimation + 1 && avancoGreen == false)
        {
            StartCoroutine("PeaoGreen4");
        }
        if (playerTurn == "GREEN" && (greenMovementBlock.Count - (greenPlayer4Steps - 1)) > SelectDiceNumAnimation &&
            greenPlayer4Steps < 69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "GREEN";
            }
            /*
            if (greenPlayer1Steps == 56)
            {
                greenPlayer1.transform.position = greenMovementBlock[greenPlayer1Steps - 1].transform.position;
            }
            */

            if (aux_green4 == 1)
            {

                greenPlayer4.transform.position = greenMovementBlock[aux_green4].transform.position;
                green_Player4_CasaAtual = greenMovementBlock[aux_green4].transform.position;
                aux_green4 = 0;
                greenPlayer4Steps++;
            }
            if (greenPlayer4Steps > 0 && avancoGreen == false)
            {

                StartCoroutine("PeaoGreen4");
            }

            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && greenPlayer4Steps == 0)
            {

                greenPlayer4.transform.position = greenMovementBlock[greenPlayer4Steps].transform.position;
                green_Player4_CasaAtual = greenMovementBlock[greenPlayer4Steps].transform.position;
                aux_green4 += 1;
                playerTurn = "GREEN";
                currentPlayerName = "GREEN PLAYER 4";
                initializeDice();
            }

            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 &&
               SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoYellow");
                        break;
                }

            }
        }
        else
        {
            if (SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoYellow");
                        break;
                }
            }

        }
    }
    #endregion
    #endregion
    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////Movimento dos Players Azuis//////////////////////////
    #region Movimento dos Players Azuis
    ////////////////////////////////////////////////////PLAYER BLUE 1//////////////////////////////////////////
    #region Movimento do Player Blue 1
    public void bluePlayer1Movement()
    {
        BluePlayer1Border.SetActive(false);
        BluePlayer2Border.SetActive(false);
        BluePlayer3Border.SetActive(false);
        BluePlayer4Border.SetActive(false);

        bluePlayer1Button.interactable = false;
        bluePlayer2Button.interactable = false;
        bluePlayer3Button.interactable = false;
        bluePlayer4Button.interactable = false;

        if (playerTurn == "BLUE" && bluePlayer1Steps > 67 &&
          blueMovementBlock.Count - (bluePlayer1Steps - 1) == SelectDiceNumAnimation + 1 && avancoBlue == false)
        {
            StartCoroutine("PeaoBlue1");
        }

        if (playerTurn == "BLUE" && (blueMovementBlock.Count - (bluePlayer1Steps - 1)) > SelectDiceNumAnimation
             && bluePlayer1Steps < 69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "BLUE";
            }
            /*
            if (bluePlayer1Steps == 76)
            {

                bluePlayer1.transform.position = blueMovementBlock[bluePlayer1Steps - 1].transform.position;
            }
            */

            if (aux_blue1 == 1)
            {
                bluePlayer1.transform.position = blueMovementBlock[aux_blue1].transform.position;
                bluePlayer1_CasaAtual = blueMovementBlock[aux_blue1].transform.position;
                aux_blue1 = 0;
               bluePlayer1Steps++;
            }

            if (bluePlayer1Steps > 0 && avancoBlue == false)
            {

                StartCoroutine("PeaoBlue1");
            }

            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && bluePlayer1Steps == 0)
            {

                bluePlayer1.transform.position = blueMovementBlock[bluePlayer1Steps].transform.position;
                bluePlayer1_CasaAtual = blueMovementBlock[bluePlayer1Steps].transform.position;
                aux_blue1 += 1;
                playerTurn = "BLUE";
                currentPlayerName = "BLUE PLAYER 21";
                initializeDice();
            }

            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 &&
                 SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoYellow");
                        break;
                }

            }

        }
        else
        {
            if (SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoYellow");
                        break;
                }
            }

        }
    }
    #endregion
    ////////////////////////////////////////////////////PLAYER BLUE 2///////////////////////////////////////////
    #region Movimento do Player Blue 2
    public void bluePlayer2Movement()
    {
        BluePlayer1Border.SetActive(false);
        BluePlayer2Border.SetActive(false);
        BluePlayer3Border.SetActive(false);
        BluePlayer4Border.SetActive(false);

        bluePlayer1Button.interactable = false;
        bluePlayer2Button.interactable = false;
        bluePlayer3Button.interactable = false;
        bluePlayer4Button.interactable = false;

        if (playerTurn == "BLUE" && bluePlayer2Steps > 67 &&
           blueMovementBlock.Count - (bluePlayer2Steps - 1) == SelectDiceNumAnimation + 1 && avancoBlue == false)
        {
            StartCoroutine("PeaoBlue2");
        }

        if (playerTurn == "BLUE" && (blueMovementBlock.Count - (bluePlayer2Steps - 1)) > SelectDiceNumAnimation
            && bluePlayer2Steps < 69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "BLUE";
                
            }
            /*
            if (bluePlayer2Steps == 56)
            {

                bluePlayer2.transform.position = blueMovementBlock[bluePlayer2Steps - 1].transform.position;
            }
            */

            if (aux_blue2 == 1)
            {
                bluePlayer2.transform.position = blueMovementBlock[aux_blue2].transform.position;
                bluePlayer2_CasaAtual = blueMovementBlock[aux_blue2].transform.position;
                aux_blue2 = 0;
                bluePlayer2Steps++;
            }

            if (bluePlayer2Steps > 0 && avancoBlue == false)
            {
                StartCoroutine("PeaoBlue2");
            }

            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && bluePlayer2Steps == 0)
            {

                bluePlayer2.transform.position = blueMovementBlock[bluePlayer2Steps].transform.position;
                bluePlayer2_CasaAtual = blueMovementBlock[bluePlayer2Steps].transform.position;
                aux_blue2 += 1;
                playerTurn = "BLUE";
                currentPlayerName = "BLUE PLAYER 2";
                initializeDice();
            }

            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 &&
                SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoYellow");
                        break;
                }

            }

        }
        else
        {
            if (SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoYellows");
                        break;
                }
            }

        }
    }
    #endregion
    ////////////////////////////////////////////////////PLAYER BLUE 3////////////////////////////////////////////
    #region Movimento do Player Blue 3
    public void bluePlayer3Movement()
    {
        BluePlayer1Border.SetActive(false);
        BluePlayer2Border.SetActive(false);
        BluePlayer3Border.SetActive(false);
        BluePlayer4Border.SetActive(false);

        bluePlayer1Button.interactable = false;
        bluePlayer2Button.interactable = false;
        bluePlayer3Button.interactable = false;
        bluePlayer4Button.interactable = false;

        if (playerTurn == "BLUE" && bluePlayer3Steps > 67 &&
           blueMovementBlock.Count - (bluePlayer3Steps - 1) == SelectDiceNumAnimation + 1 && avancoBlue == false)
        {
            StartCoroutine("PeaoBlue3");
        }

        if (playerTurn == "BLUE" && (blueMovementBlock.Count - (bluePlayer3Steps - 1)) > SelectDiceNumAnimation
            && bluePlayer3Steps < 69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "BLUE";

            }
            /*
            if (bluePlayer2Steps == 56)
            {

                bluePlayer2.transform.position = blueMovementBlock[bluePlayer2Steps - 1].transform.position;
            }
            */

            if (aux_blue3 == 1)
            {
                bluePlayer3.transform.position = blueMovementBlock[aux_blue3].transform.position;
                bluePlayer3_CasaAtual = blueMovementBlock[aux_blue3].transform.position;
                aux_blue3 = 0;
                bluePlayer3Steps++;
            }

            if (bluePlayer3Steps > 0 && avancoBlue == false)
            {
                StartCoroutine("PeaoBlue3");
            }

            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && bluePlayer3Steps == 0)
            {

                bluePlayer3.transform.position = blueMovementBlock[bluePlayer3Steps].transform.position;
                bluePlayer3_CasaAtual = blueMovementBlock[bluePlayer3Steps].transform.position;
                aux_blue3 += 1;
                playerTurn = "BLUE";
                currentPlayerName = "BLUE PLAYER 3";
                initializeDice();
            }

            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 &&
                SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoYellow");
                        break;
                }

            }

        }
        else
        {
            if (SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoYellow");
                        break;
                }
            }

        }
    }
    #endregion
    ////////////////////////////////////////////////////PLAYER BLUE 4//////////////////////////////////////////
    #region Movimento do Player Blue 4
    public void bluePlayer4Movement()
    {
        BluePlayer1Border.SetActive(false);
        BluePlayer2Border.SetActive(false);
        BluePlayer3Border.SetActive(false);
        BluePlayer4Border.SetActive(false);

        bluePlayer1Button.interactable = false;
        bluePlayer2Button.interactable = false;
        bluePlayer3Button.interactable = false;
        bluePlayer4Button.interactable = false;

        if (playerTurn == "BLUE" && bluePlayer4Steps > 67 &&
           blueMovementBlock.Count - (bluePlayer4Steps - 1) == SelectDiceNumAnimation + 1 && avancoBlue == false)
        {
            StartCoroutine("PeaoBlue4");
        }

        if (playerTurn == "BLUE" && (blueMovementBlock.Count - (bluePlayer4Steps - 1)) > SelectDiceNumAnimation
            && bluePlayer4Steps < 69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "BLUE";

            }
            /*
            if (bluePlayer2Steps == 56)
            {

                bluePlayer2.transform.position = blueMovementBlock[bluePlayer2Steps - 1].transform.position;
            }
            */

            if (aux_blue4 == 1)
            {
                bluePlayer4.transform.position = blueMovementBlock[aux_blue4].transform.position;
                bluePlayer4_CasaAtual = blueMovementBlock[aux_blue4].transform.position;
                aux_blue4 = 0;
                bluePlayer4Steps++;
            }

            if (bluePlayer4Steps > 0 && avancoBlue == false)
            {
                StartCoroutine("PeaoBlue4");
            }

            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && bluePlayer4Steps == 0)
            {

                bluePlayer4.transform.position = blueMovementBlock[bluePlayer4Steps].transform.position;
                bluePlayer4_CasaAtual = blueMovementBlock[bluePlayer4Steps].transform.position;
                aux_blue4 += 1;
                playerTurn = "BLUE";
                currentPlayerName = "BLUE PLAYER 4";
                initializeDice();
            }

            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 &&
                SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoYellow");
                        break;
                }

            }

        }
        else
        {
            if (SelectDiceNumAnimation != 10)
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 4:

                        StartCoroutine("TrocaDeTurnoYellow");
                        break;
                }
            }

        }
    }
    #endregion
    #endregion
    ///////////////////////////////////////////////////////////////////////////////////////////////

    ////////////////////////////ROTINA QUE CONTROLA A VELOCIDADE QUE O A MAQUINA JOGA///////////////
    #region Rolagem de dados da IA
    IEnumerator RolagemDeDadosIA()
    {
        yield return new WaitForSeconds(1.0f);
        if ((SelectDiceNumAnimation != 10 && playerTurn == "GREEN") ||
            (SelectDiceNumAnimation != 10 && playerTurn == "BLUE"))
        {
            DiceRoll();
        }
    }
    #endregion

    
    #region IA
    /*
    IEnumerator MovimentacaoPlayerGreenDadoSix()
    {
        yield return new WaitForSeconds(5.0f);
        cooldownSixDice = true;
        cooldownMovmentGreen = true;
    }
    IEnumerator MovimentacaoPlayerBlueDadoSix()
    {
        yield return new WaitForSeconds(5.0f);
        cooldownSixDice = true;
        cooldownMovmentBlue = true;
    }
    */
    #endregion

    ///////////////////////////////////STARTA A MOVIMENTACAO DO PLAYER GREEN///////////////////////////////////
    #region Escolhe qual player verde vai se movimentar e chama a função de movimento
    IEnumerator MovimentacaoPlayerGreen()
    {
        yield return new WaitForSeconds(1.0f);
        playerGreen = Random.Range(1, 4);

        if (playerGreen == 1 && !GreenPlayer1Border.activeInHierarchy)
        {
            playerGreen = 2;
            if (playerGreen == 2 && !GreenPlayer2Border.activeInHierarchy)
            {
                playerGreen = 3;

            }
            if (playerGreen == 3 && !GreenPlayer3Border.activeInHierarchy)
            {
                playerGreen = 4;

            }
        }

        if (playerGreen == 2 && !GreenPlayer2Border.activeInHierarchy)
        {
            playerGreen = 3;
            if (playerGreen == 3 && !GreenPlayer3Border.activeInHierarchy)
            {
                playerGreen = 4;

            }
            if (playerGreen == 4 && !GreenPlayer4Border.activeInHierarchy)
            {
                playerGreen = 1;

            }
        }

        if (playerGreen == 3 && !GreenPlayer3Border.activeInHierarchy)
        {
            playerGreen = 4;

            if (playerGreen == 4 && !GreenPlayer4Border.activeInHierarchy)
            {
                playerGreen = 1;

            }
            if (playerGreen == 1 && !GreenPlayer1Border.activeInHierarchy)
            {
                playerGreen = 2;
            }
        }
        if (playerGreen == 4 && !GreenPlayer4Border.activeInHierarchy)
        {
            playerGreen = 1;

            if (playerGreen == 1 && !GreenPlayer1Border.activeInHierarchy)
            {
                playerGreen = 2;
            }
            if (playerGreen == 2 && !GreenPlayer2Border.activeInHierarchy)
            {
                playerGreen = 3;

            }
        }

        if (playerGreen == 1 && GreenPlayer1Border.activeInHierarchy)
        {
            greenPlayer1Movement();

        }

        if (playerGreen == 2 && GreenPlayer2Border.activeInHierarchy)
        {
            greenPlayer2Movement();

        }

        if (playerGreen == 3 && GreenPlayer3Border.activeInHierarchy)
        {
            greenPlayer3Movement();

        }

        if (playerGreen == 4 && GreenPlayer4Border.activeInHierarchy)
        {
            greenPlayer4Movement();

        }


    }
    #endregion
    #region Escolhe qual player Green irá avancar 10 casas
    //////////////////////////////////////////////////////////////////MOV PlayerREDCATCH///////////////////////////
    IEnumerator MovimentacaoPlayerGreenCatch()
    {
        yield return new WaitForSeconds(1.0f);
        playerGreen = Random.Range(1, 4);

        if (playerGreen == 1 && greenPlayer1Steps == 0 || greenPlayer1Steps == 76
            || (greenPlayer1Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer1Steps - 1) != SelectDiceNumAnimation + 1))
        {
            playerGreen = 2;
            if (playerGreen == 2 && greenPlayer2Steps == 0 || greenPlayer2Steps == 76
                || (greenPlayer2Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer2Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerGreen = 3;

            }
            if (playerGreen == 3 && greenPlayer3Steps == 0 || greenPlayer3Steps == 76
                || (greenPlayer3Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer3Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerGreen = 4;

            }
        }

        if (playerGreen == 2 && greenPlayer2Steps == 0 || greenPlayer2Steps == 76
            || (greenPlayer2Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer2Steps - 1) != SelectDiceNumAnimation + 1))
        {
            playerGreen = 3;
            if (playerGreen == 3 && greenPlayer3Steps == 0 || greenPlayer3Steps == 76
                || (greenPlayer3Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer3Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerGreen = 4;

            }
            if (playerGreen == 4 && greenPlayer4Steps == 0 || greenPlayer4Steps == 76
                || (greenPlayer4Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer4Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerGreen = 1;

            }
        }

        if (playerGreen == 3 && greenPlayer3Steps == 0 || greenPlayer3Steps == 76
            || (greenPlayer3Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer3Steps - 1) != SelectDiceNumAnimation + 1))
        {
            playerGreen = 4;

            if (playerGreen == 4 && greenPlayer4Steps == 0 || greenPlayer4Steps == 76
                || (greenPlayer4Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer4Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerGreen = 1;

            }
            if (playerGreen == 1 && greenPlayer4Steps == 0 || greenPlayer4Steps == 76
                || (greenPlayer1Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer1Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerGreen = 2;
            }
        }
        if (playerGreen == 4 && greenPlayer4Steps == 0 || greenPlayer4Steps == 76
            || (greenPlayer4Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer4Steps - 1) != SelectDiceNumAnimation + 1))
        {
            playerGreen = 1;

            if (playerGreen == 1 && greenPlayer1Steps == 0 || greenPlayer1Steps == 76
                || (greenPlayer1Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer1Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerGreen = 2;
            }
            if (playerGreen == 2 && greenPlayer2Steps == 0 || greenPlayer2Steps == 76||
                (greenPlayer2Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer2Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerGreen = 3;

            }
        }
        
        if (playerGreen == 1 && (greenPlayer1Steps > 0 || greenPlayer1Steps != 76))
        {
            if ((greenPlayer1Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer1Steps - 1) != SelectDiceNumAnimation + 1))
            {
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoRed");
            }
            else 
            {
                greenPlayer1Movement();
            }

        }

        if (playerGreen == 2 && (greenPlayer2Steps > 0 || greenPlayer2Steps != 76))
        {
            if ((greenPlayer2Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer2Steps - 1) != SelectDiceNumAnimation + 1))
            {
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoRed");
            }
            else
            {
                greenPlayer2Movement();
            }

        }

        if (playerGreen == 3 && (greenPlayer3Steps > 0 || greenPlayer3Steps != 76))
        {
            if ((greenPlayer3Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer3Steps - 1) != SelectDiceNumAnimation + 1))
            {
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoRed");
            }
            else {
                greenPlayer3Movement();
            }

        }

        if (playerGreen == 4 && (greenPlayer4Steps > 0 || greenPlayer4Steps != 76))
        {
            if ((greenPlayer4Steps > 68 &&
              greenMovementBlock.Count - (greenPlayer4Steps - 1) != SelectDiceNumAnimation + 1)) 
            {
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoRed");
            }
            else {
                greenPlayer4Movement();
            }

        }


    }
    #endregion
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////STARTA A MOVIMENTACAO DO PLAYER BLUE////////////////////////////////////
    #region Escolhe qual player azul vai se movimentar e chama a função de movimento
    IEnumerator MovimentacaoPlayerBlue()
    {
        yield return new WaitForSeconds(1.0f);
        playerBlue = Random.Range(1, 4);

        if (playerBlue == 1 && !BluePlayer1Border.activeInHierarchy)
        {
            playerBlue = 2;
            if (playerBlue == 2 && !BluePlayer2Border.activeInHierarchy)
            {
                playerBlue = 3;

            }
            if (playerBlue == 3 && !BluePlayer3Border.activeInHierarchy)
            {
                playerBlue = 4;

            }
        }

        if (playerBlue == 2 && !BluePlayer2Border.activeInHierarchy)
        {
            playerBlue = 3;
            if (playerBlue == 3 && !BluePlayer3Border.activeInHierarchy)
            {
                playerBlue = 4;

            }
            if (playerBlue == 4 && !GreenPlayer4Border.activeInHierarchy)
            {
                playerBlue = 1;

            }
        }

        if (playerBlue == 3 && !BluePlayer3Border.activeInHierarchy)
        {
            playerBlue = 4;

            if (playerBlue == 4 && !BluePlayer4Border.activeInHierarchy)
            {
                playerBlue = 1;

            }
            if (playerBlue == 1 && !BluePlayer1Border.activeInHierarchy)
            {
                playerBlue = 2;
            }
        }
        if (playerBlue == 4 && !BluePlayer4Border.activeInHierarchy)
        {
            playerBlue = 1;

            if (playerBlue == 1 && !BluePlayer1Border.activeInHierarchy)
            {
                playerBlue = 2;
            }
            if (playerBlue == 2 && !BluePlayer2Border.activeInHierarchy)
            {
                playerBlue = 3;

            }
        }

        if (playerBlue == 1 && BluePlayer1Border.activeInHierarchy)
        {
            bluePlayer1Movement();

        }

        if (playerBlue == 2 && BluePlayer2Border.activeInHierarchy)
        {
            bluePlayer2Movement();

        }

        if (playerBlue == 3 && BluePlayer3Border.activeInHierarchy)
        {
            bluePlayer3Movement();

        }

        if (playerBlue == 4 && BluePlayer4Border.activeInHierarchy)
        {
            bluePlayer4Movement();

        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
    #region Escolhe qual player Blue irá avancar 10 casas
    //////////////////////////////////////////////////////////////////MOV PlayerREDCATCH///////////////////////////
    IEnumerator MovimentacaoPlayerBlueCatch()
    {
        yield return new WaitForSeconds(1.0f);
        playerBlue = Random.Range(1, 4);

        if (playerBlue == 1 && bluePlayer1Steps == 0 || bluePlayer1Steps == 76
            || (bluePlayer1Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer1Steps - 1) != SelectDiceNumAnimation + 1))
        {
            playerBlue = 2;
            if (playerBlue == 2 && bluePlayer2Steps == 0 || bluePlayer2Steps == 76
                || (bluePlayer2Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer2Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerBlue = 3;

            }
            if (playerBlue == 3 && bluePlayer3Steps == 0 || bluePlayer3Steps == 76
                || (bluePlayer3Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer3Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerBlue = 4;

            }
        }

        if (playerBlue == 2 && bluePlayer2Steps == 0 || bluePlayer2Steps == 76
            || (bluePlayer2Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer2Steps - 1) != SelectDiceNumAnimation + 1))
        {
            playerBlue = 3;
            if (playerBlue == 3 && bluePlayer3Steps == 0 || bluePlayer3Steps == 76
                || (bluePlayer3Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer3Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerBlue = 4;

            }
            if (playerBlue == 4 && bluePlayer4Steps == 0 || bluePlayer4Steps == 76
                || (bluePlayer4Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer4Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerBlue = 1;

            }
        }

        if (playerBlue == 3 && bluePlayer3Steps == 0 || bluePlayer3Steps == 76
            || (bluePlayer3Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer3Steps - 1) != SelectDiceNumAnimation + 1))
        {
            playerBlue = 4;

            if (playerBlue == 4 && bluePlayer4Steps == 0 || bluePlayer4Steps == 76
                || (bluePlayer4Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer4Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerBlue = 1;

            }
            if (playerBlue == 1 && bluePlayer4Steps == 0 || bluePlayer4Steps == 76
                || (bluePlayer1Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer1Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerBlue = 2;
            }
        }
        if (playerBlue == 4 && bluePlayer4Steps == 0 || bluePlayer4Steps == 76
            || (bluePlayer4Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer4Steps - 1) != SelectDiceNumAnimation + 1))
        {
            playerBlue = 1;

            if (playerBlue == 1 && bluePlayer1Steps == 0 || bluePlayer1Steps == 76
                || (bluePlayer1Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer1Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerBlue = 2;
            }
            if (playerBlue == 2 && bluePlayer2Steps == 0 || bluePlayer2Steps == 76
                || (bluePlayer2Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer2Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerBlue = 3;

            }
        }
       
        if (playerBlue == 1 && (bluePlayer1Steps > 0 || bluePlayer1Steps != 76))
        {
            if ((bluePlayer1Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer1Steps - 1) != SelectDiceNumAnimation + 1))
            {
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoYellow");
            }
            else {
           bluePlayer1Movement();
            }

        }

        if (playerBlue == 2 && (bluePlayer2Steps > 0 || bluePlayer2Steps != 76))
        {
            if ((bluePlayer2Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer2Steps - 1) != SelectDiceNumAnimation + 1))
            {
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoYellow");
            }
            else 
            {
             bluePlayer2Movement();
             
            }
        }

        if (playerBlue == 3 && (bluePlayer3Steps > 0 || bluePlayer3Steps != 76))
        {
            if ((bluePlayer3Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer3Steps - 1) != SelectDiceNumAnimation + 1))
            {
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoYellow");
            }
            else 
            {
            bluePlayer3Movement();
            }

        }

        if (playerBlue == 4 && (bluePlayer4Steps > 0 || bluePlayer4Steps != 76))
        {

            if ((bluePlayer4Steps > 68 &&
              blueMovementBlock.Count - (bluePlayer4Steps - 1) != SelectDiceNumAnimation + 1))
            {
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoYellow");
            }
            else {
                bluePlayer4Movement();
            }

        }


    }
    #endregion
    ///////////////////////////////////////////////////////MOVIMENTAÇÃO PLAYER RED/////////////////////////////
    #region Comportamento dos Players Vermelhos para andarem pelas casas do tabuleiro
    #region Player red 1
    IEnumerator PeaoRed1()
    {
        BarraContador.SetActive(false);
        redPlayer1Button.interactable = true;
        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {
            redPlayer1.transform.position = redMovementBlock[redPlayer1Steps].transform.position;
            redPlayer1_CasaAtual = redMovementBlock[redPlayer1Steps].transform.position;
            redPlayer1Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }
        if (redPlayer1Steps == 76 && statusRed != "AVANCAR")
        {
            if (redPlayer2Steps == 0 || redPlayer2Steps == 76 && redPlayer3Steps == 0 || redPlayer3Steps == 76
                && redPlayer4Steps == 0 || redPlayer4Steps == 76)
            {
                BarraContador.SetActive(false);
               
                //playerTurn = "BLUE";
                SelectDiceNumAnimation = 0;
                diceRollButton.interactable = true;
                diceRollButton.enabled = true;
                StartCoroutine("TrocaDeTurnoBlue");
                //initializeDice();
            }
            else
            {

                Debug.Log("CHEGOU NO FIM");
                redPlayer1.transform.position = redMovementBlock[redPlayer1Steps - 1].transform.position;
                avancoRed = true;
                playerTurn = "RED";
                statusRed = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }
        }
        if (statusRed == "AVANCAR" && redPlayer1Steps != 76)
        {
            Debug.Log("Nao pode Avancar");
            statusRed = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoBlue");
            // initializeDice();
        }
        if (redPlayer1_CasaAtual == greenPlayer1_CasaAtual
                   && !redMovementBlock[redPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer1.transform.position = GreenPlayer1Pos;
            greenPlayer1Steps = 0;
            greenPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (redPlayer1_CasaAtual == greenPlayer2_CasaAtual &&
            !redMovementBlock[redPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer2.transform.position = GreenPlayer2Pos;
            greenPlayer2Steps = 0;
            greenPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (redPlayer1_CasaAtual == greenPlayer3_CasaAtual &&
            !redMovementBlock[redPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer3.transform.position = GreenPlayer3Pos;
            greenPlayer3Steps = 0;
            greenPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (redPlayer1_CasaAtual == green_Player4_CasaAtual &&
            !redMovementBlock[redPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer4.transform.position = GreenPlayer4pos;
            greenPlayer4Steps = 0;
            green_Player4_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        ////////////////compara com a casa azul
        if (redPlayer1_CasaAtual == bluePlayer1_CasaAtual
                 && !redMovementBlock[redPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer1.transform.position = BluePlayer1Pos;
            bluePlayer1Steps = 0;
            bluePlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (redPlayer1_CasaAtual == bluePlayer2_CasaAtual &&
            !redMovementBlock[redPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer2.transform.position = BluePlayer2Pos;
            bluePlayer2Steps = 0;
            bluePlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (redPlayer1_CasaAtual == bluePlayer3_CasaAtual &&
            !redMovementBlock[redPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer3.transform.position = BluePlayer3Pos;
            bluePlayer3Steps = 0;
            bluePlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (redPlayer1_CasaAtual == bluePlayer4_CasaAtual &&
            !redMovementBlock[redPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer4.transform.position = BluePlayer4pos;
            bluePlayer4Steps = 0;
            bluePlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
    }
    #endregion
    #region Player red 2
    IEnumerator PeaoRed2()
    {
        BarraContador.SetActive(false);
        redPlayer2Button.interactable = true;
        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {
            redPlayer2.transform.position = redMovementBlock[redPlayer2Steps].transform.position;
            redPlayer2_CasaAtual = redMovementBlock[redPlayer2Steps].transform.position;
            redPlayer2Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }
        if (redPlayer2Steps == 76 && statusRed != "AVANCAR")
        {
            if (redPlayer1Steps == 0 || redPlayer1Steps == 76 && redPlayer3Steps == 0 || redPlayer3Steps == 76
                && redPlayer4Steps == 0 || redPlayer4Steps == 76)
            {
                BarraContador.SetActive(false);
                //playerTurn = "BLUE";
                SelectDiceNumAnimation = 0;
                diceRollButton.interactable = true;
                diceRollButton.enabled = true;
                StartCoroutine("TrocaDeTurnoBlue");
                //initializeDice();
            }
            else
            {
                Debug.Log("CHEGOU NO FIM");
                redPlayer2.transform.position = redMovementBlock[redPlayer2Steps - 1].transform.position;
                avancoRed = true;
                playerTurn = "RED";
                statusRed = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }
        }
        if (statusRed == "AVANCAR" && redPlayer2Steps != 76)
        {
            statusRed = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            //playerTurn = "BLUE";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoBlue");
            // initializeDice();
        }
        if (redPlayer2_CasaAtual == greenPlayer1_CasaAtual &&
                    !redMovementBlock[redPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer1.transform.position = GreenPlayer1Pos;
            greenPlayer1Steps = 0;
            greenPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (redPlayer2_CasaAtual == greenPlayer2_CasaAtual &&
            !redMovementBlock[redPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer2.transform.position = GreenPlayer2Pos;
            greenPlayer2Steps = 0;
            greenPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (redPlayer2_CasaAtual == greenPlayer3_CasaAtual &&
            !redMovementBlock[redPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer3.transform.position = GreenPlayer3Pos;
            greenPlayer3Steps = 0;
            greenPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (redPlayer2_CasaAtual == green_Player4_CasaAtual &&
            !redMovementBlock[redPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer4.transform.position = GreenPlayer4pos;
            greenPlayer4Steps = 0;
            green_Player4_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        ////////////////compara com a casa azul
        if (redPlayer2_CasaAtual == bluePlayer1_CasaAtual
                 && !redMovementBlock[redPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer1.transform.position = BluePlayer1Pos;
            bluePlayer1Steps = 0;
            bluePlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (redPlayer2_CasaAtual == bluePlayer2_CasaAtual &&
            !redMovementBlock[redPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer2.transform.position = BluePlayer2Pos;
            bluePlayer2Steps = 0;
            bluePlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (redPlayer2_CasaAtual == bluePlayer3_CasaAtual &&
            !redMovementBlock[redPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer3.transform.position = BluePlayer3Pos;
            bluePlayer3Steps = 0;
            bluePlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (redPlayer2_CasaAtual == bluePlayer4_CasaAtual &&
            !redMovementBlock[redPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer4.transform.position = BluePlayer4pos;
            bluePlayer4Steps = 0;
            bluePlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
    }
    #endregion
    #region Player red 3
    IEnumerator PeaoRed3()
    {
        BarraContador.SetActive(false);
        redPlayer3Button.interactable = true;
        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {
            redPlayer3.transform.position = redMovementBlock[redPlayer3Steps].transform.position;
            redPlayer3_CasaAtual = redMovementBlock[redPlayer3Steps].transform.position;
            redPlayer3Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }
        if (redPlayer3Steps == 76 && statusRed != "AVANCAR")
        {
            if (redPlayer2Steps == 0 || redPlayer2Steps == 76 && redPlayer1Steps == 0 || redPlayer1Steps == 76
                && redPlayer4Steps == 0 || redPlayer4Steps == 76)
            {
                BarraContador.SetActive(false);
                //playerTurn = "BLUE";
                SelectDiceNumAnimation = 0;
                diceRollButton.interactable = true;
                diceRollButton.enabled = true;
                //initializeDice();
                StartCoroutine("TrocaDeTurnoBlue");
            }
            else
            {
                Debug.Log("CHEGOU NO FIM");
                redPlayer3.transform.position = redMovementBlock[redPlayer3Steps - 1].transform.position;
                avancoRed = true;
                playerTurn = "RED";
                statusRed = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }
        }
        if (statusRed == "AVANCAR" && redPlayer3Steps != 76)
        {
            statusRed = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            //playerTurn = "BLUE";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoBlue");
            //initializeDice();
        }
        if (redPlayer3_CasaAtual == greenPlayer1_CasaAtual &&
                  !redMovementBlock[redPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer1.transform.position = GreenPlayer1Pos;
            greenPlayer1Steps = 0;
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (redPlayer3_CasaAtual == greenPlayer2_CasaAtual &&
            !redMovementBlock[redPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer2.transform.position = GreenPlayer2Pos;
            greenPlayer2Steps = 0;
            greenPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (redPlayer3_CasaAtual == greenPlayer3_CasaAtual &&
            !redMovementBlock[redPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer3.transform.position = GreenPlayer3Pos;
            greenPlayer3Steps = 0;
            greenPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (redPlayer3_CasaAtual == green_Player4_CasaAtual &&
            !redMovementBlock[redPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer4.transform.position = GreenPlayer4pos;
            greenPlayer4Steps = 0;
            green_Player4_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }

        ////////////////compara com a casa azul
        if (redPlayer3_CasaAtual == bluePlayer1_CasaAtual
                 && !redMovementBlock[redPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer1.transform.position = BluePlayer1Pos;
            bluePlayer1Steps = 0;
            bluePlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (redPlayer3_CasaAtual == bluePlayer2_CasaAtual &&
            !redMovementBlock[redPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer2.transform.position = BluePlayer2Pos;
            bluePlayer2Steps = 0;
            bluePlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (redPlayer3_CasaAtual == bluePlayer3_CasaAtual &&
            !redMovementBlock[redPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer3.transform.position = BluePlayer3Pos;
            bluePlayer3Steps = 0;
            bluePlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (redPlayer3_CasaAtual == bluePlayer4_CasaAtual &&
            !redMovementBlock[redPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer4.transform.position = BluePlayer4pos;
            bluePlayer4Steps = 0;
            bluePlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
    }
    #endregion
    #region Player red 4
    IEnumerator PeaoRed4()
    {
        BarraContador.SetActive(false);
        redPlayer4Button.interactable = true;
        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {
            redPlayer4.transform.position = redMovementBlock[redPlayer4Steps].transform.position;
            redPlayer4_CasaAtual = redMovementBlock[redPlayer4Steps].transform.position;
            redPlayer4Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }
        if (redPlayer4Steps == 76 && statusRed != "AVANCAR")
        {
            if (redPlayer2Steps == 0 || redPlayer2Steps == 76 && redPlayer3Steps == 0 || redPlayer3Steps == 76
                && redPlayer1Steps == 0 || redPlayer1Steps == 76)
            {
                BarraContador.SetActive(false);
                ///playerTurn = "BLUE";
                SelectDiceNumAnimation = 0;
                diceRollButton.interactable = true;
                diceRollButton.enabled = true;
                StartCoroutine("TrocaDeTurnoBlue");
                ///initializeDice();
            }
            else
            {
                Debug.Log("CHEGOU NO FIM");
                redPlayer4.transform.position = redMovementBlock[redPlayer4Steps - 1].transform.position;
                avancoRed = true;
                playerTurn = "RED";
                statusRed = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }
        }
        if (statusRed == "AVANCAR" && redPlayer4Steps != 76)
        {
            statusRed = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            //playerTurn = "BLUE";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoBlue");
           // initializeDice();
        }
        if (redPlayer4_CasaAtual == greenPlayer1_CasaAtual &&
            !redMovementBlock[redPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer1.transform.position = GreenPlayer1Pos;
            greenPlayer1Steps = 0;
            greenPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (redPlayer4_CasaAtual == greenPlayer2_CasaAtual &&
            !redMovementBlock[redPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer2.transform.position = GreenPlayer2Pos;
            greenPlayer2Steps = 0;
            greenPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (redPlayer4_CasaAtual == greenPlayer3_CasaAtual &&
            !redMovementBlock[redPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer3.transform.position = GreenPlayer3Pos;
            greenPlayer3Steps = 0;
            greenPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (redPlayer4_CasaAtual == green_Player4_CasaAtual &&
            !redMovementBlock[redPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer4.transform.position = GreenPlayer4pos;
            greenPlayer4Steps = 0;
            green_Player4_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        ////////////////compara com a casa azul
        if (redPlayer4_CasaAtual == bluePlayer1_CasaAtual
                 && !redMovementBlock[redPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer1.transform.position = BluePlayer1Pos;
            bluePlayer1Steps = 0;
            bluePlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (redPlayer4_CasaAtual == bluePlayer2_CasaAtual &&
            !redMovementBlock[redPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer2.transform.position = BluePlayer2Pos;
            bluePlayer2Steps = 0;
            bluePlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (redPlayer4_CasaAtual == bluePlayer3_CasaAtual &&
            !redMovementBlock[redPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer3.transform.position = BluePlayer3Pos;
            bluePlayer3Steps = 0;
            bluePlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (redPlayer4_CasaAtual == bluePlayer4_CasaAtual &&
            !redMovementBlock[redPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer4.transform.position = BluePlayer4pos;
            bluePlayer4Steps = 0;
            bluePlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }

    }
    #endregion
    #endregion
    #region Escolhe qual player Vermelho ira se mover quando o tempo esgotar
    IEnumerator MovimentacaoPlayerRed()
    {
        yield return new WaitForSeconds(1.0f);
        var playerRed = Random.Range(1, 4);

        if (playerRed == 1 && !redPlayer1Border.activeInHierarchy)
        {
            playerRed = 2;
            if (playerRed == 2 && !redPlayer2Border.activeInHierarchy)
            {
                playerRed = 3;

            }
            if (playerRed == 3 && !redPlayer3Border.activeInHierarchy)
            {
                playerRed = 4;

            }
        }

        if (playerRed == 2 && !redPlayer2Border.activeInHierarchy)
        {
            playerRed = 3;
            if (playerRed == 3 && !redPlayer3Border.activeInHierarchy)
            {
                playerRed = 4;

            }
            if (playerRed == 4 && !redPlayer4Border.activeInHierarchy)
            {
                playerRed = 1;

            }
        }

        if (playerRed == 3 && !redPlayer3Border.activeInHierarchy)
        {
            playerRed = 4;

            if (playerRed == 4 && !redPlayer4Border.activeInHierarchy)
            {
                playerRed = 1;

            }
            if (playerRed == 1 && !redPlayer1Border.activeInHierarchy)
            {
                playerRed = 2;
            }
        }
        if (playerRed == 4 && !redPlayer4Border.activeInHierarchy)
        {
            playerRed = 1;

            if (playerRed == 1 && !redPlayer1Border.activeInHierarchy)
            {
                playerRed = 2;
            }
            if (playerRed == 2 && !redPlayer2Border.activeInHierarchy)
            {
                playerRed = 3;

            }
        }

        if (playerRed == 1 && redPlayer1Border.activeInHierarchy)
        {
            redPlayer1Movement();

        }

        if (playerRed == 2 && redPlayer2Border.activeInHierarchy)
        {
            redPlayer2Movement();

        }

        if (playerRed == 3 && redPlayer3Border.activeInHierarchy)
        {
            redPlayer3Movement();

        }

        if (playerRed == 4 && redPlayer4Border.activeInHierarchy)
        {
            redPlayer4Movement();

        }


    }
    public void MovimentaRedPlayer()
    {
        BarraContador.SetActive(false);
        StartCoroutine("MovimentacaoPlayerRed");
    }
    #endregion
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////MOVIMENTAÇÃO PLAYER YELLOW////////////////////////////
    #region Comportamento dos Players Amarelos para andarem pelas casas do tabuleiro
    #region Peao Yellow 1
    IEnumerator PeaoYellow1()
    {
        BarraContador.SetActive(false);
        yellowPlayer1Button.interactable = true;
        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {
            yellowPlayer1.transform.position = yellowMovementBlock[yellowPlayer1Steps].transform.position;
            yellowPlayer1_CasaAtual = yellowMovementBlock[yellowPlayer1Steps].transform.position;
            yellowPlayer1Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }
        if (yellowPlayer1Steps == 76 && statusYellow != "AVANCAR")
        {
            if (yellowPlayer2Steps == 0 || yellowPlayer2Steps == 76 && yellowPlayer3Steps == 0 ||
            yellowPlayer3Steps == 76 && yellowPlayer4Steps == 0 || yellowPlayer4Steps == 76)
            {
                BarraContador.SetActive(false);
                //playerTurn = "GREEN";
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoGreen");
                //initializeDice();

            }
            else
            {
                Debug.Log("CHEGOU NO FIM");
                yellowPlayer1.transform.position = yellowMovementBlock[yellowPlayer1Steps - 1].transform.position;
                avancoYellow = true;
                playerTurn = "YELLOW";
                statusYellow = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }
        }
        
        if (statusYellow == "AVANCAR" && yellowPlayer1Steps != 76)
        {
            statusYellow = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
           // playerTurn = "GREEN";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoGreen");
            //initializeDice();
        }
        
        ///////////COMPARACAO COM OS PEOES GREEN
        if (yellowPlayer1_CasaAtual == greenPlayer1_CasaAtual
                   && !yellowMovementBlock[yellowPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer1.transform.position = GreenPlayer1Pos;
            greenPlayer1Steps = 0;
            greenPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (yellowPlayer1_CasaAtual == greenPlayer2_CasaAtual &&
            !yellowMovementBlock[yellowPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer2.transform.position = GreenPlayer2Pos;
            greenPlayer2Steps = 0;
            greenPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer1_CasaAtual == greenPlayer3_CasaAtual &&
            !yellowMovementBlock[yellowPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer3.transform.position = GreenPlayer3Pos;
            greenPlayer3Steps = 0;
            greenPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer1_CasaAtual == green_Player4_CasaAtual &&
            !yellowMovementBlock[yellowPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer4.transform.position = GreenPlayer4pos;
            greenPlayer4Steps = 0;
            green_Player4_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        /////////////////////COMPARACAO COM OS PEOES BLUE////////////////////////////////////////
        if (yellowPlayer1_CasaAtual == bluePlayer1_CasaAtual
                   && !yellowMovementBlock[yellowPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer1.transform.position = BluePlayer1Pos;
            bluePlayer1Steps = 0;
            bluePlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (yellowPlayer1_CasaAtual == bluePlayer2_CasaAtual &&
            !yellowMovementBlock[yellowPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer2.transform.position = BluePlayer2Pos;
            bluePlayer2Steps = 0;
            bluePlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer1_CasaAtual == bluePlayer3_CasaAtual &&
            !yellowMovementBlock[yellowPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer3.transform.position = BluePlayer3Pos;
            bluePlayer3Steps = 0;
            bluePlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer1_CasaAtual == bluePlayer4_CasaAtual &&
            !yellowMovementBlock[yellowPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer4.transform.position = BluePlayer4pos;
            bluePlayer4Steps = 0;
            bluePlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
    }
    #endregion
    #region Peao Yellow 2
    IEnumerator PeaoYellow2()
    {
        yellowPlayer2Button.interactable = true;
        BarraContador.SetActive(false);
        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {
            yellowPlayer2.transform.position = yellowMovementBlock[yellowPlayer2Steps].transform.position;
            yellowPlayer2_CasaAtual = yellowMovementBlock[yellowPlayer2Steps].transform.position;
            yellowPlayer2Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }
        if (yellowPlayer2Steps == 76 && statusYellow != "AVANCAR")
        {
            if (yellowPlayer1Steps == 0 || yellowPlayer1Steps == 76 && yellowPlayer3Steps == 0 ||
                yellowPlayer3Steps == 76 && yellowPlayer4Steps == 0 || yellowPlayer4Steps == 76)
            {
                BarraContador.SetActive(false);
                //playerTurn = "GREEN";
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoGreen");
                //initializeDice();

            }
            else
            {
                Debug.Log("CHEGOU NO FIM");
                yellowPlayer2.transform.position = yellowMovementBlock[yellowPlayer2Steps - 1].transform.position;
                avancoYellow = true;
                playerTurn = "YELLOW";
                statusYellow = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }

        }
        if (statusYellow == "AVANCAR" && yellowPlayer2Steps != 76)
        {
            statusYellow = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
          //  playerTurn = "GREEN";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoGreen");
            //initializeDice();
        }
        ///////////COMPARACAO COM OS PEOES GREEN
        if (yellowPlayer2_CasaAtual == greenPlayer1_CasaAtual
                   && !yellowMovementBlock[yellowPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer1.transform.position = GreenPlayer1Pos;
            greenPlayer1Steps = 0;
            greenPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (yellowPlayer2_CasaAtual == greenPlayer2_CasaAtual &&
            !yellowMovementBlock[yellowPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer2.transform.position = GreenPlayer2Pos;
            greenPlayer2Steps = 0;
            greenPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer2_CasaAtual == greenPlayer3_CasaAtual &&
            !yellowMovementBlock[yellowPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer3.transform.position = GreenPlayer3Pos;
            greenPlayer3Steps = 0;
            greenPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer2_CasaAtual == green_Player4_CasaAtual &&
            !yellowMovementBlock[yellowPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer4.transform.position = GreenPlayer4pos;
            greenPlayer4Steps = 0;
            green_Player4_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        /////////////////////COMPARACAO COM OS PEOES BLUE////////////////////////////////////////
        if (yellowPlayer2_CasaAtual == bluePlayer1_CasaAtual
                   && !yellowMovementBlock[yellowPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer1.transform.position = BluePlayer1Pos;
            bluePlayer1Steps = 0;
            bluePlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (yellowPlayer2_CasaAtual == bluePlayer2_CasaAtual &&
            !yellowMovementBlock[yellowPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer2.transform.position = BluePlayer2Pos;
            bluePlayer2Steps = 0;
            bluePlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer2_CasaAtual == bluePlayer3_CasaAtual &&
            !yellowMovementBlock[yellowPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer3.transform.position = BluePlayer3Pos;
            bluePlayer3Steps = 0;
            bluePlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer2_CasaAtual == bluePlayer4_CasaAtual &&
            !yellowMovementBlock[yellowPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer4.transform.position = BluePlayer4pos;
            bluePlayer4Steps = 0;
            bluePlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
    }
    #endregion
    #region Peao Yellow 3
    IEnumerator PeaoYellow3()
    {
        yellowPlayer3Button.interactable = true;
        BarraContador.SetActive(false);
        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {
            yellowPlayer3.transform.position = yellowMovementBlock[yellowPlayer3Steps].transform.position;
            yellowPlayer3_CasaAtual = yellowMovementBlock[yellowPlayer3Steps].transform.position;
            yellowPlayer3Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }
        if (yellowPlayer3Steps == 76 && statusYellow != "AVANCAR")
        {
            if (yellowPlayer2Steps == 0 || yellowPlayer2Steps == 76 && yellowPlayer1Steps == 0 ||
             yellowPlayer1Steps == 76 && yellowPlayer4Steps == 0 || yellowPlayer4Steps == 76)
            {
                BarraContador.SetActive(false);
                //playerTurn = "GREEN";
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoGreen");
                //initializeDice();

            }
            else
            {
                Debug.Log("CHEGOU NO FIM");
                yellowPlayer3.transform.position = yellowMovementBlock[yellowPlayer3Steps - 1].transform.position;
                avancoYellow = true;
                playerTurn = "YELLOW";
                statusYellow = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }

        }
        if (statusYellow == "AVANCAR" && yellowPlayer3Steps != 76)
        {
            statusYellow = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            //playerTurn = "GREEN";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoGreen");
            //initializeDice();
        }
        ///////////COMPARACAO COM OS PEOES GREEN
        if (yellowPlayer3_CasaAtual == greenPlayer1_CasaAtual
                   && !yellowMovementBlock[yellowPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer1.transform.position = GreenPlayer1Pos;
            greenPlayer1Steps = 0;
            greenPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (yellowPlayer3_CasaAtual == greenPlayer2_CasaAtual &&
            !yellowMovementBlock[yellowPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer2.transform.position = GreenPlayer2Pos;
            greenPlayer2Steps = 0;
            greenPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer3_CasaAtual == greenPlayer3_CasaAtual &&
            !yellowMovementBlock[yellowPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer3.transform.position = GreenPlayer3Pos;
            greenPlayer3Steps = 0;
            greenPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer3_CasaAtual == green_Player4_CasaAtual &&
            !yellowMovementBlock[yellowPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer4.transform.position = GreenPlayer4pos;
            greenPlayer4Steps = 0;
            green_Player4_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        /////////////////////COMPARACAO COM OS PEOES BLUE////////////////////////////////////////
        if (yellowPlayer3_CasaAtual == bluePlayer1_CasaAtual
                   && !yellowMovementBlock[yellowPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer1.transform.position = BluePlayer1Pos;
            bluePlayer1Steps = 0;
            bluePlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (yellowPlayer3_CasaAtual == bluePlayer2_CasaAtual &&
            !yellowMovementBlock[yellowPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer2.transform.position = BluePlayer2Pos;
            bluePlayer2Steps = 0;
            bluePlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer3_CasaAtual == bluePlayer3_CasaAtual &&
            !yellowMovementBlock[yellowPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer3.transform.position = BluePlayer3Pos;
            bluePlayer3Steps = 0;
            bluePlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer3_CasaAtual == bluePlayer4_CasaAtual &&
            !yellowMovementBlock[yellowPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer4.transform.position = BluePlayer4pos;
            bluePlayer4Steps = 0;
            bluePlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
    }
    #endregion
    #region Peao Yellow 4
    IEnumerator PeaoYellow4()
    {
        yellowPlayer4Button.interactable = true;
        BarraContador.SetActive(false); ;
        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {
            yellowPlayer4.transform.position = yellowMovementBlock[yellowPlayer4Steps].transform.position;
            yellowPlayer4_CasaAtual = yellowMovementBlock[yellowPlayer4Steps].transform.position;
            yellowPlayer4Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }
        if (yellowPlayer4Steps == 76 && statusYellow != "AVANCAR")
        {
            if (yellowPlayer2Steps == 0 || yellowPlayer2Steps == 76 && yellowPlayer3Steps == 0 ||
            yellowPlayer3Steps == 76 && yellowPlayer1Steps == 0 || yellowPlayer1Steps == 76)
            {
                BarraContador.SetActive(false);
                //playerTurn = "GREEN";
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoGreen");
                //initializeDice();

            }
            else
            {
                Debug.Log("CHEGOU NO FIM");
                yellowPlayer4.transform.position = yellowMovementBlock[yellowPlayer4Steps - 1].transform.position;
                avancoYellow = true;
                playerTurn = "YELLOW";
                statusYellow = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }

        }
        if (statusYellow == "AVANCAR" && yellowPlayer4Steps != 76)
        {
            statusYellow = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
           // playerTurn = "GREEN";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoGreen");
            //initializeDice();
        }
        ///////////COMPARACAO COM OS PEOES GREEN
        if (yellowPlayer4_CasaAtual == greenPlayer1_CasaAtual
                   && !yellowMovementBlock[yellowPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer1.transform.position = GreenPlayer1Pos;
            greenPlayer1Steps = 0;
            greenPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (yellowPlayer4_CasaAtual == greenPlayer2_CasaAtual &&
            !yellowMovementBlock[yellowPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer2.transform.position = GreenPlayer2Pos;
            greenPlayer2Steps = 0;
            greenPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer4_CasaAtual == greenPlayer3_CasaAtual &&
            !yellowMovementBlock[yellowPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer3.transform.position = GreenPlayer3Pos;
            greenPlayer3Steps = 0;
            greenPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer4_CasaAtual == green_Player4_CasaAtual &&
            !yellowMovementBlock[yellowPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            greenPlayer4.transform.position = GreenPlayer4pos;
            greenPlayer4Steps = 0;
            green_Player4_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        /////////////////////COMPARACAO COM OS PEOES BLUE////////////////////////////////////////
        if (yellowPlayer4_CasaAtual == bluePlayer1_CasaAtual
                   && !yellowMovementBlock[yellowPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer1.transform.position = BluePlayer1Pos;
            bluePlayer1Steps = 0;
            bluePlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (yellowPlayer4_CasaAtual == bluePlayer2_CasaAtual &&
            !yellowMovementBlock[yellowPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer2.transform.position = BluePlayer2Pos;
            bluePlayer2Steps = 0;
            bluePlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer4_CasaAtual == bluePlayer3_CasaAtual &&
            !yellowMovementBlock[yellowPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer3.transform.position = BluePlayer3Pos;
            bluePlayer3Steps = 0;
            bluePlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer4_CasaAtual == bluePlayer4_CasaAtual &&
            !yellowMovementBlock[yellowPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            bluePlayer4.transform.position = BluePlayer4pos;
            bluePlayer4Steps = 0;
            bluePlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
    }
    #endregion
    #endregion
    #region Escolhe qual player Amarelo ira se mover quando o tempo esgotar
    ///////////////////////////////////STARTA A MOVIMENTACAO DO PLAYER RED///////////////////////////////////
    IEnumerator MovimentacaoPlayerYellowTimer()
    {
        yield return new WaitForSeconds(1.0f);
        var playerYellow = Random.Range(1, 4);

        if (playerYellow == 1 && !YellowPlayer1Border.activeInHierarchy)
        {
            playerYellow = 2;
            if (playerYellow == 2 && !YellowPlayer2Border.activeInHierarchy)
            {
                playerYellow = 3;

            }
            if (playerYellow == 3 && !YellowPlayer3Border.activeInHierarchy)
            {
                playerYellow = 4;

            }
        }

        if (playerYellow == 2 && !YellowPlayer2Border.activeInHierarchy)
        {
            playerYellow = 3;
            if (playerYellow == 3 && !YellowPlayer3Border.activeInHierarchy)
            {
                playerYellow = 4;

            }
            if (playerYellow == 4 && !YellowPlayer4Border.activeInHierarchy)
            {
                playerYellow = 1;

            }
        }

        if (playerYellow == 3 && !YellowPlayer3Border.activeInHierarchy)
        {
            playerYellow = 4;

            if (playerYellow == 4 && !YellowPlayer4Border.activeInHierarchy)
            {
                playerYellow = 1;

            }
            if (playerYellow == 1 && !YellowPlayer1Border.activeInHierarchy)
            {
                playerYellow = 2;
            }
        }
        if (playerYellow == 4 && !YellowPlayer4Border.activeInHierarchy)
        {
            playerYellow = 1;

            if (playerYellow == 1 && !YellowPlayer1Border.activeInHierarchy)
            {
                playerYellow = 2;
            }
            if (playerYellow == 2 && !YellowPlayer2Border.activeInHierarchy)
            {
                playerYellow = 3;

            }
        }

        if (playerYellow == 1 && YellowPlayer1Border.activeInHierarchy)
        {
            yellowPlayer1Movement();

        }

        if (playerYellow == 2 && YellowPlayer2Border.activeInHierarchy)
        {
            yellowPlayer2Movement();

        }

        if (playerYellow == 3 && YellowPlayer3Border.activeInHierarchy)
        {
            yellowPlayer3Movement();

        }

        if (playerYellow == 4 && YellowPlayer4Border.activeInHierarchy)
        {
            yellowPlayer4Movement();

        }


    }

    public void MovimentaYellowPlayer()
    {
        BarraContador.SetActive(false);
        StartCoroutine("MovimentacaoPlayerYellowTimer");
    }
    #endregion
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////MOVIMENTAÇÃO PLAYER GREEN////////////////////////////////////////////////////////
    #region Comportamento dos Players Verdes para andarem pelas casas do tabuleiro
    #region Peao Green 1
    IEnumerator PeaoGreen1()
    {
        greenPlayer1Button.interactable = true;
        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {

            greenPlayer1.transform.position = greenMovementBlock[greenPlayer1Steps].transform.position;
            greenPlayer1_CasaAtual = greenMovementBlock[greenPlayer1Steps].transform.position;
            greenPlayer1Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }

        if (greenPlayer1Steps == 76 && statusGreen != "AVANCAR")
        {
            if (greenPlayer2Steps == 0 || greenPlayer2Steps == 76 && greenPlayer3Steps == 0 ||
            greenPlayer3Steps == 76 && greenPlayer4Steps == 0 || greenPlayer4Steps == 76)
            {
                //BarraYellow.SetActive(false);
               // FundoBarraYellow.SetActive(false);
                //playerTurn = "RED";
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoRed");
                //initializeDice();

            }
            else
            {
                Debug.Log("CHEGOU NO FIM");
                greenPlayer1.transform.position = greenMovementBlock[greenPlayer1Steps - 1].transform.position;
                avancoGreen = true;
                playerTurn = "GREEN";
                statusGreen = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }

        }
        if (statusGreen == "AVANCAR" && greenPlayer1Steps != 76)
        {
            statusGreen = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            //playerTurn = "RED";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoRed");
            //initializeDice();
        }
      
        if (greenPlayer1_CasaAtual == redPlayer1_CasaAtual &&
            !greenMovementBlock[greenPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer1.transform.position = redPlayer1Pos;
            redPlayer1Steps = 0;
            redPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer1_CasaAtual == redPlayer2_CasaAtual &&
            !greenMovementBlock[greenPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer2.transform.position = redPlayer2Pos;
            redPlayer2Steps = 0;
            redPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer1_CasaAtual == redPlayer3_CasaAtual &&
            !greenMovementBlock[greenPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer3.transform.position = redPlayer3Pos;
            redPlayer3Steps = 0;
            redPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer1_CasaAtual == redPlayer4_CasaAtual &&
            !greenMovementBlock[greenPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer4.transform.position = redPlayer4pos;
            redPlayer4Steps = 0;
            redPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        ////compara casa amarela
        if (greenPlayer1_CasaAtual == yellowPlayer1_CasaAtual &&
         !greenMovementBlock[greenPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer1.transform.position = YellowPlayer1Pos;
            yellowPlayer1Steps = 0;
            yellowPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer1_CasaAtual == yellowPlayer2_CasaAtual &&
            !greenMovementBlock[greenPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer2.transform.position = YellowPlayer2Pos;
            yellowPlayer2Steps = 0;
            yellowPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer1_CasaAtual == yellowPlayer3_CasaAtual &&
            !greenMovementBlock[greenPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer3.transform.position = YellowPlayer3Pos;
            yellowPlayer3Steps = 0;
            yellowPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer1_CasaAtual == yellowPlayer4_CasaAtual &&
            !greenMovementBlock[greenPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer4.transform.position = YellowPlayer4pos;
            yellowPlayer4Steps = 0;
            yellowPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
    }
    #endregion
    #region Peao Green 2
    IEnumerator PeaoGreen2()
    {
        greenPlayer2Button.interactable = true;
        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {
            greenPlayer2.transform.position = greenMovementBlock[greenPlayer2Steps].transform.position;
            greenPlayer2_CasaAtual = greenMovementBlock[greenPlayer2Steps].transform.position;
            greenPlayer2Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }
        if (greenPlayer2Steps == 76 && statusGreen != "AVANCAR")
        {
            if (greenPlayer1Steps == 0 || greenPlayer1Steps == 76 && greenPlayer3Steps == 0 ||
            greenPlayer3Steps == 76 && greenPlayer4Steps == 0 || greenPlayer4Steps == 76)
            {
                //BarraYellow.SetActive(false);
                // FundoBarraYellow.SetActive(false);
               // playerTurn = "RED";
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoRed");
                //initializeDice();

            }
            else
            {
                Debug.Log("CHEGOU NO FIM");
                greenPlayer2.transform.position = greenMovementBlock[greenPlayer2Steps - 1].transform.position;
                avancoGreen = true;
                playerTurn = "GREEN";
                statusGreen = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }

        }
        if (statusGreen == "AVANCAR" && greenPlayer2Steps != 76)
        {
            statusGreen = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            //playerTurn = "RED";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoRed");
            // initializeDice();
        }
        if (greenPlayer2_CasaAtual == redPlayer1_CasaAtual &&
            !greenMovementBlock[greenPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer1.transform.position = redPlayer1Pos;
            redPlayer1Steps = 0;
            redPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer2_CasaAtual == redPlayer2_CasaAtual &&
            !greenMovementBlock[greenPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer2.transform.position = redPlayer2Pos;
            redPlayer2Steps = 0;
            redPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer2_CasaAtual == redPlayer3_CasaAtual &&
            !greenMovementBlock[greenPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer3.transform.position = redPlayer3Pos;
            redPlayer3Steps = 0;
            redPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer2_CasaAtual == redPlayer4_CasaAtual &&
            !greenMovementBlock[greenPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer4.transform.position = redPlayer4pos;
            redPlayer4Steps = 0;
            redPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        ////compara casa amarela
        if (greenPlayer2_CasaAtual == yellowPlayer1_CasaAtual &&
         !greenMovementBlock[greenPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer1.transform.position = YellowPlayer1Pos;
            yellowPlayer1Steps = 0;
            yellowPlayer1_CasaAtual = new Vector3(0,0,0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer2_CasaAtual == yellowPlayer2_CasaAtual &&
            !greenMovementBlock[greenPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer2.transform.position = YellowPlayer2Pos;
            yellowPlayer2Steps = 0;
            yellowPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer2_CasaAtual == yellowPlayer3_CasaAtual &&
            !greenMovementBlock[greenPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer3.transform.position = YellowPlayer3Pos;
            yellowPlayer3Steps = 0;
            yellowPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer2_CasaAtual == yellowPlayer4_CasaAtual &&
            !greenMovementBlock[greenPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer4.transform.position = YellowPlayer4pos;
            yellowPlayer4Steps = 0;
            yellowPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
    }
    #endregion
    #region Peao Green 3
    IEnumerator PeaoGreen3()
    {
        greenPlayer3Button.interactable = true;
        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {
            greenPlayer3.transform.position = greenMovementBlock[greenPlayer3Steps].transform.position;
            greenPlayer3_CasaAtual = greenMovementBlock[greenPlayer3Steps].transform.position;
            greenPlayer3Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }
        if (greenPlayer3Steps == 76 && statusGreen != "AVANCAR")
        {
            if (greenPlayer1Steps == 0 || greenPlayer1Steps == 76 && greenPlayer2Steps == 0 ||
            greenPlayer2Steps == 76 && greenPlayer4Steps == 0 || greenPlayer4Steps == 76)
            {
                //BarraYellow.SetActive(false);
                // FundoBarraYellow.SetActive(false);
                //playerTurn = "RED";
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoRed");
                //initializeDice();

            }
            else
            {
                Debug.Log("CHEGOU NO FIM");
                greenPlayer3.transform.position = greenMovementBlock[greenPlayer3Steps - 1].transform.position;
                avancoGreen = true;
                playerTurn = "GREEN";
                statusGreen = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }

        }
        if (statusGreen == "AVANCAR" && greenPlayer3Steps != 76)
        {
            statusGreen = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            //playerTurn = "RED";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoRed");
            //initializeDice();
        }

        if (greenPlayer3_CasaAtual == redPlayer1_CasaAtual &&
            !greenMovementBlock[greenPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer1.transform.position = redPlayer1Pos;
            redPlayer1Steps = 0;
            redPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer3_CasaAtual == redPlayer2_CasaAtual &&
            !greenMovementBlock[greenPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer2.transform.position = redPlayer2Pos;
            redPlayer2Steps = 0;
            redPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer3_CasaAtual == redPlayer3_CasaAtual &&
            !greenMovementBlock[greenPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer3.transform.position = redPlayer3Pos;
            redPlayer3Steps = 0;
            redPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer3_CasaAtual == redPlayer4_CasaAtual &&
            !greenMovementBlock[greenPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer4.transform.position = redPlayer4pos;
            redPlayer4Steps = 0;
            redPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }

        ////compara casa amarela
        if (greenPlayer3_CasaAtual == yellowPlayer1_CasaAtual &&
         !greenMovementBlock[greenPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer1.transform.position = YellowPlayer1Pos;
            yellowPlayer1Steps = 0;
            yellowPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer3_CasaAtual == yellowPlayer2_CasaAtual &&
            !greenMovementBlock[greenPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer2.transform.position = YellowPlayer2Pos;
            yellowPlayer2Steps = 0;
            yellowPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer3_CasaAtual == yellowPlayer3_CasaAtual &&
            !greenMovementBlock[greenPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer3.transform.position = YellowPlayer3Pos;
            yellowPlayer3Steps = 0;
            yellowPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (greenPlayer3_CasaAtual == yellowPlayer4_CasaAtual &&
            !greenMovementBlock[greenPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer4.transform.position = YellowPlayer4pos;
            yellowPlayer4Steps = 0;
            yellowPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
    }
    #endregion
    #region Peao Green 4
    IEnumerator PeaoGreen4()
    {
        greenPlayer4Button.interactable = true;
        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {
            greenPlayer4.transform.position = greenMovementBlock[greenPlayer4Steps].transform.position;
            green_Player4_CasaAtual = greenMovementBlock[greenPlayer4Steps].transform.position;
            greenPlayer4Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }
        if (greenPlayer4Steps == 76 && statusGreen != "AVANCAR")
        {
            if (greenPlayer1Steps == 0 || greenPlayer1Steps == 76 && greenPlayer2Steps == 0 ||
            greenPlayer2Steps == 76 && greenPlayer3Steps == 0 || greenPlayer3Steps == 76)
            {
                //BarraYellow.SetActive(false);
                // FundoBarraYellow.SetActive(false);
                //playerTurn = "RED";
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoRed");
                //initializeDice();

            }
            else
            {
                Debug.Log("CHEGOU NO FIM");
                greenPlayer4.transform.position = greenMovementBlock[greenPlayer4Steps - 1].transform.position;
                avancoGreen = true;
                playerTurn = "GREEN";
                statusGreen = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }

        }
        if (statusGreen == "AVANCAR" && greenPlayer4Steps != 76)
        {
            statusGreen = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            //playerTurn = "RED";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoRed");
            //initializeDice();
        }
        if (green_Player4_CasaAtual == redPlayer1_CasaAtual &&
            !greenMovementBlock[greenPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer1.transform.position = redPlayer1Pos;
            redPlayer1Steps = 0;
            redPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (green_Player4_CasaAtual == redPlayer2_CasaAtual &&
            !greenMovementBlock[greenPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer2.transform.position = redPlayer2Pos;
            redPlayer2Steps = 0;
            redPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (green_Player4_CasaAtual == redPlayer3_CasaAtual &&
            !greenMovementBlock[greenPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer3.transform.position = redPlayer3Pos;
            redPlayer3Steps = 0;
            redPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (green_Player4_CasaAtual == redPlayer4_CasaAtual &&
            !greenMovementBlock[greenPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer4.transform.position = redPlayer4pos;
            redPlayer4Steps = 0;
            redPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }

        ////compara casa amarela
        if (green_Player4_CasaAtual == yellowPlayer1_CasaAtual &&
         !greenMovementBlock[greenPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer1.transform.position = YellowPlayer1Pos;
            yellowPlayer1Steps = 0;
            yellowPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (green_Player4_CasaAtual == yellowPlayer2_CasaAtual &&
            !greenMovementBlock[greenPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer2.transform.position = YellowPlayer2Pos;
            yellowPlayer2Steps = 0;
            yellowPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (green_Player4_CasaAtual == yellowPlayer3_CasaAtual &&
            !greenMovementBlock[greenPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer3.transform.position = YellowPlayer3Pos;
            yellowPlayer3Steps = 0;
            yellowPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (green_Player4_CasaAtual == yellowPlayer4_CasaAtual &&
            !greenMovementBlock[greenPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.green;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer4.transform.position = YellowPlayer4pos;
            yellowPlayer4Steps = 0;
            yellowPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoGreen = true;
            playerTurn = "GREEN";
            statusGreen = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
    }
    #endregion
    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////MOVIMENTAÇÃO PLAYER BLUE//////////////////////////////////////////////////////
    #region Comportamento dos Playes Azuis para andarem pelas casas do tabuleiro
    #region Peao Blue 1
    IEnumerator PeaoBlue1()
    {
        bluePlayer1Button.interactable = true;
        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {

            bluePlayer1.transform.position = blueMovementBlock[bluePlayer1Steps].transform.position;
            bluePlayer1_CasaAtual = blueMovementBlock[bluePlayer1Steps].transform.position;
            bluePlayer1Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }
        if (bluePlayer1Steps == 76 && statusBlue != "AVANCAR")
        {
            if (bluePlayer4Steps == 0 || bluePlayer4Steps == 76 && bluePlayer2Steps == 0 ||
            bluePlayer2Steps == 76 && bluePlayer3Steps == 0 || bluePlayer3Steps == 76)
            {
                //BarraYellow.SetActive(false);
                // FundoBarraYellow.SetActive(false);
                //playerTurn = "YELLOW";
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoYellow");
                //initializeDice();

            }
            else
            {
                Debug.Log("CHEGOU NO FIM");
                bluePlayer1.transform.position = blueMovementBlock[bluePlayer1Steps - 1].transform.position;
                avancoBlue = true;
                playerTurn = "BLUE";
                statusBlue = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }

        }
        if (statusBlue == "AVANCAR" && bluePlayer1Steps != 76)
        {
            statusBlue = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
           // playerTurn = "YELLOW";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoYellow");
            //initializeDice();
        }
        //PEOES DA CASA VERMELHA
        if (bluePlayer1_CasaAtual == redPlayer1_CasaAtual &&
            !blueMovementBlock[bluePlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer1.transform.position = redPlayer1Pos;
            redPlayer1Steps = 0;
            redPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer1_CasaAtual == redPlayer2_CasaAtual &&
            !blueMovementBlock[bluePlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer2.transform.position = redPlayer2Pos;
            redPlayer2Steps = 0;
            redPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer1_CasaAtual == redPlayer3_CasaAtual &&
            !blueMovementBlock[bluePlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer3.transform.position = redPlayer3Pos;
            redPlayer3Steps = 0;
            redPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer1_CasaAtual == redPlayer4_CasaAtual &&
            !blueMovementBlock[bluePlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer4.transform.position = redPlayer4pos;
            redPlayer4Steps = 0;
            redPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        ///PEOES DA CASA AMARELA
        if (bluePlayer1_CasaAtual == yellowPlayer1_CasaAtual &&
           !blueMovementBlock[bluePlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer1.transform.position = YellowPlayer1Pos;
            yellowPlayer1Steps = 0;
            yellowPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer1_CasaAtual == yellowPlayer2_CasaAtual &&
            !blueMovementBlock[bluePlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer2.transform.position = YellowPlayer2Pos;
            yellowPlayer2Steps = 0;
            yellowPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer1_CasaAtual == yellowPlayer3_CasaAtual &&
            !blueMovementBlock[bluePlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer3.transform.position = YellowPlayer3Pos;
            yellowPlayer3Steps = 0;
            yellowPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer1_CasaAtual == yellowPlayer4_CasaAtual &&
            !blueMovementBlock[bluePlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer4.transform.position = YellowPlayer4pos;
            yellowPlayer4Steps = 0;
            yellowPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
    }
    #endregion
    #region Peao Blue 2
    IEnumerator PeaoBlue2()
    {
        bluePlayer2Button.interactable = true;
        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {
            bluePlayer2.transform.position = blueMovementBlock[bluePlayer2Steps].transform.position;
            bluePlayer2_CasaAtual = blueMovementBlock[bluePlayer2Steps].transform.position;
            bluePlayer2Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }
        if (bluePlayer2Steps == 76 && statusBlue != "AVANCAR")
        {
            if (bluePlayer4Steps == 0 || bluePlayer4Steps == 76 && bluePlayer1Steps == 0 ||
            bluePlayer1Steps == 76 && bluePlayer3Steps == 0 || bluePlayer3Steps == 76)
            {
                //BarraYellow.SetActive(false);
                // FundoBarraYellow.SetActive(false);
                //playerTurn = "YELLOW";
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoYellow");
                // initializeDice();

            }
            else
            {
                Debug.Log("CHEGOU NO FIM");
                bluePlayer2.transform.position = blueMovementBlock[bluePlayer2Steps - 1].transform.position;
                avancoBlue = true;
                playerTurn = "BLUE";
                statusBlue = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }

        }
        if (statusBlue == "AVANCAR" && bluePlayer2Steps != 76)
        {
            statusBlue = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            //playerTurn = "YELLOW";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoYellow");
            //initializeDice();
        }
        //PEOES DA CASA VERMELHA
        if (bluePlayer2_CasaAtual == redPlayer1_CasaAtual &&
            !blueMovementBlock[bluePlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer1.transform.position = redPlayer1Pos;
            redPlayer1Steps = 0;
            redPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer2_CasaAtual == redPlayer2_CasaAtual &&
            !blueMovementBlock[bluePlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer2.transform.position = redPlayer2Pos;
            redPlayer2Steps = 0;
            redPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer2_CasaAtual == redPlayer3_CasaAtual &&
            !blueMovementBlock[bluePlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer3.transform.position = redPlayer3Pos;
            redPlayer3Steps = 0;
            redPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer2_CasaAtual == redPlayer4_CasaAtual &&
            !blueMovementBlock[bluePlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer4.transform.position = redPlayer4pos;
            redPlayer4Steps = 0;
            redPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        ///PEOES DA CASA AMARELA
        if (bluePlayer2_CasaAtual == yellowPlayer1_CasaAtual &&
           !blueMovementBlock[bluePlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer1.transform.position = YellowPlayer1Pos;
            yellowPlayer1Steps = 0;
            yellowPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer2_CasaAtual == yellowPlayer2_CasaAtual &&
            !blueMovementBlock[bluePlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer2.transform.position = YellowPlayer2Pos;
            yellowPlayer2Steps = 0;
            yellowPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer2_CasaAtual == yellowPlayer3_CasaAtual &&
            !blueMovementBlock[bluePlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer3.transform.position = YellowPlayer3Pos;
            yellowPlayer3Steps = 0;
            yellowPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer2_CasaAtual == yellowPlayer4_CasaAtual &&
            !blueMovementBlock[bluePlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer4.transform.position = YellowPlayer4pos;
            yellowPlayer4Steps = 0;
            yellowPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
    }
    #endregion
    #region Peao Blue 3
    IEnumerator PeaoBlue3()
    {
        bluePlayer3Button.interactable = true;
        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {
            bluePlayer3.transform.position = blueMovementBlock[bluePlayer3Steps].transform.position;
            bluePlayer3_CasaAtual = blueMovementBlock[bluePlayer3Steps].transform.position;
            bluePlayer3Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }
        if (bluePlayer3Steps == 76 && statusBlue != "AVANCAR")
        {
            if (bluePlayer4Steps == 0 || bluePlayer4Steps == 76 && bluePlayer1Steps == 0 ||
            bluePlayer1Steps == 76 && bluePlayer2Steps == 0 || bluePlayer2Steps == 76)
            {
                //BarraYellow.SetActive(false);
                // FundoBarraYellow.SetActive(false);
               // playerTurn = "YELLOW";
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoYellow");
                //initializeDice();

            }
            else
            {
                Debug.Log("CHEGOU NO FIM");
                bluePlayer3.transform.position = blueMovementBlock[bluePlayer3Steps - 1].transform.position;
                avancoBlue = true;
                playerTurn = "BLUE";
                statusBlue = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }

        }
        if (statusBlue == "AVANCAR" && bluePlayer3Steps != 76)
        {
            statusBlue = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            //playerTurn = "YELLOW";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoYellow");
            //initializeDice();
        }

        //PEOES DA CASA VERMELHA
        if (bluePlayer3_CasaAtual == redPlayer1_CasaAtual &&
            !blueMovementBlock[bluePlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer1.transform.position = redPlayer1Pos;
            redPlayer1Steps = 0;
            redPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer3_CasaAtual == redPlayer2_CasaAtual &&
            !blueMovementBlock[bluePlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer2.transform.position = redPlayer2Pos;
            redPlayer2Steps = 0;
            redPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer3_CasaAtual == redPlayer3_CasaAtual &&
            !blueMovementBlock[bluePlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer3.transform.position = redPlayer3Pos;
            redPlayer3Steps = 0;
            redPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer3_CasaAtual == redPlayer4_CasaAtual &&
            !blueMovementBlock[bluePlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer4.transform.position = redPlayer4pos;
            redPlayer4Steps = 0;
            redPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        ///PEOES DA CASA AMARELA
        if (bluePlayer3_CasaAtual == yellowPlayer1_CasaAtual &&
           !blueMovementBlock[bluePlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer1.transform.position = YellowPlayer1Pos;
            yellowPlayer1Steps = 0;
            yellowPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer3_CasaAtual == yellowPlayer2_CasaAtual &&
            !blueMovementBlock[bluePlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer2.transform.position = YellowPlayer2Pos;
            yellowPlayer2Steps = 0;
            yellowPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer3_CasaAtual == yellowPlayer3_CasaAtual &&
            !blueMovementBlock[bluePlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer3.transform.position = YellowPlayer3Pos;
            yellowPlayer3Steps = 0;
            yellowPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer3_CasaAtual == yellowPlayer4_CasaAtual &&
            !blueMovementBlock[bluePlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer4.transform.position = YellowPlayer4pos;
            yellowPlayer4Steps = 0;
            yellowPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
    }
    #endregion
    #region Peao Blue 4
    IEnumerator PeaoBlue4()
    {
        bluePlayer4Button.interactable = true;
        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {
            bluePlayer4.transform.position = blueMovementBlock[bluePlayer4Steps].transform.position;
            bluePlayer4_CasaAtual = blueMovementBlock[bluePlayer4Steps].transform.position;
            bluePlayer4Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }
        if (bluePlayer4Steps == 76 && statusBlue != "AVANCAR")
        {
            if (bluePlayer3Steps == 0 || bluePlayer3Steps == 76 && bluePlayer1Steps == 0 ||
            bluePlayer1Steps == 76 && bluePlayer2Steps == 0 || bluePlayer2Steps == 76)
            {
                //BarraYellow.SetActive(false);
                // FundoBarraYellow.SetActive(false);
               // playerTurn = "YELLOW";
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoYellow");
                //initializeDice();

            }
            else
            {
                Debug.Log("CHEGOU NO FIM");
                bluePlayer4.transform.position = blueMovementBlock[bluePlayer4Steps - 1].transform.position;
                avancoBlue = true;
                playerTurn = "BLUE";
                statusBlue = "AVANCAR";
                SelectDiceNumAnimation = 10;
                diceRollButton.interactable = false;
                diceRollButton.enabled = false;
                initializeDice();
            }

        }
        if (statusBlue == "AVANCAR" && bluePlayer4Steps != 76)
        {
            statusBlue = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
           /// playerTurn = "YELLOW";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoYellow");
            //// initializeDice();
        }

        //PEOES DA CASA VERMELHA
        if (bluePlayer4_CasaAtual == redPlayer1_CasaAtual &&
            !blueMovementBlock[bluePlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer1.transform.position = redPlayer1Pos;
            redPlayer1Steps = 0;
            redPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer4_CasaAtual == redPlayer2_CasaAtual &&
            !blueMovementBlock[bluePlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer2.transform.position = redPlayer2Pos;
            redPlayer2Steps = 0;
            redPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer4_CasaAtual == redPlayer3_CasaAtual &&
            !blueMovementBlock[bluePlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer3.transform.position = redPlayer3Pos;
            redPlayer3Steps = 0;
            redPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer4_CasaAtual == redPlayer4_CasaAtual &&
            !blueMovementBlock[bluePlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer4.transform.position = redPlayer4pos;
            redPlayer4Steps = 0;
            redPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        ///PEOES DA CASA AMARELA
        if (bluePlayer4_CasaAtual == yellowPlayer1_CasaAtual &&
           !blueMovementBlock[bluePlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer1.transform.position = YellowPlayer1Pos;
            yellowPlayer1Steps = 0;
            yellowPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer4_CasaAtual == yellowPlayer2_CasaAtual &&
            !blueMovementBlock[bluePlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer2.transform.position = YellowPlayer2Pos;
            yellowPlayer2Steps = 0;
            yellowPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer4_CasaAtual == yellowPlayer3_CasaAtual &&
            !blueMovementBlock[bluePlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer3.transform.position = YellowPlayer3Pos;
            yellowPlayer3Steps = 0;
            yellowPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (bluePlayer4_CasaAtual == yellowPlayer4_CasaAtual &&
            !blueMovementBlock[bluePlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.blue;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer4.transform.position = YellowPlayer4pos;
            yellowPlayer4Steps = 0;
            yellowPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoBlue = true;
            playerTurn = "BLUE";
            statusBlue = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
    }
    #endregion
    #endregion
    ////////////////////TEMPO DE TROCA DE TURNO A CADA JOGADA/////////////////////////////////////////////////
    #region Troca de Turno 
    IEnumerator TrocaDeTurnoGreen()
    {
        yield return new WaitForSeconds(SelectDiceNumAnimation);
        if (SelectDiceNumAnimation != 10 && PartidaFim==false)
        {
            BarraContador.SetActive(false);
            playerTurn = "GREEN";
            initializeDice();
        }
        else
        {
          
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            //initializeDice();

        }
    }

    IEnumerator TrocaDeTurnoRed()
    {
        yield return new WaitForSeconds(SelectDiceNumAnimation);
        if (SelectDiceNumAnimation != 10 && PartidaFim == false)
        {
            playerTurn = "RED";
            initializeDice();
        }
        else
        {
           
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            //initializeDice();
        }
    }
    IEnumerator TrocaDeTurnoBlue()
    {
        yield return new WaitForSeconds(SelectDiceNumAnimation);
       
        if (SelectDiceNumAnimation != 10 && PartidaFim == false)
        {
            BarraContador.SetActive(false);
            playerTurn = "BLUE";
            initializeDice();
        }
        else
        {
            
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
           /// initializeDice();
        }
    }
    IEnumerator TrocaDeTurnoYellow()
    {
        yield return new WaitForSeconds(SelectDiceNumAnimation);
        if (SelectDiceNumAnimation != 10 && PartidaFim == false)
        {
            playerTurn = "YELLOW";
            initializeDice();
        }
        else
        {
            
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
          //initializeDice();
        }

        
    }
    #endregion
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////Rotina Avanco//////////////////////////////////////////////////////////
    IEnumerator Desativa_TelaAvanco()
    {
        yield return new WaitForSeconds(2.0f);
        Tela_Avanco.SetActive(false);
    }

    ////////////////////////////////////////////////////////////////////////////////
    // Update is called once per frame
    void Update()
    {
        if ((yellowPlayer1Steps == 76||yellowPlayer2Steps == 76
            || yellowPlayer3Steps == 76 || yellowPlayer4Steps == 76 || 
            redPlayer1Steps == 76 || redPlayer2Steps == 76 || redPlayer3Steps == 76 || redPlayer4Steps == 76)
            &&PartidaFim==false)
        {
            PartidaFim = true;
            tempoPartida = Time.fixedTime;
           hora = System.DateTime.Now.ToShortTimeString();
           data = System.DateTime.Now.ToShortDateString();
            playerTurn = "";
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            this.gameObject.GetComponent<AudioSource>().enabled = false;
            TelaNext.SetActive(true);
           //TelaDerrota.SetActive(true);
            Debug.Log("PLAYER YELLOW/RED VENCEU!!!!");
        }
        if ((greenPlayer1Steps == 76 || greenPlayer2Steps == 76 || greenPlayer3Steps == 76
            || greenPlayer4Steps == 76|| bluePlayer1Steps == 76 || bluePlayer2Steps == 76 
            || bluePlayer3Steps == 76|| bluePlayer4Steps == 76)
            && PartidaFim == false)
        {
            PartidaFim = true;
            tempoPartida = Time.fixedTime;
            hora = System.DateTime.Now.ToShortTimeString();
            data = System.DateTime.Now.ToShortDateString();
            playerTurn = "";
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            this.gameObject.GetComponent<AudioSource>().enabled = false;
            TelaDerrota.SetActive(true);
            Debug.Log("GREEN/BLUE VENCEU!!!!");
        }
    }
}
