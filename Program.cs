using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace za1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите размерность массива(ширина и высота)\nСтолбцы:");
            int numy = Int32.Parse(Console.ReadLine());
            Console.Write("Строки:");
            int numx = Int32.Parse(Console.ReadLine());
            int[,] nums = new int[numx, numy]; //Объявление двумерного массив с указанным количеством элементов           
            int max, min;
            int sum = 0, sumx = 0, sumd = 0;
            int polozh = 0; 

            Console.WriteLine($"Введите {numx * numy} чисел");

            for (int i = 0; i < numx; i++)
            {
                for (int j = 0; j < numy; j++)
                {
                    Console.Write($"[{i}][{j}]=");
                    nums[i, j] = Convert.ToInt32(Console.ReadLine()); // Ввод элементов в массив        
                    if (nums[i,j] > 0)
                    {
                        polozh++;
                    }
                }
            }

            int[] nums2 = new int[polozh]; //Объявление одномерного массив

            for (int i = 0; i < numy; i++)
            {
                if (i == 0) Console.Write("\t");
                Console.Write($"[{i}]\t");
            }
            Console.WriteLine();
            for (int i = 0; i < numx; i++)
            {
                for (int j = 0; j < numy; j++)
                {
                    if (j == 0)
                    {
                        Console.Write($"[{i}]\t");
                    }
                    Console.Write($"{nums[i, j]}\t");
                }
                Console.WriteLine();
            }

            Console.Write("\n3 задание\t4 задание\t5 задание\t\n");
            int temp = 0;
            for (int i = 0; i < numx; i++)
            {
                for (int j = 0; j < numy; j++)
                {
                    if(nums[i,j] < 0)
                    {
                        Console.Write($"|{nums[i, j]}|\t\t|{nums[i, j]* nums[i, j]}|\t\t");
                    }
                    else
                    {
                        Console.Write("\t\t\t\t");
                    }
                    if(nums[i,j] % 3 == 0 && nums[i,j] !=0)
                    {
                        Console.Write($"|{nums[i, j]}|");
                    }
                    else
                    {
                        Console.Write("\t");
                    }
                    Console.Write("\n");

                    if(nums[i,j] % 5 == 0)
                    {
                        nums[i, j] = 0;
                    }
                    sum += nums[i, j]; // сумма всех 7 задание
                    if (i == j)
                    {
                        sumd += nums[i, j]; // сумма диагонали 9 задание
                    }
                    if (nums[i, j] > 0)
                    {
                        nums2[temp] = nums[i, j]; // перенос в одномерный массив положительные элементы
                        temp++;
                    }

                }
                
            }


            Console.ReadKey();
        }
    }
}
