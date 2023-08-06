using UnityEngine;

public class DesktopInput : IInput
{
    private const string HorizontalAxis = "Horizontal";

    public float HorizontalMove()
    {
        return Input.GetAxis(HorizontalAxis);
    }

    public bool Jump()
    {
        return Input.GetKeyDown(KeyCode.W);
    }
}

public class ReversedDesktopInput : IInput
{
    private const string HorizontalAxis = "Horizontal";

    public float HorizontalMove()
    {
        return -Input.GetAxis(HorizontalAxis);
    }

    public bool Jump()
    {
        return Input.GetKeyDown(KeyCode.S);
    }
}