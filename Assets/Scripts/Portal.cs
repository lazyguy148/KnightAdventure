using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private HashSet<GameObject> portalObjects = new HashSet<GameObject>();
    [SerializeField] private Transform destination;
    [SerializeField] private AudioSource teleSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        teleSound.Play();
        if(!collision.CompareTag("Player"))
        { return; }
        if(portalObjects.Contains(collision.gameObject)) { return; }
        if(destination.TryGetComponent(out Portal destinationPortal))
        {
            destinationPortal.portalObjects.Add(collision.gameObject);
        }

        collision.transform.position = destination.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (!collision.CompareTag("Player"))
        { return; }
        portalObjects.Remove(collision.gameObject);
    }
}
