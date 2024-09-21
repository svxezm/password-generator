using System.Text;

namespace PasswordGenerator;

class Program
{
    private static string _password = "";
    private static List<string> _usedPasswords = new List<string>();

    static void Main(string[] args)
    {
        bool isPassword = false;
        bool isClear = false;
        bool isList = false;

        string fileName = "used-passwords.txt";
        _usedPasswords = File.ReadAllLines("./" + fileName).ToList();

        StringBuilder helpMessage = new StringBuilder();

        helpMessage.AppendLine("Arguments:");
        helpMessage.AppendFormat("  --length, -L {0} Define password length.\n", "\t\t\t\t");
        helpMessage.AppendFormat("  --clear, -C {0} Clear all saved passwords.\n", "\t\t\t\t");
        helpMessage.AppendFormat("  --list, -l {0} List all saved passwords.\n", "\t\t\t\t");
        helpMessage.AppendFormat("  -H {0} Prints this help message.", "\t\t\t\t\t");

        switch(args[0]) {
            case "--length":
            case "-L":
                isPassword = true;
                break;
            case "--clear":
            case "-C":
                isClear = true;
                break;
            case "--list":
            case "-l":
                isList = true;
                break;
            case "-H":
                Console.WriteLine(helpMessage);
                break;
            default:
                Console.WriteLine(helpMessage);
                break;
        };

        if (isPassword && int.TryParse(args[1], out int length))
        {
            GeneratePassword(length, fileName);
        }
        else if (isClear)
        {
            ClearPasswords(fileName);
        }
        else if (isList)
        {
            ListPasswords(fileName);
        }
    }

    private static void GeneratePassword(int passwordLength, string fileName)
    {
        StringBuilder password = new StringBuilder("", passwordLength);
        string characters = "qwfpgjluyarstdhneiozxcvbkmQWFPGJUYARSTDHNEIOZXC" +
            "VBKMçÇ@$#%^&*()_!+=-1234567890áàâäãúüöóõíéñÄÃÂÀÁÚÜÖÓÕÍÉÑ`€";

        Random random = new Random();

        for (int i = 0; i < passwordLength; i++)
        {
            int characterIndex = random.Next(0, characters.Length);
            password.Append(characters[characterIndex]);
        }
        
        _password = password.ToString();
        _usedPasswords.Add(password.ToString());

        File.WriteAllLines("./" + fileName, _usedPasswords.ToArray());

        Console.WriteLine("Your new password is:");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(_password);
        Console.ResetColor();
    }
    
    private static void ClearPasswords(string fileName)
    {
        string[] emptyContent = ["Your passwords:\n"];
        File.WriteAllLines("./" + fileName, emptyContent);
        Console.WriteLine("Successfully cleared passwords.");
    }

    private static void ListPasswords(string fileName)
    {
        foreach (string line in _usedPasswords)
        {
            Console.WriteLine(line);
        }
    }
}
