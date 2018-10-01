using System;
using System.Collections.Generic;
using System.Threading;

namespace CastleGrimtol.Project
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Locked { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, Room> Exits { get; set; }

    // private Dictionary<string, Room> _LockedRooms { get; set; }

    //     public void AddLockedRoom()
    //     {
    //       // dungeon.Exits.Add("south", easthallway);
    //       Room easthallway = new Room("EAST HALLWAY", @"
    // You find yourself in a small hall there doesnt appear to be anything of interest here.", true);
    //       _LockedRooms.Add("south", easthallway);
    //     }

    public Room ChangeRoom(string name)
    {
      // Room targetroom = Exits[name];
      if (Exits.ContainsKey(name) && this.Locked == false)
      {
        return Exits[name];
      }
      if (Exits.ContainsKey(name) && this.Name == "SOUTH CORRIDOR" && name == "east")
      {
        return Exits[name];
      }
      if (Exits.ContainsKey(name) && this.Locked == true)
      {
        System.Console.WriteLine("");
        Console.WriteLine("Its Locked!");
        return this;
      }
      System.Console.WriteLine("");
      Console.WriteLine("I cant go that way!");
      return this;
    }

    public Item checkforitem(string itemName)
    {
      Item myitem = Items.Find(item => item.Name == itemName);
      if (myitem != null)
      {
        Items.Remove(myitem);
      }
      return myitem;
    }

    // public List<Item> ReturnItem()
    // {
    //   return List<Item>;
    // }
    public Room(string name, string description, bool locked)
    {
      Name = name;
      Description = description;
      Locked = locked;
      Exits = new Dictionary<string, Room>();
      Items = new List<Item>();
    }
  }
}