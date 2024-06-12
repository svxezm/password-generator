using System.Text;

namespace PasswordGenerator;

class Program
{
    private static string _password = "";
    private static List<string> _usedPasswords = new List<string>();

    static void Main(string[] args)
    {
        string fileName = "used-passwords.txt";
        _usedPasswords = File.ReadAllLines("./" + fileName).ToList();

        GeneratePassword(20);

        File.WriteAllLines("./" + fileName, _usedPasswords.ToArray());

        Console.WriteLine("Your new password is:");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(_password);
        Console.ResetColor();

    }

    private static void GeneratePassword(int passwordLength = 15)
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
    }
}
