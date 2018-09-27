using System.Collections.Generic;

namespace CastleGrimtol.Project
{
  public class Player : IPlayer
  {
    public string PlayerName { get; set; }
    public List<Item> Inventory { get; set; }

    public void takeitem(Item item)
    {
      Inventory.Add(item);
    }

    public Player(string playername)
    {
      PlayerName = playername;
      Inventory = new List<Item>();
    }
  }
}