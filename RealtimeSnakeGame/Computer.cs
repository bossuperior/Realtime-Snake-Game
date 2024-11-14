namespace RealtimeSnakeGame
{
    public class Computer
    {
        public Direction comMovementDirection = Direction.Up; //ทิศทางเริ่มต้นของงูฝั่งคอมจะเป็นทิศขึ้น
        public Direction GetComDirection(Coord comPos, Coord foodPos, LinkedList humanPosHistory, LinkedList comPosHistory, Coord gridDimensions) //รับค่า parameter ต่างๆเพื่อใช้เลือกเส้นทาง
        {
            // Create a LinkedList to store prioritized directions based on the target position
            LinkedList prioritizedDirections = new LinkedList();

            // Prioritize directions based on the target position (food)
            if (foodPos.X > comPos.X) prioritizedDirections.add(Direction.Right);
            if (foodPos.X < comPos.X) prioritizedDirections.add(Direction.Left);
            if (foodPos.Y > comPos.Y) prioritizedDirections.add(Direction.Down);
            if (foodPos.Y < comPos.Y) prioritizedDirections.add(Direction.Up);

            // Add remaining directions to complete the list
            if (!prioritizedDirections.contains(Direction.Right)) prioritizedDirections.add(Direction.Right);
            if (!prioritizedDirections.contains(Direction.Left)) prioritizedDirections.add(Direction.Left);
            if (!prioritizedDirections.contains(Direction.Down)) prioritizedDirections.add(Direction.Down);
            if (!prioritizedDirections.contains(Direction.Up)) prioritizedDirections.add(Direction.Up);

            // Iterate through prioritized directions to find a safe movement
            for (int i = 0; i < prioritizedDirections.size(); i++)
            {
                Direction dir = (Direction)prioritizedDirections.get(i);
                Coord nextPos = new Coord(comPos.X, comPos.Y);
                nextPos.ApplyMovementDirection(dir);

                // Check for collision with wall
                if (nextPos.X == 0 || nextPos.Y == 0 || nextPos.X == gridDimensions.X - 1 || nextPos.Y == gridDimensions.Y - 1)
                    continue;

                // Check for collision with human snake or its own tail
                if (humanPosHistory.contains(nextPos) || comPosHistory.contains(nextPos))
                    continue;

                // If no collision, choose this direction
                return dir;
            }

            // If no optimal movement is possible, check each direction for a safe fallback
            Direction[] allDirections = { Direction.Left, Direction.Right, Direction.Up, Direction.Down };
            foreach (Direction dir in allDirections)
            {
                Coord nextPos = new Coord(comPos.X, comPos.Y);
                nextPos.ApplyMovementDirection(dir);

                // Check for collision and select if safe
                if (nextPos.X > 0 && nextPos.X < gridDimensions.X - 1 &&
                    nextPos.Y > 0 && nextPos.Y < gridDimensions.Y - 1 &&
                    !humanPosHistory.contains(nextPos) && !comPosHistory.contains(nextPos))
                {
                    return dir;
                }
            }
            // If no safe movement is found, keep the current direction as a last resort
            return comMovementDirection;
        }
    }
}
