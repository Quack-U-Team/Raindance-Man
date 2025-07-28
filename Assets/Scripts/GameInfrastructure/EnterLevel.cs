using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevel : MonoBehaviour
{
    public int levelToLoad;
    private BoxCollider2D bc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
