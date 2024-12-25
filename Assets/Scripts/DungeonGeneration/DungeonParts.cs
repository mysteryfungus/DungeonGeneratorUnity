using UnityEngine;

public class Room
{
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }

    public Room(Vector2 position, Vector2 size)
    {
        Position = position;
        Size = size;
    }

    public Vector2 GetCenter()
    {
        return Position + Size / 2;
    }

    public bool Intersects(Room other) //Проверка на пересечение комнаты с другой комнатой
    {
        return !(Position.x + Size.x <= other.Position.x || 
                 Position.x >= other.Position.x + other.Size.x ||
                 Position.y + Size.y <= other.Position.y ||
                 Position.y >= other.Position.y + other.Size.y);
    }
}

public class Corridor
{
    public Vector2 Start { get; private set; }
    public Vector2 End { get; private set; }

    public Corridor(Vector2 start, Vector2 end)
    {
        Start = start;
        End = end;
    }
}
