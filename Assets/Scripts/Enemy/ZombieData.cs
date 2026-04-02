using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ZombieData", menuName = "Scriptable Objects/ZombieData")]
public class ZombieData : ScriptableObject
{
    public int Health = 100;
    public int Attack = 20;
    public float Speed = 4f;
    public float AtkInterval = 1f;
}
