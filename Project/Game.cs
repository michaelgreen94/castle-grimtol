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
      //       CurrentRoom = CurrentRoom.ChangeRoom(direction);
      //       Console.WriteLine($@"{CurrentRoom.Name}
      // {CurrentRoom.Description}");
      //returns "" if exit doesnt exist

      if (CurrentRoom != CurrentRoom.ChangeRoom(direction))
      {
        CurrentRoom = CurrentRoom.ChangeRoom(direction);
        Console.WriteLine($@"{CurrentRoom.Name}
{CurrentRoom.Description}");
      }
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
      System.Console.WriteLine("");
      Console.WriteLine($@"{CurrentRoom.Name}
{CurrentRoom.Description}");

      // GetUserInput();
    }

    public void Quit()
    {
      //quits the game
      Console.WriteLine("Are you sure you want to quit? (Y/N)");
      string newgame = Console.ReadLine();
      string Qgame = newgame.ToUpper();
      if (Qgame == "N")
      {
        return;
      }
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
Suddenly.. Your vision, hazy as a the room floods with light. Something falls through the ceiling, landing in front of you! 
Using all your strength to crawl to it.. Confused with what is happening you notice something shiny. Its a key.
Exhausted you collapse, through squinted eyes you notice a stream of water running south along the wall under a massive door.", true);
      Room easthallway = new Room("EAST HALLWAY", @"
You find yourself in a small hall there doesnt appear to be anything of interest here, a light is shining faintly at the end of the hall..", false);
      Room guardroom = new Room("GUARDROOM", @"
You smelled smoke as you moved down the hall, and rounding the corner into this room you see why. Every surface bears scorch marks and ash piles on the floor. 
The room reeks of fire and burnt flesh. Either a great battle happened here or the room bears some fire danger you cannot see for only one torch lights the room.", false);
      Room southallway = new Room("SOUTH HALLWAY", @"
The ground shattered underneath you, you wonder what could have caused this much damage. Where is everyone?", false);
      Room southcorridor = new Room("SOUTH CORRIDOR", @"
Rusting spikes line the walls and ceiling of this chamber. The dusty floor shows no sign that the walls move over it, but you can see the skeleton of some humanoid impaled on some wall spikes nearby.", true);
      Room westpit = new Room("WEST PIT", @"
Doesnt need a description but ill give you one. Youre dead, falling into a pit you are impaled by remenants of an old trap.", true);
      Room castlecourtyard = new Room("COURTYARD", @"
You step into the large castle courtyard there is a flowing fountain in the middle of the grounds. Around you, piles of ash being sifted into the air make it hard to breath.", false);
      Room northcorridor = new Room("NORTH CORRIDOR", @"
You hear something rummage in a room near you. Youve seen this place before. It cant be? could it?", false);
      Room eastpit = new Room("EAST PIT", @"
There's a hiss as you open this door, and you smell a sour odor, like something rotten or fermented. Inside you see a small room lined with dusty shelves, crates, and barrels.
It looks like someone once used this place as a larder, a large figure grabs and throws you forward. You suddenly stop falling and quit feeling pain..", false);
      Room throneroom = new Room("THRONEROOM", @"
As you unlock the door and swing it wide you see an enormous hall stretching out before you. At the opposite end of the hall sitting on the throne you see a being engulfed in flames.
Strangley it looks like you. Your alarm clock wakes you, that felt too real. You get up and get ready for work!", false);
      Item key = new Item("key", "Unlocking the Door to the Dungeon, A flurry of bats suddenly flaps through the doorway, their screeching barely audible as they careen past your heads. They flap past you into the rooms and halls beyond. They seem to have come from the hole in the ceiling..");
      Item torch = new Item("torch", "The room shakes, something is happening. Lighting up the room, the spikes retract unveiling a clear path. To the west you see a pit that will surly kill you, and to the north its the courtyard.");
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
      guardroom.Items.Add(torch);
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
      Look();
      while (playing)
      {
        if (CurrentRoom.Name == "THRONEROOM")
        {
          // System.Console.WriteLine(CurrentRoom.Description);
          // Thread.Sleep(3000);
          playing = false;
          Reset();
          return;
        }
        if (CurrentRoom.Name == "WEST PIT" || CurrentRoom.Name == "EAST PIT")
        {
          playing = false;
          // System.Console.WriteLine(CurrentRoom.Description);
          // Thread.Sleep(3000);
          Console.WriteLine("You Died!");
          Reset();
          return;
        }
        Console.WriteLine("");
        // Console.WriteLine($"{CurrentRoom.Name}");
        System.Console.Write("What would you like to do?: ");
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
        System.Console.WriteLine("");
        Console.WriteLine($"Youve added {lootableitem.Name} to your inventory");
        return;
      }
      //if the room doesnt have the item return "" saying so.
      System.Console.WriteLine("");
      Console.WriteLine("That item doesnt exist");
      return;
    }


    public void UseItem(string itemName)
    {
      var usableitem = CurrentPlayer.checkusableitem(itemName);
      //if the user has itemName in its inventory user can (use "itemName")
      if (usableitem == null)
      {
        System.Console.WriteLine("");
        Console.WriteLine("You dont have that item in your inventory");
        return;
      }
      else if (CurrentPlayer.Inventory.Contains(usableitem) && CurrentRoom.Name.ToUpper() == "SOUTH CORRIDOR")
      {
        CurrentRoom.Locked = false;
        CurrentPlayer.Inventory.Remove(usableitem);
        System.Console.WriteLine("");
        Console.WriteLine(usableitem.Description);
        return;
      }
      else if (CurrentPlayer.Inventory.Contains(usableitem) && usableitem.Name != "torch")
      {
        // removes item from users inventory
        CurrentRoom.Locked = false;
        CurrentPlayer.Inventory.Remove(usableitem);
        Console.WriteLine(usableitem.Description);
        // Thread.Sleep(3000);
        return;
      }
      //if the user doesnt have the item return "" saying so.
      else if (usableitem.Name == "torch")
      {
        System.Console.WriteLine("");
        System.Console.WriteLine("You dont need a torch here!");
        return;
      }
      System.Console.WriteLine("");
      Console.WriteLine("You dont have that item in your inventory");
      return;
    }
  }
}