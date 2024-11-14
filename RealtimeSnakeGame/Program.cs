using RealtimeSnakeGame;

    Random r = new Random();
    Coord gridDimensions = new Coord(50, 20);
    Direction movementDirection = Direction.Down;
    LinkedList humanPosHistory = new LinkedList();
    LinkedList comPosHistory = new LinkedList();

    int huTailLength = 1;
    int comTailLength = 1;
    Computer computer = new Computer();

    Coord humanPos = new Coord(r.Next(5, gridDimensions.X - 5), r.Next(5, gridDimensions.Y - 5));
    Coord comPos = new Coord(r.Next(1, gridDimensions.X - 1), r.Next(1, gridDimensions.Y - 1));
    Coord foodPos = new Coord(r.Next(1, gridDimensions.X - 1), r.Next(1, gridDimensions.Y - 1));
    int frameDelayMilli = 100;
while (true)
{
    Console.Clear();
    Console.WriteLine("Human Tail Length: " + huTailLength);
    Console.WriteLine("Computer Tail Length: " + comTailLength);
    humanPos.ApplyMovementDirection(movementDirection);
    computer.comMovementDirection = computer.GetComDirection(comPos, foodPos, humanPosHistory, comPosHistory, gridDimensions);
    comPos.ApplyMovementDirection(computer.comMovementDirection);

    // Render the game to the Console
    for (int y = 0; y < gridDimensions.Y; y++)
    {
        for (int x = 0; x < gridDimensions.X; x++)
        {
            Coord currentCoord = new Coord(x, y);

            if (humanPos.Equals(currentCoord) || humanPosHistory.contains(currentCoord))
                Console.Write("H"); // Human snake
            else if (comPos.Equals(currentCoord) || comPosHistory.contains(currentCoord))
                Console.Write("C"); // Computer snake
            else if (foodPos.Equals(currentCoord))
                Console.Write("F"); // Food
            else if (x == 0 || y == 0 || x == gridDimensions.X - 1 || y == gridDimensions.Y - 1) //x = 0-49 ,y = 0-19
                Console.Write("#");// Border
            else
                Console.Write(" ");
        }
        Console.WriteLine();
    }
    // Check if computer snake has picked up food
    if (comPos.Equals(foodPos))
    {
        comTailLength++;
        foodPos = new Coord(r.Next(1, gridDimensions.X - 1), r.Next(1, gridDimensions.Y - 1));
    }
    // Check if snake has picked up food
    if (humanPos.Equals(foodPos))
    {
        huTailLength++;
        foodPos = new Coord(r.Next(1, gridDimensions.X - 1), r.Next(1, gridDimensions.Y - 1));
    }
    // Check for game over conditions - human hits wall or its own tail
    if (humanPos.X == 0 || humanPos.Y == 0 || humanPos.X == gridDimensions.X - 1 ||
             humanPos.Y == gridDimensions.Y - 1)
    {
        Console.WriteLine("Game Over! Human snake collided.");
        break;
    }
    // Check for game over conditions - computer hits wall or its own tail
    if (comPos.X == 0 || comPos.Y == 0 || comPos.X == gridDimensions.X - 1 ||
        comPos.Y == gridDimensions.Y - 1 || comPosHistory.contains(comPos))
    {
        Console.WriteLine("Game Over! Computer snake collided.");
        break;
    }
    // Check for collision between human and computer
    if (humanPos.Equals(comPos) || humanPosHistory.contains(comPos) || comPosHistory.contains(humanPos))
    {
        // Reduce the tail length of the snake that was hit
        if (humanPosHistory.contains(comPos) || humanPos.Equals(comPos))
        {
            comTailLength--;
            comPosHistory.remove(0);
            Console.WriteLine("Computer collided with Human! Computer tail length reduced.");
        }
        else if (comPosHistory.contains(humanPos) || comPos.Equals(humanPos))
        {
            huTailLength--;
            humanPosHistory.remove(0);
            Console.WriteLine("Human collided with Computer! Human tail length reduced.");
        }

        // End the game if any snake's tail length reaches zero
        if (huTailLength == 0)
        {
            Console.WriteLine("Game Over! Human snake has lost all tail segments.");
            break;
        }
        if (comTailLength == 0)
        {
            Console.WriteLine("Game Over! Computer snake has lost all tail segments.");
            break;
        }
    }

    // Add the positions to the history
    humanPosHistory.add(new Coord(humanPos.X, humanPos.Y));
    comPosHistory.add(new Coord(comPos.X, comPos.Y));

    // Trim the history to maintain tail length
    if (humanPosHistory.size() > huTailLength)
        humanPosHistory.remove(0);
    if (comPosHistory.size() > comTailLength)
        comPosHistory.remove(0);

    DateTime time = DateTime.Now;

    // Delay for the specified frame time while checking for player input
    while ((DateTime.Now - time).TotalMilliseconds < frameDelayMilli)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKey key = Console.ReadKey(true).Key; // true to not display pressed keys
            // Assign new direction based on the pressed key
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    movementDirection = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    movementDirection = Direction.Right;
                    break;
                case ConsoleKey.UpArrow:
                    movementDirection = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    movementDirection = Direction.Down;
                    break;
            }
        }
    }
}

