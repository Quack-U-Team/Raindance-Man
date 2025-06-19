using System.Runtime.CompilerServices;
using UnityEngine;

public class StairsScript : MonoBehaviour
{
    public int floorAddition = 1;
    public LevelManager levelManager;
    public int thisFloor = 0;
    public int floor;
    public Transform tpTransform;
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
                    other.transform.position = tpTransform.position;
                    levelManager.ChangeFloor(floor);
                }
            }
        }
        
       
    }

  
}
