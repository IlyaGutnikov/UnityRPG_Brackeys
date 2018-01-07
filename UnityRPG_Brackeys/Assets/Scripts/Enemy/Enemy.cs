using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    private PlayerManager _playerManager = null;
    private CharacterStats _myStats = null;

    void Start()
    {
        _playerManager = PlayerManager.Instance;
        _myStats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        base.Interact();
        var playerCombat = _playerManager.Player.GetComponent<CharacterCombat>();
        if (playerCombat != null)
        {
            playerCombat.Attack(_myStats);
        }
    }
}
