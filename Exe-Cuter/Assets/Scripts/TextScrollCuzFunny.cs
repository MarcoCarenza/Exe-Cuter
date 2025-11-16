using UnityEngine;

public class TextScrollCuzFunny : MonoBehaviour
{
    public bool activate;
    public GameObject textCrawl;
    public float sped;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            textCrawl.transform.position += Vector3.left * (10f * Time.deltaTime * sped);
        }
    }

    public void STARTTHEDAMNTHING()
    {
        activate = true;
    }
}
