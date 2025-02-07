using System;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
    private IMovable movement;
    private IState state;
    [SerializeField] private Player player;

    private void Start()
    {
        movement = new MovementService(player.rb);
        state = new StateService(player);
        player.Initialize(movement, state);
    }
}
