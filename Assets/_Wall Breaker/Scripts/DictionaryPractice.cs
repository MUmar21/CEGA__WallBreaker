using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DictionaryPractice : MonoBehaviour
{
    List<Inventory> inventory = new List<Inventory>();
    public Dictionary<string, Inventory> inventoryData = new Dictionary<string, Inventory>();

    private void Start()
    {
        Inventory inventoryP1 = new Inventory()
        {
            Name = "HealthPotion",
            Count = 3
        };

        Inventory inventoryP2 = new Inventory()
        {
            Name = "DamagePotion",
            Count = 5
        };

        inventoryData.Add("Player1", inventoryP1);
        inventoryData.Add("Player2", inventoryP2);


        var name = inventoryData.ContainsKey("Player2") ? inventoryData["Player2"].Name : null;
        var count = inventoryData.ContainsKey("Player2") ? inventoryData["Player2"].Count : 0;

        //foreach(var item in inventoryData.Keys)
        //{
        //    if(item == "Player1")
        //    {
        //        var n = inventoryData[item].Name;
        //        var c = inventoryData[item].Count;
        //    }
        //}
    }
}

public struct Inventory 
{
    public string Name;
    public int Count;
}
