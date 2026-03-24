class Command
{
    private string commandWord;
    private string secondWord;
    private string thirdWord;

    public string CommandWord { get { return commandWord; } }
    public string SecondWord  { get { return secondWord; } }
    public string ThirdWord   { get { return thirdWord; } }

    public Command(string firstWord, string secondWord, string thirdWord)
    {
        commandWord = firstWord;
        this.secondWord = secondWord;
        this.thirdWord = thirdWord;
    }

    public bool IsUnknown()
    {
        return commandWord == null;
    }

    public bool HasSecondWord()
    {
        return secondWord != null;
    }

    public bool HasThirdWord()
    {
        return thirdWord != null;
    }
}
