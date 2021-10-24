using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerModel : MonoBehaviour
{
    public PlayerType playerType;
    public PlayerLevel level;

    // Material of the player
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        // Humans start with level 2, CPUs can start at anything (can they?)
        if (playerType == PlayerType.human)
        {
            level = PlayerLevel.Level2;
        }
        else
        {
            level = (PlayerLevel)Random.Range(0, (int)PlayerLevel.Level64);
        }
        
        SetMaterial();
        SetScale();
    }


    // Changes the color of the material when player created / levels up 
    private void SetMaterial()
    {
        meshRenderer.material.color = level.GetColor();
    }

    private void SetScale() {
        transform.localScale = level.GetScale();
    }

    /// <summary> Levels up a player to the next level, if possible. </summary>
    /// <returns> <b>true</b> if the player has been leveled up, <b>false</b> if they are max level. </returns>
    public bool LevelUp()
    {
        // if not at the highest level, can level-up
        if (level < PlayerLevel.Level2048)
        {
            level += 1;
            print("Leveled up to " + level);
            SetMaterial();
            SetScale();
            return true;
        }

        // at highest level -- cannot level-up
        return false;
    }
}
