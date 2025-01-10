namespace Math_For_Maria_Vasilevna
{
    class Program
    {
        static void Main()
        {
            string PrimeNumsFilePath = Path.GetTempPath() + "\\Prime_Numbers.json";
            
            if (File.Exists(PrimeNumsFilePath) == false)
            {
                var file = File.Create(PrimeNumsFilePath);
                file.Close();
                File.WriteAllText(PrimeNumsFilePath, "[2]");
            }
            
            ExpressionGenerator expressionGenerator = new ExpressionGenerator(PrimeNumsFilePath);
            Random random  = new Random();
            string answer;
            int lenguage = 1;
            expressionGenerator.SumDiapazoneMultiplier = 1;
            expressionGenerator.SumDefaultDiapazone = 10;
            expressionGenerator.DevideDiapazone = 100;
            
            Menu:
                
                Console.Clear();
                
                if (lenguage == 1) Console.WriteLine("1.Сгенерировать выражение\n2.Настройки генерации чисел\n3.Сменить язык\n4.Информация про простые числа\nВ данной программе Enter используется, как \"Назад\"");
                else Console.WriteLine("1.Generate expression\n2.Number Generation Settings\n3.Change lenguage\n4.Information about prime numbers\nIn this program, Enter is used as \"Back\"");
                answer = Console.ReadLine();
                
                switch (answer)
                {
                    case "1":
                        Console.Clear();
                        
                        int expressionResultLowDiapazone, expressionResultUppDiapazone, expressionLength, expressionsCount;
                        
                        if (lenguage == 1) Console.WriteLine("Введите нижний диапазон ответа выражения:");
                        else Console.WriteLine("Enter the low range of result:");
                        answer = Console.ReadLine();
                        if (int.TryParse(answer, out expressionResultLowDiapazone));
                        else
                        {
                            if (lenguage == 1) Console.WriteLine("Не удается преобразовать в число");
                            else Console.WriteLine("Can't convert to a number");
                            Console.ReadLine();
                            goto Menu;
                        }
                        
                        if (lenguage == 1) Console.WriteLine("Введите верхний диапазон ответа выражения:");
                        else Console.WriteLine("Enter the upp range of result:");
                        answer = Console.ReadLine();
                        if (int.TryParse(answer, out expressionResultUppDiapazone));
                        else
                        {
                            if (lenguage == 1) Console.WriteLine("Не удается преобразовать в число");
                            else Console.WriteLine("Can't convert to a number");
                            Console.ReadLine();
                            goto Menu;
                        }

                        if (expressionResultLowDiapazone > expressionResultUppDiapazone)
                        {
                            if (lenguage == 1) Console.WriteLine("Неверно задан диапазон");
                            else Console.WriteLine("Еhe range is empty");
                            Console.ReadLine();
                            goto Menu;
                        }
                        
                        if (lenguage == 1) Console.WriteLine("Введите длину выражений:");
                        else Console.WriteLine("Enter expression length:");
                        answer = Console.ReadLine();
                        if (int.TryParse(answer, out expressionLength));
                        else
                        {
                            if (lenguage == 1) Console.WriteLine("Не удается преобразовать в число");
                            else Console.WriteLine("Can't convert to a number");
                            Console.ReadLine();
                            goto Menu;
                        }
                        
                        if (lenguage == 1) Console.WriteLine("Введите количество выражений:");
                        else Console.WriteLine("Enter the number of expressions:");
                        answer = Console.ReadLine();
                        if (int.TryParse(answer, out expressionsCount));
                        else
                        {
                            if (lenguage == 1) Console.WriteLine("Не удается преобразовать в число");
                            else Console.WriteLine("Can't convert to a number");
                            Console.ReadLine();
                            goto Menu;
                        }
                        
                        Console.Clear();

                        for (int i = 0; i < expressionsCount; i++)
                        {
                            Console.WriteLine(expressionGenerator.CreateExpression(random.Next(expressionResultLowDiapazone, expressionResultUppDiapazone), expressionLength).MakeNegativeParentheses().ToString());
                        }
                        
                        break;
                    
                    case "2":
                        Settings:
                        
                        Console.Clear();

                        if (lenguage == 1) Console.Write("1.Разброс рандомных чисел при генерации суммы и разности в зависимости от числа: ");
                        else Console.Write("1.SumDiapazoneMultiplier: ");
                        Console.Write(expressionGenerator.SumDiapazoneMultiplier);
                        
                        if (lenguage == 1) Console.Write("\n2.Минимальный диапазон чисел при генерации суммы и разности: ");
                        else Console.Write("\n2.SumDefaultDiapazone: ");
                        Console.Write(expressionGenerator.SumDefaultDiapazone);
                        
                        if (lenguage == 1) Console.Write("\n3.Максимальное увеличение числа при генерации деления: ");
                        else Console.Write("\n3.DevideDiapazone: ");
                        Console.WriteLine(expressionGenerator.DevideDiapazone);
                        
                        answer = Console.ReadLine();
                        int value;
                        
                        Console.Clear();
                        
                        switch (answer)
                        {
                            case "1":
                                Console.Clear();
                                if (lenguage == 1) Console.WriteLine("Разброс рандомных чисел при генерации суммы и разности в зависимости от числа:");
                                else Console.WriteLine("Enter SumDiapazoneMultiplier");
                                answer = Console.ReadLine();
                                if(int.TryParse(answer,out value)) expressionGenerator.SumDiapazoneMultiplier = value;
                                goto Settings;
                            case "2":
                                Console.Clear();
                                if (lenguage == 1) Console.WriteLine("Минимальный диапазон чисел при генерации суммы и разности:");
                                else Console.WriteLine("Enter SumDefaultDiapazone");
                                answer = Console.ReadLine();
                                if(int.TryParse(answer,out value)) expressionGenerator.SumDefaultDiapazone = value;
                                goto Settings;
                            case "3":
                                Console.Clear();
                                if (lenguage == 1) Console.WriteLine("Максимальное увеличение числа при генерации деления:");
                                else Console.WriteLine("Enter DevideDiapazone");
                                answer = Console.ReadLine();
                                if(int.TryParse(answer,out value)) expressionGenerator.DevideDiapazone = value;
                                goto Settings;
                        }
                        goto Menu;
                    
                    case "3":
                        lenguage = 1 - lenguage;
                        goto Menu;
                        
                    case "4":
                        Primes:
                        
                        Console.Clear();
                        
                        if (lenguage == 1) Console.Write("Расположение файла: ");
                        else Console.Write("File path: ");
                        Console.Write(PrimeNumsFilePath);
                        
                        if (lenguage == 1) Console.Write("\nКоличество простых чисел: ");
                        else Console.Write("\nNumber of prime numbers: ");
                        Console.Write(expressionGenerator.Primes.Count);
                        
                        if (lenguage == 1) Console.Write("\nПоследнее простое число: ");
                        else Console.Write("\nLast prime number: ");
                        Console.Write(expressionGenerator.Primes.Last());
                        
                        if (lenguage == 1) Console.WriteLine("\n1.Добавить простые числа\n2.Очистить базу данных");
                        else Console.WriteLine("\n1.Add prime numbers\n2.Clear prime numbers");
                        answer = Console.ReadLine();
                        switch (answer)
                        {
                            case "1":
                                Console.Clear();
                                if (lenguage == 1) Console.WriteLine("Введите вверхнюю границу");
                                else Console.WriteLine("Enter maxvalue");
                                answer = Console.ReadLine();
                                int maxvalue;
                                if(int.TryParse(answer,out maxvalue)) expressionGenerator.AddPrimes(maxvalue);
                                goto Primes;
                            case "2":
                                expressionGenerator.ClearPrimes();
                                break;
                        }
                        goto Menu;
                        
                    default:
                        if (lenguage == 1) Console.WriteLine("Неизвестная команда");
                        else Console.WriteLine("Unknown command");
                        break;
                }
                Console.WriteLine();
                if (lenguage == 1) Console.WriteLine("Хотите продолжить?");
                else Console.WriteLine("Continue?");
                answer = Console.ReadLine().ToLower();
                if (answer == "да" || answer == "yes" || answer == "д" || answer == "y") goto Menu;
        }
    }
}