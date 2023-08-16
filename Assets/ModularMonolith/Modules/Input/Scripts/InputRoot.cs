using UnityEngine;

internal class InputRoot : CompositeRoot
{
    [SerializeField] private PlayerMovement _playerMovement;

    private IInput _input;

    public override void Compose()
    {
        _input = new DesktopInput();

        _playerMovement.Initialize(_input);
    }
}
