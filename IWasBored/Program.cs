// Field
char[,] points = { 
    { ' ', ' ', ' ' }, 
    { ' ', ' ', ' ' }, 
    { ' ', ' ', ' ' } 
};


#region Interactive menu
// Displays active option in cyan
void displayActiveOption(string option)
{
    ConsoleColor currentColor = Console.ForegroundColor;

    Console.Write("|\t");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(option);
    Console.ForegroundColor = currentColor;
}

// Returns index of user's choise in menu
int setInteractiveMenu(string[] options, string caption)
{
    int currentOption = 0;
    do
    {
        Console.WriteLine("_____________________________________________________________");
        Console.WriteLine($"|\t{caption}: ");
        Console.WriteLine("|_____________________________________________________________");
        for (int i = 0; i < options.Length; i++)
        {
            if (i == currentOption)
                displayActiveOption(options[i]);
            else
                Console.WriteLine($"|\t{options[i]}");
        }
        Console.WriteLine("|_____________________________________________________________");
        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
        ConsoleKey pressedKey = Console.ReadKey().Key;

        switch (pressedKey)
        {
            case ConsoleKey.Spacebar:
            case ConsoleKey.Enter: return currentOption;

            case ConsoleKey.W:
            case ConsoleKey.UpArrow: if (--currentOption < 0) currentOption = options.Length - 1; break;

            case ConsoleKey.S:
            case ConsoleKey.DownArrow: if (++currentOption >= options.Length) currentOption = 0; break;
        }
    } while (true);
} 
#endregion

// Checks combinations
string determineWinner()
{
    // Checking X lines
    for (int y = 0; y < 3; y++)
    {
        if (points[y, 0] == 'X' && points[y, 1] == 'X' && points[y, 2] == 'X')
        {
            return "X";
        }
        else if (points[y, 0] == 'O' && points[y, 1] == 'O' && points[y, 2] == 'O')
        {
            return "O";
        }
    }

    // Checking Y lines
    for (int x = 0; x < 3; x++)
    {
        if (points[0, x] == 'X' && points[1, x] == 'X' && points[2, x] == 'X')
        {
            return "X";
        }
        else if (points[0, x] == 'O' && points[1, x] == 'O' && points[2, x] == 'O')
        {
            return "O";
        }
    }

    //Checking crosses
    if (points[0, 0] == 'X' && points[1, 1] == 'X' && points[2, 2] == 'X')
    {
        return "X";
    }
    else if (points[0, 0] == 'O' && points[1, 1] == 'O' && points[2, 2] == 'O')
    {
        return "O";
    }

    if (points[0, 2] == 'X' && points[1, 1] == 'X' && points[2, 0] == 'X')
    {
        return "X";
    }
    else if (points[0, 2] == 'O' && points[1, 1] == 'O' && points[2, 0] == 'O')
    {
        return "O";
    }

    return "none";
}

// Displays field with occupied points, if there are
void displayField()
{
    Console.WriteLine($"  1 2 3");
    Console.WriteLine($"1 {points[0, 0]}|{points[0, 1]}|{points[0, 2]}");
    Console.WriteLine($"  -----");
    Console.WriteLine($"2 {points[1, 0]}|{points[1, 1]}|{points[1, 2]}");
    Console.WriteLine($"  -----");
    Console.WriteLine($"3 {points[2, 0]}|{points[2, 1]}|{points[2, 2]}");
}

#region Menu

bool isExit = false;
string[] options = new string[2] { "Play", "Exit" };
do
{
    switch (setInteractiveMenu(options, "Menu"))
    {
        case 0:
            Console.Clear();
            bool isPlayer1 = true;

            string winner = "none";
            do
            {
                try
                {
                    displayField();

                    #region Getting position

                    Console.Write("Input position (e.g. 1 2 or 12): ");
                    string pos = Console.ReadLine();

                    int x = -1, y = -1;
                    // Checking if given string isn't empty
                    if (!string.IsNullOrEmpty(pos) && !string.IsNullOrWhiteSpace(pos))
                    {
                        // Checking format
                        if (pos.Length <= 3)
                        {
                            // Choosing format of position
                            if (pos.Length == 3)
                            {
                                x = int.Parse(pos.Split(' ')[0]) - 1;
                                y = int.Parse(pos.Split(' ')[1]) - 1;
                            }
                            else if (pos.Length == 2)
                            {
                                x = int.Parse($"{pos[0]}") - 1;
                                y = int.Parse($"{pos[1]}") - 1;
                            }
                            else
                            {
                                throw new Exception("Incorrect string format");
                            }
                        }
                        else
                        {
                            throw new Exception("Incorrect string format");
                        }
                    }
                    else
                    {
                        throw new Exception("Position is empty");
                    }

                    #endregion

                    // Setting point
                    points[x, y] = points[x, y] == ' ' ?
                        (isPlayer1 ? 'X' : 'O') :
                        throw new Exception("This position is already accupied, try again...");

                    // Every turn we are checking if we have winner
                    winner = determineWinner();

                    // Changing player on its oposite
                    isPlayer1 = !isPlayer1;

                    Console.Clear();
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
            } while (winner == "none");

            Console.WriteLine($"Player {winner} has won!\n");

            Console.ReadKey();
            Console.Clear();
            break;
        case 1:
            Console.Clear();

            Console.WriteLine("Bye then(");
            isExit = true;
            Console.ReadKey();
            break;
    }
} while (!isExit);

#endregion