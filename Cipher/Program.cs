using Terminal.Gui;
// A cipher app
namespace Cipher
{
    class Cipher
    {
        static string[] Characters =
            {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "ä", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "ö", "p", "q", "r", "s", "t", "u", "ü", "v", "w", "x", "y", "z", "A", "Ä", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "Ö", "P", "Q", "R", "S", "T", "U", "Ü", "V", "W", "X", "Y", "Z", ",", ".", ";", ":", "\"", "!", "?", " "};
        static int CharLen = Characters.Length;

        static string encrypt(string plain, string key)
        {
            string cipher = "";

            for (int i = 0; i < plain.Length; i++)
            {
                char plainChar = plain[i];
                char keyChar = key[i % key.Length];

                /*Console.WriteLine($"Plain Char: {plainChar}\nPlain Key: {keyChar}\n");*/

                int plainIndex = Array.IndexOf(Characters, plainChar.ToString());
                int keyIndex = Array.IndexOf(Characters, keyChar.ToString());

                /*Console.WriteLine($"Plain Index: {plainIndex}\nKey Index: {keyIndex}\n");*/

                if (plainIndex == -1 || keyIndex == -1)
                {
                    throw new ArgumentException("All characters from the input strings must be in the char array.");
                }

                int cipherIndex = (plainIndex + keyIndex) % CharLen;

                cipher += Characters[cipherIndex];
            }

            return cipher;
        }

        static string decrypt(string cipher, string key)
        {
            string plain = "";

            for (int i = 0; i < cipher.Length; i++)
            {
                char chiperChar = cipher[i];
                char keyChar = key[i % key.Length];

                int chiperIndex = Array.IndexOf(Characters, chiperChar.ToString());
                int keyIndex = Array.IndexOf(Characters, keyChar.ToString());

                if (chiperIndex == -1 || keyIndex == -1)
                {
                    throw new ArgumentException("All characters from the input strings must be in the char array.");
                }

                int plainIndex = (chiperIndex - keyIndex) % CharLen;

                if (plainIndex < 0)
                {
                    plainIndex += CharLen;
                }

                plain += Characters[plainIndex];
            }

            return plain;
        }

        public static void Main()
        {
            /*Application.Init();*/
            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_File", new MenuItem [] {
                    new MenuItem ("_Quit", "", () => {
                        Application.RequestStop ();
                    })
                }),
            });

            string plain = "This is a longer text! With stuff.";
            string key = "awlekjfsl";

            string cipher = encrypt(plain, key);
            string decrypted = decrypt(cipher, key);

            Console.WriteLine(cipher);
            Console.WriteLine(decrypted);

            var restext = new TextField(cipher)
            {
                Y = Pos.Top(menu) + 1
            };

            /*var win = new ExampleWindow();*/

            // Add both menu and win in a single call
            /*Application.Top.Add(menu, restext);*/
            /*Application.Run();*/
            /*Application.Shutdown();*/
        }

    }

    class ExampleWindow : Window
    {
        public TextField usernameText;

        public ExampleWindow()
        {
            Title = "Example App";

            var usernameLabel = new Label()
            {
                Text = "Username:"
            };

            usernameText = new TextField("")
            {
                X = Pos.Right(usernameLabel) + 1,
                Width = Dim.Fill(),
            };

            var passwordLabel = new Label()
            {
                Text = "Password:",
                X = Pos.Left(usernameLabel),
                Y = Pos.Bottom(usernameLabel) + 1
            };

            var passwordText = new TextField("")
            {
                Secret = true,
                X = Pos.Left(usernameText),
                Y = Pos.Top(passwordLabel),
                Width = Dim.Fill(),
            };

            var btnLogin = new Button()
            {
                Text = "Login",
                Y = Pos.Bottom(passwordLabel) + 1,
                X = Pos.Center(),
                IsDefault = true,
            };

            btnLogin.Clicked += () =>
            {
                if (usernameText.Text == "admin" && passwordText.Text == "password")
                {
                    MessageBox.Query("Logging In", "Login Successful", "Ok");
                    Application.RequestStop();
                }
                else
                {
                    MessageBox.ErrorQuery("Logging In", "Incorrect Username or Passwrod", "Ok");
                }
            };

            Add(usernameLabel, usernameText, passwordLabel, passwordText, btnLogin);
        }
    }
}