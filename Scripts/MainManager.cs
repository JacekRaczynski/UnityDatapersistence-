using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO; 
public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.persistentDataPath + "/savefile.json";
         if (File.Exists(path))
        {
            // Read the file as a string
            string json = File.ReadAllText(path);
            Debug.Log("DDDDDDDDDDDD"+json);
            SaveGame saveGame = JsonUtility.FromJson<SaveGame>(json) ;
            GameManager.Instance.saveGame.bestPlayerName = saveGame.bestPlayerName;
            GameManager.Instance.saveGame.theBestScore = saveGame.theBestScore;
              if(GameManager.Instance.saveGame != null){

        BestScoreText.text = $"Best Score :{GameManager.Instance.saveGame.bestPlayerName} {GameManager.Instance.saveGame.theBestScore}";
        }
        }
   
      
        
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
       // GameManager.Instance.saveGame = new SaveGame("","",0);
        if(m_Points > GameManager.Instance.saveGame.theBestScore){
            GameManager.Instance.saveGame.theBestScore = m_Points;
            GameManager.Instance.saveGame.bestPlayerName = GameManager.Instance.saveGame.playerName;
            
        BestScoreText.text = $"Best Score :{GameManager.Instance.saveGame.bestPlayerName} {GameManager.Instance.saveGame.theBestScore}";
         string json = JsonUtility.ToJson(GameManager.Instance.saveGame) ;
          Debug.Log("DDDDDDDDDDDDaaaaaaaaaWIN!!!!"+json);
         File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); 
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
