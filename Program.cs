namespace Math_For_Maria_Vasilevna
{
    class Program
    {
        static void Main()
        {
            string FilePath = Path.GetTempPath() + "Simple_Numbers.json";
            
            if (File.Exists(FilePath) == false)
            {
                var file = File.Create(FilePath);
                file.Close();
                File.WriteAllText(FilePath, "[2]");
            }
            
            PrimeGenerator primeGenerator = new PrimeGenerator(FilePath);
            
            Random random  = new Random();
            int answer;
            
            Console.WriteLine("Enter answer: (Number / Empty for Random)");
            string mod = Console.ReadLine();
            if (mod != "")
            {
                answer = int.Parse(mod);
            }
            else
            {
                answer = random.Next(-1000,1000);
            }
            
            primeGenerator.SumDiapazoneMultiplier = 1;
            primeGenerator.SumDefaultDiapazone = 5;
            primeGenerator.DevideDiapazone = 11;
            
            Console.WriteLine("Enter prime length: ");
            int primelen = int.Parse(Console.ReadLine());
            
            Console.WriteLine();
            Console.WriteLine(primeGenerator.CreatePrime(answer, primelen).MakeNegativeScobes().ToString());
            Console.WriteLine();
        }
    }
}