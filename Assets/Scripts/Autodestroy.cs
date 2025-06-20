using UnityEngine;

public class Autodestroy : MonoBehaviour
{
    public float delay;
    private void Start()
    {
        Invoke("DelayedDestroy", delay);
    }

    void DelayedDestroy()
    {
        Destroy(this.gameObject);
    }    

}
