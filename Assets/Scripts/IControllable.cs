 using UnityEngine;

public interface IControllable
{
    float HorAxis { get; set; }
    float VertAxis { get; set; }
    Vector2 TouchPosition { get; set; }
}