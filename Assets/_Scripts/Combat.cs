using UnityEngine;

public class Combat : MonoBehaviour
{
    private AnimationManager animationManager;
    

    void Awake()
    {
        animationManager = GameManager.Instance.animationManager;
    }

    private void Update()
    {
 
 
        if (InputManager.Instance == null)
            return;

        if (animationManager.swordEquipped && InputManager.Instance.attackPressed)
        {
            
            InputManager.Instance.attackPressed = false; // Konsumuj input
        }

        if (animationManager.swordEquipped && InputManager.Instance.interactPressed)
        {
            animationManager.CastSpell();
            InputManager.Instance.interactPressed = false; // Konsumuj input
        }
    }
}
