using Newtonsoft.Json;
using System.IO;

namespace Math_For_Maria_Vasilevna;

    public class PrimeGenerator
    {
        public PrimeGenerator(string SimpleNumsPath)
        {
            DBFilePath = SimpleNumsPath;
            Simples = JsonConvert.DeserializeObject<List<int>>(File.ReadAllText(DBFilePath));
        }
        string DBFilePath;
        List<int> Simples;
        public int SumDiapazoneMultiplier { get; set; }
        public int SumDefaultDiapazone { get; set; }
        public int DevideDiapazone { get; set; }
        public Prime CreatePrime(int answer, int length)
        {
            Random rand = new Random();
            Prime prime = new Prime();
            
            prime.Values.Add(answer);
            prime.Scobes.AddRange(new int[2]); 

            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(MakeNegativeScobes(prime).ToString());
                
                switch (rand.Next(4))
                {
                    case 0:
                        MakeSum(ref prime,rand.Next(prime.Values.Count));
                        break;
                    case 1:
                        MakeSubtraction(ref prime,rand.Next(prime.Values.Count));
                        break;
                    case 2:
                        MakeDevide(ref prime,rand.Next(prime.Values.Count));
                        break;
                    case 3:
                        MakeMultiplication(ref prime,rand.Next(prime.Values.Count));
                        break;
                }
            }
            return prime;
        }
        void MakeSum(ref Prime prime,int index)
        {
            Random rand = new Random();
            int sum = prime.Values[index];
            int fstdiapazone = -SumDiapazoneMultiplier * sum;
            int snddiapazone = (SumDiapazoneMultiplier + 1) * sum;
            int fstvalue = rand.Next(Math.Min(fstdiapazone, snddiapazone) - SumDefaultDiapazone, Math.Max(fstdiapazone, snddiapazone) + SumDefaultDiapazone);
            int sndvalue = sum - fstvalue;
            
            prime.Values.RemoveAt(index);
            prime.Values.Insert(index, fstvalue);
            prime.Values.Insert(index + 1, sndvalue);
            
            prime.Operations.Insert(index, '+');
            
            prime.Scobes.InsertRange(index * 2 + 1,new int[2]);

            bool isscobsmaked = true ;
            
            //Это страшное isscobsmaked мне кокрас-таки лень писать, ведь для всех четырех функций оно разное
            
            if (isscobsmaked)
            {
                prime.Scobes[index * 2]++;
                prime.Scobes[index * 2 + 3]++;
            }
        }
        void MakeSubtraction(ref Prime prime,int index)
        {
            Random rand = new Random();
            int subtraction = prime.Values[index];
            int fstdiapazone = -SumDiapazoneMultiplier * subtraction;
            int snddiapazone = (SumDiapazoneMultiplier + 1) * subtraction;
            int fstvalue = rand.Next(Math.Min(fstdiapazone, snddiapazone) - SumDefaultDiapazone, Math.Max(fstdiapazone, snddiapazone) + SumDefaultDiapazone);
            int sndvalue = subtraction + fstvalue;
            
            prime.Values.RemoveAt(index);
            prime.Values.Insert(index, sndvalue);
            prime.Values.Insert(index + 1, fstvalue);
            
            prime.Operations.Insert(index, '-');
            
            prime.Scobes.InsertRange(index * 2 + 1,new int[2]);

            bool isscobsmaked = true;
            
            if (isscobsmaked)
            {
                prime.Scobes[index * 2]++;
                prime.Scobes[index * 2 + 3]++;
            }
        }
        void MakeDevide(ref Prime prime,int index)
        {
            Random rand = new Random();
            int devide = prime.Values[index];
            int fstvalue = rand.Next(1, DevideDiapazone) * (rand.Next(1) * 2 - 1);
            int sndvalue = devide * fstvalue;
            
            prime.Values.RemoveAt(index);
            prime.Values.Insert(index, sndvalue);
            prime.Values.Insert(index + 1, fstvalue);
            
            prime.Operations.Insert(index, '/');
            
            prime.Scobes.InsertRange(index * 2 + 1,new int[2]);
            
            bool isscobsmaked = true;
            
            if (isscobsmaked)
            {
                prime.Scobes[index * 2]++;
                prime.Scobes[index * 2 + 3]++;
            }
        }
        
        void MakeMultiplication(ref Prime prime, int index)
        {
            Random rand = new Random();
            int multiplication = prime.Values[index];
            List<int> multipliers = ToSimpleMultipliers(multiplication);
            int fstvalue = 1;
            foreach (int multiplier in multipliers)
            {
                if (rand.Next(2) == 0) fstvalue *= multiplier;
            }
            int sndvalue = multiplication / fstvalue;
            
            if (multiplication == 0) fstvalue = rand.Next(DevideDiapazone);
            
            prime.Values.RemoveAt(index);
            if (rand.Next(2) == 0)
            {
                prime.Values.Insert(index, fstvalue);
                prime.Values.Insert(index + 1, sndvalue );
            }
            else
            {
                prime.Values.Insert(index, sndvalue);
                prime.Values.Insert(index + 1, fstvalue );
            }
            
            prime.Operations.Insert(index, '*');
            
            prime.Scobes.InsertRange(index * 2 + 1,new int[2]);
            
            bool isscobsmaked = true;
            
            if (isscobsmaked)
            {
                prime.Scobes[index * 2]++;
                prime.Scobes[index * 2 + 3]++;
            }
        }

        public List<int> ToSimpleMultipliers(int number)
        {
            List<int> Multipliers = new List<int>();
            
            if (number < 0)
            {
                Multipliers.Add(-1);
                number = -number;
            }
            
            AddSimples(number);

            for (int i = 0; Simples[i] <= number && number != 1; i++)
            {
                while (number % Simples[i] == 0)
                {
                    Multipliers.Add(Simples[i]);
                    number /= Simples[i];
                }
            }
            return Multipliers;
        }
        
        public void AddSimples(double maxvalue)
        {
            int lastsim = Simples.Last();
            
            for (int i = lastsim + 1; i <= maxvalue; i++)
            {
                bool issimple = true;
                
                for (int j = 0; Simples[j] <= Math.Sqrt(i) && issimple; j++)
                {
                    if (i % Simples[j] == 0) issimple = false;
                }
                
                if (issimple) Simples.Add(i);
            }
            File.WriteAllText(DBFilePath, JsonConvert.SerializeObject(Simples));
        }
        public static Prime MakeNegativeScobes(Prime inputprime)
        {
            Prime outputprime = inputprime;
            for (int i = 0; i < outputprime.Values.Count; i++)
            {
                if (outputprime.Values[i] < 0 && outputprime.Scobes[i * 2] == 0)
                {
                    outputprime.Scobes[i * 2]++;
                    outputprime.Scobes[i * 2 + 1]++;
                }
            }
            return outputprime;
        }
    }

    public class Prime
    {
        public List<int> Values = new();
        public List<int> Scobes = new();
        public List<char> Operations = new();

        public string ToString()
        {
            string result = "";
            for (int i = 0; i < Math.Max(Values.Count, Math.Max(Operations.Count, Scobes.Count / 2)); i++)
            {
                if (i < Scobes.Count / 2)
                {
                    for (int j = 0; j < Scobes[i * 2]; j++)
                    {
                        result += '(';
                    }
                }

                if (i < Values.Count) result += Values[i];
                
                if (i < Scobes.Count / 2)
                {
                    for (int j = 0; j < Scobes[i * 2 + 1]; j++)
                    {
                        result += ')';
                    }
                }

                if (i < Operations.Count) result += Operations[i];
            }
            return result;
        }
    }