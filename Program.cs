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
        public static string nickname;
        static int PlayerHp;
        static int PlayerLvl;
        static int PlayerXp;
        static int PlayerAtt;
        static double PlayerDeff = 1.5;
        static string[] EnemyNameArray = {"Мышь", "Паук", "Волк", "Гоблин", "Страж" };
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
            Console.ReadKey();
        }

        static void ShowStats()
        {
            Clear();
            Console.WriteLine("{0} Hp:{1} Lvl:{2} Xp:{3}", nickname, PlayerHp, PlayerLvl, PlayerXp);
        }

        static void ShowStatsEnemy()
        {
            Console.WriteLine("Enemy:{0}\tHp:{1}\tLvl:{2}\tDamage:{3}", EnemyName, EnemyHp, EnemyLvl+1, EnemyAtt);
        }



        static void Menu()
        {
           
            code = 0; pun = 1; kur[0] = '>';
            do
            {
                Clear();
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

        static void SearchEnemy() //Генерация Врага
        {
            EnemyName = EnemyNameArray[Rand(0, EnemyNameArray.Length)];
            EnemyLvl = Rand(EnemyPlaceLvl[Place] / 2, EnemyPlaceLvl[Place]) - 1;
            EnemyHp = Rand(EnemyHpArray[EnemyLvl] / 2, EnemyHpArray[EnemyLvl] + EnemyHpArray[EnemyLvl] / 2);
            EnemyAtt = Rand(EnemyAttArray[EnemyLvl] / 2, EnemyAttArray[EnemyLvl] + EnemyAttArray[EnemyLvl] / 2);   
        }
        static void Test()
        {
            
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
            while (true)
            {
                if (PlayerHp > 0)
                {
                    if (EnemyHp > 0)
                    {
                        code = 0; pun = 1; kur[0] = '>';
                        do
                        {
                            Clear();
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
                        WinMesg();
                    }
                }
                else
                {
                    Death();
                }
            }
        }

        static void WinMesg()
        {
            code = 0; pun = 1; kur[0] = '>';
            do
            {
                Clear();
                ShowStats();
                Console.WriteLine("Победа! Желаете продолжить искать врагов или же хотите вернуться домой?\n{0}В бой!\n{1}Вернуться", kur[0], kur[1]);
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
        static void NewGame()
        {
            Console.Write("Добро пожаловать в игру.\nВведите свой ник:");
            nickname = Console.ReadLine();
            PlayerHp = 100;
            PlayerLvl = 1;
            PlayerXp = 0;
            PlayerAtt = 5;
            
        }
        static void Main(string[] args)
        { 
            NewGame();
            ShowStats();
            Menu();
        }
    }
}
