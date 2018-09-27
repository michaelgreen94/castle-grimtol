using System;
using System.Collections.Generic;
using System.Threading;

namespace CastleGrimtol.Project
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, Room> Exits { get; set; }

    public Room ChangeRoom(string name)
    {
      if (Exits.ContainsKey(name))
      {
        return Exits[name];
      }
      Console.WriteLine("I cant go that way!");
      return this;
    }

    // public List<Item> ReturnItem()
    // {
    //   return List<Item>;
    // }
    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      Exits = new Dictionary<string, Room>();
      Items = new List<Item>();
    }
  }
}