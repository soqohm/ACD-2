using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
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
        public static void PrintOnlyEvenDigits(LinkedList<int> list)
        {
            if (list.First is null) return;
            if (list.First.Value % 2 == 0) Console.WriteLine(list.First.Value);
            list.RemoveFirst();
            PrintOnlyEvenDigits(list);
        }

        // Печать элементов списка с четными индексами
        public static void PrintOnlyEvenElements<T>(LinkedList<T> list)
        {
            void Print(LinkedListNode<T> node, bool isEvenIndex)
            {
                if (node is null) return;
                if (isEvenIndex) Console.WriteLine(node.Value);
                Print(node.Next, !isEvenIndex);
            }
            Print(list.First, false);
        }

        // Нахождение второго максимального числа в списке
        public static void Test7()
        {
        }

        // Поиск всех файлов в заданном каталоге по всей иерархии вниз
        public static void Test8()
        {
        }
    }
}