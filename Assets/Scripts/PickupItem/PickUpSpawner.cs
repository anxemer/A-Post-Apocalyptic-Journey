using Assets.Scripts.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefaps;
    public void DropItem()
    {
        Instantiate(itemPrefaps,transform.position,Quaternion.identity);
    }
    
}
