using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using FullSearchSamples.Services.Impl;

namespace Sample02
{
    internal class Program
    {
        internal class Sample02
        {
            static void Main(string[] args)
            {
                var host = Host.CreateDefaultBuilder(args)
                    .ConfigureServices(services => {

                        #region Configure EF DBContext Service (CardStorageService Database)

                        services.AddDbContext<DocumentDbContext>(options =>
                        {
                            options.UseSqlServer(@"data source=XOMA\SQLEXPRESS;initial catalog=DocumentsDatabase;MultipleActiveResultSets=True;App=EntityFramework;Trusted_Connection=True;");
                        });

                        #endregion

                        #region Configure Repositories

                        #endregion


                    })
                    .Build();


                //var documentsSet = DocumentExtractor.DocumentsSet().Take(10000).ToArray();
                //Простой поиск слова в строке
                //new SimpleSearcher().Search("Monday", documentsSet);
                //new SimpleSearcherV2().SearchV1("Monday", documentsSet);
                //new SimpleSearcherV2().SearchV2("Monday", documentsSet);

                //BenchmarkSwitcher.FromAssembly(typeof(Sample02).Assembly).Run(args, new BenchmarkDotNet.Configs.DebugInProcessConfig());
                //BenchmarkRunner.Run<SearchBenchmarkV1>();
            }
        }

        [MemoryDiagnoser] // Учет памяти при тестировании
        [WarmupCount(1)] //Прогрев теста
        [IterationCount(5)] //Тест выполняется 5 раз и результаты усредняются
        public class SearchBenchmarkV1
        {
            private readonly string[] _documentsSet;


            //Не учитывается при тестировании
            public SearchBenchmarkV1()
            {
                _documentsSet = DocumentExtractor.DocumentsSet().Take(10000).ToArray();
            }

            [Benchmark] //Метка теста
            public void SimpleSearch()
            {
                new SimpleSearcherV2().SearchV3("Monday", _documentsSet).ToArray();
            }

        }
    }
}