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
      Console.ForegroundColor = ConsoleColor.White;
      string input = Console.ReadLine();
      string[] command = input.Split();
      string useraction;
      string usercommand;
      if (input.Length == 0)
      {
        return;
      }
      else if (command.Length == 1)
      {
        useraction = command[0];
        useraction = useraction.ToUpper();
        if (useraction == "HELP")
        {
          Help();
          return;
        }
        if (useraction == "QUIT")
        {
          Quit();
          return;
        }
        if (useraction == "LOOK")
        {
          Look();
          return;
        }
        if (useraction == "INVENTORY")
        {
          Inventory();
          return;
        }
        Console.WriteLine("Not sure about that friend, try a different command.");
      }
      else if (command.Length == 2)
      {
        useraction = command[0];
        usercommand = command[1];
        useraction = useraction.ToUpper();
        usercommand = usercommand.ToLower();
        if (useraction == "GO")
        {
          Go(usercommand);
          return;
        }
        if (useraction == "TAKE")
        {
          TakeItem(usercommand);
          return;
        }
        if (useraction == "USE")
        {
          UseItem(usercommand);
          return;
        }
        Console.WriteLine("Not sure about that friend, try a different command.");
      }
      else
      {
        Console.WriteLine("Not sure about that friend, try a different command.");
      }
    }

    public void Go(string direction)
    {
      CurrentRoom = CurrentRoom.ChangeRoom(direction);
      //       if (CurrentRoom != CurrentRoom.ChangeRoom(direction))
      //       {
      //         Console.WriteLine($@"{CurrentRoom.Name}
      // {CurrentRoom.Description}");
      //       }
    }

    public void Help()
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("");
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
      Console.WriteLine("Press ENTER to exit menu!");
      GetUserInput();
      return;
    }

    public void Inventory()
    {

    }

    public void Look()
    {
      Console.WriteLine(CurrentRoom.Description);
      GetUserInput();
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
      Console.Clear();
      //used dungeon room description generator for this dungeon description www.padnd.com!!!!
      Room dungeon = new Room("DUNGEON", @"
A crack in the ceiling allows a trickle of water to flow down to the floor. The water pools near the base of the wall.. 
Followed by a massive crash in front of you. Your vision is hazy as a the room floods with light. Its a boy! 
You use all your strength to crawl to him.. He's dead, Confused with what is happening you notice something in the kids hand. Its a key.
Exhausted you collapse and see a stream of water running south along the wall past a locked door leading to a hallway.");
      Room easthallway = new Room("EAST HALLWAY", "Youre in the easthallway now");
      Room guardroom = new Room("GUARDROOM", "");
      Room southallway = new Room("SOUTH HALLWAY", "");
      Room southcorridor = new Room("SOUTH CORRIDOR", "");
      Room westpit = new Room("WEST PIT", "");
      Room castlecourtyard = new Room("COURTYARD", "");
      Room northcorridor = new Room("NORTH CORRIDOR", "");
      Room eastpit = new Room("EAST PIT", "");
      Room throneroom = new Room("THRONEROOM", "");
      Item key = new Item("DUNGEON KEY", "");

      //exit doesnt exist until player unlocks door with key from body
      dungeon.Exits.Add("south", easthallway);
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
      Console.Write("Whats your Name?: ");
      string newplayername = Console.ReadLine();
      Player player = new Player(newplayername);
      Console.WriteLine("Thanks for that! lets begin!");
      Console.WriteLine("");
      Console.Write("Type HELP for a quick rundown or type command: ");
      GetUserInput();
      while (playing)
      {
        Console.WriteLine("");
        Console.WriteLine($@"{CurrentRoom.Name}:
{CurrentRoom.Description}");
        GetUserInput();
      }

    }

    public void TakeItem(string itemName)
    {
      // if (CurrentRoom.Items.
    }

    public void UseItem(string itemName)
    {
      throw new System.NotImplementedException();
    }
  }
}