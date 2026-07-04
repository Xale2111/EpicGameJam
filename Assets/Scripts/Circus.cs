using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Circus : MonoBehaviour
{
    [SerializeField] private Transform circusAreaTpPoint;
    [SerializeField] private CinemachineCamera currentCam;
    [SerializeField] CinemachineCamera cam;
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = circusAreaTpPoint.position;
            currentCam.Follow = null;
            currentCam.Priority = 0;
            cam.Priority = 1;
        }
    }
}
