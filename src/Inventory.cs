using System.Collections.Generic;

class Inventory
{
    private Dictionary<string, Item> items;
    private int maxWeight;

    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        items = new Dictionary<string, Item>();
    }

   public bool Put(Item item)
{
    if (item == null) return false;
    return Put(item.Description, item);
}


public bool Put(string itemName, Item item)
{
    if (item == null || itemName == null)
        return false;

    if (item.Weight > FreeWeight())
        return false;

    items[itemName.ToLower()] = item;
    return true;
}

    public Item Get(string itemName)
{
    if (itemName == null)
        return null;

    string key = itemName.ToLower();

    if (!items.ContainsKey(key))
        return null;

    Item item = items[key];
    items.Remove(key);
    return item;
}



    public int TotalWeight()
    {
        int total = 0;
        foreach (Item item in items.Values)
        {
            total += item.Weight;
        }
        return total;
    }

    public int FreeWeight()
    {
        return maxWeight - TotalWeight();
    }

    public string Show()
    {
        if (items.Count == 0)
            return "Nothing.";

        string result = "";
        foreach (Item item in items.Values)
        {
            result += item + "\n";
        }
        return result;
    }
}
