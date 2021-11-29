using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class PlayerModel : MonoBehaviour
{
    public PlayerType playerType;
    public PlayerLevel level;
    private int maxHealth;
    private int currentHealth;

    public Image healthBar;

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
            var playerLevel = GameController.instance.level;
            level = (PlayerLevel)Random.Range(0, (int)playerLevel);
        }
        
        SetMaterial();
        SetScale();
        SetHealthFull();
    }


    /// <summary>Changes the color of the material when player created / levels up</summary>
    private void SetMaterial()
    {
        meshRenderer.material.color = level.GetColor();
    }

    /// <summary>Changes the size of the player when player created / levels up</summary>
    private void SetScale() {
        transform.localScale = level.GetScale();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    /// <summary>Sets the health of the player to full health for their level.</summary>
    private void SetHealthFull()
    {
        // The exponent of the player's level, e.g. 2 for level4.
        var levelExponent = ((int)level) + 1;
        maxHealth = levelExponent * (int)(Math.Pow(2, (double)levelExponent));
        currentHealth = maxHealth;
        SetHealthBar((float)currentHealth / maxHealth);
    }

    /// <summary>Decrements the health of a player by a given amount of damage.</summary>
    /// <returns>True if the player is still alive (health is greater than 0), and false if they have no more health.</returns>
    public bool DecrementHealth(int damage)
    {
        if (currentHealth <= damage)
        {
            currentHealth = 0;
            SetHealthBar(0);
            
            return false;
        }
        currentHealth -= damage;
        SetHealthBar((float)currentHealth / maxHealth);
        return true;
    }

    /// <returns>The amount of a damage the player does. It is equal to their level numerical value, e.g. a level 4 player does 4 damage.</returns>
    public int GetDamageAmount()
    {
        return level.GetNumericalValue();
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
            SetHealthFull();
            GameController.instance.UpdateScore();
            return true;
        }

        // at highest level -- cannot level-up
        return false;
    }

    void SetHealthBar(float value)
    {
        if (healthBar)
        {
            healthBar.fillAmount = value;
        }
    }
}
