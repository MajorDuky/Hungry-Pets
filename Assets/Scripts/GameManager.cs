using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int countFedEnemies;
    [SerializeField] private MainUIHandler mainUIHandler;
    // Start is called before the first frame update
    void Start()
    {
        countFedEnemies = 0;
    }
    
    public void AddFedEnemy()
    {
        countFedEnemies++;
        mainUIHandler.UpdateFedEnemies(countFedEnemies);
    }


}
