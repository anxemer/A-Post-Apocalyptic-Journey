using Assets.Scripts.InteractorSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promt;
    [SerializeField] private PickUpSpawner spawner;
    public string InteractionPromt => _promt;

    public bool Interact(Interactor interactor)
    {
        spawner.DropItem();
        Debug.Log("Opening Chest");
        return true;
    }
    public void DestroyChest(Interactor interactor)
    {
        Destroy(gameObject);
    }


}
