using System.Collections.Generic;

namespace CastleGrimtol.Project
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, Room> Exits { get; set; }

    // public Dictionary<string, Room> ChangeRoom(string name, Room room)
    // {
    //   if (Exits.ContainsKey(name))
    //   {
    //     return;
    //   }
    //   return;
    // }

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