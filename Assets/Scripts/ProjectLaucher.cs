using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectLaucher : MonoBehaviour
{
    [SerializeField] private GameObject bowPrefaps;
    [SerializeField] private GameObject bulletPrefaps;
    [SerializeField] private Transform positionSpawn;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    public void FireProjectTile()
    {
        if(playerController.currentWeaponNo == 0)
        {
            Instantiate(bowPrefaps, positionSpawn.position, positionSpawn.rotation);

        }
        else
        {
            Instantiate(bulletPrefaps, positionSpawn.position, positionSpawn.rotation);

        }

    }
   
}
