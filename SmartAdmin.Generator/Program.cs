using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartAdmin.Generator.Core;

namespace SmartAdmin.Generator
{
    public class Program
    {
        const int MILLISECONDS = 1000;

        static void Main(string[] args)
        {
            // Header...
            WriteHeader();
            TimeSleep(MILLISECONDS);

            // Ask...
            WriteToConsole("Para gerar contexto completo digite: 1");
            WriteToConsole("Para gerar somente camada de dados digite: 2");

            var AnswerQuestion = String.Empty;

            // Verify...
            do { 

                AnswerQuestion = ReadFromConsole();
                if ((AnswerQuestion == "1") || (AnswerQuestion == "2") || (AnswerQuestion == "3")) { break; }

            } while ((AnswerQuestion != "1") || (AnswerQuestion != "2") || (AnswerQuestion != "3"));

            // Processing...
            MakeProcess(AnswerQuestion);
        }

        //ok
        private static void MakeProcess(string AnswerQuestion)
        {
            if (AnswerQuestion == "1")
            {
                // Start here!
                Console.ForegroundColor = ConsoleColor.Gray;

                MakeModelsWithDataAnnotation();
                MakeBase();
                MakeMappers();
                MakeContext();
                MakeRepository();
                MakeUnitOfWork();
                MakeDomain();

                // Final here!
                WriteToConsole(" ");
                WriteToConsole("Processo executado com sucesso!");
                ReadFromConsole();
            }
            else if (AnswerQuestion == "2")
            {
                // Start here!
                Console.ForegroundColor = ConsoleColor.Gray;

                MakeModelsWithDataAnnotation();
                MakeMappers();
                MakeContext();
                MakeUnitOfWork();

                // Final here!
                WriteToConsole(" ");
                WriteToConsole("Processo executado com sucesso!");
                ReadFromConsole();
            }
            else if (AnswerQuestion == "3")
            {
                // Final here!
                MakeDomain();

                // Final here!
                WriteToConsole(" ");
                WriteToConsole("Processo executado com sucesso!");
                ReadFromConsole();
            }
        }

        //ok
        public static void MakeModelsWithOutDataAnnotation()
        {
            var ConfigTable = new ConfigTables();
            var BuildClass = new Data();
            var GroupTables = ConfigTable.GetTableMapper();

            WriteToConsole(" ");
            WriteToConsole("Gerando Dto(s)...");

            foreach (var Table in GroupTables)
            {
                WriteToConsole(BuildClass.BuildModel(Table));
                TimeSleep(MILLISECONDS);
            }
        }

        //ok
        public static void MakeModelsWithDataAnnotation()
        {
            var ConfigTable = new ConfigTables();
            var BuildClass = new Data();
            var GroupTables = ConfigTable.GetTableMapper();

            WriteToConsole(" ");
            WriteToConsole("Gerando Dto(s)...");

            foreach (var Table in GroupTables)
            {
                WriteToConsole(BuildClass.BuildModelDataAnnotations(Table));
                TimeSleep(MILLISECONDS);
            }

            MakeDataAnnotations();
        }

        //ok
        public static void MakeDataAnnotations()
        {
            var ConfigTable = new ConfigTables();
            var BuildClass = new Data();
            var GroupTables = ConfigTable.GetTableMapper();

            WriteToConsole(" ");
            WriteToConsole("Gerando Metadata(s)...");

            foreach (var Table in GroupTables)
            {
                WriteToConsole(BuildClass.BuildDataAnnotations(Table));
                TimeSleep(MILLISECONDS);
            }
        }

        //ok
        public static void MakeBase()
        {
            var BuildClass = new Data();

            WriteToConsole(" ");
            WriteToConsole("Gerando BaseModel...");
            WriteToConsole(BuildClass.BuildBase());

            TimeSleep(MILLISECONDS);
        }

        //ok
        public static void MakeMappers()
        {
            var ConfigTable = new ConfigTables();
            var BuildClass = new Data();
            var GroupTables = ConfigTable.GetTableMapper();

            WriteToConsole(" ");
            WriteToConsole("Gerando Mappers...");

            foreach (var Table in GroupTables)
            {
                WriteToConsole(BuildClass.BuildMapper(Table));
                TimeSleep(MILLISECONDS);
            }
        }     
        
        //ok
        public static void MakeContext()
        {
            var ConfigTable = new ConfigTables();
            var BuildClass = new Data();
            var GroupTables = ConfigTable.GetTableMapper();

            WriteToConsole(" ");
            WriteToConsole("Gerando Contexto...");

            WriteToConsole(BuildClass.BuildContext(GroupTables));
            TimeSleep(MILLISECONDS);
        }

        //ok
        public static void MakeRepository()
        {
            var BuildClass = new Data();

            WriteToConsole(" ");
            WriteToConsole("Gerando Repositorio...");

            WriteToConsole(BuildClass.BuildRepository());
            TimeSleep(MILLISECONDS);
        }   

        //ok
        public static void MakeUnitOfWork()
        {
            var ConfigTable = new ConfigTables();
            var BuildClass = new Data();
            var GroupTables = ConfigTable.GetTableMapper();

            WriteToConsole(" ");
            WriteToConsole("Gerando UnitOfWork...");

            WriteToConsole(BuildClass.BuildUnitOfWork(GroupTables));
            TimeSleep(MILLISECONDS);
        }

        //ok
        public static void MakeDomain()
        {
            var ConfigTable = new ConfigTables();
            var BuildClass = new Domain();
            var GroupTables = ConfigTable.GetTableMapper();

            WriteToConsole(" ");
            WriteToConsole("Gerando classes de domínio...");

            foreach (var Table in GroupTables)
            {
                WriteToConsole(BuildClass.BuildDomain(Table.Key, Table.Value));
                TimeSleep(MILLISECONDS);
            }

            WriteToConsole(BuildClass.BuildUnitOfWork(GroupTables));
            TimeSleep(MILLISECONDS);
        }

        #region FUNCTIONS OF SHELL PROMPT
        private static void TimeSleep(int Milliseconds)
        {
            var StopWatch = Stopwatch.StartNew();
            Thread.Sleep(Milliseconds);
            StopWatch.Stop();
        }

        private static void WriteHeader()
        {
            Console.Title = typeof(Program).Name;
            Console.ForegroundColor = ConsoleColor.Yellow;

            var ConsoleCopyRight = new StringBuilder();

            Console.WriteLine("#***********************************************************");
            Console.WriteLine("# Console gerador de DbContext");
            Console.WriteLine("# Repositorio GitHub - https://github.com/rodrigoarf");
            Console.WriteLine("# Software - https://windows.github.com/");
            Console.WriteLine("#");

            Console.WriteLine("# > Repository Pattern");
            Console.WriteLine("#   - Context");
            Console.WriteLine("#   - Table Models and Mappers");
            Console.WriteLine("#   - Generic Repository");
            Console.WriteLine("#");

            Console.WriteLine("# > Domain Pattern");
            Console.WriteLine("#   - Domain entity(s) with and methods of the manipulation");
            Console.WriteLine("#   - Domain entity(s) partial extensions, for business logic");
            Console.WriteLine("#");

            Console.WriteLine("# > Service Pattern");
            Console.WriteLine("#   - Entities models with encapsulated domain methods");
            Console.WriteLine("#   - Entities (DTO - Data Transfer Object)");
            Console.WriteLine("#");
            Console.WriteLine("#***********************************************************");

            WriteToConsole(ConsoleCopyRight.ToString());
        }

        public static ConsoleKeyInfo ReadKeyPress()
        {
            return (Console.ReadKey());
        }

        public static string ReadFromConsole()
        {
            return (Console.ReadLine());
        }

        public static void WriteToConsole(string message = "")
        {
            if (message.Length > 0)
            {
                Console.WriteLine("prompt: " + message);
            }
        }

        public static void WriteEnterLine()
        {
            Console.WriteLine(String.Empty);
            Console.ReadLine();
        }

        public static string Execute(string command)
        {
            return string.Format("Comando {0} executado.", command);
        }
        #endregion
    }
}
