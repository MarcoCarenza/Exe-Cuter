using UnityEngine;

public class TextScrollCuzFunny : MonoBehaviour
{
    public bool fuckingActivatePlease;
    public GameObject textCrawl;
    public float sped;
    public int myInteger = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fuckingActivatePlease = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (myInteger == 1)
        {
            textCrawl.transform.position += Vector3.left * (10f * Time.deltaTime * sped);
        }
    }

    public void STARTTHEDAMNTHING()
    {
        Debug.Log("HELO!");
        myInteger = 1;
        fuckingActivatePlease = true;
    }
}
