using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Drawer : MonoBehaviour
{   
    public AudioClip openSFX;
    public AudioClip closeSFX;


    [SerializeField] private Vector3 moveOffset;
    [SerializeField] private float moveSpeed;
    private AudioSource audioSource;
    private Vector3 startPosition;
    private Coroutine moveCoroutine;
    
    void Start()
    {
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    public void OnHoverEnter()
    {
        Vector3 targetPosition = startPosition + moveOffset;
        StartMove(targetPosition);

        audioSource.clip = openSFX;
        audioSource.Stop();
        audioSource.Play();
    }

    public void OnHoverExit()
    {
        audioSource.clip = closeSFX;
        audioSource.Stop();
        audioSource.Play();
        StartMove(startPosition);
    }

    private void StartMove(Vector3 targetPosition)
    {
        // 如果当前有程序在执行则停止
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        // 执行
        moveCoroutine = StartCoroutine(MoveTo(targetPosition));
    }
    private IEnumerator MoveTo(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            yield return null;
        }
        transform.position = targetPosition;
    }
}
