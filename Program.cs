using System;
namespace MathForMariaVasilevna
{
    class Program
    {
        static void Main()
        {
            Random random  = new Random();
            Prime prime = new Prime();
            prime.diapazone = 1000;
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(prime.CreatePrime(220, 5));
            }
            Console.ReadLine();
        }
    }
    class Prime
    {
        public int diapazone { get; set; }
        public string CreatePrime(int answer, int length)
        {
            Random random = new Random();
            int[] scobes = new int[2];
            int[] values = {answer};
            char[] operations = new char[0];
            string result = string.Empty;
            for (int i = 0; i < length; i++)
            {
                MakeSum(ref values, ref operations, random.Next(values.Length));
            }
            for (int i = 0; i < 5; i++)
            {
                result += values[i].ToString();
                result += " " + operations[i] + " ";
            }
            result += values.Last().ToString();
            return result;
        }
        void MakeSum(ref int[] vals,ref char[] opers, int index)
        {
            Random rand = new Random();
            int sum = vals[index];
            int sndval = rand.Next(-diapazone, diapazone);
            DeleteItem(ref vals, index);
            AddItem(ref vals, index, sndval);
            AddItem(ref vals, index + 1, sum - sndval);
            AddItem(ref opers, index, '+');
        }
        void AddItem(ref int[] array, int index, int value)
        {
            int[] newarr = new int[array.Length + 1];
            for (int i = 0; i < index; i++)
            {
                newarr[i] = array[i];
            }
            newarr[index] = value;
            for (int i = index + 1; i < newarr.Length; i++)
            {
                newarr[i] = array[i - 1];
            }
            array = newarr;
        }
        void AddItem(ref char[] array, int index, char operation)
        {
            char[] newarr = new char[array.Length + 1];
            for (int i = 0; i < index; i++)
            {
                newarr[i] = array[i];
            }
            newarr[index] = operation;
            for (int i = index + 1; i < newarr.Length; i++)
            {
                newarr[i] = array[i - 1];
            }
            array = newarr;
        }
        void DeleteItem(ref int[] array, int index)
        {
            int[] newarr = new int[array.Length - 1];
            for (int i = 0; i < index; i++)
            {
                newarr[i] = array[i];
            }
            for (int i = index; i < newarr.Length; i++)
            {
                newarr[i] = array[i + 1];
            }
            array = newarr;
        }

        string NegativeScobe(int value)
        {
            if (value < 0)
            {
                return "(" + value + ")";
            }
            else
            {
                return value.ToString();
            }
        }
    }
}