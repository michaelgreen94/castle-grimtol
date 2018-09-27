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
      Console.WriteLine(input[1]);
      Console.ForegroundColor = ConsoleColor.White;
      input = input.ToUpper();
      if (input == "HELP")
      {
        Help();
      }
      if (input == "QUIT")
      {
        Quit();
      }
      // if (input == "GO")
      // {
      //   Go();
      // }
      // if (input == "TAKE")
      // {
      //   TakeItem();
      // }
      // if (input == "USE")
      // {
      //   UseItem();
      // }
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
      // Room room = new Room();
      // ChangeRoom();
    }

    public void Help()
    {
      Console.Clear();
      Console.ForegroundColor = ConsoleColor.Green;
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
      GetUserInput();
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
      // Console.BackgroundColor = ConsoleColor.Black;
      // Console.ForegroundColor = ConsoleColor.White;
      Console.Clear();
      //used dungeon room description generator for this dungeon description www.padnd.com!!!!
      Room dungeon = new Room("dungeon", @"
A crack in the ceiling allows a trickle of water to flow down to the floor. The water pools near the base of the wall.. 
Suddenly you hear a massive crash in front of you. Your vision is hazy as a the room floods with light.
Its a boy! You use all your strength to crawl to him.. He's dead, Confused with what is happening you notice something in the kids hand. Its a key.
Exhausted you collapse and see a stream of water running south along the wall and past a locked door into the hall.");
      // Room dungeondoor = new Room("door", "Hmm.. locked and heavy, theres no way youre breaking through it.");
      Room easthallway = new Room("easthallway", "");
      Room guardroom = new Room("guardroom", "");
      Room southallway = new Room("southhallway", "");
      Room southcorridor = new Room("soutcorridor", "");
      Room westpit = new Room("westpit", "");
      Room castlecourtyard = new Room("courtyard", "");
      Room northcorridor = new Room("northcorridor", "");
      Room eastpit = new Room("eastpit", "");
      Room throneroom = new Room("throneroom", "");
      Item key = new Item("dungeon key", "");

      //exit doesnt exist until player unlocks door with key from body
      // dungeon.Exits.Add("south", easthallway);
      easthallway.Exits.Add("north", dungeon);
      easthallway.Exits.Add("south", guardroom);
      guardroom.Exits.Add("north", easthallway);
      guardroom.Exits.Add("west", southallway);
      southallway.Exits.Add("east", guardroom);
      southallway.Exits.Add("west", southcorridor);
      southcorridor.Exits.Add("west", westpit);
      southcorridor.Exits.Add("east", southallway);
      southcorridor.Exits.Add("north", castlecourtyard);
      castlecourtyard.Exits.Add("south", southcorridor);
      castlecourtyard.Exits.Add("north", northcorridor);
      northcorridor.Exits.Add("south", castlecourtyard);
      northcorridor.Exits.Add("east", eastpit);
      northcorridor.Exits.Add("north", throneroom);
      dungeon.Items.Add(key);
      CurrentRoom = dungeon;
    }

    public void StartGame()
    {
      playing = true;
      Setup();
      // Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("Waking with excruciating pain... Darkness, You can't see anything.");
      Thread.Sleep(3000);
      Console.WriteLine("");
      // Console.ForegroundColor = ConsoleColor.White;
      // Console.ResetColor();
      Console.Write("Type HELP for a quick rundown or type command: ");
      string startgameinput = Console.ReadLine();
      if (startgameinput.ToUpper() == "HELP")
      {
        Help();
      }
      while (playing)
      {
        Console.Clear();
        Console.WriteLine($"{CurrentRoom.Name}: {CurrentRoom.Description}");
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