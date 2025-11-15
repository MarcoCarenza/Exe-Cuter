using UnityEngine;
using UnityEngine.Playables;

public class WalkieTalkie : MonoBehaviour
{
    private PlayableDirector pd;
    public PlayableAsset scene1;
    public PlayableAsset scene2;
    public PlayableAsset scene3;

    void Start()
    {
        pd = GetComponent<PlayableDirector>();
    }

    // Call this in an Unity Event or Hard-code it...
    public void PlayScene(int sceneNumber)
    {
        pd.Stop();

        if (sceneNumber == 1)
        {
            pd.Play(scene1);
        }
        if (sceneNumber == 2)
        {
            pd.Play(scene2);
        }
        if (sceneNumber == 3)
        {
            pd.Play(scene3);
        }
    }

    void Update()
    {
        // For testing.
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     PlayScene(1);
        // }
        //  if (Input.GetKeyDown(KeyCode.W))
        // {
        //     PlayScene(2);
        // }
        //  if (Input.GetKeyDown(KeyCode.E))
        // {
        //     PlayScene(3);
        // }
    }
    
}
