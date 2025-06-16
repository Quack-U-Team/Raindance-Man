using System.Runtime.CompilerServices;
using UnityEngine;

public class StairsScript : MonoBehaviour
{
    public int floorAddition = 1;
    public LevelManager levelManager;
    public int thisFloor = 0;
    public int floor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (levelManager.canChangeFloor)
        {
            floor = levelManager.currentFloor + floorAddition;
            
            if (other.CompareTag("Player"))
            {

                if (levelManager != null)
                {

                    levelManager.ChangeFloor(floor);
                }
            }
        }
        if (other.CompareTag("Player"))
        {
            levelManager.canChangeFloor = false;
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && levelManager.currentFloor == thisFloor)
        {
            levelManager.canChangeFloor = true;
            Debug.Log("Player exited stairs trigger, can change floor now.");
        }
    }
}
