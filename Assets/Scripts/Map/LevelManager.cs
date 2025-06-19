using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int NumeroLivelli = 2;
    [SerializeField] public GameObject[] Layers ;
    public PlayerMovement Player;
    public int currentFloor = 0;
    public bool canChangeFloor = true;
    [SerializeField] private GameObject[] miasmaBlocks;


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
        if (Player.collectiblesFound == 1)
        {
            miasmaBlocks[0].SetActive(false);
        }
        if (Player.collectiblesFound == 2)
        {
            for (int i = 0; i <= Layers.Length; i++)
            {
                miasmaBlocks[i].SetActive(false);
            }
        }
        if (Player.collectiblesFound == 3)
        {
            //funzionevittoria da aggiungere
        }
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
