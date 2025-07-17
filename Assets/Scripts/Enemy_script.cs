using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_script : MonoBehaviour
{
    public GameObject Enemy;
    public float EnemyHP = 100;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void takeDamage(int Melee_damage)
    {
        EnemyHP -= Melee_damage;
        Debug.Log("damage taken");
        if (EnemyHP <= 0)
        {
            Destroy(Enemy);
        }
    }
}




