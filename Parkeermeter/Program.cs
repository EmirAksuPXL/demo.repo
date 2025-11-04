using System.Text;

namespace Parkeermeter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime StartTime = DateTime.MinValue;
            DateTime EndTime = DateTime.MinValue;
            bool ProgramActive = true;
            bool sessionActive = false;

            while (ProgramActive)
            {
                ShowTitle();
                int choice = ShowMenu();

                switch (choice)
                {
                    case 1:
                        if (sessionActive)
                        {
                            ShowError();
                        }
                        else
                        {
                            StartTime = StartSession();
                            sessionActive = true;
                        }
                        break;

                        case 2:
                        if (!sessionActive)
                        {
                            ShowError();
                        }
                        else
                        {
                            EndTime = StopSession(StartTime);
                            sessionActive = false;
                        }
                        break;
                             case 3: // Ticket
                        if (!sessionActive && StartTime != DateTime.MinValue && EndTime != DateTime.MinValue)
                        {
                            PrintTicket(StartTime, EndTime);
                        }
                        else if (sessionActive)
                        {
                            ShowError();
                        }
                        else
                        {
                            ShowError();
                        }
                        break;

                    case 4: // Sluit
                        ProgramActive = false;
                        break;

                    default: // Ongeldige optie
                        ShowError();
                        break;
                }

                
            }
            


        }
        static void ShowTitle()
        {
            Console.WriteLine("+-------------+");
            Console.WriteLine("| P(arking)XL |");
            Console.WriteLine("+-------------+");
        }
        static int ShowMenu()
        {
            Console.WriteLine("1 - Start");
            Console.WriteLine("2 - Stop");
            Console.WriteLine("3 - Ticket");
            Console.WriteLine("4 - Sluit");
            Console.Write("Uw keuze: ");
            int.TryParse(Console.ReadLine(), out int input);
            return input;
            
        }
        static void ShowError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Beëndig eerst de lopende sessie.");
            Console.ResetColor();
            Console.ReadKey();
        }
        static DateTime StartSession()
        {
            DateTime startTime= DateTime.Now;
            bool validInput = false;
            

            while (!validInput)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Starttijd: ");
                string TimeChosen = Console.ReadLine();
                Console.ResetColor();

                if (DateTime.TryParse(TimeChosen, out startTime))
                {
                    if (startTime <= DateTime.Now)
                    {
                        validInput = true;
                    }
                }
            }
            Console.WriteLine("Sessie gestart! Druk op enter om verder te gaan…");
            Console.ReadKey();
            return startTime;

        }
        static DateTime StopSession(DateTime startTime)
        {
          
            DateTime endTime = DateTime.Now;
            bool validInput = false;


            while (!validInput)
            {
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Eindtijd: ");
                string TimeChosen = Console.ReadLine();
                Console.ResetColor();

                if (DateTime.TryParse(TimeChosen, out endTime))
                {
                    if (endTime > startTime && endTime <= DateTime.Now)
                    {
                        validInput = true;
                    }
                }
            }
            Console.WriteLine("Sessie gestopt! Druk op enter om verder te gaan…");
            Console.ReadKey();
            return endTime;
        }
        static decimal CalculatePrice(double minuten)
        {
            if (minuten <= 15)
            {
                return 0m;
            }

            // Trek de gratis 15 minuten af
            double betaaldeTijd = minuten - 15;

            // Bereken aantal begonnen halve uren
            int aantalHalveUren = (int)Math.Ceiling(betaaldeTijd / 30);

            // Bereken prijs (€0,6 per half uur)
            decimal prijs = aantalHalveUren * 0.6m;

            // Maximum van €8
            if (prijs > 8m)
            {
                prijs = 8m;
            }

            return prijs;
        }
        static void PrintTicket(DateTime startTime, DateTime endTime)
        {
            TimeSpan totalTime = endTime - startTime;
            double minuten = totalTime.TotalMinutes;
            decimal price = CalculatePrice(minuten);

            
            StringBuilder ticket = new StringBuilder();
            ShowTitle();
            ticket.AppendLine();
            ticket.AppendLine("Starttijd:");
            ticket.AppendLine($"    {startTime:dd/MM/yyyy}");
            ticket.AppendLine($"    {startTime:HH:mm}");
            ticket.AppendLine("Eindtijd:");
            ticket.AppendLine($"    {endTime:dd/MM/yyyy}");
            ticket.AppendLine($"    {endTime:HH:mm}");
            ticket.AppendLine("Duur:");
            ticket.AppendLine($"    {minuten:0} minuten");
            ticket.AppendLine("Prijs:");
            ticket.AppendLine($"    € {price:0.00}");
            ticket.AppendLine();
            ticket.AppendLine("------------------");
            Console.Clear();
            Console.WriteLine(ticket.ToString());
            Console.WriteLine();
            Console.WriteLine("Druk op enter om verder te gaan...");
            Console.ReadKey();
            

        }
    }
}
