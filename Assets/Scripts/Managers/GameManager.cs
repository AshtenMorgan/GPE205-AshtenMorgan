using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // instantiates Game Manager
    public static GameManager instance;
    // Instantiates List of all players
    public List<PlayerController> players;
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
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Function for spawning player and player controller
    public void SpawnPlayer()
    {
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        GameObject newPawnObj = Instantiate(tankPawnPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        MasterController newController = newPlayerObj.GetComponent<MasterController>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();
        newController.pawn = newPawn;
    }

    //Prefabs
    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;
}
