using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Game
{
    class Program
    {
        static int code, pun;
        static char[] kur = new char[255];
        //Player
        public static string nickname;
        static int PlayerHp;
        static int PlayerLvl;
        static int PlayerXp;
        static int PlayerAtt;
        static double PlayerDeff;
        static string[] PlayerInv = { "Пусто", "Пусто", "Пусто", "Пусто", "Пусто", "Пусто", "Пусто", "Кулаки" };
        static int[] PlayerInvStats = new int[8];
        static int Money;
        static string[] ItemRary = { "Ветхий", "Почти новый", "Новый", "Стильный", "Многообещающий", "Отличный", "Элитный", "Королевский", "Драконий", "Проклятый", "Подарок от дедушки" };
        static string[] ArmorArray = { "Шлем", "Нагрудник", "Наплечник", "Наручи", "Перчатки", "Наколенники", "Поножи" };
        static string[] WeaponArray = { "Деревянная Палка", "Камень", "Топор", "Удочка","Лопата","Вилы","Ведро","Нож","Кинжал","Меч","Копье","Булава","Лук", "Посох Мага", "Позвонить другу"};
        static int KillMob;
        ////////
        static string[] EnemyNameArray = {"Мышь", "Паук", "Волк", "Гоблин", "Страж","Гном","Кабан","Змея","Скелет","Мертвец","Слизень","Орк","Бандит","Демон","Гаргулья", "Киреял"};
        static string EnemyName;
        static int EnemyHp;
        static int EnemyLvl;
        static int EnemyAtt;
        static int[] EnemyPlaceLvl = { 2, 4, 7, 10 };
        static int[] EnemyAttArray = { 10, 15, 20, 30, 40, 55, 70, 90, 125, 150 };
        static int[] EnemyHpArray = { 50, 100, 200, 250, 300, 400, 500, 800, 1000 };
        static int Place;
        static Random rnd = new Random();

        static int Rand(int min, int max)
        {
            
            int a = rnd.Next(min, max);
            return a;
        }
        static int Kur(int maxi)
        {
            ConsoleKey btn = Console.ReadKey().Key;
            if (btn == ConsoleKey.Enter)
            {
                kur[pun - 1] = ' ';
                return pun;
            }
            if (btn == ConsoleKey.W || btn == ConsoleKey.UpArrow)
            {
                if (pun == 1)
                {
                    kur[0] = ' ';
                    pun = maxi;
                    kur[maxi - 1] = '>';
                    return 0;
                }
                else
                {
                    kur[pun - 1] = ' ';
                    kur[pun - 2] = '>';
                    pun--;
                    return 0;
                }
            }
            else if (btn == ConsoleKey.S || btn == ConsoleKey.DownArrow)
            {
                if (pun == maxi)
                {
                    kur[maxi - 1] = ' ';
                    pun = 1;
                    kur[0] = '>';
                    return 0;
                }
                else
                {
                    kur[pun - 1] = ' ';
                    kur[pun] = '>';
                    pun++;
                    return 0;
                }
            }
            else return 0;
        }

        static void Clear()
        {
            Console.Clear();
        }

        static void Death()
        {
            Clear();
            Console.WriteLine("Ты погиб");
            NewGame();
        }

        static void SearchEnemy() //Генерация Врага
        {
            EnemyName = EnemyNameArray[Rand(0, EnemyNameArray.Length)];
            EnemyLvl = Rand(EnemyPlaceLvl[Place] / 2, EnemyPlaceLvl[Place] + 1);
            int LvlId = EnemyLvl - 1;
            EnemyHp = Rand(EnemyHpArray[LvlId] / 2, EnemyHpArray[LvlId] + EnemyHpArray[LvlId] / 2);
            EnemyAtt = Rand(EnemyAttArray[LvlId] / 2, EnemyAttArray[LvlId] + EnemyAttArray[LvlId] / 2);
        }

        static int DropMoney()
        {
            int money = Rand(Convert.ToInt32((EnemyLvl * 4 + EnemyHp + EnemyAtt) * 0.1), Convert.ToInt32((EnemyLvl * 4 + EnemyHp + EnemyAtt) * 0.5));
            return money;
        }


        static void WinMesg(int drop)
        {
            code = 0; pun = 1; kur[0] = '>';
            do
            {
                Money += drop;
                ShowStats();
                Console.WriteLine("Победа! Вы получили {2} монет с этого монстра\nЖелаете продолжить искать врагов или же хотите вернуться домой?\n{0}В бой!\n{1}Вернуться", kur[0], kur[1], drop);
                code = Kur(2);
            } while (code == 0);
            if (code == 1)
            {
                Battle();
            }
            else
            {
                Menu();
            }
        }
        static void ShowStats()
        {
            Clear();
            Console.WriteLine("{0}\tHp:{1}\tLvl:{2}\tXp:{3}\tMoney:{4}", nickname, PlayerHp, PlayerLvl, PlayerXp, Money);
        }

        static void ShowStatsEnemy()
        {
            Console.WriteLine("Враг:{0}\tHp:{1}\tLvl:{2}\tDamage:{3}", EnemyName, EnemyHp, EnemyLvl, EnemyAtt);
        }

        //Магазин создается
        static int[,] ShopSystem()
        {
            int[,] shop = new int[4,5];
            for(int i = 0; i < shop.Length; i++)
            {
                if (Rand(0,4) < 4)
                {
                    shop[0, i] = 0;
                    shop[1, i] = Rand(0, 6);
                    //shop[2, i] = Rand(PlayerLvl/2, PlayerLvl*2) + Rand(0, KillMob) + 
                }
            }
            return shop;
        }
        static void Shop()
        {
            int[,] ShopItem = new int[4,5];
            ShopItem = ShopSystem();
            ShowStats();
            Console.WriteLine("\tМагазин\nТовары на сегодня:");
        }
        static void Menu()
        {
           
            code = 0; pun = 1; kur[0] = '>';
            do
            {
                ShowStats();
                Console.WriteLine("{0}Искать монстров\n{1}Выход", kur[0], kur[1]);
                code = Kur(2);
            } while (code == 0);
            if (code == 1)
            {
                Battle();
            }
            else
            {
                Environment.Exit(0);
            }
            
        }

        static void Test()
        {
            Place = 1;
            ShowStats();
            while (true)
            {
                SearchEnemy();
                ShowStatsEnemy();
                Console.ReadKey();
            }
        }


        static void Battle()
        {
            SearchEnemy();
            int drop = DropMoney();
            while (true)
            {
                if (PlayerHp > 0)
                {
                    if (EnemyHp > 0)
                    {
                        code = 0; pun = 1; kur[0] = '>';
                        do
                        {
                            ShowStats();
                            ShowStatsEnemy();
                            Console.Write("{0}Ударить\n{1}Бежать", kur[0], kur[1]);
                            code = Kur(2);
                        } while (code == 0);
                        if (code == 1)
                        {
                            EnemyHp -= Rand(PlayerAtt/2, PlayerAtt+PlayerAtt/2);
                            int EnemyAttack = Convert.ToInt32(EnemyAtt / PlayerDeff);
                            PlayerHp -= Rand(EnemyAttack / 2, EnemyAttack + EnemyAttack / 2);
                        }
                        else
                        {
                            Menu();
                        }
                    }
                    else
                    {
                        WinMesg(drop);
                    }
                }
                else
                {
                    Death();
                }
            }
        }

        static void NewGame()
        {
            Console.Write("Добро пожаловать в игру.\nВведите свой ник:");
            nickname = Console.ReadLine();
            if (nickname == "God")
            {
                PlayerHp = 10000;
            }
            else
            {
                PlayerHp = 100;
            }
            PlayerLvl = 1;
            PlayerXp = 0;
            PlayerAtt = 5;
            PlayerDeff = 1.5;
            Money = 0;
            
        }
        static void Main(string[] args)
        {

            NewGame();
            ShowStats();
            Menu();
        }
    }
}
