using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // instantiates Game Manager
    public static GameManager instance;
    // Instantiates List of all players
    public List<PlayerController> players;
    public List<AIController> enemies;
    public List<EnemySpawnPoint> enemySpawnPoints;
    public Transform playerSpawnPoint;
    public int numberOfEnemies;
    // Function calls even before Start()
    private void Awake()
    {
        // If Game Manager doesn't exist
        if (instance == null)
        {
            // Instantiate one
            instance = this;
            // and Don't destroy it
            DontDestroyOnLoad(gameObject);
        }
        // Otherwise
        else
        {
            // Destroy it
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        SpawnPlayer();
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Function for spawning player and player controller
    public void SpawnPlayer()
    {
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnPoint.position, Quaternion.identity) as GameObject;
        MasterController newController = newPlayerObj.GetComponent<MasterController>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();
        newController.pawn = newPawn;
    }
    public void SpawnEnemies()
    {
        EnemySpawnPoint[] enemySpawnPoints = new EnemySpawnPoint[numberOfEnemies]; // Initializes List of Enemy Spawn Points "called enemySpawnPoints" with a length based on the number of Enemies
        for (int currentEnemy = 0; currentEnemy < numberOfEnemies; currentEnemy++) // For loop for instantiating enemies until the numberofEnemies has been reached
        {
            EnemySpawnPoint enemySpawnPoint = enemySpawnPoints[currentEnemy]; // For this loop, the Spawn Point is for the enemy with the index of "currentEnemy", starting at 0 and ending at currentEnemy 
            enemyPawnPrefab = enemySpawnPoint.EnemyPrefab; // the enemyPawnPrefab game object, for this loop, is set to the Enemy Prefab for the current enemySpawnPoint.

            GameObject newAIControllerObj = Instantiate(aiControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject; // Instantiate AI Controller for this enemy
            GameObject newEnemyPawnObj = Instantiate(enemyPawnPrefab, enemySpawnPoint.position, Quaternion.identity) as GameObject;

            MasterController newController = newAIControllerObj.GetComponent<MasterController>();
            Pawn newPawn = newEnemyPawnObj.GetComponent<Pawn>();
            newController.pawn = newPawn;
        }
    }

    //Prefabs
    public GameObject playerControllerPrefab;
    public GameObject aiControllerPrefab;
    public GameObject enemyPawnPrefab;
    public GameObject tankPawnPrefab;
}
