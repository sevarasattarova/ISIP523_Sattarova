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
    public void TakeDamage(Enemy attacker)
    {
        int actualDamage = attacker.Attack;

        if (IsDefending)
        {
            Random random = new Random();
            if (random.NextDouble() < 0.4)
            {
                IsDefending = false;
                Console.WriteLine(" Вы успешно уклонились от атаки");
                return;
            }
            else
            {
                int blockedDamage = (int)(actualDamage - actualDamage * CurrentArmor.uroven);

                if (attacker.Type == EnemyType.Skeleton)
                {
                    blockedDamage = 0;
                }

                actualDamage = actualDamage - blockedDamage;
                Console.WriteLine($" Вы блокируете {blockedDamage} урона");
            }
            IsDefending = false;
        }
        else
        {
            if (attacker.Type != EnemyType.Skeleton)
            {
                actualDamage = actualDamage;
            }
        }

        Health -= actualDamage;
        Console.WriteLine($" Вы получаете {actualDamage} урона!");
        Console.WriteLine($" Ваше здоровье: {Health}/{MaxHealth}");
    }
    public void Heal()
    {
        Health = MaxHealth;
        Console.WriteLine(" Ваше здоровье полностью восстановлено!");
    }
    public void Defend()
    {
        IsDefending = true;
    }

    public void Attack(Enemy enemy)
    {
        int playerDamage = CurrentWeapon.uron;
        Console.WriteLine($" Вы наносите {playerDamage} урона!");

        if (enemy.Health <= 0)
        {
            Console.WriteLine($" {enemy.Type} побежден!");
        }
        else
        {
            Console.WriteLine($" Здоровье {enemy.Type}: {enemy.Health}/{enemy.MaxHealth}");
        }
    }
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

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public virtual void AttackPlayer(Player player)
    {
        int damage = Attack;
        Random random = new Random();

        switch (Type)
        {
            case EnemyType.Goblin:
                if (random.NextDouble() < 0.2) 
                {
                    damage *= 2;
                    Console.WriteLine($"{Type} наносит критический удар!");
                }
                break;

            case EnemyType.Mage:
                if (random.NextDouble() < 0.15) 
                {
                    player.IsFrozen = true;
                    Console.WriteLine($"{Type} замораживает вас! Вы пропустите следующий ход");
                }
                break;
        }

        if (!player.IsFrozen || Type != EnemyType.Mage)
        {
            Console.WriteLine($"{Type} атакует!");
            player.TakeDamage(this);
        }
    }
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

            public class Game
            {
                private Player player;
                private Random random;
                private int Count;
                private List<Orug> weapons;
                private List<Brone> armors;

                public Game()
                {
                    player = new Player();
                    random = new Random();
                    Count = 0;

                }