using UnityEngine;

public class FollowDrawer : MonoBehaviour
{
    public GameObject box;
    [SerializeField] private Vector3 moveOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = box.transform.position + moveOffset;
    }
}
