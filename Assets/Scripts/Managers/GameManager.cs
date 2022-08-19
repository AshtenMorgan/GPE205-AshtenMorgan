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
    public List<EnemySpawnPoint> enemySpawnPoints; // The number of items on this list and the numberOfEnemies should generally be the same
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
    public void SpawnEnemies() // Function reads the list of Spawn Points and spawns an enemy at each point, stopping at the determined numberOfEnemies, even if more points exist
    { 
        EnemySpawnPoint[] tempSpawnPoint = new EnemySpawnPoint[numberOfEnemies]; // Initializes List of Enemy Spawn Points "called enemySpawnPoints" with a length based on the number of Enemies

        for (int currentEnemy = 0; currentEnemy < numberOfEnemies; currentEnemy++) // For loop for instantiating enemies until the numberofEnemies has been reached
        {
            tempSpawnPoint[currentEnemy] = enemySpawnPoints[currentEnemy]; // tells tempSpawnPoint that it is currently looking at the index equivalent to the index of enemySpawnPoints.
                                                                           // Index (based on currentEnemy) increments each loop to spawn an enemy on the next spawn point on the enemySpawnPoints list.

            GameObject newAIControllerObj = Instantiate(aiControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject; // Instantiate AI Controller for this enemy
            GameObject newEnemyPawnObj = Instantiate(enemyPawnPrefab, tempSpawnPoint[currentEnemy].transform.position, Quaternion.identity) as GameObject; // Instantiate enemy pawn at the position of the current tempSpawnPoint

            MasterController newController = newAIControllerObj.GetComponent<MasterController>(); // Gets the MasterController from the instantiated AI Controller
            Pawn newPawn = newEnemyPawnObj.GetComponent<Pawn>(); // gets the pawn from the instantiated enemy Pawn object
            newController.pawn = newPawn; // sets the pawn on the AI Controller (newController) to newPawn, which is the Pawn from the instantiated enemy pawn GameObject
        }
    }
    public void DeactivateAllStates() // Function for deactivating all game states
    {
        TitleScreenStateObject.SetActive(false);
        MainMenuStateObject.SetActive(false);
        OptionsScreenStateObject.SetActive(false);
        CreditsScreenStateObject.SetActive(false);
        GameplayStateObject.SetActive(false);
        GameOverScreenStateObject.SetActive(false);
    }
    public void ActivateTitleScreen()
    {
        DeactivateAllStates(); // Deactivates all states
        TitleScreenStateObject.SetActive(true); // Activates the Title Screen State Object
    }
    public void ActivateMainMenu()
    {
        DeactivateAllStates(); // Deactivates all states
        MainMenuStateObject.SetActive(true); // Activates the Title Screen State Object
    }
    public void ActivateOptionsScreen()
    {
        DeactivateAllStates(); // Deactivates all states
        OptionsScreenStateObject.SetActive(true); // Activates the Title Screen State Object
    }
    public void ActivateCreditsScreen()
    {
        DeactivateAllStates(); // Deactivates all states
        CreditsScreenStateObject.SetActive(true); // Activates the Title Screen State Object
    }
    public void ActivateGameplayState()
    {
        DeactivateAllStates(); // Deactivates all states
        GameplayStateObject.SetActive(true); // Activates the Title Screen State Object
    }
    public void ActivateGameOverScreen()
    {
        DeactivateAllStates(); // Deactivates all states
        GameOverScreenStateObject.SetActive(true); // Activates the Title Screen State Object
    }

    //Prefabs
    public GameObject playerControllerPrefab;
    public GameObject aiControllerPrefab;
    public GameObject enemyPawnPrefab;
    public GameObject tankPawnPrefab;

    // Game State Objects
    public GameObject TitleScreenStateObject;
    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject CreditsScreenStateObject;
    public GameObject GameplayStateObject;
    public GameObject GameOverScreenStateObject;
}
