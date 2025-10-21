using System;

public class Orug
{
    public string Name { get; set; }
    public int uron { get; set; }

    public Orug(string name, int damage)
    {
        Name = name;
        uron = damage;
    }
}

public class Brone
{
    public string Name { get; set; }
    public double uroven { get; set; }

    public Brone(string name, double defense)
    {
        Name = name;
        uroven = defense;
    }
}

public class Player
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public Orug CurrentWeapon { get; set; }
    public Brone CurrentArmor { get; set; }
    public bool IsFrozen { get; set; }
    public bool IsDefending { get; set; }

    public Player()
    {
        MaxHealth = 100;
        Health = MaxHealth;
        CurrentWeapon = new Orug("Меч", 3);
        CurrentArmor = new Brone("Алмазный доспех", 0.9);
        IsDefending = false;
    }
    public void TakeDamage()
    {

    }
    public void Heal()
    {

    }
    public void Defend()
    {

    }
    public void Attack()
    {

    }

    public enum EnemyType
    {
        Goblin,
        Skeleton,
        Mage
    }
    public class Enemy
    {
        public EnemyType Type { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public Enemy(EnemyType type, int health, int attack, int defense)
        {
            Type = type;
            Health = health;
            MaxHealth = health;
            Attack = attack;
            Defense = defense;
        }