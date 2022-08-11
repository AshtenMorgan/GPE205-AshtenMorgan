using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] roomTiles;
    public int rows;
    public int columns;
    public float roomWidth = 50.0f;
    public float roomHeight = 50.0f;
    private Room[,] tileMap;
    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
        OpenDoors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject RandomRoomTile()
    {
        return roomTiles[Random.Range(0, roomTiles.Length)]; // Returns random room tile between 0 and the number of room tiles in the list
    }
    public void GenerateMap() // Function for generating a random map
    {
        tileMap = new Room[columns, rows]; // Initializes a tilemap as Rooms with columns and rows that will be determined below
        
        for (int currentRow = 0; currentRow < rows; currentRow++) // for each row of the tileMap
        {
            for (int currentCol = 0; currentCol < columns; currentCol++) // for each of the columns in the rows of the tileMap
            {
                float xPosition = roomWidth * currentCol; // Determine the x coord based on the currentCol
                float zPosition = roomHeight * currentRow; // Determine the z coord based no the currentRow
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition); // Generate values for a Vector 3 based on the x and z positions

                GameObject tempRoomObj = Instantiate(RandomRoomTile(), newPosition, Quaternion.identity) as GameObject; // Instantiate room tiles at the calculated positions

                tempRoomObj.transform.parent = this.transform; // sets the parent of the room tiles

                tempRoomObj.name = "Room_" + currentCol + "," + currentRow; // gives each room tile a unique name based on their order in the columns and rows

                Room tempRoom = tempRoomObj.GetComponent<Room>(); // Get a room

                if (currentRow == 0)
                {
                    Destroy(tempRoom.doorNorth);
                }
                else if (currentRow == rows-1)
                {
                    Destroy(tempRoom.doorSouth);
                }
                else
                {
                    Destroy(tempRoom.doorNorth);
                    Destroy(tempRoom.doorSouth);
                }
                if (currentCol == 0)
                {
                    Destroy(tempRoom.doorEast);
                }
                else if (currentCol == columns - 1)
                {
                    Destroy(tempRoom.doorWest);
                }
                else
                {
                    Destroy(tempRoom.doorEast);
                    Destroy(tempRoom.doorWest);
                }

                tileMap[currentCol, currentRow] = tempRoom; // Save the room to the tileMap array
            }
        }
    }
    public void OpenDoors()
    {
    }
}
