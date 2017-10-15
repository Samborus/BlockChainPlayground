
using ConsoleApp1.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ConsoleApp1
{

    class Program21
    {

        static void Main(string[] args)
        {
#if (TEST)
            int domainCount = 1;
            Parallel.For(0, domainCount, i => 
            {
                AppDomain domain1 = AppDomain.CreateDomain($@"0{i}");
                domain1.DoCallBack(new CrossAppDomainDelegate(Do));
                AppDomain.Unload(domain1);
            });
#else
            Do();
#endif
        }

        static internal void Do()
        {            
            Worker1Factory.Instance.Do();
        }

        static internal void Do_Old()
        {            
            float f1 = 1f;
            double d1 = 1e06d;
            decimal dec1 = 1.0M;
            long l1 = 0xff;
            int c = unchecked(1 + int.MaxValue);

            short sh1 = 0xf;
            byte bytek1 = 0x00000001;
            byte bytek2 = 0x00000010;
            var res = bytek1 | bytek2;
            char[] tablica = { 'a', 'b', 'c' };
            //Write($@"Wynik: {res}, {default(decimal)}");
            Utilizer.Do();
            ReadKey(); 
        }

        static void Do1() => Do();
        static void Say1() => Write("i said\n");

        static void Say() // string cos = "hello"
        {
            WriteLine("hiy \n");
        }        
    }
}
