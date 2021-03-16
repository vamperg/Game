using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Game
{
    class Program
    {
        static int code, pun;
        static char[] kur = new char[255];
        static string PathToSave = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Legend of Hero";
        static string[] SaveInt = new string[255];
        static double GlobalTime = 0;
        static int Fight = 0;
        static int Report = 0;
        static int BattlePow = 0;
        static int RecordBattlePow = 0;
        //Player
        public static string nickname;
        static bool ShopUpdate = true;
        static int[,] ShopItem = new int[5, 5];
        static int TimeShop = 30000;
        static int PlayerMaxHp;
        static int PlayerHp;
        static int PlayerLvl;
        static int PlayerXp;
        static int PlayerAtt;
        static double PlayerDeff;
        static int[,] PlayerInv = new int[3, 8];
        static int GuildRank = 0;
        static string[] GRankTitle = { "Медный", "Железный", "Серебряный", "Золотой", "Платиновый", "Адамантовый" };
        static int Money;
        static string[] ItemRary = { "Ветхий", "Почти новый", "Новый", "Стильный", "Многообещающий", "Отличный", "Элитный", "Королевский", "Драконий", "Проклятый", "Подарок от дедушки" };
        static string[] ArmorArray = { "Шлем", "Нагрудник", "Наплечник", "Наручи", "Перчатки", "Наколенники", "Поножи" ,"Оружие"};
        static string[] WeaponArray = { "Деревянная Палка", "Камень", "Удочка" ,"Топор","Лопата","Вилы","Ведро","Нож","Кинжал","Меч","Копье","Булава","Лук", "Посох Мага", "Позвонить другу"};
        static int KillMob = 0;
        ////////
        static string[] Color = { "Gray", "Cyan", "DarkGreen", "Green", "DarkMagenta", "Magenta", "DarkBlue","Blue", "DarkRed", "Red", "Yellow" };
        static string[] EnemyNameArray = {"Мышь", "Паук", "Волк", "Гоблин", "Страж","Гном","Кабан","Змея","Скелет","Мертвец","Слизень","Орк","Бандит","Демон","Гаргулья", "Киреял"};
        static string EnemyName;
        static int EnemyHp;
        static int EnemyLvl;
        static int EnemyAtt;
        static int[] EnemyPlaceLvl = { 2, 4, 7, 10 };
        static int[] EnemyAttArray = { 5, 10, 60, 90, 140, 255, 570, 990, 2125, 5150 };
        static int[] EnemyHpArray = { 20, 50, 400, 750, 2000, 7400, 14500, 35800, 57000, 99500};
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
                    
                    kur[maxi - 1] = '#';
                    return 0;
                }
                else
                {
                    kur[pun - 1] = ' ';
                    kur[pun - 2] = '#';
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
                    kur[0] = '#';
                    return 0;
                }
                else
                {
                    kur[pun - 1] = ' ';
                    kur[pun] = '#';
                    pun++;
                    return 0;
                }
            }
            
            else return 0;
        }

        static void ResetColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
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

        static void Help()
        {
            Clear();
            SetCursor(Console.WindowWidth / 3+10, Console.CursorTop);
            Console.WriteLine("П О М О Щ Ь");
            Console.WriteLine("УПРАВЛЕНИЕ\nA|D или W|S и Enter чтобы подтвердить");
            Console.WriteLine();
            Console.ReadKey();
        }

        static void Death()
        {
            code = 0; pun = 1; kur[0] = '>';
            do
            {
                Clear();
                Console.WriteLine("Ты погиб");
                Console.WriteLine("Загрузить ближайшее сохранение?\n{0}Да\n{1}Нет", kur[0], kur[1]);
                code = Kur(2);
            } while (code == 0);
            if (code == 1)
            {
                Load();
                Menu();
            }
            else
            {
                Start();
            }
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
                    PlayerDeff += Math.Round(PlayerInv[2, i] / 10.0, 2);
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
            EnemyHp = Rand(EnemyHpArray[LvlId] / 2, EnemyHpArray[LvlId] + EnemyHpArray[LvlId] / 2) + Rand(PlayerHp/4, PlayerHp/2) + Rand(PlayerAtt/4, PlayerAtt);
            EnemyAtt = Rand(EnemyAttArray[LvlId] / 2, EnemyAttArray[LvlId] + EnemyAttArray[LvlId] / 2) + Rand(PlayerAtt / 4, PlayerAtt / 2);
        }

        static int DropMoney()
        {
            int money = Rand(Convert.ToInt32((EnemyLvl * 2 + EnemyHp + EnemyAtt*1.5) * 0.08), Convert.ToInt32((EnemyLvl *  2 + EnemyHp + EnemyAtt*1.5) * 0.2));
            if (BattlePow > 0)
            {
                money += Rand(Convert.ToInt32((EnemyLvl * 2 + EnemyHp + EnemyAtt) * 0.1), Convert.ToInt32((EnemyLvl * 2 + EnemyHp + EnemyAtt) * 0.14) * BattlePow);
            }
            return money;
        }

        static void WinMesg(int drop)
        {
            Clear();
            
            if (Report < drop && PlayerLvl > 1)
            {
                drop -= Report;
                Report = 0;
            }


            Money += drop;
            KillMob += 1;
            code = 0; pun = 1; kur[0] = '>';
            do
            { 
                ShowStats();
                SetColor("Yellow");
                Console.WriteLine("Победа! Гильдия заплатила вам {0} монет с этого монстра\nЖелаете продолжить искать врагов или же хотите вернуться домой?", drop);
                
                ResetColor();
                Console.WriteLine("{0}В бой!\n{1}Вернуться", kur[0], kur[1]);
                code = Kur(2);
            } while (code == 0);
            if (code == 1)
            {
                BattlePow++;
                Battle();
                
            }
            else
            {
                Guild();
                BattlePow = 0;
            }
        }
        static void ShowStats()
        {
            Clear();
            SetColor("Cyan");
            Console.WriteLine("{0}|Здоровье:{1}|Сила:{2}|Защита:{3}|Уровень:{4}[{5}]|Деньги:{6}|Убито монстров:{7}|Прошло времени:{8}Сек.Bat{9}\n", nickname, PlayerHp, PlayerAtt, PlayerDeff, PlayerLvl, PlayerXp, Money, KillMob,GlobalTime/1000, BattlePow);
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
            SetCursor(Console.WindowWidth /2 - Console.WindowWidth/10, Console.CursorTop);
            Console.WriteLine("Инвентарь");
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
                int WeaponStatus = shop[0, i] == 1 ? Rand(shop[1, i] * 2, shop[1, i] * 5) : 0;
                //Бафф
                if (shop[0, i] == 1)
                {
                    shop[2, i] = 1 + Rand(PlayerLvl / 20, PlayerLvl / 15) + Rand(0, KillMob/4+1) + Rand(0, Place * 3 + 1) + WeaponStatus / 4;
                }
                else
                {
                    shop[2, i] = 3 + Rand(PlayerLvl / 2, PlayerLvl) + Rand(KillMob/4, KillMob * 2) + Rand(0, Place * 2 + 1);
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
                    shop[3, i] = Rand(0, 7);
                }
                else if (rarity >= 98 && rarity <=100)
                {
                    shop[3, i] = Rand(2, 10);
                }
                ///Цена
                
                shop[4, i] = Rand(PlayerLvl, PlayerLvl * 5) + Rand(shop[2, i]*4, shop[2, i] * 8) + Rand(rarity / 2, rarity - rarity / 2) + WeaponStatus;

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
                        Console.WriteLine("{0}[Да]\n{1}[Нет]", kur[0], kur[1]);
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
                            SetColor("Red");
                            Console.Write("У тебя денег не хватает!\n>Назад");
                            ResetColor();
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
            Save();
            Clear();
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
                SetColor("Yellow");
                SetCursor(Console.WindowWidth / 3, Console.CursorTop+1);
                Console.WriteLine("Л У Ч Ш А Я  Л А В К А");
                SetCursor(Console.WindowWidth / 4, Console.CursorTop);
                Console.WriteLine("Товары на сегодня(До обновления товаров:{0})\n", TimeShop/1000);
                for (int i = 0; i < ShopItem.GetLength(1); i++)
                {
                    SetColor(Color[ShopItem[3, i]]);
                    if (ShopItem[0, i] == 0)
                    {
                        SetCursor(Console.WindowWidth / 16, Console.CursorTop);
                        Console.WriteLine("{0}{1}", kur[i], ArmorArray[ShopItem[1, i]]);
                    }
                    else
                    {
                        SetCursor(Console.WindowWidth / 16, Console.CursorTop);
                        Console.WriteLine("{0}{1}", kur[i], WeaponArray[ShopItem[1, i]]);
                    }
                    ResetColor();
                }
               
                SetCursor(Console.WindowWidth / 16, Console.CursorTop);
                Console.WriteLine("{0}[Уйти]", kur[ShopItem.GetLength(1)]);
                
                select = pun - 1;
                SetCursor(0, Console.CursorTop + 2);
                for (int i = 0; i < Console.WindowWidth / 4; i++)
                {
                    Console.Write("----");
                }
                if (select < 5)
                {
                    
                    if (ShopItem[2, select] == 0)
                    {
                        SetCursor(Console.WindowWidth / 4, Console.CursorTop);
                        Console.WriteLine("Продано!");
                    }
                    else
                    {
                        SetColor(Color[ShopItem[3, select]]);
                        SetCursor(Console.WindowWidth / 2, 5);
                        Console.WriteLine("{0} Качество:{1}", (ShopItem[0, select] == 0) ? ArmorArray[ShopItem[1, select]] : ArmorArray[7], ItemRary[ShopItem[3, select]]);
                        SetCursor(Console.WindowWidth / 2, 6);
                        Console.WriteLine("Цена:{0}", ShopItem[4, select]);
                        SetCursor(Console.WindowWidth / 2, 7);
                        Console.WriteLine("Бафф:+{0}",  ShopItem[2, select]);
                        ResetColor();
                    }
                }
                ResetColor();
                SetCursor(0, 15);
                ShowInv();
                code = Kur(6);
            } while (code == 0);
            select = pun - 1;
            if (code > 0 && code < 6)
            {
                if (ShopItem[2, select] == 0)
                {
                    Clear();
                    SetCursor(Console.WindowWidth / 2, 5);
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
                BuyOrSell();
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
                SetColor("Magenta");
                SetCursor(7 * 14, y + 4);
                Console.Write("{0}Назад", kur[temp]);
                ResetColor();
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
            Clear();
            code = 0; pun = 1; kur[0] = '>';
            do
            {
                ShowStats();
                SetColor("Yellow");
                Console.WriteLine("\n\t\t\t\tП Р И В Е Т,  В Ы  У  Т О Р Г О В Ц А !\n\t\t\t\t   ЧТО ИМЕННО ВЫ ХОТИТЕ СДЕЛАТЬ?");
                ResetColor();
                Console.WriteLine("{0}Купить снаряжение\n{1}Продать старые вещи\n{2}Назад", kur[0], kur[1], kur[2]);
                SetCursor(0, 10);
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

        static void Lazary()//Будет дорабатываться
        {
            PlayerHp = PlayerMaxHp;
            if (Money > 30 && PlayerLvl > 1)
            Money -= Rand(5, 30);
            Guild();
        }
        static void Guild()
        {
            Save();
            code = 0; pun = 1; kur[0] = '#';
            do
            {
                Clear();
                SetColor("Yellow");               
                SetCursor(Console.WindowWidth / 4, Console.CursorTop);
                Console.WriteLine("Д О Б Р О  П О Ж А Л О В А Т Ь  В  Г И Л Ь Д И Ю!");                
                SetColor("Green");
                SetCursor(Console.WindowWidth / 4-2, Console.CursorTop+1);
                Console.WriteLine("ТУТ ТЫ НАЙДЕШЬ ВСЕ, ЧТОБЫ СТАТЬ ИЗВЕСТНЫМ И БОГАТЫМ!");
                ResetColor();
                SetColor("Red");
                Console.WriteLine("\n\tТВОЙ РАНГ В ГИЛЬДИИ:{0}\t\tШТРАФЫ:{1}\t\tУБИТО МОНСТРОВ:{2}\tРЕКОРД УБИТЫХ:{3}", GRankTitle[GuildRank], Report, KillMob, RecordBattlePow);
                SetColor("Cyan");
                SetCursor(Console.WindowWidth / 4, Console.CursorTop + 1);
                Console.WriteLine("[{0}]\tЗдоровье:{1}\tСила:{2}\tЗащита:{3}\tМонет:{4}",nickname, PlayerHp, PlayerAtt, PlayerDeff, Money);
                ResetColor();
                Console.WriteLine("\n{0}Зачистка подземелий\n{1}Лазарет\n{2}Оплатить штрафы\n{3}Уйти", kur[0], kur[1], kur[2],kur[3]);
                code = Kur(4);
            } while (code == 0);
            if(code == 1)
            {
                Battle();
            }
            else if(code == 2)
            {
                Lazary();
            }
            else if(code == 3)
            {
                if (Money > Report)
                {
                    if (Report != 0)
                    {
                        Money -= Report;
                        Report = 0;
                        Guild();
                    }
                    Guild();
                }
                Guild();
            }
            else
            {
                Menu();
            }
        }

        static void Menu()
        {
            InvBaff();
            Save();
            
            code = 0; pun = 1; kur[0] = '>';
            do
            {
                ShowStats();
                Console.WriteLine("{0}Гильдия\n{1}Торговец\n{2}Помощь по игре\n{3}Выход", kur[0],kur[1] ,kur[2], kur[3]);
                ShowInv();
                code = Kur(4);
            } while (code == 0);
            if (code == 1)
            {
                Guild();
            }
            else if (code == 3)
            {
                Help();
            }
            else if (code == 2)
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
                            Fight = 1;
                            EnemyHp -= Rand(PlayerAtt / 2, PlayerAtt + PlayerAtt / 2);
                            int EnemyAttack = Convert.ToInt32(EnemyAtt / PlayerDeff);
                            PlayerHp -= Rand(EnemyAttack / 2, EnemyAttack + EnemyAttack / 2);
                        }
                        else
                        {
                            Clear();
                            if (BattlePow > 1)
                            {
                                Console.WriteLine("Поздравляю, ты смог выдержать {0} {1}!\nТы молодец!",BattlePow, BattlePow < 5 ? "Монстра" : "Монстров" );
                                Thread.Sleep(500);
                                Console.ReadKey();
                                RecordBattlePow = BattlePow;
                                BattlePow = 0;
                            }
                            if (Fight == 1)
                            {
                                SetColor("Red");
                                Report = Rand(1, Convert.ToInt32((EnemyLvl * 1.2 + EnemyHp + EnemyAtt * 1.5) * 0.06));
                                Console.Write("За побег начисляется штраф и при следующей награде гильдия их будет взыскать!\nТвой общий штраф:{0}\n>Вернуться домой", Report);
                                Console.ReadKey();
                                Fight = 0;
                                Guild();
                            }
                            Guild();

                        }
                    }
                    else
                    {
                        Fight = 0;
                        WinMesg(drop);
                    }
                }
                else
                {
                    Fight = 0;
                    Death();
                }
            }
        }

        static void NewGame()
        {
            Clear();
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
                GlobalTime = Convert.ToDouble(load[i + 5]);
                RecordBattlePow = Convert.ToInt32(load[i + 6]);
                Report = Convert.ToInt32(load[i + 7]);

               

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
            SaveInt[i + 5] = Convert.ToString(GlobalTime);
            SaveInt[i + 6] = Convert.ToString(Report);
            SaveInt[i + 7] = Convert.ToString(RecordBattlePow);
            File.WriteAllLines(PathToSave + "\\save.ini", SaveInt);
        }
        static void Time(object args)
        {
            TimeShop -= 100;
            if(TimeShop <= 0){
                TimeShop = 30000;
                ShopUpdate = true;
            }
            GlobalTime += 100;
            Console.SetWindowSize(120, 30);
        }
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetBufferSize(120, 30);
            Timer persec = new Timer(Time, null, 0, 100);
            Console.Title = "Legend of Hero";
            ResetColor();
            Start();
            Menu();
        }
    }
}
