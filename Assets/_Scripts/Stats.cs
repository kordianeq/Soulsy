using UnityEngine;


public class Stats : MonoBehaviour
{
    public AnimationManager animationMenager;
    public int health = 100;
    public int maxHealth = 100;
    public int level = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Chceckpoint()
    {
        animationMenager.SitOnCheckpoint();
    }
}
