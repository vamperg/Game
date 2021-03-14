using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Security.Cryptography;

namespace Game
{
    class Program
    {
        static int code, pun;
        static char[] kur = new char[255];
        static string PathToSave = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Legend of Hero";
        static string[] SaveInt = new string[255];
        //Player
        public static string nickname;
        static bool ShopUpdate = true;
        static int[,] ShopItem = new int[5, 5];
        static int PlayerMaxHp;
        static int PlayerHp;
        static int PlayerLvl;
        static int PlayerXp;
        static int PlayerAtt;
        static double PlayerDeff;
        static int[,] PlayerInv = new int[3, 8];
        static int Money;
        static string[] ItemRary = { "Ветхий", "Почти новый", "Новый", "Стильный", "Многообещающий", "Отличный", "Элитный", "Королевский", "Драконий", "Проклятый", "Подарок от дедушки" };
        static string[] ArmorArray = { "Шлем", "Нагрудник", "Наплечник", "Наручи", "Перчатки", "Наколенники", "Поножи" ,"Оружие"};
        static string[] WeaponArray = { "Деревянная Палка", "Камень", "Топор", "Удочка","Лопата","Вилы","Ведро","Нож","Кинжал","Меч","Копье","Булава","Лук", "Посох Мага", "Позвонить другу"};
        static int KillMob = 0;
        ////////
        static string[] Color = { "Gray", "Cyan", "DarkGreen", "Green", "DarkMagenta", "Magenta", "DarkBlue","Blue", "DarkRed", "Red", "Yellow" };
        static string[] EnemyNameArray = {"Мышь", "Паук", "Волк", "Гоблин", "Страж","Гном","Кабан","Змея","Скелет","Мертвец","Слизень","Орк","Бандит","Демон","Гаргулья", "Киреял"};
        static string EnemyName;
        static int EnemyHp;
        static int EnemyLvl;
        static int EnemyAtt;
        static int[] EnemyPlaceLvl = { 2, 4, 7, 10 };
        static int[] EnemyAttArray = { 10, 15, 20, 30, 40, 55, 70, 90, 125, 150 };
        static int[] EnemyHpArray = { 50, 100, 200, 250, 300, 400, 500, 800, 1000, 1500};
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
            if (btn == ConsoleKey.Enter || btn == ConsoleKey.Spacebar)
            {
                kur[pun - 1] = ' ';
                return pun;
            }
            if (btn == ConsoleKey.W || btn == ConsoleKey.UpArrow || btn == ConsoleKey.A)
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
            else if (btn == ConsoleKey.S || btn == ConsoleKey.DownArrow || btn == ConsoleKey.D)
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

        static void ResetColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void SetColor(string color)
        {
            switch (color)
            {
                case "Gray":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "Cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "DarkGreen":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case "Green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "DarkMagenta":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case "Magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "DarkBlue":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case "Blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "DarkRed":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "Red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "Yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
        }

        static void Clear()
        {
            Console.Clear();
        }

        static void SetCursor(int x, int y)
        {
            Console.SetCursorPosition(x,y);
        }

        static void Death()
        {
            Clear();
            Console.WriteLine("Ты погиб");
            Console.ReadKey();
        }

        static void InvBaff()
        {
            
            PlayerDeff = 1.5f;
            PlayerMaxHp = 100;
            PlayerAtt = 5;
            for(int i = 0; i < PlayerInv.GetLength(1); i++)
            {
                if(PlayerInv[1,i] != 0)
                {
                    PlayerDeff += PlayerInv[2, i] / 7.0;
                    if (i != 7)
                    {
                        PlayerMaxHp += PlayerInv[1, i];
                    }
                    else PlayerAtt += PlayerInv[1, i];
                }
            }
            
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

        static void HealRoom()
        {
            ShowStats();
            PlayerHp = PlayerMaxHp;
            Menu();
        }

        static void WinMesg(int drop)
        {
            Money += drop;
            KillMob += 1;
            code = 0; pun = 1; kur[0] = '>';
            do
            { 
                ShowStats();
                SetColor("Yellow");
                Console.WriteLine("Победа! Вы получили {0} монет с этого монстра\nЖелаете продолжить искать врагов или же хотите вернуться домой?", drop);
                ResetColor();
                Console.WriteLine("{0}В бой!\n{1}Вернуться", kur[0], kur[1]);
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
            SetColor("Cyan");
            Console.WriteLine("{0}\tЗдоровье:{1}\tСила:{2}\tЗащита:{3}\tУровень:{4}\tОпыт:{5}\tДеньги:{6}\tУбито монстров:{7}", nickname, PlayerHp, PlayerAtt, PlayerDeff, PlayerLvl, PlayerXp, Money, KillMob);
            ResetColor();
        }
        static string[] SetItem(int id)
        {
            string[] item = new string[3];

            item[0] = id == 7 ? WeaponArray[PlayerInv[0, id]] : ArmorArray[PlayerInv[0, id]];
            item[1] = Convert.ToString(PlayerInv[1, id]);
            item[2] = ItemRary[PlayerInv[2, id]];
            if (PlayerInv[1, id] == 0)
            {
                item[0] = ArmorArray[id];
                item[1] = "0";
                item[2] = "";
            }
            
            return item;
        }
        static void ShowInv()
        {

            string[] itemInfo;
            SetColor("DarkBlue");
            Console.WriteLine("\n\t\tИнвентарь:");
            int x = Console.CursorTop;
            for (int i = 0; i<PlayerInv.GetLength(1); i++)
            {
                itemInfo = SetItem(i);
                SetColor(itemInfo[2] != "" ? Color[PlayerInv[2, i]] : "Gray");
                SetCursor(i * 14, x);
                Console.WriteLine("{0}",itemInfo[0]);
                SetCursor(i * 14, Console.CursorTop);
                Console.WriteLine("{0}", itemInfo[1] == "0" ? "Отсутствует" : "Бафф:+"+itemInfo[1]);
                SetCursor(i * 14, Console.CursorTop);
                Console.WriteLine("{0}", itemInfo[2]);
                ResetColor();
            }
        }
        static void ShowStatsEnemy()
        {
            SetColor("Red");
            Console.WriteLine("Враг:{0}\tЗдоровье:{1}\tУровень:{2}\tСила:{3}", EnemyName, EnemyHp, EnemyLvl, EnemyAtt);
            ResetColor();
        }

        //Магазин создается
        static int[,] ShopSystem()
        {
            int[,] shop = new int[5,5];
            for(int i = 0; i < shop.GetLength(1); i++)
            {
                //Тип
                if (Rand(0, 2) ==0)
                {
                    shop[0, i] = 0;
                    shop[1, i] = Rand(0, 6);
                }
                else
                {
                    shop[0, i] = 1;
                    shop[1, i] = Rand(0, WeaponArray.Length-1);
                }
                //Бафф
                if (shop[0, i] == 1)
                {
                    shop[2, i] = 1 + Rand(PlayerLvl / 2, PlayerLvl * 3) + Rand(0, KillMob + 1) + Rand(0, Place * 5 + 1);
                }
                else
                {
                    shop[2, i] = 5 + Rand(PlayerLvl / 2, PlayerLvl * 5) + Rand(KillMob, KillMob * 3) + Rand(0, Place * 10 + 1);
                }
                ///Качество
                int rarity = Rand(0, 100);
                if (rarity < 70)
                {
                    shop[3, i] = Rand(0, 3);
                }
                else if (rarity >= 70 && rarity < 90)
                {
                    shop[3, i] = Rand(0, 6);
                }
                else if (rarity >= 90 && rarity < 98)
                {
                    shop[3, i] = Rand(2, 7);
                }
                else if (rarity >= 98 && rarity <=100)
                {
                    shop[3, i] = Rand(4, 10);
                }
                ///Цена
                shop[4, i] = Rand(PlayerLvl, PlayerLvl * 5) + Rand(shop[2, i], shop[2, i] * 4) + Rand(rarity/2, rarity - rarity/2);

            }
            return shop;
        }

        static void ShopBuyMessage(int select)
        {
           
            for (int i = 0; i < PlayerInv.GetLength(1); i++)
            {
                int invId = (ShopItem[0, select] == 0) ? i : 7;
                if (invId == 7 && PlayerInv[1, 7] == 0 && ShopItem[0,select] != 0 || ShopItem[1, select] == i && invId != 7 && PlayerInv[1, i] == 0)
                {
                    code = 0; pun = 1; kur[0] = '>';
                    do
                    {
                        ShowStats();
                        SetColor(Color[ShopItem[3, select]]);
                        Console.WriteLine("Слот [{0}] свободен, желаете приобрести товар?\nЦена:{1}\nКачество:{2}\nБафф:+{3}", ArmorArray[invId], ShopItem[4, select], ItemRary[ShopItem[3, select]], ShopItem[2, select]);

                        ResetColor();
                        Console.WriteLine("{0}[Да]\t\t{1}[Нет]", kur[0], kur[1]);
                        code = Kur(2);
                    } while (code == 0);
                    if (code == 1)
                    {
                        if (Money >= ShopItem[4, select])
                        {
                            Money -= ShopItem[4, select];
                            ShowStats();
                            Console.Write("Поздравляю с покупкой!\n>Спасибо");
                            for (int j = 0; j < 3; j++)
                            {
                                PlayerInv[j, invId] = ShopItem[j + 1, select];
                            }
                            ShopItem[2, select] = 0;
                            Console.ReadKey();
                            Shop();
                        }
                        else
                        {
                            ShowStats();
                            Console.Write("У тебя денег не хватает!\n>Назад");
                            Console.ReadKey();
                            Shop();
                        }
                        
                    }
                    else
                    {
                        Shop();
                    }

                }
                else if (invId == 7 && PlayerInv[1, 7] !=0 && ShopItem[0,select] == 1|| PlayerInv[1, i] != 0 && ShopItem[1, select] == i && ShopItem[0,select] == 0)
                {
                    Clear();
                    Console.WriteLine("Слот [{0}] занят! Продайте этот предмет, чтобы освободить место\n>Назад", (ShopItem[0, select] == 0) ? ArmorArray[ShopItem[1, select]] : ArmorArray[7]);
                    Console.ReadKey();
                    Shop();
                }
                
            }
        }

        static void Shop()
        {
            InvBaff();
            if (ShopUpdate)
            {
                ShopUpdate = false;                
                ShopItem = ShopSystem();
            }
            code = 0; pun = 1; kur[0] = '>';
            int select;
            do
            {
                ShowStats();
                Console.WriteLine("\tМагазин\nТовары на сегодня:");
                for (int i = 0; i < ShopItem.GetLength(1); i++)
                {
                    SetColor(Color[ShopItem[3, i]]);
                    if (ShopItem[0, i] == 0)
                    {
                        Console.WriteLine("{0}{1}", kur[i], ArmorArray[ShopItem[1, i]]);
                    }
                    else
                    {

                        Console.WriteLine("{0}{1}", kur[i], WeaponArray[ShopItem[1, i]]);
                    }
                    ResetColor();
                }
                Console.WriteLine("{0}[Уйти]", kur[ShopItem.GetLength(1)]);
                
                select = pun - 1;
                if (select < 5)
                {
                    Console.WriteLine("\n-----------------------------------\n");
                    if (ShopItem[2, select] == 0)
                    {
                        Console.WriteLine("Продано!");
                    }
                    else
                    {
                        SetColor(Color[ShopItem[3, select]]);

                        Console.WriteLine("{0}\nКачество:{1}\nЦена:{2}\nБафф:+{3}", (ShopItem[0, select] == 0) ? ArmorArray[ShopItem[1, select]] : ArmorArray[7], ItemRary[ShopItem[3, select]], ShopItem[4, select], ShopItem[2, select]);
                    }
                }
                ResetColor();
                ShowInv();
                code = Kur(6);
            } while (code == 0);
            select = pun - 1;
            if (code > 0 && code < 6)
            {
                if (ShopItem[2, select] == 0)
                {
                    Clear();
                    Console.WriteLine("Товар продан!\nВыбери что-то другое, или приходи в другой раз.");
                    Console.ReadKey();
                    Shop();
                }
                else
                {
                    ShopBuyMessage(select);
                }
            }
            else
            {
                Menu();
            }
        }

        static void Seller()
        {
            InvBaff();
            int temp = 0;
            int[] num = new int[8];
            code = 0; pun = 1; kur[0] = '>';
            do
            {
                temp = 0;
                ShowStats();
                ShowInv();
                int y = Console.CursorTop;
                for (int i = 0; i < PlayerInv.GetLength(1); i++)
                {
                    
                    if (PlayerInv[1, i] != 0)
                    {
                        num[temp] = i;
                        ++temp;
                    }
                    
                }
                for (int i = 0; i < temp; i++)
                {
                    SetCursor(num[i] * 14, y + 1);
                    Console.Write("{0}Продать", kur[i]);

                }
                SetCursor(0, y + 2);
                Console.Write("\n{0}Назад", kur[temp]);
                code = Kur(temp+1);
            } while (code == 0);
            if (code < temp + 1)
            {
                int sel = num[code-1];
                int price = Rand(0, PlayerLvl) + Rand(PlayerInv[1, sel], PlayerInv[1, sel] * 4) + Rand(PlayerInv[2, sel] / 2, PlayerInv[2, sel] - PlayerInv[2, sel] / 2);
                Money += price;
                for (int i = 0; i < ShopItem.GetLength(1); i++)
                {
                    if (PlayerInv[0, sel] == ShopItem[1, i] && ShopItem[2,i] == 0)
                    {
                        ShopItem[2, i] = PlayerInv[1, sel];
                    }
                }
                for (int i = 0; i<3; i++)
                {
                    PlayerInv[i, sel] = 0;                              
                }
                Seller();
            }
            else
            {
                BuyOrSell();
            }
        }
        static void BuyOrSell()
        {
            code = 0; pun = 1; kur[0] = '>';
            do
            {
                ShowStats();
                SetColor("Yellow");
                Console.WriteLine("\nПривет, вы у торговца.\nЧто именно вы хотите сделать?");
                ResetColor();
                Console.WriteLine("{0}Купить\n{1}Продать\n{2}Назад", kur[0], kur[1], kur[2]);
                ShowInv();
                code = Kur(3);
            } while (code == 0);
            if (code == 1)
            {
                Shop();
            }
            else if (code == 2)
            {
                Seller();
            }
            else
            {
                Menu();
            }
        }
        static void Menu()
        {
            InvBaff();
            code = 0; pun = 1; kur[0] = '>';
            do
            {
                ShowStats();
                Console.WriteLine("{0}Искать монстров\n{1}Вылечиться\n{2}Торговец\n{3}Выход", kur[0],kur[1], kur[2], kur[3]);
                ShowInv();
                code = Kur(4);
            } while (code == 0);
            if (code == 1)
            {
                Battle();
            }
            else if (code == 2)
            {
                Save();
                HealRoom();
                
            }
            else if (code == 3)
            {
                BuyOrSell();
            }
            else
            {
                
                Environment.Exit(0);
            }
            

        }

        static void Battle()
        {
            ShopUpdate = true;
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
            Directory.CreateDirectory(PathToSave);
            nickname = Console.ReadLine();
            if (nickname == "God")
            {
                Money = 1000;
            }
            else
            {
                Money = 0;
            }
            PlayerHp = 100;
            PlayerLvl = 1;
            KillMob = 0;
            PlayerXp = 0;
            PlayerAtt = 5;
            PlayerDeff = 1.5;
            
            
        }
        static bool IsLoad()
        {
            FileInfo file = new FileInfo(PathToSave + "\\save.ini");

            if (file.Exists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void Start()
        {
            if (IsLoad())
            {
                code = 0; pun = 1; kur[0] = '>';
                do
                {
                    Clear();
                    Console.WriteLine("{0}Загрузить сохранение\n{1}Новая Игра\n{2}Выйти", kur[0], kur[1], kur[2]);
                    code = Kur(3);
                } while (code == 0);
                switch (code)
                {
                    case 1:
                        Load();
                        break;
                    case 2:
                        NewGame();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
            else
            {
                NewGame();
            }
        }
        static void Load()
        {
            if (IsLoad())
            {
                
                string[] load = File.ReadAllLines(PathToSave + "\\save.ini", Encoding.UTF8);
                int i = 24;
                int sep = 0;
                for (int l = 0; l < 3; l++)
                {
                    for (int s = 0; s < 8; s++)
                    {
                        PlayerInv[l, s] = Convert.ToInt32(load[sep]);
                        sep++;
                    }


                }
                PlayerHp = 100;

                PlayerLvl = Convert.ToInt32(load[i]);
                PlayerXp = Convert.ToInt32(load[i+1]);
                KillMob = Convert.ToInt32(load[i+2]);
                Money = Convert.ToInt32(load[i+3]);
                nickname = load[i+4];

               

            }
        }
        static void Save()
        {
            FileInfo file = new FileInfo(PathToSave + "\\save.ini");
            //StreamWriter sw = new StreamWriter(PathToSave + "\\save.ini", true, Encoding.ASCII);
            int i = 0;
            for (int k = 0; k < 3; k++)
            {
                for (int s = 0; s < 8; s++)
                {
                    SaveInt[i] = Convert.ToString(PlayerInv[k,s]);
                    i++;
                }
            
                
            }
            SaveInt[i] = Convert.ToString(PlayerLvl);
            SaveInt[i + 1] = Convert.ToString(PlayerXp);
            SaveInt[i + 2] = Convert.ToString(KillMob);
            SaveInt[i + 3] = Convert.ToString(Money);
            SaveInt[i + 4] = Convert.ToString(nickname);
            File.WriteAllLines(PathToSave + "\\save.ini", SaveInt);
        }
        static void Main(string[] args)
        {
            Console.Title = "Legend of Hero";
            ResetColor();
            Start();
            InvBaff();
            ShowStats();
            Menu();
        }
    }
}
