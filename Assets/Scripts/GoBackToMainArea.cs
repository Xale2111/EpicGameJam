using Unity.Cinemachine;
using UnityEngine;

public class GoBackToMainArea : MonoBehaviour
{
    [SerializeField] private Transform mainAreaTp;
    [SerializeField] private CinemachineCamera currentCam;
    [SerializeField] CinemachineCamera cam;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = mainAreaTp.position;
            cam.Follow = other.transform;
            currentCam.Priority = 0;
            cam.Priority = 1;
        }
    }
}
