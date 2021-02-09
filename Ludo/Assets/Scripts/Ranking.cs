using Mono.Data.SqliteClient;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    public GameObject TelaRanking;
    public GameObject TelaRanking_modoRapido;

    //Arrays da Interface Modo Rapido
    public Text[] Nomes_MR;
    public Text[] Tempo_Partida_MR;
    public Text[] Data_Hora_MR;
    //Arrays da Interface Modo Normal
    public Text[] Nomes_Normal;
    public Text[] Tempo_Partida_Normal;
    public Text[] Data_Hora_Normal;

    private ColetaDeDadosMR dados;
    ///BD
    public static IDbConnection connection;
    public static IDbCommand command;
    public static IDataReader reader;
    public static string urlDataBase;
    //= "URI=file:" + Application.dataPath + "/MasterDB.sqlite";
    private void Awake()
    {
        urlDataBase = "URI=file:" + Application.dataPath + "/MasterDB.sqlite";
    }
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

    ///Consulta de dados do Modo Rápido
    public void ConsultAll_ModoRapido()
    {
        var i = 0;
        using (var connection = new SqliteConnection(urlDataBase))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                
               command.CommandText = "SELECT  * FROM ranking_FastMode  ORDER BY tempoPartida ASC LIMIT 5";
              
                command.CommandType = CommandType.Text;
                
              using (var reader = command.ExecuteReader())
              {
                  while (reader.Read())
                  {
                      Nomes_MR[i].text= (string)reader[1];
                      Tempo_Partida_MR[i].text = (string)reader[2];
                      Data_Hora_MR[i].text = (string)reader[3] + "-" + (string)reader[4];
                      print(
                            (string)reader[1] + "\n" +
                             (string)reader[2] + "S" + "\n" +
                              (string)reader[3] + "\n" +
                            (string)reader[4]);
                      i++;
                  }
                  Debug.Log(urlDataBase);
              }
            }

        }
        }

    /////////Consulta do Modo Normal
    public void ConsultAll_ModoNormal()
    {
        var i = 0;
        using (var connection = new SqliteConnection(urlDataBase))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                
                command.CommandText = "SELECT * FROM ranking_NormalMode  ORDER BY tempoPartida ASC LIMIT 5";
                 command.CommandType = CommandType.Text;
                
                 using (var reader = command.ExecuteReader())
                 {
                     while (reader.Read())
                     {

                         Nomes_Normal[i].text = (string)reader[1];
                         Tempo_Partida_Normal[i].text = (string)reader[2];
                         Data_Hora_Normal[i].text = (string)reader[3] + "-" + (string)reader[4];
                         print(
                               (string)reader[1] + "\n" +
                                (string)reader[2] + "S" + "\n" +
                                 (string)reader[3] + "\n" +
                               (string)reader[4]);
                         i++;
                     }
                 }
            }

        }
    }

    void Start()
    {
     urlDataBase = "URI=file:" + Application.dataPath + "/MasterDB.sqlite";
    dados= GameObject.FindObjectOfType<ColetaDeDadosMR>();
        Connection_ModoNormal();
        Connection_ModoRapido();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /////////////Referente as telas de Ranking///////////////
    public void btn_ranking()
    {
        ConsultAll_ModoNormal();

        TelaRanking.SetActive(true);
       
    }
    public void Next()
    {
        TelaRanking_modoRapido.SetActive(true);
        ConsultAll_ModoRapido();
    }
    public void Close()
    {
        TelaRanking.SetActive(false);
        TelaRanking_modoRapido.SetActive(false);
    }
    /////////////////////
}
