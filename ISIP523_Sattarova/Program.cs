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
          public override void AttackPlayer(Player player)
    {
        int damage = Attack;
        Random random = new Random();

        switch (BossType)
        {
            case BossType.VVG:
                if (random.NextDouble() < 0.3)
                {
                    damage *= 2;
                    Console.WriteLine($"{BossType} наносит критический удар!");
                }
                break;

            case BossType.ArchmageCPP:
                if (random.NextDouble() < 0.25) 
                {
                    player.IsFrozen = true;
                    Console.WriteLine($"{BossType} замораживает вас! Вы пропустите следующий ход");
                }
                break;

            case BossType.PestovCMinus:
                if (random.NextDouble() < 0.3)
                {
                    player.IsFrozen = true;
                    Console.WriteLine($"{BossType} замораживает вас! Вы пропустите следующий ход");
                }
                break;
        }

        if (!player.IsFrozen || (BossType != BossType.ArchmageCPP && BossType != BossType.PestovCMinus))
        {
            Console.WriteLine($"{BossType} атакует!");
            player.TakeDamage(this);
        }
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
    public void StartGame()
    {
        Console.WriteLine("Добро пожаловать в игру!");
        Console.WriteLine("Сражайтесь с врагами, находите сокровища и выживайте!\n");

        while (player.Health > 0)
        {
            Play();
        }

        GameOver();
    }

    private void Play()
    {
        Count++;
        PrintPlayerStatus();

        if (player.IsFrozen)
        {
            FrozenCheck();
            return;
        }

        ShowMenu();
    }

    private void PrintPlayerStatus()
    {
        Console.WriteLine($"==ХОД {Count}==");
        Console.WriteLine($"{player.Health}/{player.MaxHealth}");
        Console.WriteLine($"Оружие {player.CurrentWeapon}");
        Console.WriteLine($"Доспехи {player.CurrentArmor}");
    }

    private void FrozenCheck()
    {
        Console.WriteLine(" Вы заморожены и пропускаете ход!");
        player.IsFrozen = false;
    }

    private void ShowMenu()
    {
        Console.WriteLine("\n1. Продолжить путешествие");
        Console.WriteLine("2. Выйти из игры");

        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
                ProcessTurn();
                break;
            case "2":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Неверный ввод!");
                ShowMenu();
                break;
        }
    }

    private void ProcessTurn()
    {
        if (Count % 10 == 0) 
        {
            FindBoss();
        }
        else
        {
            if (random.NextDouble() < 0.5) 
            {
                FindEnemy();
            }
            else
            {
                FindChest();
            }
        }
    }

    private void FindEnemy()
    {
        var enemyType = (EnemyType)random.Next(0, 3);
        Enemy enemy = CreateEnemy(enemyType);
        Console.WriteLine($"Ваш враг {enemyType}");
        PrintEnemyInfo(enemy);
        Combat(enemy);
    }

    private Enemy CreateEnemy(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Goblin: return new Enemy(EnemyType.Goblin, 30, 8, 5);
            case EnemyType.Skeleton: return new Enemy(EnemyType.Skeleton, 25, 10, 3);
            case EnemyType.Mage: return new Enemy(EnemyType.Mage, 20, 12, 1);
            default: return new Enemy(EnemyType.Goblin, 30, 8, 5);
        }
    }

    private void PrintEnemyInfo(Enemy enemy)
    {
        Console.WriteLine($" Здоровье врага: {enemy.Health} |  Атака: {enemy.Attack} |  Защита: {enemy.Defense}");
    }

    private void FindBoss()
    {
        var bossType = (BossType)random.Next(0, 4);
        Boss boss = CreateBoss(bossType);
        Console.WriteLine($"Ваш босс {boss.BossType}");
        PrintEnemyInfo(boss);
        Combat(boss);
    }

    private Boss CreateBoss(BossType type)
    {
        switch (type)
        {
            case BossType.VVG: return new Boss(BossType.VVG, 40, 10, 3);
            case BossType.Kovalevsky: return new Boss(BossType.Kovalevsky, 35, 12, 4);
            case BossType.ArchmageCPP: return new Boss(BossType.ArchmageCPP, 25, 15, 2);
            case BossType.PestovCMinus: return new Boss(BossType.PestovCMinus, 20, 18, 1);
            default: return new Boss(BossType.VVG, 40, 10, 3);
        }
    }

    private void Combat(Enemy enemy)
    {
        {
            while (enemy.Health > 0 && player.Health > 0)
            {
                ShowCombatMenu();

                string input = Console.ReadLine().Trim();
                if (input == "1")
                {
                    player.Attack(enemy);
                }
                else if (input == "2")
                {
                    player.Defend();
                    Console.WriteLine("Вы готовитесь к защите!");
                }
                else
                {
                    Console.WriteLine("Неверный ввод!");
                    continue;
                }

                if (enemy.Health > 0)
                {
                    enemy.AttackPlayer(player);
                }
            }
        }

    }

    private void ShowCombatMenu()
    {
        Console.WriteLine("\n1. Атаковать");
        Console.WriteLine("2. Защищаться");
    }

    private void FindChest()
    {
        Console.WriteLine("\n Вы нашли сундук!");

        var chestType = random.Next(0, 3);

        switch (chestType)
        {
            case 0:
                GiveHealing();
                break;
            case 1:
                GiveWeapon();
                break;
            case 2:
                GiveArmor();
                break;
        }
    }

    private void GiveHealing()
    {
        Console.WriteLine(" В сундуке лечебное зелье!");
        player.Heal();
    }

    private void GiveWeapon()
    
        {
            var newWeapon = weapons[random.Next(0, weapons.Count)];
            Console.WriteLine($"В сундуке: {newWeapon}");
            Console.WriteLine($"Ваше текущее оружие: {player.CurrentWeapon}");

            Console.WriteLine("1. Взять новое оружие");
            Console.WriteLine("2. Оставить текущее");

            if (Console.ReadLine() == "1")
            {
                player.CurrentWeapon = newWeapon;
                Console.WriteLine("Вы экипировали новое оружие!");
            }
        }

    private void GiveArmor()
    {
        var newArmor = armors[random.Next(0, armors.Count)];
        Console.WriteLine($"В сундуке: {newArmor}");
        Console.WriteLine($"Ваши текущие доспехи: {player.CurrentArmor}");

        Console.WriteLine("1. Взять новые доспехи");
        Console.WriteLine("2. Оставить текущие");

        if (Console.ReadLine() == "1")
        {
            player.CurrentArmor = newArmor;
            Console.WriteLine("Вы экипировали новые доспехи!");
        }
    }

    private void GameOver()
    {
        Console.WriteLine("\nИгра окончена!");
        Console.WriteLine($"Вы продержались {Count} ходов");
        Console.WriteLine("Спасибо за игру!");
    }



    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.StartGame();
        }
    }
}