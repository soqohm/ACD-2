using System;
using System.Collections.Generic;
using System.IO;

namespace AlgorithmsDataStructures20
{
    public static class Recursion
    {
        // Возведение числа N в степень M
        public static double RaiseDigitToDegree(double digit, int degree)
        {
            if (degree == 0) return 1;
            if (degree < 0) return 1 / RaiseDigitToDegree(digit, -degree);
            return digit * RaiseDigitToDegree(digit, degree - 1);
        }

        // Вычисление суммы цифр числа
        public static uint CalcSumOfDigits(uint sourceOfDigits)
        {
            if (sourceOfDigits == 0) return 0;
            return (sourceOfDigits % 10) + CalcSumOfDigits(sourceOfDigits / 10);
        }

        // Расчет длины списка, для которого разрешены только операции удаления первого элемента и получение длины
        public static int CalcLengthOfList<T>(LinkedList<T> list)
        {
            if (list.First is null) return 0;
            list.RemoveFirst();
            return CalcLengthOfList(list) + 1;
        }

        // Проверка, является ли строка палиндромом
        public static bool IsPalindrome(string text)
        {
            text = System.Text.RegularExpressions.Regex.Replace(text.ToLower(), @"[^a-zа-я0-9]", "");
            
            bool Check(int leftIndex, int rightIndex)
            {
                if (leftIndex >= rightIndex) return true;
                if (text[leftIndex] != text[rightIndex]) return false;
                return Check(leftIndex + 1, rightIndex - 1);
            }
            if (text.Length <= 1) return false;
            return Check(0, text.Length - 1);
        }

        // Печать только четных значений из списка

        //public static void PrintOnlyEvenDigits(LinkedList<int> list) // предыдущая версия
        //{
        //    if (list.First is null) return;
        //    if (list.First.Value % 2 == 0) Console.WriteLine(list.First.Value);
        //    list.RemoveFirst();
        //    PrintOnlyEvenDigits(list);
        //}

        public static void PrintOnlyEvenDigits(List<int> list) // поправил "список менять уже нельзя"
        {
            void Print(int index)
            {
                if (index + 1 > list.Count) return;
                if (list[index] % 2 == 0) Console.WriteLine(list[index]);
                Print(index + 1);
            }
            Print(0);
        }

        // Печать элементов списка с четными индексами

        //public static void PrintOnlyEvenElements<T>(LinkedList<T> list) // предыдущая версия 1
        //{
        //    void Print(LinkedListNode<T> node, bool isEvenIndex)
        //    {
        //        if (node is null) return;
        //        if (isEvenIndex) Console.WriteLine(node.Value);
        //        Print(node.Next, !isEvenIndex);
        //    }
        //    Print(list.First, false);
        //}

        //public static void PrintOnlyEvenElements<T>(List<T> list) // предыдущая версия 2
        //{
        //    void Print(int index)
        //    {
        //        if (index + 1 > list.Count) return;
        //        if ((index + 1) % 2 == 0) Console.WriteLine(list[index]);
        //        Print(index + 1);
        //    }
        //    Print(0);
        //}

        public static void PrintOnlyEvenElements<T>(List<T> list) // поправил "одна проверка лишняя"
        {
            void Print(int index)
            {
                if (index + 1 > list.Count) return;
                Console.WriteLine(list[index]);
                Print(index + 2);
            }
            Print(1);
        }

        // Нахождение второго максимального числа в списке (с учетом, что максимальных может быть несколько)

        //public static List<int> FindSecondMaxValues(List<int> list) // предыдущая версия 1
        //{
        //    var maxValues = new List<int>();
        //    var secondMaxValues = new List<int>();

        //    void Find(int index)
        //    {
        //        if (index + 1 > list.Count) return;
        //        SetMaxValues(list[index], ref maxValues, ref secondMaxValues);
        //        Find(index + 1);
        //    }
        //    Find(0);
        //    return secondMaxValues;
        //}

        //static void SetMaxValues(int value, ref List<int> maxValues, ref List<int> secondMaxValues)
        //{
        //    if (maxValues.Count == 0)
        //        maxValues = new List<int>() { value };
        //    else if (value == maxValues[0])
        //        maxValues.Add(value);
        //    else if (value > maxValues[0])
        //    {
        //        secondMaxValues = maxValues;
        //        maxValues = new List<int>() { value };
        //    }
        //    else if (secondMaxValues.Count == 0)
        //        secondMaxValues = new List<int>() { value };
        //    else if (value == secondMaxValues[0])
        //        secondMaxValues.Add(value);
        //    else if (value > secondMaxValues[0] && value < maxValues[0])
        //        secondMaxValues = new List<int>() { value };
        //}

        //public static int? FindSecondMaxValue(List<int> list) // предыдущая версия 2
        //{
        //    var max = int.MinValue;
        //    var secondMax = int.MinValue;

        //    void Find(int index)
        //    {
        //        if (index + 1 > list.Count) return;
        //        if (list[index] > max)
        //        {
        //            secondMax = max;
        //            max = list[index];
        //        }
        //        else if (list[index] > secondMax)
        //            secondMax = list[index];
        //        Find(index + 1);
        //    }
        //    Find(0);
        //    return secondMax == int.MinValue ? (int?)null : secondMax;
        //}

        public static int? FindSecondMaxValue(List<int> list) // поправил "не брать краевые константы"
        {
            if (list.Count < 2) return null;
            var max = Math.Max(list[0], list[1]);
            var secondMax = Math.Min(list[0], list[1]);

            void Find(int index)
            {
                if (index + 1 > list.Count) return;
                if (list[index] > max)
                {
                    secondMax = max;
                    max = list[index];
                }
                else if (list[index] > secondMax)
                    secondMax = list[index];
                Find(index + 1);
            }
            Find(2);
            return secondMax;
        }

        // Поиск всех файлов в заданном каталоге по всей иерархии вниз

        //public static List<string> RecursiveFileSearch(string startPath) // предыдущая версия
        //{
        //    var files = new List<string>();
        //    if (!Directory.Exists(startPath)) return files;

        //    void Find(string path)
        //    {
        //        files.AddRange(Directory.GetFiles(path));
        //        foreach (var e in Directory.GetDirectories(path))
        //            Find(e);
        //    }
        //    Find(startPath);
        //    return files;
        //}

        public static IEnumerable<string> RecursiveFileSearch(string startPath) // поправил "сделать одной функцией"
        {
            foreach (var e in Directory.GetFiles(startPath))
                yield return e;
            foreach (var e in Directory.GetDirectories(startPath))
                RecursiveFileSearch(e);
        }
    }
}