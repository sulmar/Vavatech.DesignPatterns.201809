using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.Singleton
{
    class Program
    {
        static void Main(string[] args)
        {

            FileLogger logger = FileLogger.Instance;
            logger.Log("Hello World");

            FileLogger logger2 = FileLogger.Instance;
            logger2.Log("Hello 2");

            FileLogger logger3 = CsvFileLogger.Instance;
            logger3.Log("Hello 3");

            DbLogger dbLogger = GenericSingleton<DbLogger>.Instance;

            //CollectionService services = new CollectionService();
            //services.AddSingleton<DbLogger, DbLogger>();

        }
    }

    public class CsvFileLogger : FileLogger
    {
        protected CsvFileLogger()
            : base()
        {

        }
    }

    public class DbLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class FileLogger
    {

        protected FileLogger()
        {

        }

        private static object syncLock = new object();

        private static FileLogger instance;
        public static FileLogger Instance
        {
            get
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        instance = new FileLogger();
                    }
                }

                return instance;
            }
        }

        public void Log(string message)
            {
            Console.WriteLine($"Save to file: {message}");
        }
    }

    sealed class LazySingleton<T>
        where T : new()
    {
        private static Lazy<T> lazy = new Lazy<T>(() => new T(), true);

        private LazySingleton()
        {
        }

        public static T Instance => lazy.Value;
       
    }


    public class GenericSingleton<T>
        where T : new()
    {
        protected GenericSingleton()
        {
        }

        private static object syncLock = new object();

        private static T instance;
        public static T Instance
        {
            get
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        instance = new T();
                    }
                }

                return instance;
            }
        }

        public void Log(string message)
            {
            Console.WriteLine($"Save to file: {message}");
        }
    }
}
