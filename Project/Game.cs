using System;
using System.Collections.Generic;
using System.Threading;

namespace CastleGrimtol.Project
{
  public class Game : IGame
  {
    public Room CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }

    bool playing;

    public void GetUserInput()
    {
      string input = Console.ReadLine();
      input = input.ToUpper();
      if (input == "HELP")
      {
        Help();
      }
      if (input == "QUIT")
      {
        Quit();
      }
      if (input == "GO")
      {
        Go();
      }
      if (input == "TAKE")
      {
        TakeItem();
      }
      if (input == "USE")
      {
        UseItem();
      }
      if (input == "LOOK")
      {
        Look();
      }
      if (input == "INVENTORY")
      {
        Inventory();
      }
    }

    public void Go(string direction)
    {
      throw new System.NotImplementedException();
    }

    public void Help()
    {
      Console.ForegroundColor = ConsoleColor.DarkGreen;
      Console.WriteLine("Smart Choice friend.. Lets take a look");
      Console.WriteLine(@"
      -Go <Direction> Moves the player from room to room
      -Use <ItemName> Uses an item in a room or from your inventory
      -Take <ItemName> Places an item into the player inventory and removes it from the room
      -Look Prints the description of the room again
      -Inventory Prints the players inventory
      -Help Shows a list of commands and actions
      -Quit Quits the Game
      ");
      Console.WriteLine("Press anything to exit help menu!");
      Console.ReadLine();
      Setup();
      return;
    }

    public void Inventory()
    {
      throw new System.NotImplementedException();
    }

    public void Look()
    {
      throw new System.NotImplementedException();
    }

    public void Quit()
    {
      playing = false;
    }

    public void Reset()
    {
      throw new System.NotImplementedException();
    }

    public void Setup()
    {
      Console.BackgroundColor = ConsoleColor.Black;
      Console.ForegroundColor = ConsoleColor.White;
      Console.Clear();
      //used dungeon room description generator for this dungeon description www.padnd.com!!!!
      Room dungeon = new Room("dungeon", "A crack in the ceiling above the middle of the north wall allows a trickle of water to flow down to the floor. The water pools near the base of the wall, and a rivulet runs along the wall an out into the hall. The water smells fresh.");
      Room dungeondoor = new Room("door", "Hmm.. locked and heavy, theres no way youre breaking through it.");
      Room dtoghallway = new Room("hallway", "");
      Room guardroom = new Room();
      Room gtoi1hallway = new Room();
      Room intersection1 = new Room();
      Room hole1 = new Room();
      Room castlecourtyard = new Room();
      Room castlecourtyarddoor = new Room();
      Room intersection2 = new Room();
      Room hole2 = new Room();
      Room throneroom = new Room();
      Item key = new Item();
      dungeon.Exits.Add("south", dungeondoor);
      dungeondoor.Exits.Add("north", dungeon);
      dungeondoor.Exits.Add("south", dtoghallway);
      dtoghallway.Exits.Add("north", dungeon);
      dtoghallway.Exits.Add("south", guardroom);
      guardroom.Exits.Add("north", dtoghallway);
      guardroom.Exits.Add("west", gtoi1hallway);
      gtoi1hallway.Exits.Add("east", guardroom);
      gtoi1hallway.Exits.Add("west", intersection1);
      intersection1.Exits.Add("west", hole1);
      intersection1.Exits.Add("east", gtoi1hallway);
      intersection1.Exits.Add("north", castlecourtyard);
      castlecourtyard.Exits.Add("south", intersection1);
      castlecourtyard.Exits.Add("west", castlecourtyarddoor);
      castlecourtyarddoor.Exits.Add("east", castlecourtyard);
      castlecourtyard.Exits.Add("north", intersection2);
      intersection2.Exits.Add("south", castlecourtyard);
      intersection2.Exits.Add("east", hole2);
      intersection2.Exits.Add("north", throneroom);
      dungeon.Items.Add(key);
      CurrentRoom = dungeon;
    }

    public void StartGame()
    {
      playing = true;
      Setup();
      Console.ForegroundColor = ConsoleColor.DarkRed;
      Console.WriteLine("Waking with excruciating pain... Darkness, nothing but darkness.");
      Thread.Sleep(3000);
      Console.WriteLine("");
      Console.ForegroundColor = ConsoleColor.White;
      // Console.ResetColor();
      Console.Write("Type Help to get a quick rundown on commands: ");
      string startgameinput = Console.ReadLine();
      if (startgameinput.ToUpper() == "HELP")
      {
        Help();
      }
      while (playing)
      {
        Console.WriteLine("Whats next?");
        GetUserInput();
      }

    }

    public void TakeItem(string itemName)
    {
      throw new System.NotImplementedException();
    }

    public void UseItem(string itemName)
    {
      throw new System.NotImplementedException();
    }
  }
}