using System.Data;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.SqliteClient;
using UnityEngine.SceneManagement;

public class ColetaDeDadosMR : MonoBehaviour
{
    public string nome;
    public Text nameText;
    public GameObject TelaVitoria;
    public GameObject TelaColetaDados;
   
    public LudoModoRapido gm2;
    public Ranking ranking;
    public GameManager gm;

    ///Variaveis BD Modo Rapido
    private string duracaoPartida_MR;
    private string data_MR, hora_MR, nomePlayer_MR;

    //Variaveis BD Modo normal
    private string duracaoPartida_normal;
    private string data_normal, hora_normal, nomePlayer_normal;
    

    //BD
    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;

    string urlDataBase;
    //= "URI=file:" + Application.dataPath+ "/MasterDB.sqlite";

    private void Awake()
    {
        urlDataBase = "URI=file:" + Application.dataPath + "/MasterDB.sqlite";
    }

    void Start()
    {
      
        gm2 = GameObject.FindObjectOfType<LudoModoRapido>();
        gm = GameObject.FindObjectOfType<GameManager>();
        ranking = GameObject.FindObjectOfType<Ranking>();
        Connection_ModoRapido();
        Connection_ModoNormal();
       
    }

    //////CRIA CONEXÃO COM O BD PARA O MODO RÁPIDO///////////////
    public void Connection_ModoRapido()
    {
      connection = new SqliteConnection(urlDataBase);

       command = connection.CreateCommand();

        string sql_MR;
        
       connection.Open();
       sql_MR = "CREATE TABLE IF NOT EXISTS ranking_FastMode(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,nome VARCHAR(50),tempoPartida VARCHAR(50),data VARCHAR(50),hora VARCHAR(50));";
     
        command.CommandText = sql_MR;
        command.ExecuteNonQuery();
    }
    //////CRIA CONEXÃO COM O BD PARA O MODO NORMAL///////////////
    public void Connection_ModoNormal()
    {
        connection = new SqliteConnection(urlDataBase);

        command = connection.CreateCommand();

        string sql_Normal;

        connection.Open();
        sql_Normal = "CREATE TABLE IF NOT EXISTS ranking_NormalMode(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,nome VARCHAR(50),tempoPartida VARCHAR(50),data VARCHAR(50),hora VARCHAR(50));";
        command.CommandText = sql_Normal;
        command.ExecuteNonQuery();
    }
    ////INSERE NO BD OS DADOS DA PARTIDA DO MODO RÁPIDO/////////////////////////////////
    public void Insert_ModoRapido()
    {
        
        nomePlayer_MR = nameText.text;
        data_MR = gm2.data;
        hora_MR = gm2.hora;
        using (var connection = new SqliteConnection(urlDataBase))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                using (var command = connection.CreateCommand())
                {
                    try
                    {
                       var query = "INSERT INTO ranking_FastMode VALUES(null,@nome,@tempoPartida,@data,@hora);";
                        command.CommandText = query;
                        command.Parameters.Add(new SqliteParameter("@nome", nomePlayer_MR));
                        command.Parameters.Add(new SqliteParameter("@data", data_MR));
                        command.Parameters.Add(new SqliteParameter("@hora", hora_MR));
                        command.Parameters.Add(new SqliteParameter("@tempoPartida", duracaoPartida_MR));

                        command.Transaction = transaction;

                        var rows = command.ExecuteNonQuery();

                        transaction.Commit();
                        Debug.Log("INSERIDO");
                      //  print(rows + " affected");
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        print(e.Message);
                        //Debug.Log("");
                    }
                }
            }
        }
      
    }
    ////INSERE NO BD OS DADOS DA PARTIDA DO MODO NORMAL//////////////////////////////////////////
    public void Insert_ModoNormal()
    {
       
        nomePlayer_normal = nameText.text;
        data_normal = gm.data;
        hora_normal = gm.hora;
        using (var connection = new SqliteConnection(urlDataBase))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                using (var command = connection.CreateCommand())
                {
                    try
                    {
                        var query = "INSERT INTO ranking_NormalMode VALUES(null,@nome,@tempoPartida,@data,@hora);";
                        command.CommandText = query;
                        command.Parameters.Add(new SqliteParameter("@nome", nomePlayer_normal));
                        command.Parameters.Add(new SqliteParameter("@data", data_normal));
                        command.Parameters.Add(new SqliteParameter("@hora", hora_normal));
                        command.Parameters.Add(new SqliteParameter("@tempoPartida", duracaoPartida_normal));

                        command.Transaction = transaction;

                        var rows = command.ExecuteNonQuery();

                        transaction.Commit();
                        Debug.Log("INSERIDO");
                        //  print(rows + " affected");
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        print(e.Message);
                        //Debug.Log("");
                    }
                }
            }
        }

    }

    ///Consulta de dados do Modo Normal
    public void ConsultAll_ModoNormal()
    {
        using (var connection = new SqliteConnection(urlDataBase))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {

                command.CommandText = "SELECT * FROM ranking_NormalMode  ORDER BY tempoPartida DESC LIMIT 5";
                command.CommandType = CommandType.Text;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        print(
                              (string)reader[1] + "\n" +
                               (string)reader[2] + "S" + "\n" +
                                (string)reader[3] + "\n" +
                              (string)reader[4]);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    //Referente ao modo rápido
    public void Next_ModoRapido()
    {
       duracaoPartida_MR = gm2.tempoPartida.ToString();
       Insert_ModoRapido();
       TelaColetaDados.GetComponent<AudioSource>().enabled = false;
       TelaColetaDados.SetActive(false);
       TelaVitoria.SetActive(true);
    }
    public void Restart_ModoRapido()
    {
        SceneManager.LoadScene("Ludo(Modo Rapido)");
    }
    //Referente ao modo Normal
    public void Next_ModoNormal()
    {
        duracaoPartida_normal = gm.tempoPartida.ToString();
        Insert_ModoNormal();
        TelaColetaDados.GetComponent<AudioSource>().enabled = false;
        TelaColetaDados.SetActive(false);
        TelaVitoria.SetActive(true);
    }
    public void Restart_ModoNormal()
    {
        SceneManager.LoadScene("Ludo");
    }
    public void VoltaMenu()
    {
        Music_Menu.Musica_ParouDeTocar = false;
        Music_Menu.Musica_tocando = true;
        SceneManager.LoadScene("Menu");
    }
}