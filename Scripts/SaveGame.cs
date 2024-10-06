using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveGame 
{
       public  string playerName ;
    public  string bestPlayerName;
    
    public  int theBestScore;
    public SaveGame(string playerName, string bestPlayerName, int theBestScore){
        this.playerName = playerName;
        this.bestPlayerName = bestPlayerName;
        this.theBestScore = theBestScore;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
