class Player
{
    public Room CurrentRoom { get; set; }
    public Inventory Backpack { get; }

    private int health;

    public Player(Room startRoom)
    {
        CurrentRoom = startRoom;
        health = 100;
        Backpack = new Inventory(25);
    }

    public void Damage(int amount)
    {
        health -= amount;
        if (health < 0) health = 0;
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > 100) health = 100;
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public int GetHealth()
    {
        return health;
    }

    public bool DropToChest(string itemName)
{
    Item item = Backpack.Get(itemName);

    if (item == null)
    {
        Console.WriteLine("You don't have that item.");
        return false;
    }

    bool ok = CurrentRoom.Chest.Put(item);

    if (!ok)
    {
        Backpack.Put(item);
        Console.WriteLine("You can't drop that item here.");
        return false;
    }

    Console.WriteLine($"You dropped the {item.Description}.");

    return true;
}

public string Use(string itemName)
{
    if (itemName == null)
        return "Use what?";

    Item item = Backpack.Get(itemName);
    if (item == null)
        return "You don't have that item.";

    string desc = item.Description.ToLower();

    if (desc == "medkit")
    {
        Heal(20);
        return $"You used the {item.Description} and healed 20 points.";
    }

    Backpack.Put(item);
    return "You can't use that.";
}

public bool TakeFromChest(string itemName)
{
    if (itemName == null)
    {
        Console.WriteLine("Take what?");
        return false;
    }

    Item item = CurrentRoom.Chest.Get(itemName);

    if (item == null)
    {
        Console.WriteLine("That item is not in this room.");
        return false;
    }

    if (!Backpack.Put(item))
    {
        CurrentRoom.Chest.Put(item);
        Console.WriteLine("That item is too heavy.");
        return false;
    }

    Console.WriteLine($"You picked up the {item.Description}.");
    return true;
}







}
