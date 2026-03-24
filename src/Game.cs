using System;

class Game
{
    private Parser parser;
    private Player player;
    private Room startRoom;

    private bool roofUnlocked = false;

    private Room elevatorRoom;


    private bool finished;
    public Game()
    {
        parser = new Parser();
        CreateRooms();
    }


    private void CreateRooms()
    {
        // Floor 1
        Room outside = new Room("outside the main entrance of the university");
        Room storage = new Room("in a storage room (old boxes everywhere)");

        // Floor 2
        Room securityRoom = new Room("in a security room");
        Room armory = new Room("in an armory (you might find a weapon)");

        // Floor 3
        Room medicalRoom = new Room("in a medical room (supplies are still here)");
        Room brokenFloor = new Room("on a broken floor (one step could kill you)");

        // Floor 4
        Room controlRoom = new Room("in the control room (a strong guard blocks the way)");

        // Floor 5
        Room roof = new Room("on the roof (freedom is close)");
        elevatorRoom = new Room("in the elevator (it feels unstable)");



        // ===== Exits =====

        // Floor 1
        outside.AddExit("north", storage);
        outside.AddExit("up", securityRoom);

        storage.AddExit("south", outside);

        // Floor 2
        securityRoom.AddExit("north", armory);
        securityRoom.AddExit("down", outside);
        securityRoom.AddExit("up", medicalRoom);

        armory.AddExit("south", securityRoom);

        // Floor 3
        medicalRoom.AddExit("north", brokenFloor);
        medicalRoom.AddExit("down", securityRoom);
        medicalRoom.AddExit("up", controlRoom);

        brokenFloor.AddExit("south", medicalRoom);

        // Floor 4
        controlRoom.AddExit("down", medicalRoom);
        controlRoom.AddExit("up", roof);
        controlRoom.AddExit("east", elevatorRoom);
        elevatorRoom.AddExit("west", controlRoom);


        // Floor 5
        roof.AddExit("down", controlRoom);

        // ===== Items =====
        outside.Chest.Put(new Item(2, "Key"));
        armory.Chest.Put(new Item(5, "Weapon"));
        medicalRoom.Chest.Put(new Item(5, "Medkit"));
        controlRoom.Chest.Put(new Item(3, "Treasure"));


        // Start location
        // Start location
        startRoom = outside;
        player = new Player(startRoom);
        roofUnlocked = false;


    }


    public void Play()
    {
        bool playAgain = true;

        while (playAgain)
        {
            finished = false;

            PrintWelcome();

            while (!finished)
            {
                Command command = parser.GetCommand();
                ProcessCommand(command);
            }

            playAgain = AskPlayAgain();
        }

        Console.WriteLine("Goodbye!");
    }


    private void PrintWelcome()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("        WELCOME TO ZUUL TOWER");
        Console.WriteLine("========================================");
        Console.WriteLine();
        Console.WriteLine("You are trapped inside a mysterious 5-floor building.");
        Console.WriteLine("Your goal is to reach the roof and escape.");
        Console.WriteLine();
        Console.WriteLine("Be careful:");
        Console.WriteLine("- Some rooms contain enemies.");
        Console.WriteLine("- You will lose health when moving.");
        Console.WriteLine();
        Console.WriteLine("To win:");
        Console.WriteLine("Find useful items, survive the dangers,");
        Console.WriteLine("and unlock the way to the roof.");
        Console.WriteLine();
        Console.WriteLine("Commands:");
        Console.WriteLine("go <direction>");
        Console.WriteLine("take <item>");
        Console.WriteLine("drop <item>");
        Console.WriteLine("use <item>");
        Console.WriteLine("status");
        Console.WriteLine("help");
        Console.WriteLine("quit");
        Console.WriteLine();
        Console.WriteLine("Good luck...");
        Console.WriteLine();

        Console.WriteLine(player.CurrentRoom.GetLongDescription());
    }


    private void ProcessCommand(Command command)
    {
        if (command.IsUnknown())
        {
            // إذا السطر فاضي (CommandWord = "")
            if (command.CommandWord == "")
            {
                return;
            }

            // غير ذلك: أمر غير معروف فعلاً
            Console.WriteLine("I don't know what you mean...");
            return;
        }



        switch (command.CommandWord)
        {
            case "help":
                PrintHelp();
                break;
            case "go":
                GoRoom(command);
                break;
            case "look":
                Console.WriteLine(player.CurrentRoom.GetLongDescription());
                break;
            case "take":
                Take(command);
                break;
            case "drop":
                Drop(command);
                break;
            case "use":
                Use(command);
                break;

            case "status":
                Console.WriteLine($"Health: {player.GetHealth()}");
                Console.WriteLine("Backpack: " + player.Backpack.Show());
                break;

            case "quit":
                finished = true;
                break;
        }

    }
    private void PrintHelp()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("HOW TO PLAY");
        Console.WriteLine("========================================");
        Console.WriteLine("Move using: go up / go down / go north / go south / go east / go west");
        Console.WriteLine("Pick items: take <item>");
        Console.WriteLine("Drop items: drop <item>");
        Console.WriteLine(" Use items: use <item> (e.g., use medkit or use key up)");
        Console.WriteLine("Check health and inventory: status");
        Console.WriteLine();
        Console.WriteLine("Your goal is to reach the roof and escape.");
        Console.WriteLine("Be careful of traps and enemies!");
    }


    private bool AskPlayAgain()
    {
        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.WriteLine("1) Play again");
        Console.WriteLine("2) Quit");
        Console.WriteLine("========================================");
        Console.Write("Choose: ");

        string choice = Console.ReadLine().Trim();

        if (choice == "1")
        {
            // إعادة تهيئة اللعبة بالكامل (الغرف + اللاعب + العناصر)
            CreateRooms();
            return true;
        }

        return false;
    }

    private void Take(Command command)
    {
        if (!command.HasSecondWord())
        {
            Console.WriteLine("Take what?");
            return;
        }

        if (command.HasThirdWord())
        {
            Console.WriteLine("Take only takes two words.");
            return;
        }

        player.TakeFromChest(command.SecondWord);
    }

    private void Drop(Command command)
    {
        if (!command.HasSecondWord())
        {
            Console.WriteLine("Drop what?");
            return;
        }

        if (command.HasThirdWord())
        {
            Console.WriteLine("Drop only takes two words.");
            return;
        }

        player.DropToChest(command.SecondWord);
    }


    private void Use(Command command)
    {
        if (!command.HasSecondWord())
        {
            Console.WriteLine("Use what?");
            return;
        }

        // ✅ Third word support: use key up
        if (command.HasThirdWord())
        {
            string item = command.SecondWord.ToLower();
            string target = command.ThirdWord.ToLower();

            if (item == "key" && target == "up")
            {
                // only works in control room and only if player has the key
                if (!player.CurrentRoom.GetShortDescription().Contains("control room"))
                {
                    Console.WriteLine("You can't use the key like that here.");
                    return;
                }

                Item key = player.Backpack.Get("key");
                if (key == null)
                {
                    Console.WriteLine("You need the Key first.");
                    return;
                }

                // put it back (key is not consumed)
                player.Backpack.Put(key);

                if (!roofUnlocked)
                {
                    roofUnlocked = true;
                    Console.WriteLine("You unlocked the door to the roof!");
                    return;
                }

                Console.WriteLine("The roof door is already unlocked.");
                return;
            }

            Console.WriteLine("You can't use that.");
            return;
        }

        // normal 2-word use (e.g., use medkit)
        Console.WriteLine(player.Use(command.SecondWord));
    }



    private void GoRoom(Command command)
    {
        if (!command.HasSecondWord())
        {
            Console.WriteLine("Go where?");
            return;
        }

        if (command.HasThirdWord())
        {
            Console.WriteLine("Go only takes two words.");
            return;
        }

        string direction = command.SecondWord.ToLower();

        // Lock the roof: from control room you need to unlock "up" first
        if (player.CurrentRoom.GetShortDescription().Contains("control room")
            && direction == "up"
            && !roofUnlocked)
        {
            Console.WriteLine("The door to the roof is locked. Maybe a key can open it? (Hint: use key up)");
            return;
        }

        Room nextRoom = player.CurrentRoom.GetExit(direction);

        if (nextRoom == null)
        {
            Console.WriteLine("There is no door to " + direction + "!");
            return;
        }

        // Move
        player.CurrentRoom = nextRoom;
        Console.WriteLine(player.CurrentRoom.GetLongDescription());

        // =========================
        // 1) EVENTS THAT END/INTERRUPT IMMEDIATELY
        // =========================

        // Elevator trap: instantly sends you back to the start (no tiredness)
        if (player.CurrentRoom == elevatorRoom)
        {
            Console.WriteLine("The elevator suddenly drops! You are thrown back to the ground floor...");
            player.CurrentRoom = startRoom;
            Console.WriteLine(player.CurrentRoom.GetLongDescription());
            return;
        }

        // Deadly pit: instant game over (no tiredness)
        if (player.CurrentRoom.GetShortDescription().Contains("broken floor"))
        {
            Console.WriteLine("You fall into a deadly pit. Game over!");
            finished = true;
            return;
        }

        // Control room: strong guard check (may end game)
        if (player.CurrentRoom.GetShortDescription().Contains("control room"))
        {
            Console.WriteLine("Hint: You are very close to winning! You can go up, or explore east... but be careful.");

            Item w = player.Backpack.Get("weapon");
            if (w != null)
            {
                player.Backpack.Put(w);
                Console.WriteLine("You defeat the strong guard with your weapon!");
            }
            else
            {
                Console.WriteLine("A strong guard kills you instantly. Game over!");
                finished = true;
                return;
            }
        }

        // Victory on roof: end game immediately (no tiredness after win)
        if (player.CurrentRoom.GetShortDescription().Contains("roof"))
        {
            Console.WriteLine("You reached the roof. You have Won!");
            finished = true;
            return;
        }

        // =========================
        // 2) NON-FINAL EVENTS
        // =========================

        // Weak enemy in security room
        if (player.CurrentRoom.GetShortDescription().Contains("security room"))
        {
            Item w = player.Backpack.Get("weapon");
            if (w != null)
            {
                player.Backpack.Put(w);
            }
            else
            {
                player.Damage(20);
                Console.WriteLine("A weak robot attacks you! (-20 HP)");
            }
        }

        // =========================
        // 3) TIREDNESS ALWAYS LAST
        // =========================
        player.Damage(5);
        Console.WriteLine("You feel tired from moving. (-5 HP)");

        if (!player.IsAlive())
        {
            Console.WriteLine("You have died. Game over!");
            finished = true;
            return;
        }
    }

}