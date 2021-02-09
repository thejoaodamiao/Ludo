using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


public class GameManager : MonoBehaviour
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
    public Vector3 BluePlayer1Pos,  BluePlayer2Pos, BluePlayer3Pos, BluePlayer4pos;

    public Transform diceRoll;
    public Transform redDiceRoll, blueDiceRoll, greenDiceRoll, yellowDiceRoll;


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

    private int redPlayer1Steps=0, redPlayer2Steps, redPlayer3Steps, redPlayer4Steps;
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
    private int playerRed;

    ///AUXILIAR DOS PLAYERS AMARELOS 
    int aux_casasTablueiro = 0, aux_casasTabuleiro2 = 0,aux_casasTabuleiro3=0,aux_casasTabuleiro4=0;

    ///CASAS DO TABULEIRO OCUPADAS PLAYERS AMARELOS
    Vector3 yellowPlayer1_CasaAtual, yellowPlayer2_CasaAtual, yellowPlayer3_CasaAtual, yellowPlayer4_CasaAtual;

    ///AUXILIAR DOS PLAYERS GREEN
    int aux_red1 = 0, aux_red2 = 0, aux_red3 = 0, aux_red4 = 0;

    ///CASAS DO TABULEIRO OCUPADAS PLAYERS GREEN
    Vector3 redPlayer1_CasaAtual, redPlayer2_CasaAtual, redPlayer3_CasaAtual, red_Player4_CasaAtual;

    //Verifica cooldown
    private bool cooldownSixDice = false;
    private bool cooldownMovmentRed = false;
    //dice var
    public Text diceNumber;
    
    //Permite avançar quando algum peao chega na meta
    public bool avancoRed=false;
    public int SelectDiceNumAnimation;
    public string playerTurn = "YELLOW";
    public bool avancoYellow=false;
    //dice Roll IA
    public Animator diceIARoll;
   

    //VARIAVEIS DE CONTROLE
    public string statusRed="NAO AVANCOU";
    public string statusYellow="NAO AVANCOU";

    //TIMERS
    public GameObject Barrinha;
    public GameObject BarraYellow;
    public GameObject FundoBarraYellow;
    public Timer timer;
    public float tempoPartida = 0f;
    public bool PartidaFim=false;
    //Capturar hora
    public string hora;
    //Capturar data
    public string data;
    ///Coleta de dados
    public static string Teste;
    public GameObject TelaNext;

    //Sons
    public AudioClip Som_Dado;
    public float volume = 0.5f;

    public AudioClip Som_Captura;

    public AudioClip Som_Movement;

    public GameObject Tela_Derrota;

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
        //SceneManager.LoadScene("Main");
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////
    
   ///////////////////////CHAMA A TELA DE PARTIDA CONCLUÍDA////////////////////////////////////////
    IEnumerator GameCompletedCourotine()
    {
        yield return new WaitForSeconds(1.5f);
        gameCompleteScreen.SetActive(false);
    }

    public void yesCompletedGame()
    {
        soundManager.buttonAudioSource.Play();
        SceneManager.LoadScene("Main");
    }
    public void noCompletedGame()
    {
        soundManager.buttonAudioSource.Play();
        SceneManager.LoadScene("Ludo");
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////

    ///INICIALIZA O DADO,VERIFICANDO DE QUEM É A VEZ DE JOGAR E SETA A POSIÇÃO DO DADO DE ACORDO COM A VEZ DO JOGADOR
    //SE O PLAYER FOR A MAQUINA(VERMELHO) STARTA A ROTINA "RolagemDeDadosRed"
    #region Posicao do Dado
    private void initializeDice()
    {
        diceRollButton.interactable = true;
        switch (MenuManager.quantidadePlayers)
        {
            case 2:
                if (avancoYellow == true)
                {
                    frameRed.SetActive(false);
                    Debug.Log("AVANCO PERMITIDO");
                    playerTurn = "YELLOW";
                    diceNumber.text = "Roll";
                    diceRoll.position = yellowDiceRoll.position;
                    avancoYellow = false;
       
                    if (aux_casasTablueiro > 0||yellowPlayer1Steps>0)
                    {
                        yellowPlayer1Button.interactable = true;
                        YellowPlayer1Border.SetActive(true);
                    }
                    if (aux_casasTabuleiro2 > 0|| yellowPlayer2Steps > 0)
                    {
                        yellowPlayer2Button.interactable = true;
                        YellowPlayer1Border.SetActive(true);
                    }
                    if (aux_casasTabuleiro3 > 0 || yellowPlayer3Steps > 0)
                    {
                        yellowPlayer3Button.interactable = true;
                        YellowPlayer1Border.SetActive(true);
                    }
                    if (aux_casasTabuleiro4 > 0 || yellowPlayer4Steps > 0)
                    {
                        yellowPlayer4Button.interactable = true;
                        YellowPlayer1Border.SetActive(true);
                    }
                    
                }
                if (avancoRed == true)
                {
                    Debug.Log("AVANCO PERMITIDO");
                    playerTurn = "RED";
                    diceNumber.text = "Roll";
                    diceRoll.position = redDiceRoll.position;
                    avancoRed = false;
           
                    if (redPlayer1Steps > 0|| aux_red1>0)
                    {
                        redPlayer1Button.interactable = true;
                        redPlayer1Border.SetActive(true);
                    }
                    if (redPlayer2Steps > 0||aux_red2>0)
                    {
                        redPlayer2Button.interactable = true;
                        redPlayer1Border.SetActive(true);
                    }
                    if (redPlayer3Steps > 0||aux_red3>0)
                    {
                        redPlayer3Button.interactable = true;
                        redPlayer1Border.SetActive(true);
                    }
                    if (redPlayer4Steps > 0||aux_red4>0)
                    {
                        redPlayer4Button.interactable = true;
                        redPlayer1Border.SetActive(true);
                    }
                    StartCoroutine("MovimentacaoPlayerRedCatch");
                }
                if (playerTurn == "YELLOW"&& SelectDiceNumAnimation!=10&statusYellow!="AVANCAR")
                {
                    ////ATIVA OS PLAYERS  AMARELOS(CORRIGIR PARA SO ATIVAR PLAYERS QUANDO ROLAR O DADO)
                    yellowPlayer1Button.interactable = true;
                    yellowPlayer2Button.interactable = true;
                    yellowPlayer3Button.interactable = true;
                    yellowPlayer4Button.interactable = true;
                    ////DESATIVA  OS PLAYERS  Vermelhos
                    redPlayer1Button.interactable = false;
                    redPlayer2Button.interactable = false;
                    redPlayer3Button.interactable = false;
                    redPlayer4Button.interactable = false;
                    ///SETA A POSIÇÃO DO DADO PARA O LADO AMARELO
                    diceNumber.text = "Roll";
                    diceRoll.position = yellowDiceRoll.position;
                    ///ATIVA O FRAME AMARELO E DESATIVA O FRAME VERMELHO
                    frameYellow.SetActive(true);
                    frameRed.SetActive(false);
                }

                if (playerTurn == "RED"&&SelectDiceNumAnimation!=10&&statusYellow!="AVANCAR" 
                    && statusRed!="AVANCAR")
                {
                    Barrinha.SetActive(false);
                    ////ATIVA OS PLAYERS  RED(CORRIGIR PARA SO ATIVAR PLAYERS QUANDO ROLAR O DADO)
                    redPlayer1Button.interactable = true;
                    redPlayer2Button.interactable = true;
                    redPlayer3Button.interactable = true;
                    redPlayer4Button.interactable = true;
                    ////DESATIVA  OS PLAYERS  AMARELOS
                    yellowPlayer1Button.interactable = false;
                    yellowPlayer2Button.interactable = false;
                    yellowPlayer3Button.interactable = false;
                    yellowPlayer4Button.interactable = false;
                    ///SETA A POSIÇÃO DO DADO PARA O LADO VERDE
                    diceNumber.text = "Roll";
                    diceRoll.position = redDiceRoll.position;
                    ///ATIVA O FRAME RED E DESATIVA O FRAME AMARELO
                  
                   frameRed.SetActive(true);
                    frameYellow.SetActive(false);
                    ///ROLA O DADO AUTOMATICAMENTE DA IA
                   
                    StartCoroutine("RolagemDeDadosRed");

                }
                if (statusYellow == "AVANCAR")
                {
                    frameRed.SetActive(false);
                }
                if (statusRed == "AVANCAR")
                {
                    frameYellow.SetActive(false);
                }
                ///DESATIVA BORDAS Amarelas
                YellowPlayer1Border.SetActive(false);
                YellowPlayer2Border.SetActive(false);
                YellowPlayer3Border.SetActive(false);
                YellowPlayer4Border.SetActive(false);
                ///DESATIVA BORDAS Vermelhas
                redPlayer1Border.SetActive(false);
                redPlayer2Border.SetActive(false);
                redPlayer3Border.SetActive(false);
                redPlayer4Border.SetActive(false);
                break;
        }
    }
    #endregion
    ////////////////////////////////////////////////////////////////////////////////////////////
   
    /////////////ROLA O DADO//////////////////////////////////////////////////
    #region Rolagem de Dado
    public void DiceRoll()
    {
        //diceRoll.GetComponent<Animator>().Play("diceButton");
        StartCoroutine("Sound_dice");
        SelectDiceNumAnimation = Random.Range(0, 9);
        diceNumber.gameObject.SetActive(false);

        if (playerTurn == "YELLOW")
        {
            Timer.ContadorTempo = 0;
            Barrinha.SetActive(true);

        }
        else
        {
            Barrinha.SetActive(false);
        }
        StartCoroutine("legendary");

        ////DEPOIS DE ROLAR OS DADOS CHAMA A ROTINA PARA DAR SEQUENCIA A JOGADA E A MOVIMENTACAO DO PLAYER
        StartCoroutine("PlayerNotInitialed");
        
    }
   
    ///////////////////////////////////////////////////////////////////////////////////////////////////////

    IEnumerator legendary()
    {
        yield return new WaitForSeconds(0.5f);
        diceNumber.gameObject.SetActive(true);
        diceNumber.text = SelectDiceNumAnimation.ToString();

        if (playerTurn == "RED")
        {
            Timer.ContadorTempo = 0;
        }

    }
   ////////////////////////////////SOM DO DADO///////////////////
    IEnumerator Sound_dice()
    {
        AudioSource.PlayClipAtPoint(Som_Dado, Camera.main.transform.position, volume);
        yield return new WaitForSeconds(1.5f);
       
    }
    #endregion
    //////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////STARTA A JOGADA DO PLAYER/////////////////////////////////////////
    #region Inicia a Jogada do Player da Vez
    IEnumerator PlayerNotInitialed()
    {
        yield return new WaitForSeconds(1f);

        switch (playerTurn)
        {
            //////////VERIFICA SE OS PLAYERS TÃO NO TABULEIRO/////////////////////////
            case "YELLOW":
              
                if (SelectDiceNumAnimation==9 || SelectDiceNumAnimation == 0 && yellowPlayer1Steps==0)
                {
                    YellowPlayer1Border.SetActive(true);
                    yellowPlayer1Button.interactable = true;
                }
                if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && yellowPlayer2Steps == 0)
                {
                    YellowPlayer2Border.SetActive(true);
                    yellowPlayer2Button.interactable = true;
                }
                if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && yellowPlayer3Steps == 0)
                {
                    YellowPlayer3Border.SetActive(true);
                    yellowPlayer3Button.interactable = true;
                }
                if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && yellowPlayer4Steps == 0)
                {
                    YellowPlayer4Border.SetActive(true);
                    yellowPlayer4Button.interactable = true;
                }

                //////////VERIFICA SE OS PLAYERS TÃO NO TABULEIRO/////////////////////////
                if (aux_casasTablueiro > 0||yellowPlayer1Steps>0 )
                {
                    YellowPlayer1Border.SetActive(true);
                    yellowPlayer1Button.interactable = true;
                }

                if (aux_casasTabuleiro2 > 0 || yellowPlayer2Steps > 0)
                {
                    YellowPlayer2Border.SetActive(true);
                    yellowPlayer2Button.interactable = true;
                }
                if (aux_casasTabuleiro3 > 0 || yellowPlayer3Steps > 0 )
                {
                    YellowPlayer3Border.SetActive(true);
                    yellowPlayer3Button.interactable = true;
                }
                if (aux_casasTabuleiro4 > 0 || yellowPlayer4Steps > 0 )
                {
                    YellowPlayer4Border.SetActive(true);
                    yellowPlayer4Button.interactable = true;
                }
                ///////////////////////////////////////////////////////////////////////////////////////

                /////////////////DESATIVA PLAYERS VENCEDORES OU IMPOSSIBILITADOS DE ANDAR/////////////////////////
                if ((yellowPlayer1Steps == 76 ||
                     yellowMovementBlock.Count - (yellowPlayer1Steps) < SelectDiceNumAnimation) ||
                     (yellowPlayer1Steps > 68 &&
                      yellowMovementBlock.Count - (yellowPlayer1Steps - 1) != SelectDiceNumAnimation+1))
                {
                    yellowPlayer1Button.interactable = false;
                    YellowPlayer1Border.SetActive(false);
                }

                if ((yellowPlayer2Steps == 76 ||
                    yellowMovementBlock.Count - (yellowPlayer2Steps) < SelectDiceNumAnimation)
                    || (yellowPlayer2Steps > 68 &&
            yellowMovementBlock.Count - (yellowPlayer2Steps - 1) != SelectDiceNumAnimation + 1))
                {
                    yellowPlayer2Button.interactable = false;
                    YellowPlayer2Border.SetActive(false);
                }
                if (yellowPlayer3Steps == 76 ||
                    yellowMovementBlock.Count - (yellowPlayer3Steps) < SelectDiceNumAnimation
                    || (yellowPlayer3Steps > 68 &&
            yellowMovementBlock.Count - (yellowPlayer3Steps - 1) != SelectDiceNumAnimation + 1))
                {
                    yellowPlayer3Button.interactable = false;
                    YellowPlayer3Border.SetActive(false);
                }
                if ((yellowPlayer4Steps == 76 ||
                    yellowMovementBlock.Count - (yellowPlayer4Steps) < SelectDiceNumAnimation)
                    || (yellowPlayer4Steps > 68 &&
            yellowMovementBlock.Count - (yellowPlayer4Steps - 1) != SelectDiceNumAnimation + 1))
                {
                    yellowPlayer4Button.interactable = false;
                    YellowPlayer4Border.SetActive(false);
                }
                //////////////////////Se o Numero for 0///////////////////////////////////////
                if (SelectDiceNumAnimation == 0)
                {
                    if (yellowPlayer1Steps != 0 || aux_casasTablueiro != 0)
                    {
                        YellowPlayer1Border.SetActive(false);
                        yellowPlayer1Button.interactable = false;
                    }
                    if (yellowPlayer2Steps != 0 || aux_casasTabuleiro2 != 0)
                    {
                        YellowPlayer2Border.SetActive(false);
                        yellowPlayer2Button.interactable = false;
                    }
                    if (yellowPlayer3Steps != 0 || aux_casasTabuleiro3 != 0)
                    {
                        YellowPlayer3Border.SetActive(false);
                        yellowPlayer3Button.interactable = false;
                    }
                    if (yellowPlayer4Steps != 0 || aux_casasTabuleiro4 != 0)
                    {
                        YellowPlayer4Border.SetActive(false);
                        yellowPlayer4Button.interactable = false;
                    }
                }
                ///////SE NAO TIVER PLAYER ATIVOS EM CENA  O TURNO PASSA A SER DO PLAYER VERDE///////////////
                if (!YellowPlayer1Border.activeInHierarchy&& !YellowPlayer2Border.activeInHierarchy 
                    && !YellowPlayer3Border.activeInHierarchy &&
                    !YellowPlayer4Border.activeInHierarchy)
                {
                    yellowPlayer1Button.interactable = false;
                    yellowPlayer2Button.interactable = false;
                    yellowPlayer3Button.interactable = false;
                    yellowPlayer4Button.interactable = false;

                    switch (MenuManager.quantidadePlayers)
                    {
                        case 2:
                           
                            playerTurn = "RED";
                            diceRollButton.interactable = true;
                            initializeDice();
                            break;
                    }
                }
                break;

                case "RED":
               
                ///////////////BRILHO DAS BORDAS DE PLAYER DISPONIVEIS/////////////////////////////
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
                ///////////////////////////////////////////////////////////////////////////////////////////////////
              
                if (aux_red1 > 0 || redPlayer1Steps > 0)
                {
                    redPlayer1Border.SetActive(true);
                    redPlayer1Button.interactable = true;
                  
                }
                if (aux_red2 > 0 || redPlayer2Steps > 0)
                {
                    redPlayer2Border.SetActive(true);
                    redPlayer2Button.interactable = true;

                }
                if (aux_red3 > 0 || redPlayer3Steps > 0)
                {
                    redPlayer3Border.SetActive(true);
                    redPlayer3Button.interactable = true;

                }
                if (aux_red4 > 0 || redPlayer4Steps > 0)
                {
                    redPlayer4Border.SetActive(true);
                    redPlayer4Button.interactable = true;

                }
                ///////////////////////////////////////////////////////////////////////////////////////


                /////////////////Desativa players Vencedores ou Players Incapacitados//////////////////
                if ((redPlayer1Steps == 76 ||
                    redMovementBlock.Count - (redPlayer1Steps) < SelectDiceNumAnimation)
                    || (redPlayer1Steps > 68 &&
              redMovementBlock.Count - (redPlayer1Steps - 1) != SelectDiceNumAnimation + 1))
                {
                    redPlayer1Button.interactable = false;
                    redPlayer1Border.SetActive(false);
                }

                if ((redPlayer2Steps == 76 ||
                    redMovementBlock.Count - (redPlayer2Steps) < SelectDiceNumAnimation)
                    || (redPlayer2Steps > 68 &&
              redMovementBlock.Count - (redPlayer2Steps - 1) != SelectDiceNumAnimation + 1))
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
                    || (redPlayer4Steps > 68 &&
              redMovementBlock.Count - (redPlayer4Steps - 1) != SelectDiceNumAnimation + 1))
                {
                    redPlayer4Button.interactable = false;
                    redPlayer4Border.SetActive(false);
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////Se o Numero for 0///////////////////////////////////////
                if (SelectDiceNumAnimation == 0)
                {
                    if (redPlayer1Steps != 0 || aux_red1 != 0)
                    {
                        redPlayer1Border.SetActive(false);
                        redPlayer1Button.interactable = false;
                    }
                    if (redPlayer2Steps != 0 || aux_red2 != 0)
                    {
                        redPlayer2Border.SetActive(false);
                        redPlayer2Button.interactable = false;
                    }
                    if (redPlayer3Steps != 0 || aux_red3 != 0)
                    {
                        redPlayer3Border.SetActive(false);
                        redPlayer3Button.interactable = false;
                    }
                    if (redPlayer4Steps != 0 || aux_red4 != 0)
                    {
                        redPlayer4Border.SetActive(false);
                        redPlayer4Button.interactable = false;
                    }
                }
                StartCoroutine("MovimentacaoPlayerRed");

                if (!redPlayer1Border.activeInHierarchy&& !redPlayer2Border.activeInHierarchy&&!redPlayer3Border.activeInHierarchy
                        && !redPlayer4Border.activeInHierarchy)
                {
                   redPlayer1Button.interactable = false;
                   redPlayer2Button.interactable = false;
                   redPlayer3Button.interactable = false;
                   redPlayer4Button.interactable = false;

                    switch (MenuManager.quantidadePlayers)
                    {
                        case 2:
                            playerTurn = "YELLOW";
                            diceRollButton.interactable = true;
                            initializeDice();
                            break;
                    }

                }
                break;
        }
    }
    #endregion
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        if (Music_Menu.Musica_tocando == true)
        {
            Music_Menu.Musica_ParouDeTocar = true;
        }
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 30;

        timer = GameObject.FindObjectOfType<Timer>();

        randNo = new System.Random();
        diceRollAnimation.SetActive(false);
        dice2RollAnimation.SetActive(false);
        dice3RollAnimation.SetActive(false);
        dice4RollAnimation.SetActive(false);
        dice5RollAnimation.SetActive(false);
        dice6RollAnimation.SetActive(false);

        /////DETERMINA POSIÇÃO INICIAL DOS PLAYERS

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
        

        /////////////DESATIVA AS BORDAS DOS PLAYERS
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
        ////////////////////////DESATIVA OS PLAYERS BLUE E GREEN//////////////////////////////////
        bluePlayer1.SetActive(false);
        bluePlayer2.SetActive(false);
        bluePlayer3.SetActive(false);
        bluePlayer4.SetActive(false);
        greenPlayer1.SetActive(false);
        greenPlayer2.SetActive(false);
        greenPlayer3.SetActive(false);
        greenPlayer4.SetActive(false);
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
        switch (MenuManager.quantidadePlayers)
        {
            case 2:
                playerTurn = "YELLOW";
                diceRoll.position = yellowDiceRoll.position;
                frameRed.SetActive(false);
                frameGreen.SetActive(false);
                frameBlue.SetActive(false);
                frameYellow.SetActive(true);
                break;
        }

    }
    //////////////////////////////MOVIMENTO DOS PLAYERS AMARELOS//////////////////////////////////
    #region Movimento dos Players Amarelos

    ////////////////////////////////YELLOW PLAYER1////////////////////////////////////////////////
    #region  Movimento do Yellow Player 1
    public void yellowPlayer1Movement()
    {
        Barrinha.SetActive(false);
        YellowPlayer1Border.SetActive(false);
        YellowPlayer2Border.SetActive(false);
        YellowPlayer3Border.SetActive(false);
        YellowPlayer4Border.SetActive(false);

        //yellowPlayer1Button.interactable = false;
        yellowPlayer2Button.interactable = false;
        yellowPlayer3Button.interactable = false;
        yellowPlayer4Button.interactable = false;

    
        if (playerTurn== "YELLOW" && yellowPlayer1Steps>67 && 
            yellowMovementBlock.Count - (yellowPlayer1Steps-1) == SelectDiceNumAnimation + 1 && avancoYellow == false)
        {
            StartCoroutine("PeaoYellow1");
        }
        ///////////VERIFICA SE O PLAYER TA APTO PARA SE MOVIMENTAR DE ACORDO COM O DADO TIRADO////////////////////
         if (playerTurn == "YELLOW" && (yellowMovementBlock.Count - (yellowPlayer1Steps-1)) > SelectDiceNumAnimation &&
            yellowPlayer1Steps<69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "YELLOW";
            }

            if (aux_casasTablueiro == 1)
            {
                yellowPlayer1.transform.position = yellowMovementBlock[aux_casasTablueiro].transform.position;
                yellowPlayer1_CasaAtual = yellowMovementBlock[aux_casasTablueiro].transform.position;
                aux_casasTablueiro = 0;
                yellowPlayer1Steps++;
            }
            ///CASO OS PASSOS DO PLAYER SEJAM MAIORES QUE 0, O PLAYER SE MOVIMENTA ATRAVÉS DE ROTINA QUE ESTÁ SENDO CHAMADA/////
            if (yellowPlayer1Steps > 0 && avancoYellow==false)
            {
                StartCoroutine("PeaoYellow1");
                Debug.Log("Passei por aqui primeiro");
            }
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && yellowPlayer1Steps == 0)
            {
                Barrinha.SetActive(false);
                yellowPlayer1.transform.position = yellowMovementBlock[yellowPlayer1Steps].transform.position;
                yellowPlayer1_CasaAtual = yellowMovementBlock[yellowPlayer1Steps].transform.position;
                aux_casasTablueiro += 1;
                playerTurn = "YELLOW";
                currentPlayerName = "YELLOW PLAYER 1";
                initializeDice();
            }
            ////////////SE O DADO FOR DIFERENTE DESSES CHAMA A ROTINA PRA TROCAR DE TURNO/////////////////////
            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 &&
                SelectDiceNumAnimation != 10 && statusYellow != "AVANCAR" )
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 2:
                       
                        StartCoroutine("TrocaDeTurnoRed");
                        break;
                }
                //initializeDice();
            }
           
           
        }
        else
        {
            if (SelectDiceNumAnimation != 10 && statusYellow != "AVANCAR")
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 2:

                        StartCoroutine("TrocaDeTurnoRed");
                        break;
                }
               // initializeDice();
            }
        }
      
    }
    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////YELLOW PLAYER 2////////////////////////////////////
    #region Movimento do Yellow Player 2

    public void yellowPlayer2Movement()
    {
        Barrinha.SetActive(false);
        YellowPlayer1Border.SetActive(false);
        YellowPlayer2Border.SetActive(false);
        YellowPlayer3Border.SetActive(false);
        YellowPlayer4Border.SetActive(false);

        yellowPlayer1Button.interactable = false;
       // yellowPlayer2Button.interactable = false;
        yellowPlayer3Button.interactable = false;
        yellowPlayer4Button.interactable = false;

  
        if (playerTurn == "YELLOW" && yellowPlayer2Steps > 67 &&
           yellowMovementBlock.Count - (yellowPlayer2Steps - 1) == SelectDiceNumAnimation + 1 && avancoYellow == false)
        {
            StartCoroutine("PeaoYellow2");
        }

        if (playerTurn == "YELLOW" && (yellowMovementBlock.Count - (yellowPlayer2Steps - 1)) > SelectDiceNumAnimation
            &&yellowPlayer2Steps<69)
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
            if (aux_casasTabuleiro2 == 1)
            {
                yellowPlayer2.transform.position = yellowMovementBlock[aux_casasTabuleiro2].transform.position;
                yellowPlayer2_CasaAtual = yellowMovementBlock[aux_casasTabuleiro2].transform.position;
                aux_casasTabuleiro2 = 0;
                yellowPlayer2Steps++;
            }

            if (yellowPlayer2Steps > 0 && avancoYellow == false)
            {
                Debug.Log("AVANCARRRRRRR!");
                StartCoroutine("PeaoYellow2");
            }

            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && yellowPlayer2Steps == 0)
            {
                Barrinha.SetActive(false);
                yellowPlayer2.transform.position = yellowMovementBlock[yellowPlayer2Steps].transform.position;
                yellowPlayer2_CasaAtual = yellowMovementBlock[yellowPlayer2Steps].transform.position;

                aux_casasTabuleiro2 += 1;
                playerTurn = "YELLOW";
                currentPlayerName = "YELLOW PLAYER 2";
                initializeDice();
            }
            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 &&
                SelectDiceNumAnimation != 10 && statusYellow != "AVANCAR" )
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 2:

                        StartCoroutine("TrocaDeTurnoRed");
                        break;
                }
                //initializeDice();
            }


        }
        else
        {
            if (SelectDiceNumAnimation != 10 && statusYellow != "AVANCAR")
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 2:

                        StartCoroutine("TrocaDeTurnoRed");
                        break;
                }
               // initializeDice();
            }
        }
        
    }
    #endregion
    /////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////Yellow PLAYER 3///////////////////////////////////
    #region Movimento do Yellow Player 3

    public void yellowPlayer3Movement()
    {
        Barrinha.SetActive(false);
        YellowPlayer1Border.SetActive(false);
        YellowPlayer2Border.SetActive(false);
        YellowPlayer3Border.SetActive(false);
        YellowPlayer4Border.SetActive(false);

        yellowPlayer1Button.interactable = false;
        yellowPlayer2Button.interactable = false;
        //yellowPlayer3Button.interactable = false;
        yellowPlayer4Button.interactable = false;

       

        if (playerTurn == "YELLOW" && yellowPlayer3Steps > 67 &&
           yellowMovementBlock.Count - (yellowPlayer3Steps - 1) == SelectDiceNumAnimation + 1 && avancoYellow == false)
        {
            StartCoroutine("PeaoYellow3");
        }

        if (playerTurn == "YELLOW" && (yellowMovementBlock.Count - (yellowPlayer3Steps - 1)) > SelectDiceNumAnimation
            &&yellowPlayer3Steps<69)
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

            if (aux_casasTabuleiro3 == 1)
            {
                yellowPlayer3.transform.position = yellowMovementBlock[aux_casasTabuleiro3].transform.position;
                yellowPlayer3_CasaAtual = yellowMovementBlock[aux_casasTabuleiro3].transform.position;
                aux_casasTabuleiro3 = 0;
                yellowPlayer3Steps++;
            }

            if (yellowPlayer3Steps > 0 && avancoYellow == false)
            {

                StartCoroutine("PeaoYellow3");
            }

           

            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && yellowPlayer3Steps == 0)
            {
                Barrinha.SetActive(false);
                yellowPlayer3.transform.position = yellowMovementBlock[yellowPlayer3Steps].transform.position;
                yellowPlayer3_CasaAtual = yellowMovementBlock[yellowPlayer3Steps].transform.position;
               
                aux_casasTabuleiro3 += 1;
                playerTurn = "YELLOW";
                currentPlayerName = "YELLOW PLAYER 3";
                initializeDice();
            }
            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 &&
                SelectDiceNumAnimation != 10 && statusYellow != "AVANCAR" )
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 2:

                        StartCoroutine("TrocaDeTurnoRed");
                        break;
                }
                //initializeDice();
            }



        }
        else
        {
            if (SelectDiceNumAnimation != 10 && statusYellow != "AVANCAR")
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 2:
                        StartCoroutine("TrocaDeTurnoRed");
                        break;
                }
                //initializeDice();
            }
        }
       
    }
    #endregion
    /////////////////////////////////////////////////////////////////////////////////////////////
    #region Movimento do Yellow Player 4
    /////////////////////////////////////YELLLOW PLAYER4/////////////////////////////////////////////
    public void yellowPlayer4Movement()
    {
        Barrinha.SetActive(false);
        YellowPlayer1Border.SetActive(false);
        YellowPlayer2Border.SetActive(false);
        YellowPlayer3Border.SetActive(false);
        YellowPlayer4Border.SetActive(false);

        yellowPlayer1Button.interactable = false;
        yellowPlayer2Button.interactable = false;
        yellowPlayer3Button.interactable = false;
        //yellowPlayer4Button.interactable = false;

       
        if (playerTurn == "YELLOW" && yellowPlayer4Steps > 67 &&
           yellowMovementBlock.Count - (yellowPlayer4Steps - 1) == SelectDiceNumAnimation + 1 && avancoYellow == false)
        {
            StartCoroutine("PeaoYellow4");
        }

        if (playerTurn == "YELLOW" && (yellowMovementBlock.Count - (yellowPlayer4Steps - 1)) > SelectDiceNumAnimation
            && avancoYellow == false && yellowPlayer4Steps<69)
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

            if (aux_casasTabuleiro4 == 1)
            {

                yellowPlayer4.transform.position = yellowMovementBlock[aux_casasTabuleiro4].transform.position;
                yellowPlayer4_CasaAtual = yellowMovementBlock[aux_casasTabuleiro4].transform.position;
                aux_casasTabuleiro4 = 0;
                yellowPlayer4Steps++;
            }

            if (yellowPlayer4Steps > 0 && avancoYellow == false)
            {
                StartCoroutine("PeaoYellow4");
            }
            
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && yellowPlayer4Steps == 0)
            {
                Barrinha.SetActive(false);
                yellowPlayer4.transform.position = yellowMovementBlock[yellowPlayer4Steps].transform.position;
                yellowPlayer4_CasaAtual = yellowMovementBlock[yellowPlayer4Steps].transform.position;
             
                aux_casasTabuleiro4 += 1;
                playerTurn = "YELLOW";
                currentPlayerName = "YELLOW PLAYER 4";
                initializeDice();
            }
            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0 &&
                SelectDiceNumAnimation != 10 && statusYellow != "AVANCAR")
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 2:

                        StartCoroutine("TrocaDeTurnoRed");
                        break;
                }
                //initializeDice();
            }



        }
        else
        {
            if (SelectDiceNumAnimation != 10 && statusYellow != "AVANCAR")
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 2:
                        //tem que ser green
                        StartCoroutine("TrocaDeTurnoRed");
                        break;
                }
                //initializeDice();
            }
        }
        
    }
    #endregion
    /////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////Movimento dos Players Vermelhos///////////////////
    #region Movimento dos Players Vermelhos
    ////////////////////////////////////////////////////PLAYER red 1////////////////////////////////////////
    #region  Movimento do Player red 1
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
            &&redPlayer1Steps<69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "RED";
                StartCoroutine("MovimentacaoPlayerRedDadoSix");
            }
            /*
            if (redPlayer1Steps == 76)
            {
              redPlayer1.transform.position = redMovementBlock[redPlayer1Steps - 1].transform.position;
                
            }
           */
            if (aux_red1 == 1)
            {
                redPlayer1.transform.position = redMovementBlock[aux_red1].transform.position;
                redPlayer1_CasaAtual = redMovementBlock[aux_red1].transform.position;
                aux_red1 = 0;
                redPlayer1Steps++;
            }

            if (redPlayer1Steps > 0&&avancoRed==false)
            {
                StartCoroutine("PeaoRed1"); 
            }

            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && redPlayer1Steps == 0)
            {
                redPlayer1.transform.position = redMovementBlock[redPlayer1Steps].transform.position;
                redPlayer1_CasaAtual = redMovementBlock[redPlayer1Steps].transform.position;
                aux_red1 += 1;
                playerTurn = "RED";
                currentPlayerName = "RED PLAYER 1";
                initializeDice();
            }

            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0
                && SelectDiceNumAnimation!= 10 && statusRed != "AVANCAR")
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 2:

                        StartCoroutine("TrocaDeTurnoYellow");
                        break;
                }
               
            }
        }
        else
        {
            if (SelectDiceNumAnimation != 10 && statusRed != "AVANCAR")
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 2:
                   // SelectDiceNumAnimation = 0;
                        StartCoroutine("TrocaDeTurnoYellow");
                        break;
                }
            }
        }
       

    }
    #endregion
    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////PLAYER RED 2//////////////////////////////////////////////
    #region Movimento do Player red 2
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
            && redPlayer2Steps<69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "RED";
                StartCoroutine("MovimentacaoPlayerRedDadoSix");
            }
            /*
            if (redPlayer2Steps == 76)
            { 
                redPlayer2.transform.position = redMovementBlock[redPlayer2Steps - 1].transform.position;
            }
            */
            //Estava pulando uma casa por algum motivo
            if (aux_red2 == 1)
            {
                redPlayer2.transform.position = redMovementBlock[aux_red2].transform.position;
                redPlayer2_CasaAtual = redMovementBlock[aux_red2].transform.position;
                aux_red2 = 0;
                redPlayer2Steps++;
            }
            if (redPlayer2Steps > 0&&avancoRed==false)
            {
                StartCoroutine("PeaoRed2");
            }
           

            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && redPlayer2Steps == 0)
            {
                redPlayer2.transform.position = redMovementBlock[redPlayer2Steps].transform.position;
                redPlayer2_CasaAtual = redMovementBlock[redPlayer2Steps].transform.position;
                aux_red2 += 1;
                playerTurn = "RED";
                currentPlayerName = "RED PLAYER 2";
                initializeDice();
            }

            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation 
                != 0 && SelectDiceNumAnimation != 10 && statusRed != "AVANCAR")
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 2:

                        StartCoroutine("TrocaDeTurnoYellow");
                        break;
                }
            }
        }
        else
        {
            if (SelectDiceNumAnimation != 10 && statusRed != "AVANCAR")
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 2:
                       // SelectDiceNumAnimation = 0;
                        StartCoroutine("TrocaDeTurnoYellow");
                        break;
                }

            }
        }


    }
    #endregion
    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////PLAYER RED 3/////////////////////////////////////////////////////////////////
    #region Movimento do red Player 3
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
            redPlayer3Steps<69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "RED";
                StartCoroutine("MovimentacaoPlayerRedDadoSix");
            }
            /*
            if (redPlayer3Steps == 76)
            { 
              redPlayer3.transform.position = redMovementBlock[redPlayer3Steps - 1].transform.position;
            }
           */

            if (aux_red3 == 1)
            {
                redPlayer3.transform.position = redMovementBlock[aux_red3].transform.position;
                redPlayer3_CasaAtual = redMovementBlock[aux_red3].transform.position;
                aux_red3 = 0;
                redPlayer3Steps++;
            }

            if (redPlayer3Steps > 0 && avancoRed == false)
            {
                StartCoroutine("PeaoRed3");
            }
           

            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && redPlayer3Steps == 0)
            {
               
                redPlayer3.transform.position = redMovementBlock[redPlayer3Steps].transform.position;
                redPlayer3_CasaAtual = redMovementBlock[redPlayer3Steps].transform.position;
                aux_red3 += 1;
                playerTurn = "RED";
                currentPlayerName = "RED PLAYER 3";
                initializeDice();
            }

            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation != 0
                &&SelectDiceNumAnimation!= 10 && statusRed != "AVANCAR")
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 2:

                        StartCoroutine("TrocaDeTurnoYellow");
                        break;
                    case 3:
                        playerTurn = "RED";
                        break;
                    case 4:
                        playerTurn = "RED";
                        break;
                }

            }
        }
        else
        {
            if (SelectDiceNumAnimation != 10 && statusRed != "AVANCAR")
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 2:
                        //SelectDiceNumAnimation = 0;
                        StartCoroutine("TrocaDeTurnoYellow");
                        break;

                }
            }
        }


    }
    #endregion
    ///////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////PLAYER RED 4/////////////////////////////////////////////
    #region Movimento do red Player 4
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

        if (playerTurn == "RED" && (redMovementBlock.Count - (redPlayer4Steps - 1)) > SelectDiceNumAnimation&&
            redPlayer4Steps<69)
        {
            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0)
            {
                playerTurn = "RED";
                StartCoroutine("MovimentacaoPlayerRedDadoSix");
            }
            /*
            if (redPlayer4Steps == 76)
            {
                
            redPlayer4.transform.position = redMovementBlock[redPlayer4Steps - 1].transform.position;
            }
            */
          
            if (aux_red4 == 1)
            {
                redPlayer4.transform.position = redMovementBlock[aux_red4].transform.position;
                red_Player4_CasaAtual = redMovementBlock[aux_red4].transform.position;
                aux_red4 = 0;
                redPlayer4Steps++;
            }

            if (redPlayer4Steps > 0&&avancoRed==false)
            {
            StartCoroutine("PeaoRed4");
            }

           

            if (SelectDiceNumAnimation == 9 || SelectDiceNumAnimation == 0 && redPlayer4Steps == 0)
            {
                
                redPlayer4.transform.position = redMovementBlock[redPlayer4Steps].transform.position;
                red_Player4_CasaAtual = redMovementBlock[redPlayer4Steps].transform.position;
                aux_red4 += 1;
                playerTurn = "RED";
                currentPlayerName = "RED PLAYER 4";
                initializeDice();
            }

            else if (SelectDiceNumAnimation != 9 || SelectDiceNumAnimation 
                != 0&&SelectDiceNumAnimation!=10 && statusRed != "AVANCAR")
            {
                switch (MenuManager.quantidadePlayers)
                {
                    case 2:

                        StartCoroutine("TrocaDeTurnoYellow");
                        break;
                }

            }

        }
        else
        {
            if (SelectDiceNumAnimation != 10 && statusRed != "AVANCAR")
           {
                switch (MenuManager.quantidadePlayers)
                {
                    case 2:
                        //SelectDiceNumAnimation = 0;
                        StartCoroutine("TrocaDeTurnoYellow");
                        break;
                }
            }
        }
    }
    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
    /////////////////////////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////ROTINA QUE CONTROLA A VELOCIDADE QUE O A MAQUINA JOGA////////////
    #region Rolagem de dados IA
    IEnumerator RolagemDeDadosRed()
    {
       yield return new WaitForSeconds(1.0f);
        if (SelectDiceNumAnimation != 10 && playerTurn == "RED")
        {
            //diceIARoll.Play("diceButton");
            DiceRoll();
        }
    }
    #endregion
    ////////////////////////////////////////////////////////////////////////////////////////////
    #region IA
    ///MOVIMENTACAO DOS PLAYERS GREEN QUANDO SE TIRA 6 NOS DADOS E TEM MAIS DE UM PLAYER GREEN ATIVO NO TABULEIRO
    IEnumerator MovimentacaoPlayerRedDadoSix()
    {
        yield return new WaitForSeconds(5.0f);
        cooldownSixDice = true;
        cooldownMovmentRed = true;
    }
    #endregion
    ////////////////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////STARTA A MOVIMENTACAO DO PLAYER RED//////////////////////
    #region Escolhe qual player Vermelho e vai se movimentar e chama a função de movimento
    IEnumerator MovimentacaoPlayerRed()
    {  
        yield return new WaitForSeconds(1.0f);
        playerRed = Random.Range(1, 4);
       
        if (playerRed == 1 && !redPlayer1Border.activeInHierarchy)
        {
            playerRed=2;
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
            playerRed= 3;
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
                playerRed=  1;

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
    #endregion
    #region Escolhe qual player Red irá avancar 10 casas
    IEnumerator MovimentacaoPlayerRedCatch()
    {
        yield return new WaitForSeconds(1.0f);
        playerRed = Random.Range(1, 4);

        if (playerRed == 1 && (redPlayer1Steps==0)||(redPlayer1Steps==76)
            || (redPlayer1Steps > 68 && redMovementBlock.Count - (redPlayer1Steps - 1) != SelectDiceNumAnimation + 1))
        {
            playerRed = 2;
            if (playerRed == 2 && (redPlayer2Steps==0)||(redPlayer2Steps == 76)
                || (redPlayer2Steps > 68 && redMovementBlock.Count - (redPlayer2Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerRed = 3;

            }
            if (playerRed == 3 && (redPlayer3Steps==0)||(redPlayer3Steps == 76)||
                (redPlayer3Steps > 68 && redMovementBlock.Count - (redPlayer3Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerRed = 4;

            }
        }

        if (playerRed == 2 && (redPlayer2Steps==0)||(redPlayer2Steps == 76)||
            (redPlayer2Steps > 68 && redMovementBlock.Count - (redPlayer2Steps - 1) != SelectDiceNumAnimation + 1))
        {
            playerRed = 3;
            if (playerRed == 3 && (redPlayer3Steps==0)||(redPlayer3Steps == 76)||
                (redPlayer3Steps > 68 && redMovementBlock.Count - (redPlayer3Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerRed = 4;

            }
            if (playerRed == 4 && (redPlayer4Steps==0)||(redPlayer4Steps == 76)||
                 (redPlayer4Steps > 68 && redMovementBlock.Count - (redPlayer4Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerRed = 1;

            }
        }

        if (playerRed == 3 && (redPlayer3Steps==0)||(redPlayer3Steps == 76)|| 
            (redPlayer3Steps > 68 && redMovementBlock.Count - (redPlayer3Steps - 1) != SelectDiceNumAnimation + 1))
        {
            playerRed = 4;

            if (playerRed == 4 && (redPlayer4Steps==0)||(redPlayer4Steps == 76)||
                 (redPlayer4Steps > 68 && redMovementBlock.Count - (redPlayer4Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerRed = 1;

            }
            if (playerRed == 1 && (redPlayer1Steps==0)||(redPlayer1Steps == 76)
                || (redPlayer1Steps > 68 && redMovementBlock.Count - (redPlayer1Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerRed = 2;
            }
        }
        if (playerRed == 4 && (redPlayer4Steps==0)||(redPlayer4Steps == 76)||
             (redPlayer4Steps > 68 && redMovementBlock.Count - (redPlayer4Steps - 1) != SelectDiceNumAnimation + 1))
        {
            playerRed = 1;

            if (playerRed == 1 && (redPlayer1Steps==0)||(redPlayer1Steps == 76)
                || (redPlayer1Steps > 68 && redMovementBlock.Count - (redPlayer1Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerRed = 2;
            }
            if (playerRed == 2 && (redPlayer2Steps==0)||(redPlayer2Steps == 76)||
                (redPlayer2Steps > 68 && redMovementBlock.Count - (redPlayer2Steps - 1) != SelectDiceNumAnimation + 1))
            {
                playerRed = 3;

            }
        }
       
        if (playerRed == 1 && (redPlayer1Steps > 0 ||redPlayer1Steps != 76))
        {
            if((redPlayer1Steps > 68 && redMovementBlock.Count - (redPlayer1Steps - 1) != SelectDiceNumAnimation + 1))
            {
                statusRed = "NAO AVANCAR";
                avancoRed = false;
                SelectDiceNumAnimation = 0;
                diceRollButton.interactable = true;
                diceRollButton.enabled = true;
                StartCoroutine("TrocaDeTurnoYellow");
            }
            else
            {
                redPlayer1Movement();
            }
           

        }

        if (playerRed == 2 && (redPlayer2Steps > 0 || redPlayer2Steps != 76))
        {
            if ((redPlayer2Steps > 68 && redMovementBlock.Count - (redPlayer2Steps - 1) != SelectDiceNumAnimation + 1))
            {
                statusRed = "NAO AVANCAR";
                avancoRed = false;
                SelectDiceNumAnimation = 0;
                diceRollButton.interactable = true;
                diceRollButton.enabled = true;
                StartCoroutine("TrocaDeTurnoYellow");
            }
            else
            {
                redPlayer2Movement();
            }
           

        }

        if (playerRed == 3 && (redPlayer3Steps > 0 || redPlayer3Steps != 76))
        {
            if ((redPlayer3Steps > 68 && redMovementBlock.Count - (redPlayer3Steps - 1) != SelectDiceNumAnimation + 1))
            {
                statusRed = "NAO AVANCAR";
                avancoRed = false;
                SelectDiceNumAnimation = 0;
                diceRollButton.interactable = true;
                diceRollButton.enabled = true;
                StartCoroutine("TrocaDeTurnoYellow");
            }
            else
            {
                redPlayer3Movement();
            }
           

        }

        if (playerRed == 4 && (redPlayer4Steps > 0 || redPlayer4Steps != 76))
        {
            if ((redPlayer4Steps > 68 && redMovementBlock.Count - (redPlayer4Steps - 1) != SelectDiceNumAnimation + 1))
            {
                statusRed = "NAO AVANCAR";
                avancoRed = false;
                SelectDiceNumAnimation = 0;
                diceRollButton.interactable = true;
                diceRollButton.enabled = true;
                StartCoroutine("TrocaDeTurnoYellow");
            }
            else
            {
                redPlayer4Movement();
            }
            

        }


    }
    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////////

    /////////////////////////////////////SISTEMA DE ESCOLHA DE PEOES YELLOW////////////////////////
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
        Barrinha.SetActive(false);
        StartCoroutine("MovimentacaoPlayerYellowTimer");
    }
    #endregion
    //////////////////////////////////////MOVIMENTAÇÃO PLAYER YELLOW///////////////////////////////
    #region Comportamento dos Players Amarelos para andarem pelas casas do tabuleiro
    #region Peao Yellow1
    IEnumerator PeaoYellow1()
    {
        Barrinha.SetActive(false);

        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {
            yellowPlayer1.transform.position = yellowMovementBlock[yellowPlayer1Steps].transform.position;
            yellowPlayer1_CasaAtual = yellowMovementBlock[yellowPlayer1Steps].transform.position;
            yellowPlayer1Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }

        if (yellowPlayer1Steps == 76 && statusYellow!="AVANCAR")
        {
            if ((aux_casasTabuleiro2==0&&yellowPlayer2Steps==0|| yellowPlayer2Steps == 76) && (aux_casasTabuleiro3 == 0&&yellowPlayer3Steps==0  ||
            yellowPlayer3Steps == 76)&& (aux_casasTabuleiro4 == 0 && yellowPlayer4Steps==0  || yellowPlayer4Steps == 76))
            {
                AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                // BarraContador.SetActive(false);
                //playerTurn = "GREEN";
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoRed");
                //initializeDice();
            }
            
            else
            {
                if ((yellowPlayer2Steps > 68 &&
               yellowMovementBlock.Count - (yellowPlayer2Steps - 1) != 10 + 1) && (yellowPlayer3Steps > 68 &&
               yellowMovementBlock.Count - (yellowPlayer3Steps - 1) != 10 + 1) && (yellowPlayer4Steps > 68 &&
               yellowMovementBlock.Count - (yellowPlayer4Steps - 1) != 10 + 1))
                {
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    SelectDiceNumAnimation = 0;
                    diceRollButton.enabled = true;
                    diceRollButton.interactable = true;
                    StartCoroutine("TrocaDeTurnoRed");
                }
                if(((yellowPlayer2Steps==0&&aux_casasTabuleiro2==0)||(yellowMovementBlock.Count - (yellowPlayer2Steps - 1) < 10 + 1))&&
                    ((yellowPlayer3Steps == 0 && aux_casasTabuleiro3 == 0) || (yellowMovementBlock.Count - (yellowPlayer3Steps - 1) < 10 + 1)) &&
                      ((yellowPlayer4Steps == 0 && aux_casasTabuleiro4 == 0)||(yellowMovementBlock.Count - (yellowPlayer4Steps - 1) < 10 + 1)))
                {
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    SelectDiceNumAnimation = 0;
                    diceRollButton.enabled = true;
                    diceRollButton.interactable = true;
                    StartCoroutine("TrocaDeTurnoRed");
                }
                else
                {
                    Tela_Avanco.SetActive(true);
                    cor_player.color= Color.yellow;
                    StartCoroutine("Desativa_TelaAvanco");
                    Debug.Log("CHEGOU NO FIM");
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    //yellowPlayer1.transform.position = yellowMovementBlock[yellowPlayer1Steps - 1].transform.position;
                    avancoYellow = true;
                    playerTurn = "YELLOW";
                    statusYellow = "AVANCAR";
                    SelectDiceNumAnimation = 10;
                    diceRollButton.interactable = false;
                    diceRollButton.enabled = false;
                    initializeDice();
                }
            }
        }
        
        if (statusYellow == "AVANCAR" && yellowPlayer1Steps!=76)
        {
            statusYellow = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            // playerTurn = "GREEN";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoRed");
            //initializeDice();
        }
        
        if (yellowPlayer1_CasaAtual == redPlayer1_CasaAtual
                   && !yellowMovementBlock[yellowPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            Debug.Log("o numero eh 10");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer1.transform.position = redPlayer1Pos;
            redPlayer1Steps = 0;
            redPlayer1_CasaAtual= new Vector3(0, 0, 0); ;
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
        }
        if (yellowPlayer1_CasaAtual == redPlayer2_CasaAtual &&
            !yellowMovementBlock[yellowPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer2.transform.position = redPlayer2Pos;
            redPlayer2Steps = 0;
            redPlayer1_CasaAtual= new Vector3(0, 0, 0); ;
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
            // yellowPlayer1Movement();



        }
        if (yellowPlayer1_CasaAtual == redPlayer3_CasaAtual &&
            !yellowMovementBlock[yellowPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer3.transform.position = redPlayer3Pos;
            redPlayer3Steps = 0;
            redPlayer3_CasaAtual= new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
            //yellowPlayer1Movement();

        }
        if (yellowPlayer1_CasaAtual == red_Player4_CasaAtual &&
            !yellowMovementBlock[yellowPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer4.transform.position = redPlayer4pos;
            redPlayer4Steps = 0;
            red_Player4_CasaAtual=new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
            //yellowPlayer1Movement();


        }
     //ESSA FUNCAO EH REFERENTE A QUANDO O PLAYER ESCOLHIDO PARA AVANCAR FOR UM INCAPACITADO
       if (yellowPlayer1Steps > 68 &&
       yellowMovementBlock.Count - (yellowPlayer1Steps - 1) != SelectDiceNumAnimation + 1
       &&statusYellow=="AVANCAR!")
        {
            statusYellow = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            diceRollButton.enabled = true;
            diceRollButton.interactable = true;
            StartCoroutine("TrocaDeTurnoRed");
        }

        }
    #endregion
    #region Peao Yellow2
    IEnumerator PeaoYellow2()
    {
        Barrinha.SetActive(false);
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
           if ((aux_casasTablueiro == 0 && yellowPlayer1Steps==0  || yellowPlayer1Steps == 76) && (aux_casasTabuleiro3 == 0 && yellowPlayer3Steps==0  ||
               yellowPlayer3Steps == 76)&& (aux_casasTabuleiro4 == 0 && yellowPlayer4Steps==0 || yellowPlayer4Steps == 76))
             {
                AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                // BarraContador.SetActive(false);
                //playerTurn = "GREEN";
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoRed");
                //initializeDice();

            }
           
            else
            {
                if ((yellowPlayer1Steps > 68 &&
                yellowMovementBlock.Count - (yellowPlayer1Steps - 1) != 10 + 1) && (yellowPlayer3Steps > 68 &&
                yellowMovementBlock.Count - (yellowPlayer3Steps - 1) != 10 + 1) && (yellowPlayer4Steps > 68 &&
                yellowMovementBlock.Count - (yellowPlayer4Steps - 1) != 10 + 1))
                {
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    SelectDiceNumAnimation = 0;
                    diceRollButton.enabled = true;
                    diceRollButton.interactable = true;
                    StartCoroutine("TrocaDeTurnoRed");
                }
                if (((yellowPlayer1Steps == 0 && aux_casasTablueiro == 0) || (yellowMovementBlock.Count - (yellowPlayer1Steps - 1) < 10 + 1)) &&
                  ((yellowPlayer3Steps == 0 && aux_casasTabuleiro3 == 0) || (yellowMovementBlock.Count - (yellowPlayer3Steps - 1) < 10 + 1)) &&
                    ((yellowPlayer4Steps == 0 && aux_casasTabuleiro4 == 0) || (yellowMovementBlock.Count - (yellowPlayer4Steps - 1) < 10 + 1)))
                {
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    SelectDiceNumAnimation = 0;
                    diceRollButton.enabled = true;
                    diceRollButton.interactable = true;
                    StartCoroutine("TrocaDeTurnoRed");
                }
                else
                {
                    Tela_Avanco.SetActive(true);
                    cor_player.color = Color.yellow;
                    StartCoroutine("Desativa_TelaAvanco");
                    Debug.Log("CHEGOU NO FIM");
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    //yellowPlayer2.transform.position = yellowMovementBlock[yellowPlayer2Steps - 1].transform.position;
                    avancoYellow = true;
                    playerTurn = "YELLOW";
                    statusYellow = "AVANCAR";
                    SelectDiceNumAnimation = 10;
                    diceRollButton.interactable = false;
                    diceRollButton.enabled = false;
                    initializeDice();
                }
            }

        }
        if (statusYellow == "AVANCAR"  && yellowPlayer2Steps != 76)
        {
            statusYellow = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            // playerTurn = "GREEN";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoRed");
            //initializeDice();
        }
        if (yellowPlayer2_CasaAtual == redPlayer1_CasaAtual &&
                    !yellowMovementBlock[yellowPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer1.transform.position = redPlayer1Pos;
            redPlayer1Steps = 0;
            redPlayer1_CasaAtual= new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer2_CasaAtual == redPlayer2_CasaAtual &&
            !yellowMovementBlock[yellowPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer2.transform.position = redPlayer2Pos;
            redPlayer2Steps = 0;
            redPlayer2_CasaAtual= new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();


        }
        if (yellowPlayer2_CasaAtual == redPlayer3_CasaAtual &&
            !yellowMovementBlock[yellowPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer3.transform.position = redPlayer3Pos;
            redPlayer3Steps = 0;
            redPlayer3_CasaAtual= new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();


        }
        if (yellowPlayer2_CasaAtual == red_Player4_CasaAtual &&
            !yellowMovementBlock[yellowPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer4.transform.position = redPlayer4pos;
            redPlayer4Steps = 0;
            red_Player4_CasaAtual= new Vector3(0, 0, 0); ;
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();


        }
        //ESSA FUNCAO EH REFERENTE A QUANDO O PLAYER ESCOLHIDO PARA AVANCAR FOR UM INCAPACITADO
        if (yellowPlayer2Steps > 68 &&
        yellowMovementBlock.Count - (yellowPlayer2Steps - 1) != SelectDiceNumAnimation + 1
        && statusYellow == "AVANCAR!")
        {
            statusYellow = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            diceRollButton.enabled = true;
            diceRollButton.interactable = true;
            StartCoroutine("TrocaDeTurnoRed");
        }

    }
    #endregion
    #region Peao Yellow3
    IEnumerator PeaoYellow3()
    {
        Barrinha.SetActive(false);
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
          
            if ((aux_casasTabuleiro2 == 0 && yellowPlayer2Steps==0|| yellowPlayer2Steps == 76) && (aux_casasTablueiro == 0 &&yellowPlayer1Steps==0 ||
             yellowPlayer1Steps == 76) && (aux_casasTabuleiro4 == 0&&yellowPlayer4Steps==0 || yellowPlayer4Steps == 76))
            
            {
                AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                // BarraContador.SetActive(false);
                //playerTurn = "GREEN";
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoRed");
                //initializeDice();

            }
           
            else
            {
                if ((yellowPlayer2Steps > 68 &&
               yellowMovementBlock.Count - (yellowPlayer2Steps - 1) != 10 + 1) && (yellowPlayer1Steps > 68 &&
               yellowMovementBlock.Count - (yellowPlayer1Steps - 1) != 10 + 1) && (yellowPlayer4Steps > 68 &&
               yellowMovementBlock.Count - (yellowPlayer4Steps - 1) != 10 + 1))
                {
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    SelectDiceNumAnimation = 0;
                    diceRollButton.enabled = true;
                    diceRollButton.interactable = true;
                    StartCoroutine("TrocaDeTurnoRed");
                }

                if (((yellowPlayer1Steps == 0 && aux_casasTablueiro == 0) || (yellowMovementBlock.Count - (yellowPlayer1Steps - 1) < 10 + 1)) &&
                ((yellowPlayer2Steps == 0 && aux_casasTabuleiro2 == 0) || (yellowMovementBlock.Count - (yellowPlayer2Steps - 1) < 10 + 1)) &&
                  ((yellowPlayer4Steps == 0 && aux_casasTabuleiro4 == 0) || (yellowMovementBlock.Count - (yellowPlayer4Steps - 1) < 10 + 1)))
                {
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    SelectDiceNumAnimation = 0;
                    diceRollButton.enabled = true;
                    diceRollButton.interactable = true;
                    StartCoroutine("TrocaDeTurnoRed");
                }
                else
                {
                    Tela_Avanco.SetActive(true);
                    cor_player.color = Color.yellow;
                    StartCoroutine("Desativa_TelaAvanco");
                    Debug.Log("CHEGOU NO FIM");
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    // yellowPlayer3.transform.position = yellowMovementBlock[yellowPlayer3Steps - 1].transform.position;
                    avancoYellow = true;
                    playerTurn = "YELLOW";
                    statusYellow = "AVANCAR";
                    SelectDiceNumAnimation = 10;
                    diceRollButton.interactable = false;
                    diceRollButton.enabled = false;
                    initializeDice();
                }
            }

        }
        if (statusYellow == "AVANCAR" && yellowPlayer3Steps != 76)
        {
            statusYellow = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            // playerTurn = "GREEN";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoRed");
            //initializeDice();
        }
        if (yellowPlayer3_CasaAtual == redPlayer1_CasaAtual &&
                  !yellowMovementBlock[yellowPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer1.transform.position = redPlayer1Pos;
            redPlayer1Steps = 0;
            redPlayer1_CasaAtual= new Vector3(0, 0, 0); ;
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();

        }
        if (yellowPlayer3_CasaAtual == redPlayer2_CasaAtual &&
            !yellowMovementBlock[yellowPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer2.transform.position = redPlayer2Pos;
            redPlayer2Steps = 0;
            redPlayer2_CasaAtual= new Vector3(0, 0, 0); ;
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice(); ;


        }
        if (yellowPlayer3_CasaAtual == redPlayer3_CasaAtual &&
            !yellowMovementBlock[yellowPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer3.transform.position = redPlayer3Pos;
            redPlayer3Steps = 0;
            redPlayer3_CasaAtual= new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();
           



        }
        if (yellowPlayer3_CasaAtual == red_Player4_CasaAtual &&
            !yellowMovementBlock[yellowPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer4.transform.position = redPlayer4pos;
            redPlayer4Steps = 0;
            red_Player4_CasaAtual= new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();


        }
        //ESSA FUNCAO EH REFERENTE A QUANDO O PLAYER ESCOLHIDO PARA AVANCAR FOR UM INCAPACITADO
        if (yellowPlayer3Steps > 68 &&
        yellowMovementBlock.Count - (yellowPlayer3Steps - 1) != SelectDiceNumAnimation + 1
        && statusYellow == "AVANCAR!")
        {
            statusYellow = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            diceRollButton.enabled = true;
            diceRollButton.interactable = true;
            StartCoroutine("TrocaDeTurnoRed");
        }


    }
    #endregion
    #region Peao Yellow4
    IEnumerator PeaoYellow4()
    {
        Barrinha.SetActive(false);
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
            if ((aux_casasTabuleiro2 == 0 && yellowPlayer2Steps==0 || yellowPlayer2Steps == 76) && (aux_casasTabuleiro3 == 0 && yellowPlayer3Steps==0 ||
            yellowPlayer3Steps == 76) && (aux_casasTablueiro == 0&& yellowPlayer1Steps==0|| yellowPlayer1Steps == 76))
            {
                AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                // BarraContador.SetActive(false);
                //playerTurn = "GREEN";
                SelectDiceNumAnimation = 0;
                diceRollButton.enabled = true;
                diceRollButton.interactable = true;
                StartCoroutine("TrocaDeTurnoRed");
                //initializeDice();

            }
            
            else
            {
                if ((yellowPlayer2Steps > 68 &&
              yellowMovementBlock.Count - (yellowPlayer2Steps - 1) != 10 + 1) && (yellowPlayer3Steps > 68 &&
              yellowMovementBlock.Count - (yellowPlayer3Steps - 1) != 10 + 1) && (yellowPlayer1Steps > 68 &&
              yellowMovementBlock.Count - (yellowPlayer1Steps - 1) != 10 + 1))
                {
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    SelectDiceNumAnimation = 0;
                    diceRollButton.enabled = true;
                    diceRollButton.interactable = true;
                    StartCoroutine("TrocaDeTurnoRed");
                }
                if (((yellowPlayer1Steps == 0 && aux_casasTablueiro == 0) || (yellowMovementBlock.Count - (yellowPlayer1Steps - 1) < 10 + 1)) &&
               ((yellowPlayer2Steps == 0 && aux_casasTabuleiro2 == 0) || (yellowMovementBlock.Count - (yellowPlayer2Steps - 1) < 10 + 1)) &&
                 ((yellowPlayer3Steps == 0 && aux_casasTabuleiro3 == 0) || (yellowMovementBlock.Count - (yellowPlayer3Steps - 1) < 10 + 1)))
                {
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    SelectDiceNumAnimation = 0;
                    diceRollButton.enabled = true;
                    diceRollButton.interactable = true;
                    StartCoroutine("TrocaDeTurnoRed");
                }

                else
                {
                    Tela_Avanco.SetActive(true);
                    cor_player.color = Color.yellow;
                    StartCoroutine("Desativa_TelaAvanco");
                    Debug.Log("CHEGOU NO FIM");
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    //yellowPlayer4.transform.position = yellowMovementBlock[yellowPlayer4Steps - 1].transform.position;
                    avancoYellow = true;
                    playerTurn = "YELLOW";
                    statusYellow = "AVANCAR";
                    SelectDiceNumAnimation = 10;
                    diceRollButton.interactable = false;
                    diceRollButton.enabled = false;
                    initializeDice();
                }
            }

        }
        if (statusYellow == "AVANCAR"  && yellowPlayer4Steps != 76)
        {
            statusYellow = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            // playerTurn = "GREEN";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoRed");
            //initializeDice();
        }
        if (yellowPlayer4_CasaAtual == redPlayer1_CasaAtual &&
            !yellowMovementBlock[yellowPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer1.transform.position = redPlayer1Pos;
            redPlayer1Steps = 0;
            redPlayer1_CasaAtual= new Vector3(0, 0, 0); 
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();


        }
        if (yellowPlayer4_CasaAtual == redPlayer2_CasaAtual &&
            !yellowMovementBlock[yellowPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer2.transform.position = redPlayer2Pos;
            redPlayer2Steps = 0;
            redPlayer2_CasaAtual= new Vector3(0, 0, 0); 
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();



        }
        if (yellowPlayer4_CasaAtual == redPlayer3_CasaAtual &&
            !yellowMovementBlock[yellowPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer3.transform.position = redPlayer3Pos;
            redPlayer3Steps = 0;
            redPlayer3_CasaAtual= new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();



        }
        if (yellowPlayer4_CasaAtual == red_Player4_CasaAtual &&
            !yellowMovementBlock[yellowPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.yellow;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            redPlayer4.transform.position = redPlayer4pos;
            redPlayer4Steps = 0;
            red_Player4_CasaAtual= new Vector3(0, 0, 0);
            avancoYellow = true;
            playerTurn = "YELLOW";
            statusYellow = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            initializeDice();



        }
        //ESSA FUNCAO EH REFERENTE A QUANDO O PLAYER ESCOLHIDO PARA AVANCAR FOR UM INCAPACITADO
       

    }
    #endregion
    #endregion
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////MOVIMENTAÇÃO PLAYER RED///////////////////////////////////////////////////////////
    #region Comportamento dos Players Vermelhos para andarem pelas casas do tabuleiro
    #region Peao Red1
    IEnumerator PeaoRed1()
    {
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
            if ((redPlayer2Steps == 0 && aux_red2==0|| redPlayer2Steps == 76) && (redPlayer3Steps == 0 && aux_red3==0 || redPlayer3Steps == 76) 
                && (redPlayer4Steps == 0 && aux_red4==0|| redPlayer4Steps == 76))
            {
                AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                //playerTurn = "BLUE";
                SelectDiceNumAnimation = 0;
                diceRollButton.interactable = true;
                diceRollButton.enabled = true;
                StartCoroutine("TrocaDeTurnoYellow");
                //initializeDice();
            }
          
            else
            {
                if ((redPlayer2Steps > 68 &&
     redMovementBlock.Count - (redPlayer2Steps - 1) != 10 + 1) &&
       (redPlayer3Steps > 68 && redMovementBlock.Count - (redPlayer3Steps - 1) != 10 + 1) &&
       (redPlayer4Steps > 68 && redMovementBlock.Count - (redPlayer4Steps - 1) != 10 + 1))
                {
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    statusRed = "NAO AVANCAR";
                    SelectDiceNumAnimation = 0;
                    diceRollButton.interactable = true;
                    diceRollButton.enabled = true;
                    StartCoroutine("TrocaDeTurnoYellow");
                }
                if (((redPlayer4Steps == 0 && aux_red4 == 0) || (redMovementBlock.Count - (redPlayer4Steps - 1) < 10 + 1)) &&
              ((redPlayer2Steps == 0 && aux_red2 == 0) || (redMovementBlock.Count - (redPlayer2Steps - 1) < 10 + 1)) &&
                ((redPlayer3Steps == 0 && aux_red3 == 0) || (redMovementBlock.Count - (redPlayer3Steps - 1) < 10 + 1)))
                {
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    statusRed = "NAO AVANCAR";
                    SelectDiceNumAnimation = 0;
                    diceRollButton.interactable = true;
                    diceRollButton.enabled = true;
                    StartCoroutine("TrocaDeTurnoYellow");
                }
                else
                {
                    Tela_Avanco.SetActive(true);
                    cor_player.color = Color.red;
                    StartCoroutine("Desativa_TelaAvanco");
                    Debug.Log("CHEGOU NO FIM");
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    //redPlayer1.transform.position = redMovementBlock[redPlayer1Steps - 1].transform.position;
                    avancoRed = true;
                    playerTurn = "RED";
                    statusRed = "AVANCAR";
                    SelectDiceNumAnimation = 10;
                    diceRollButton.interactable = false;
                    diceRollButton.enabled = false;
                    initializeDice();
                }
            }
        }
        if (statusRed == "AVANCAR"&&redPlayer1Steps!=76)
        {
            Debug.Log("Nao pode Avancar");
            statusRed = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoYellow");
            // initializeDice();
        }
        if (cooldownMovmentRed == true)
        {
            cooldownMovmentRed = false;
            initializeDice();

        }
        if (redPlayer1_CasaAtual == yellowPlayer1_CasaAtual &&
            !redMovementBlock[redPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer1.transform.position = YellowPlayer1Pos;
            yellowPlayer1Steps = 0;
            yellowPlayer1_CasaAtual= new Vector3(0,0,0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
           
            initializeDice();
        }
        if (redPlayer1_CasaAtual == yellowPlayer2_CasaAtual &&
            !redMovementBlock[redPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer2.transform.position = YellowPlayer2Pos;
            yellowPlayer2Steps = 0;
            yellowPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
           
            initializeDice();
        }
        if (redPlayer1_CasaAtual == yellowPlayer3_CasaAtual &&
            !redMovementBlock[redPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer3.transform.position = YellowPlayer3Pos;
            yellowPlayer3Steps = 0;
            yellowPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
           
            initializeDice();
        }
        if (redPlayer1_CasaAtual == yellowPlayer4_CasaAtual &&
            !redMovementBlock[redPlayer1Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer4.transform.position = YellowPlayer4pos;
            yellowPlayer4Steps = 0;
            yellowPlayer4_CasaAtual = new Vector3(0, 0, 0);
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
    #region Peao Red2
    IEnumerator PeaoRed2()
    {
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
            if ((redPlayer1Steps == 0 && aux_red1==0 || redPlayer1Steps == 76) && (redPlayer3Steps == 0 && aux_red3==0|| redPlayer3Steps == 76)
                && (redPlayer4Steps == 0 && aux_red4==0 || redPlayer4Steps == 76))
            {
                AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                //playerTurn = "BLUE";
                SelectDiceNumAnimation = 0;
                diceRollButton.interactable = true;
                diceRollButton.enabled = true;
                StartCoroutine("TrocaDeTurnoYellow");
                //initializeDice();
            }
           
            else
            {
                if ((redPlayer1Steps > 68 &&
  redMovementBlock.Count - (redPlayer1Steps - 1) != 10 + 1) &&
    (redPlayer3Steps > 68 && redMovementBlock.Count - (redPlayer3Steps - 1) != 10 + 1) &&
    (redPlayer4Steps > 68 && redMovementBlock.Count - (redPlayer4Steps - 1) != 10 + 1))
                {
                    statusRed = "NAO AVANCAR";
                    SelectDiceNumAnimation = 0;
                    diceRollButton.interactable = true;
                    diceRollButton.enabled = true;
                    StartCoroutine("TrocaDeTurnoYellow");
                }
                if (((redPlayer4Steps == 0 && aux_red4 == 0) || (redMovementBlock.Count - (redPlayer4Steps - 1) < 10 + 1)) &&
              ((redPlayer1Steps == 0 && aux_red1 == 0) || (redMovementBlock.Count - (redPlayer1Steps - 1) < 10 + 1)) &&
                ((redPlayer3Steps == 0 && aux_red3 == 0) || (redMovementBlock.Count - (redPlayer3Steps - 1) < 10 + 1)))
                {
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    statusRed = "NAO AVANCAR";
                    SelectDiceNumAnimation = 0;
                    diceRollButton.interactable = true;
                    diceRollButton.enabled = true;
                    StartCoroutine("TrocaDeTurnoYellow");
                }
                else
                {
                    Tela_Avanco.SetActive(true);
                    cor_player.color = Color.red;
                    StartCoroutine("Desativa_TelaAvanco");
                    Debug.Log("CHEGOU NO FIM");
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    //redPlayer2.transform.position = redMovementBlock[redPlayer2Steps - 1].transform.position;
                    avancoRed = true;
                    playerTurn = "RED";
                    statusRed = "AVANCAR";
                    SelectDiceNumAnimation = 10;
                    diceRollButton.interactable = false;
                    diceRollButton.enabled = false;
                    initializeDice();
                }
            }
        }
        if (statusRed == "AVANCAR" && redPlayer2Steps != 76)
        {
            statusRed = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            //playerTurn = "BLUE";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoYellow");
            // initializeDice();
        }
        if (cooldownMovmentRed == true)
        {
            cooldownMovmentRed = false;
            initializeDice();

        }
        if (redPlayer2_CasaAtual == yellowPlayer1_CasaAtual &&
            !redMovementBlock[redPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer1.transform.position = YellowPlayer1Pos;
            yellowPlayer1Steps = 0;
            yellowPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
          
            initializeDice();
        }
        if (redPlayer2_CasaAtual == yellowPlayer2_CasaAtual &&
            !redMovementBlock[redPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer2.transform.position = YellowPlayer2Pos;
            yellowPlayer2Steps = 0;
            yellowPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
           
            initializeDice();
        }
        if (redPlayer2_CasaAtual == yellowPlayer3_CasaAtual &&
            !redMovementBlock[redPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer3.transform.position = YellowPlayer3Pos;
            yellowPlayer3Steps = 0;
            yellowPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
           
            initializeDice(); ;
        }
        if (redPlayer2_CasaAtual == yellowPlayer4_CasaAtual &&
            !redMovementBlock[redPlayer2Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer4.transform.position = YellowPlayer4pos;
            yellowPlayer4Steps = 0;
            yellowPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
           
            initializeDice();
        }
        //ESSA FUNCAO EH REFERENTE A QUANDO O PLAYER ESCOLHIDO PARA AVANCAR FOR UM INCAPACITADO
        

    }
    #endregion
    #region Peao Red3
    IEnumerator PeaoRed3()
    {
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
            if ((redPlayer2Steps == 0 && aux_red2==0|| redPlayer2Steps == 76)&& (redPlayer1Steps == 0 && aux_red1==0|| redPlayer1Steps == 76)
                && (redPlayer4Steps == 0 && aux_red4== 0|| redPlayer4Steps == 76))
            {
                AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                //playerTurn = "BLUE";
                SelectDiceNumAnimation = 0;
                diceRollButton.interactable = true;
                diceRollButton.enabled = true;
                //initializeDice();
                StartCoroutine("TrocaDeTurnoYellow");
            }
           
            else
            {
                if ((redPlayer2Steps > 68 &&
   redMovementBlock.Count - (redPlayer2Steps - 1) != 10 + 1) &&
     (redPlayer1Steps > 68 && redMovementBlock.Count - (redPlayer1Steps - 1) != 10 + 1) &&
     (redPlayer4Steps > 68 && redMovementBlock.Count - (redPlayer4Steps - 1) != 10 + 1))
                {
                    statusRed = "NAO AVANCAR";
                    SelectDiceNumAnimation = 0;
                    diceRollButton.interactable = true;
                    diceRollButton.enabled = true;
                    StartCoroutine("TrocaDeTurnoYellow");
                }

                if (((redPlayer4Steps == 0 && aux_red4 == 0) || (redMovementBlock.Count - (redPlayer4Steps - 1) < 10 + 1)) &&
             ((redPlayer1Steps == 0 && aux_red1 == 0) || (redMovementBlock.Count - (redPlayer1Steps - 1) < 10 + 1)) &&
               ((redPlayer2Steps == 0 && aux_red2 == 0) || (redMovementBlock.Count - (redPlayer2Steps - 1) < 10 + 1)))
                {
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    statusRed = "NAO AVANCAR";
                    SelectDiceNumAnimation = 0;
                    diceRollButton.interactable = true;
                    diceRollButton.enabled = true;
                    StartCoroutine("TrocaDeTurnoYellow");
                }


                else
                {
                    Tela_Avanco.SetActive(true);
                    cor_player.color = Color.red;
                    StartCoroutine("Desativa_TelaAvanco");
                    Debug.Log("CHEGOU NO FIM");
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    //redPlayer3.transform.position = redMovementBlock[redPlayer3Steps - 1].transform.position;
                    avancoRed = true;
                    playerTurn = "RED";
                    statusRed = "AVANCAR";
                    SelectDiceNumAnimation = 10;
                    diceRollButton.interactable = false;
                    diceRollButton.enabled = false;
                    initializeDice();
                }
            }
        }
        if (statusRed == "AVANCAR" && redPlayer3Steps != 76)
        {
            statusRed = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            //playerTurn = "BLUE";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoYellow");
            //initializeDice();
        }
        if (cooldownMovmentRed == true)
        {
            cooldownMovmentRed = false;
            initializeDice();

        }

        if (redPlayer3_CasaAtual == yellowPlayer1_CasaAtual &&
            !redMovementBlock[redPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer1.transform.position = YellowPlayer1Pos;
            yellowPlayer1Steps = 0;
            yellowPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
          
            initializeDice();
        }
        if (redPlayer3_CasaAtual == yellowPlayer2_CasaAtual &&
            !redMovementBlock[redPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer2.transform.position = YellowPlayer2Pos;
            yellowPlayer2Steps = 0;
            yellowPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            
            initializeDice();
        }
        if (redPlayer3_CasaAtual == yellowPlayer3_CasaAtual &&
            !redMovementBlock[redPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer3.transform.position = YellowPlayer3Pos;
            yellowPlayer3Steps = 0;
            yellowPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            
            initializeDice();
        }
        if (redPlayer3_CasaAtual == yellowPlayer4_CasaAtual &&
            !redMovementBlock[redPlayer3Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer4.transform.position = YellowPlayer4pos;
            yellowPlayer4Steps = 0;
            yellowPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            
            initializeDice();
        }
        //ESSA FUNCAO EH REFERENTE A QUANDO O PLAYER ESCOLHIDO PARA AVANCAR FOR UM INCAPACITADO
       

    }
    #endregion
    #region Peao Red4
    IEnumerator PeaoRed4()
    {
        redPlayer4Button.interactable = true;
        for (int i = 0; i < SelectDiceNumAnimation; i++)
        {
            redPlayer4.transform.position = redMovementBlock[redPlayer4Steps].transform.position;
            red_Player4_CasaAtual = redMovementBlock[redPlayer4Steps].transform.position;
            redPlayer4Steps += 1;
            AudioSource.PlayClipAtPoint(Som_Movement, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(1.0f);
        }
        if (redPlayer4Steps == 76 && statusRed != "AVANCAR")
        {
            if ((redPlayer2Steps == 0 && aux_red2==0|| redPlayer2Steps == 76) && (redPlayer3Steps == 0 && aux_red3==0 || redPlayer3Steps == 76)
                && (redPlayer1Steps == 0 && aux_red1==0 || redPlayer1Steps == 76))
            {
                AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                //BarraContador.SetActive(false);
                //playerTurn = "BLUE";
                SelectDiceNumAnimation = 0;
                diceRollButton.interactable = true;
                diceRollButton.enabled = true;
                //initializeDice();
                StartCoroutine("TrocaDeTurnoYellow");
            }
           
            else
            {
                if ((redPlayer2Steps > 68 &&
   redMovementBlock.Count - (redPlayer2Steps - 1) != 10 + 1) &&
     (redPlayer3Steps > 68 && redMovementBlock.Count - (redPlayer3Steps - 1) != 10 + 1) &&
     (redPlayer1Steps > 68 && redMovementBlock.Count - (redPlayer1Steps - 1) != 10 + 1))
                {
                    statusRed = "NAO AVANCAR";
                    SelectDiceNumAnimation = 0;
                    diceRollButton.interactable = true;
                    diceRollButton.enabled = true;
                    StartCoroutine("TrocaDeTurnoYellow");
                }
                if (((redPlayer2Steps == 0 && aux_red4 == 0) || (redMovementBlock.Count - (redPlayer2Steps - 1) < 10 + 1)) &&
             ((redPlayer1Steps == 0 && aux_red1 == 0) || (redMovementBlock.Count - (redPlayer1Steps - 1) < 10 + 1)) &&
               ((redPlayer3Steps == 0 && aux_red3 == 0) || (redMovementBlock.Count - (redPlayer3Steps - 1) < 10 + 1)))
                {
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    statusRed = "NAO AVANCAR";
                    SelectDiceNumAnimation = 0;
                    diceRollButton.interactable = true;
                    diceRollButton.enabled = true;
                    StartCoroutine("TrocaDeTurnoYellow");
                }
                else
                {
                    Tela_Avanco.SetActive(true);
                    cor_player.color = Color.red;
                    StartCoroutine("Desativa_TelaAvanco");
                    Debug.Log("CHEGOU NO FIM");
                    AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
                    //redPlayer4.transform.position = redMovementBlock[redPlayer4Steps - 1].transform.position;
                    avancoRed = true;
                    playerTurn = "RED";
                    statusRed = "AVANCAR";
                    SelectDiceNumAnimation = 10;
                    diceRollButton.interactable = false;
                    diceRollButton.enabled = false;
                    initializeDice();
                }
            }
        }
        if (statusRed == "AVANCAR" && redPlayer4Steps != 76)
        {
            statusRed = "NAO AVANCAR";
            SelectDiceNumAnimation = 0;
            //playerTurn = "BLUE";
            diceRollButton.interactable = true;
            diceRollButton.enabled = true;
            StartCoroutine("TrocaDeTurnoYellow");
            //initializeDice();
        }
        if (cooldownMovmentRed == true)
        {
            cooldownMovmentRed = false;
            initializeDice();

        }
        if (red_Player4_CasaAtual == yellowPlayer1_CasaAtual &&
            !redMovementBlock[redPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer1.transform.position = YellowPlayer1Pos;
            yellowPlayer1Steps = 0;
            yellowPlayer1_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
           
            initializeDice();
        }
        if (red_Player4_CasaAtual == yellowPlayer2_CasaAtual &&
            !redMovementBlock[redPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer2.transform.position = YellowPlayer2Pos;
            yellowPlayer2Steps = 0;
            yellowPlayer2_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            
            initializeDice(); 
        }
        if (red_Player4_CasaAtual == yellowPlayer3_CasaAtual &&
            !redMovementBlock[redPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer3.transform.position = YellowPlayer3Pos;
            yellowPlayer3Steps = 0;
            yellowPlayer3_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
           
            initializeDice();
        }
        if (red_Player4_CasaAtual == yellowPlayer4_CasaAtual &&
            !redMovementBlock[redPlayer4Steps-1].gameObject.CompareTag("Casa_Segura"))
        {
            Tela_Avanco.SetActive(true);
            cor_player.color = Color.red;
            StartCoroutine("Desativa_TelaAvanco");
            AudioSource.PlayClipAtPoint(Som_Captura, Camera.main.transform.position, volume);
            yellowPlayer4.transform.position = YellowPlayer4pos;
            yellowPlayer4Steps = 0;
            yellowPlayer4_CasaAtual = new Vector3(0, 0, 0);
            avancoRed = true;
            playerTurn = "RED";
            statusRed = "AVANCAR";
            SelectDiceNumAnimation = 10;
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
           
            initializeDice();
        }
        //ESSA FUNCAO EH REFERENTE A QUANDO O PLAYER ESCOLHIDO PARA AVANCAR FOR UM INCAPACITADO
        

    }
    #endregion
    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    ////////////////////TEMPO DE TROCA DE TURNO A CADA JOGADA/////////////////////////////////////////////////
    #region Troca de Turno
    IEnumerator TrocaDeTurnoRed()
    {
        yield return new WaitForSeconds(SelectDiceNumAnimation);
        if (SelectDiceNumAnimation != 10 && PartidaFim == false)
        {
            playerTurn = "RED";
            Barrinha.SetActive(false);
            initializeDice();
        }
        else
        {
            playerTurn = "YELLOW";
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            //initializeDice();
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
            playerTurn = "RED";
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            //initializeDice();
        }


    }
    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////Rotina Avanco//////////////////////////////////////////////////////////
    IEnumerator Desativa_TelaAvanco()
    {
        yield return new WaitForSeconds(2.0f);
        Tela_Avanco.SetActive(false);
    }

   /////////////////////////// ///DETERMINA O PLAYER VENCEDOR//////////////////////////////////////////////////////////////
    void Update()
    {
      //VOLTAR AO NORMAL
        if (yellowPlayer1Steps == 76 && yellowPlayer2Steps == 76 &&  yellowPlayer3Steps == 76 
            && yellowPlayer4Steps == 76 && PartidaFim == false)
        {
            PartidaFim = true;
            tempoPartida = Time.fixedTime;
            hora = System.DateTime.Now.ToShortTimeString();
            data = System.DateTime.Now.ToShortDateString();
            playerTurn = "";
            diceRollButton.interactable = false;
            diceRollButton.enabled = false;
            this.gameObject.GetComponent<AudioSource>().enabled = false;
            //Tela_Derrota.SetActive(true);
            TelaNext.SetActive(true);
            // Debug.Log("PLAYER YELLOW");
        }
        if (redPlayer1Steps == 76 && redPlayer2Steps == 76 && redPlayer3Steps == 76 && redPlayer4Steps == 76
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
            Tela_Derrota.SetActive(true);
            ///TelaNext.SetActive(true);
            //Debug.Log("RED VENCEU!!!!");
        }
    }
}
