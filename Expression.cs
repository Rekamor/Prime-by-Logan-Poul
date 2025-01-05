using Newtonsoft.Json;
using System.IO;

namespace Math_For_Maria_Vasilevna;

    public class ExpressionGenerator
    {
        public ExpressionGenerator(string SimpleNumsPath)
        {
            DBFilePath = SimpleNumsPath;
            Primes = JsonConvert.DeserializeObject<List<int>>(File.ReadAllText(DBFilePath));
        }
        string DBFilePath;
        public List<int> Primes;
        public int SumDiapazoneMultiplier { get; set; }
        public int SumDefaultDiapazone { get; set; }
        public int DevideDiapazone { get; set; }
        public Expression CreateExpression(int answer, int length)
        {
            Random rand = new Random();
            Expression expression = new Expression();
            
            expression.Values.Add(answer);
            expression.Parentheses.AddRange(new int[2]); 

            for (int i = 0; i < length; i++)
            {
                switch (rand.Next(4))
                {
                    case 0:
                        MakeSum(ref expression,rand.Next(expression.Values.Count));
                        break;
                    case 1:
                        MakeSubtraction(ref expression,rand.Next(expression.Values.Count));
                        break;
                    case 2:
                        MakeDevide(ref expression,rand.Next(expression.Values.Count));
                        break;
                    case 3:
                        MakeMultiplication(ref expression,rand.Next(expression.Values.Count));
                        break;
                }
            }
            return expression;
        }
        void MakeSum(ref Expression expression, int index)
        {
            Random rand = new Random();
            int sum = expression.Values[index];
            int fstdiapazone = -SumDiapazoneMultiplier * sum;
            int snddiapazone = (SumDiapazoneMultiplier + 1) * sum;
            int fstvalue = rand.Next(Math.Min(fstdiapazone, snddiapazone) - SumDefaultDiapazone, Math.Max(fstdiapazone, snddiapazone) + SumDefaultDiapazone);
            int sndvalue = sum - fstvalue;
            
            expression.Values.RemoveAt(index);
            expression.Values.Insert(index, fstvalue);
            expression.Values.Insert(index + 1, sndvalue);
            
            expression.Operations.Insert(index, '+');
            
            expression.Parentheses.InsertRange(index * 2 + 1,new int[2]);
            
            if (ParenthesesLogic(expression, index))
            {
                expression.Parentheses[index * 2]++;
                expression.Parentheses[index * 2 + 3]++;
            }
        }
        void MakeSubtraction(ref Expression expression, int index)
        {
            Random rand = new Random();
            int subtraction = expression.Values[index];
            int fstdiapazone = -SumDiapazoneMultiplier * subtraction;
            int snddiapazone = (SumDiapazoneMultiplier + 1) * subtraction;
            int fstvalue = rand.Next(Math.Min(fstdiapazone, snddiapazone) - SumDefaultDiapazone, Math.Max(fstdiapazone, snddiapazone) + SumDefaultDiapazone);
            int sndvalue = subtraction + fstvalue;
            
            expression.Values.RemoveAt(index);
            expression.Values.Insert(index, sndvalue);
            expression.Values.Insert(index + 1, fstvalue);
            
            expression.Operations.Insert(index, '-');
            
            expression.Parentheses.InsertRange(index * 2 + 1,new int[2]);

            if (ParenthesesLogic(expression, index))
            {
                expression.Parentheses[index * 2]++;
                expression.Parentheses[index * 2 + 3]++;
            }
        }
        void MakeDevide(ref Expression expression, int index)
        {
            Random rand = new Random();
            int devide = expression.Values[index];
            int fstvalue = rand.Next(1, DevideDiapazone) * (rand.Next(1) * 2 - 1);
            int sndvalue = devide * fstvalue;
            
            expression.Values.RemoveAt(index);
            expression.Values.Insert(index, sndvalue);
            expression.Values.Insert(index + 1, fstvalue);
            
            expression.Operations.Insert(index, '/');
            
            expression.Parentheses.InsertRange(index * 2 + 1,new int[2]);
            
            if (ParenthesesLogic(expression, index))
            {
                expression.Parentheses[index * 2]++;
                expression.Parentheses[index * 2 + 3]++;
            }
        }
        void MakeMultiplication(ref Expression expression, int index)
        {
            Random rand = new Random();
            int multiplication = expression.Values[index];
            List<int> multipliers = ToPrimeMultipliers(multiplication);
            int fstvalue = 1;
            foreach (int multiplier in multipliers)
            {
                if (rand.Next(2) == 0) fstvalue *= multiplier;
            }
            int sndvalue = multiplication / fstvalue;
            
            if (multiplication == 0) fstvalue = rand.Next(DevideDiapazone);
            
            expression.Values.RemoveAt(index);
            if (rand.Next(2) == 0)
            {
                expression.Values.Insert(index, fstvalue);
                expression.Values.Insert(index + 1, sndvalue );
            }
            else
            {
                expression.Values.Insert(index, sndvalue);
                expression.Values.Insert(index + 1, fstvalue );
            }
            
            expression.Operations.Insert(index, '*');
            
            expression.Parentheses.InsertRange(index * 2 + 1,new int[2]);
            
            if (ParenthesesLogic(expression, index))
            {
                expression.Parentheses[index * 2]++;
                expression.Parentheses[index * 2 + 3]++;
            }
        }
        public List<int> ToPrimeMultipliers(int number)
        {
            List<int> Multipliers = new List<int>();
            
            if (number < 0)
            {
                Multipliers.Add(-1);
                number = -number;
            }
            
            AddPrimes(number);

            for (int i = 0; Primes[i] <= number && number != 1; i++)
            {
                while (number % Primes[i] == 0)
                {
                    Multipliers.Add(Primes[i]);
                    number /= Primes[i];
                }
            }
            return Multipliers;
        }
        public void AddPrimes(double maxvalue)
        {
            int lastprime = Primes.Last();
            
            for (int i = lastprime + 1; i <= maxvalue; i++)
            {
                bool isprime = true;
                
                for (int j = 0; Primes[j] <= Math.Sqrt(i) && isprime; j++)
                {
                    if (i % Primes[j] == 0) isprime = false;
                }
                
                if (isprime) Primes.Add(i);
            }
            if (maxvalue > lastprime) File.WriteAllText(DBFilePath, JsonConvert.SerializeObject(Primes));
        }
        public void ClearPrimes()
        {
            Primes = new List<int>();
            Primes.Add(2);
            File.WriteAllText(DBFilePath, JsonConvert.SerializeObject(Primes));
        }
        private bool ParenthesesLogic(Expression expression, int index)
        {
            string leftOperations = "/";
            string rightOperations = "";

            if (expression.Operations[index] == '+' || expression.Operations[index] == '-')
            {
                leftOperations += "-*";
                rightOperations += "/*";
            }
                
            bool isparenthesesmaked = false;
            
            if (index > 0 && expression.Parentheses[index * 2] == 0)
            {
                for (int i = 0; i < leftOperations.Length && isparenthesesmaked == false; i++)
                {
                     isparenthesesmaked = expression.Operations[index - 1] == leftOperations[i];
                }
            }
            if (index < expression.Operations.Count - 1 && expression.Parentheses[index * 2 + 1] == 0)
            {
                for (int i = 0; i < rightOperations.Length && isparenthesesmaked == false; i++)
                {
                    isparenthesesmaked = expression.Operations[index + 1] == rightOperations[i];
                }
            }
            return isparenthesesmaked;
        }
    }

    public class Expression
    {
        public List<int> Values = new();
        public List<int> Parentheses = new();
        public List<char> Operations = new();

        public string ToString()
        {
            string result = "";
            for (int i = 0; i < Math.Max(Values.Count, Math.Max(Operations.Count, Parentheses.Count / 2)); i++)
            {
                if (i < Parentheses.Count / 2)
                {
                    for (int j = 0; j < Parentheses[i * 2]; j++)
                    {
                        result += '(';
                    }
                }

                if (i < Values.Count) result += Values[i];
                
                if (i < Parentheses.Count / 2)
                {
                    for (int j = 0; j < Parentheses[i * 2 + 1]; j++)
                    {
                        result += ')';
                    }
                }

                if (i < Operations.Count) result += Operations[i];
            }
            return result;
        }
        public Expression MakeNegativeParentheses()
        {
            Expression outputprime = this;
            for (int i = 0; i < outputprime.Values.Count; i++)
            {
                if (outputprime.Values[i] < 0 && outputprime.Parentheses[i * 2] == 0)
                {
                    outputprime.Parentheses[i * 2]++;
                    outputprime.Parentheses[i * 2 + 1]++;
                }
            }
            return outputprime;
        }
    }