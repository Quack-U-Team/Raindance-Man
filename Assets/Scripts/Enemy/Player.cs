using UnityEngine;
using UnityEngine.U2D;

public class Player : MonoBehaviour, IHittable
{

    public float speed = 5f;
    public float coolDownSpeed = 5f;

    void Start()
    {

    }

    public void OnHitSuffered()
    {
        if (coolDownSpeed == 0)
        {
            speed = 5f;
        }
        else
        {
            for (int i = 0; i < coolDownSpeed; i++)
            {

                speed = 1f;

            }
        }
    }

}
