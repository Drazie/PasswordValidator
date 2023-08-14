using System;

string[] lines = { "a 1-5: abcdj", "z 2-4: asfalseiruqwo", "b 3-6: hhkkbjjjb", "f 2-3: ff234sdgh" };
string[] testLines = { "sample data" };

int validPasswords = CountValidPasswords(lines);

Console.WriteLine($"Number of valid passwords: {validPasswords}.");

Console.ReadLine();


//method to run through all lines and detect valid ones
static int CountValidPasswords(string[] lines)
{
    int validPasswords = 0;

    foreach (string line in lines)
    {
        if (IsPasswordValid(line))
        {
            validPasswords++;
        }
    }

    return validPasswords;
}

//summary method to check validity of password in the given line
static bool IsPasswordValid(string line)
{
    (string requirement, string password) = DivideRequirementAndPassword(line);
    (char requiredChar, int min, int max) = SplitRequiredCharAndMinMax(requirement);
    int charCount = CountCharacterOccurrences(password, requiredChar);

    return IsCountWithinRange(charCount, min, max);
}

//splits given line into requirement and password 
static (string, string) DivideRequirementAndPassword(string line)
{
    string[] parts = line.Split(':');
    ExHandles(parts.Length, $"Line incorrect, ':' missing. Check {line}");

    return (parts[0].Trim(), parts[1].Trim());
}

//splits requirement into character and its required range
static (char, int, int) SplitRequiredCharAndMinMax(string requirement)
{
    //requirement = "a -1-20";
    string[] requirementParts = requirement.Split(' ');
    char requiredChar = Char.Parse(requirementParts[0]);

    string[] minMax = requirementParts[1].Split('-');
    ExHandles(minMax.Length, $"Something is wrong with symbol range requirement. Check: {requirement}");

    return (requiredChar, int.Parse(minMax[0]), int.Parse(minMax[1]));
}

//counts required character occurrences in the given password
static int CountCharacterOccurrences(string str, char character)
{
    int count = 0;
    foreach (char c in str)
    {
        if (c == character)
        {
            count++;
        }
    }
    return count;
}

//checks if required character's count is within the required range
static bool IsCountWithinRange(int charCount, int min, int max)
{
    return charCount >= min && charCount <= max;
}

//Basic exception handler to check length of line sub-parts. If true it will warn the user and shut down the process.
//Sorry for magic number 2 confusion. Yes, it is hardcode. Still ex handles putting in work. :)
static void ExHandles(int actualLength, string message)
{
    if (actualLength != 2)
    {
        Console.WriteLine(message);
        Console.ReadLine();
        Environment.Exit(1);
    }
}


