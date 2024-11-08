using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct InventoryItem
{
    public int id;
    public int count;
}

[System.Serializable]
public class GameData
{
    // Le transform du personnage ou sinon lui laisser un spawn par defaut => a definir


    // Inventory Player
    //public string COUNT;
    //public string IDITEM;

    public List<InventoryItem> inventoryItems;
   

    // MinerData
    //public float MINING_SPEED;
    //public int MINING_RANGE;


    // Inventory Miner
    // Objet 
    // Son slot
    // Sa quantité


    // Transform Miner
    // position


    // Transform Resources
    // position


    // ConvoyorData
    //public float BELT_SPEED;
    

    // Transform Conveyor
    // position départ
    // position arrivée


    // Transform Waypoints Conveyor
    // position
}
