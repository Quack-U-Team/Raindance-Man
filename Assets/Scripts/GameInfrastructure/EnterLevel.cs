using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevel : MonoBehaviour
{
    public GameObject LoadingUI;
    public int levelToLoad;
    private BoxCollider2D bc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(EnterTheLevel());
    }

    IEnumerator EnterTheLevel()
    {
        LoadingUI.SetActive(true);
        PlayerMovement.instance.Freeze();
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(levelToLoad);
    }
}
