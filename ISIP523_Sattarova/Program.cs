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

        public enum BossType
        {
            VVG,
            Kovalevsky,
            ArchmageCPP,
            PestovCMinus
        }
        public class Boss : Enemy
        {
            public BossType BossType { get; set; }

            public Boss(BossType type, int baseHealth, int baseAttack, int baseDefense)
                : base(GetEnemyTypeFromBoss(type), 0, 0, 0)
            {
                BossType = type;

                switch (type)
                {
                    case BossType.VVG:
                        Health = (int)(baseHealth * 2.0);
                        Attack = (int)(baseAttack * 1.5);
                        Defense = (int)(baseDefense * 1.2);
                        break;

                    case BossType.Kovalevsky:
                        Health = (int)(baseHealth * 2.5);
                        Attack = (int)(baseAttack * 1.3);
                        Defense = (int)(baseDefense * 1.4);
                        break;

                    case BossType.ArchmageCPP:
                        Health = (int)(baseHealth * 1.8);
                        Attack = (int)(baseAttack * 1.6);
                        Defense = (int)(baseDefense * 1.1);
                        break;

                    case BossType.PestovCMinus:
                        Health = (int)(baseHealth * 1.3);
                        Attack = (int)(baseAttack * 1.8);
                        Defense = (int)(baseDefense * 0.6);
                        break;
                }

                MaxHealth = Health;
            }

            private static EnemyType GetEnemyTypeFromBoss(BossType bossType)
            {
                switch (bossType)
                {
                    case BossType.VVG:
                        return EnemyType.Goblin;
                    case BossType.Kovalevsky:
                        return EnemyType.Skeleton;
                    case BossType.ArchmageCPP:
                        return EnemyType.Mage;
                    case BossType.PestovCMinus:
                        return EnemyType.Skeleton;
                    default:
                        return EnemyType.Goblin;
                }
            }

