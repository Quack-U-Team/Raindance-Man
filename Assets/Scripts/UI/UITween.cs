using UnityEngine;

public class UITween : MonoBehaviour
{
    public GameObject transitionCircle, spinningCane;
    public bool shrink;

    public GameObject loadingUI;

    bool loadingAnimations = false;

    void Start()
    {
        LeanTween.init(800);
        Transition();
    }

    public void Transition()
    {
        if (shrink)
        {
            LeanTween.scale(transitionCircle, Vector3.zero, 0.2f);
        }
        else
        {
            LeanTween.scale(transitionCircle, new Vector3(1,1,1), 0.4f).setOnComplete(ActivateLoadingScreen);
        }
    }

    void ActivateLoadingScreen()
    {
        loadingUI.SetActive(true);
        loadingAnimations = true;
    }

    void Update()
    {
        spinningCane.transform.Rotate(Vector3.up * 0.8f);
    }
}
