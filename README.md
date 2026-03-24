
# 🧭 World of Zuul – Text Adventure Game (C#)

## 📌 Project Overview
World of Zuul is a console-based text adventure game developed in C#.  
The game is built as part of a Software Development module focused on Object-Oriented Programming (OOP).

In this game, the player explores different rooms, collects items, manages health, and completes objectives to win.

---

## 🎮 Gameplay Description
The player navigates through a world of connected rooms using text commands.

### Core Mechanics:
- Move between rooms (north, south, east, west, up, down)
- Explore environments using `look`
- Pick up and drop items
- Manage inventory with weight limits
- Lose health when moving
- Use items to progress
- Win or lose depending on actions

---

## 🧠 Learning Goals
This project demonstrates:

- Object-Oriented Programming (OOP)
- Class design (Player, Room, Item, Inventory)
- Use of properties and methods
- Collections (Dictionary, List)
- Game loop implementation
- Command parsing system
- Basic game architecture

---

## 🏗️ Project Structure


/Zuul
│
├── Program.cs → Entry point of the game
├── Game.cs → Game logic and loop
├── Room.cs → Room structure and exits
├── Player.cs → Player state (health, inventory)
├── Item.cs → Item definition
├── Inventory.cs → Item storage system
├── Parser.cs → User input handling
├── Command.cs → Command object
├── CommandLibrary.cs → Valid commands
└── Zuul.csproj → Project configuration


---

## ⚙️ Installation & Running

### 1. Install .NET
Download and install the latest .NET SDK:  
https://dotnet.microsoft.com/download

### 2. Open Terminal
Navigate to the project folder where the `.csproj` file is located.

### 3. Run the Game
```bash
dotnet run

The game will start in the console.

🎯 Available Commands
Command	Description
help	Show all commands
go <direction>	Move to another room
look	Show room description
status	Show health and inventory
take <item>	Pick up an item
drop <item>	Drop an item
use <item>	Use an item
quit	Exit the game
🧩 Game Features
Multiple interconnected rooms
Inventory system with weight limit
Health system (player takes damage when moving)
Items that can be collected and used
Command-based interaction
Win and lose conditions
🧱 Technical Concepts Used
Object-Oriented Programming
Classes and objects
Encapsulation (private fields, public methods)
Composition (Player has Inventory)
Collections
Dictionary for storing items
List for managing commands
Game Architecture
Game loop (while loop)
Command pattern
Separation of concerns
🗺️ UML Design

The project is based on UML class diagrams to structure the system design.

Main relationships:

Player → has Inventory
Room → has exits and items
Game → controls flow and player state
🚀 How It Works
The game starts in Program.cs
A Game object is created
The Play() method starts the game loop
The player enters commands
Commands are parsed and executed
The game continues until:
Player dies
Player wins
Player quits
🧪 Example Gameplay
> go north
You are in the hallway.

> look
You see a key.

> take key
Item added to inventory.

> status
Health: 90
Inventory: key

> use key east
Door unlocked!
📈 Possible Improvements
Add enemies or combat system
Add puzzles or locked rooms
Multiplayer support
GUI instead of console
Save/load system
👨‍💻 Author

Raad Al-Wajeeh
Software Development Student
