using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera camera;

    public Transform StartLoc;
    public Transform EndLoc; 

    public float lerpSpeed;

    public bool isLerping;
    public GameObject ScreenSpaceCanvas;
    private float currentLerpTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main;
        isLerping = false;
        camera.transform.position = StartLoc.position;  

        ScreenSpaceCanvas.SetActive(false);       
    }

    void Update()
    {
        if (!isLerping) return;
        camera.transform.position = Vector3.Slerp(StartLoc.position, EndLoc.position, currentLerpTime);
        currentLerpTime += Time.deltaTime * lerpSpeed;
        if (currentLerpTime >= 1)
        {
            ScreenSpaceCanvas.SetActive(true);
        }
    }

    public void StartGame()
    {
        isLerping = true;
    }

    public void EndGame()
    {
        
    }
}
