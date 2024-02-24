using UnityEngine;
using UnityEngine.Events;

public class TriggerArea : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collider> enterEvent, exitEvent;
    private void OnTriggerEnter(Collider other)
    {
        enterEvent?.Invoke(other);
    }
    private void OnTriggerExit(Collider other)
    {
        exitEvent?.Invoke(other);
    }
}