using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera camera;

    public Transform StartLoc;
    public Transform EndLoc; 

    public float lerpSpeed;

    public bool isLerping;
    private float currentLerpTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main;
        isLerping = false;
        camera.transform.position = StartLoc.position;         
    }

    void Update()
    {
        if (!isLerping) return;
        camera.transform.position = Vector3.Slerp(StartLoc.position, EndLoc.position, currentLerpTime);
        currentLerpTime += Time.deltaTime * lerpSpeed;
    }

    public void StartGame()
    {
        isLerping = true;
    }
}
