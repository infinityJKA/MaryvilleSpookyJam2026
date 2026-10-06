using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] Door door;

    public int currentCollisions = 0;

    private void Awake()
    {
        currentCollisions = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Interactable") )
        {
            Debug.Log("Pressure plate triggered by: " + other.gameObject.name + " | CurrentCollisions: "+currentCollisions);
            if (currentCollisions == 0) door.OpenDoor();
            currentCollisions += 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Interactable"))
        {

            currentCollisions -= 1;
            Debug.Log("Pressure plate left by: " + other.gameObject.name + " | CurrentCollisions: " + currentCollisions);
            if (currentCollisions == 0) door.CloseDoor();
        }
    }



}
