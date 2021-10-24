using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerModel : MonoBehaviour
{
    // Array of tuples representing material colors for the different levels
    private Tuple<byte, byte, byte>[] materialColors = new Tuple<byte, byte, byte>[]
    {
        Tuple.Create((byte)236, (byte)77, (byte)134),
        Tuple.Create((byte)94, (byte)221, (byte)93),
        Tuple.Create((byte)71, (byte)138, (byte)255),
        Tuple.Create((byte)230, (byte)116, (byte)81),
        Tuple.Create((byte)255, (byte)248, (byte)0),
        Tuple.Create((byte)255, (byte)141, (byte)230),
        Tuple.Create((byte)145, (byte)57, (byte)255),
        Tuple.Create((byte)255, (byte)108, (byte)54),
        Tuple.Create((byte)0, (byte)159, (byte)79),
        Tuple.Create((byte)83, (byte)255, (byte)231),
        Tuple.Create((byte)217, (byte)177, (byte)58),
        Tuple.Create((byte)255, (byte)0, (byte)80)
    };
    
    private Vector3[] scales = new Vector3[] {
        new Vector3(1.0f,1.0f,1.0f),
        new Vector3(1.25f,1.25f,1.25f),
        new Vector3(1.5f,1.5f,1.5f),
        new Vector3(1.75f,1.75f,1.75f),
        new Vector3(2.0f,2.0f,2.0f),
        new Vector3(2.25f,2.25f,2.25f),
        new Vector3(2.5f,2.5f,2.5f),
        new Vector3(2.75f,2.75f,2.75f),
        new Vector3(3.0f,3.0f,3.0f),
        new Vector3(3.25f,3.25f,3.25f),
        new Vector3(3.5f,3.5f,3.5f),
        new Vector3(3.75f,3.75f,3.75f)
    };
    
    
    public PlayerType playerType;
    public PlayerLevel level;

    // Material of the player
    private MeshRenderer meshrenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshrenderer = GetComponent<MeshRenderer>();
        
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

    // Update is called once per frame
    void Update()
    {
        
    }

    // Changes the color of the material when player created / levels up 
    void SetMaterial()
    {
        (byte red, byte green, byte blue) = materialColors[(int) level];
        Color32 color = new Color32(red, green, blue, (byte)255);
        meshrenderer.material.color = color;
    }

    void SetScale() {
        Vector3 scale = scales[(int) level];
        transform.localScale = scale;
    }
    
    // public void SetLevel(PlayerLevel newLevel)
    // {
    //     level = newLevel;
    //     SetMaterial();
    // }

    public bool LevelUp()
    {
        // if not at the highest level, can level-up
        if (playerType == PlayerType.human && level < PlayerLevel.Level2048)
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
