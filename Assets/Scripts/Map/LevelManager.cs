using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int NumeroLivelli = 2;
    [SerializeField] public GameObject[] Layers ;
    public PlayerMovement Player;
    public int currentFloor = 0;
    public bool canChangeFloor = true;


    private void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Layers[currentFloor].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeFloor(int floor)
    {
       
        if (currentFloor >= 0 && currentFloor < NumeroLivelli)
        {
            Debug.Log("Changing floor from: " + currentFloor + " to: " + floor);
            Layers[currentFloor].SetActive(false);

            currentFloor = floor;
            Layers[currentFloor].SetActive(true);
        }

        
       
    }

}
