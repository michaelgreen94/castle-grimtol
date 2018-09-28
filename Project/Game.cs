using System;
using System.Collections.Generic;
using System.Linq;
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
      //list of accepted commands the game will accept from the user.
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
      //changes the user from one room to another if the exit to the other room exists
      CurrentRoom = CurrentRoom.ChangeRoom(direction);
      //returns "" if exit doesnt exist

      //       if (CurrentRoom != CurrentRoom.ChangeRoom(direction))
      //       {
      //         Console.WriteLine($@"{CurrentRoom.Name}
      // {CurrentRoom.Description}");
      //       }
    }

    public void Help()
    {
      //help menu for the user to give a list of commands
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
      if (CurrentPlayer.Inventory.Count == 0)
      {
        Console.WriteLine("Your inventory is empty");
        return;
      }
      foreach (Item inventoryitem in CurrentPlayer.Inventory)
      {
        if (inventoryitem != null)
        {
          Console.WriteLine(inventoryitem.Name);
          return;
        }
      }
    }

    public void Look()
    {
      //gives user current description of the room they are in
      Console.WriteLine(CurrentRoom.Description);
      GetUserInput();
    }

    public void Quit()
    {
      //quits the game
      playing = false;
    }

    public void Reset()
    {
      //resets game state? starts new game?
      Console.WriteLine("Would you like to play again? (Y/N)");
      string newgame = Console.ReadLine();
      string Ngame = newgame.ToUpper();
      if (Ngame == "Y")
      {
        StartGame();
      }
      return;
    }

    public void Setup()
    {
      //creates rooms and items for the game

      Console.Clear();
      //used dungeon room description generator for this dungeon description www.padnd.com!!!!
      Room dungeon = new Room("DUNGEON", @"
A crack in the ceiling allows a trickle of water to flow down to the floor. The water pools near the base of the wall.. 
Followed by a massive crash in front of you. Your vision is hazy as a the room floods with light. Its a boy! 
You use all your strength to crawl to him.. He's dead, Confused with what is happening you notice something in the kids hand. Its a key.
Exhausted you collapse and see a stream of water running south along the wall past a locked door leading to a hallway.");
      Room easthallway = new Room("EAST HALLWAY", @"
You find yourself in a small hall there doesnt appear to be anything of interest here.");
      Room guardroom = new Room("GUARDROOM", @"
You see a room with several sleeping guards, The room smells of sweaty men. The bed closest to you is empty and there are several uniforms tossed about.");
      Room southallway = new Room("SOUTH HALLWAY", @"
You find yourself in a small hall there doesnt appear to be anything of interest here.");
      Room southcorridor = new Room("SOUTH CORRIDOR", @"
The first real choice youve come across.. do you go North to the courtyard or west breaking through a door.");
      Room westpit = new Room("WEST PIT", @"
Doesnt need a description but ill give you one. Youre dead.");
      Room castlecourtyard = new Room("COURTYARD", @"
You step into the large castle courtyard there is a flowing fountain in the middle of the grounds and a few guards patrolling the area.");
      Room northcorridor = new Room("NORTH CORRIDOR", @"
Another one of these choices, this ones up to you, no hints. North or East?");
      Room eastpit = new Room("EAST PIT", @"
Doesnt need a description but ill give you one. Youre dead.");
      Room throneroom = new Room("THRONEROOM", @"
As you unlock the door and swing it wide you see an enormous hall stretching out before you. At the opposite end of the hall sitting on his throne you see the dark lord.
You Won!");
      Item key = new Item("key", "Unlocks the Door to the Dungeon");

      //Exits for rooms the player will be in

      dungeon.Exits.Add("south", easthallway);
      //exit for first room doesnt exist until player unlocks door with key from body
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

      Console.Write("Whats your Name?: ");
      string newplayername = Console.ReadLine();
      CurrentPlayer = new Player(newplayername);
      Console.WriteLine("Thanks for that! lets begin!");
      Console.WriteLine("");
    }

    public void StartGame()
    {
      playing = true;
      Setup();
      Console.Write("Type HELP for a quick rundown or type command: ");
      GetUserInput();
      while (playing)
      {
        if (CurrentRoom.Name == "THRONEROOM")
        {
          playing = false;
          Reset();
          return;
        }
        if (CurrentRoom.Name == "WEST PIT" || CurrentRoom.Name == "EAST PIT")
        {
          playing = false;
          Console.WriteLine("You Died!");
          Reset();
          return;
        }
        Console.WriteLine("");
        Console.WriteLine($@"{CurrentRoom.Name}:
{CurrentRoom.Description}");
        GetUserInput();
      }

    }

    public void TakeItem(string itemName)
    {
      //if the room contains the item (take "itemName") should add item to inventory.
      var lootableitem = CurrentRoom.checkforitem(itemName);

      //run player method that adds item to inventory
      if (lootableitem != null)
      {
        CurrentPlayer.Inventory.Add(lootableitem);
        return;
      }
      //if the room doesnt have the item return "" saying so.
      Console.WriteLine("That item doesnt exist");
      return;
    }


    public void UseItem(string itemName)
    {
      var usableitem = CurrentPlayer.checkusableitem(itemName);
      //if the user has itemName in its inventory user can (use "itemName")
      if (CurrentPlayer.Inventory.Contains(usableitem))
      {
        // removes item from users inventory
        CurrentPlayer.Inventory.Remove(usableitem);
        return;
      }
      //if the user doesnt have the item return "" saying so.
      Console.WriteLine("You dont have that item in your inventory");
    }
  }
}