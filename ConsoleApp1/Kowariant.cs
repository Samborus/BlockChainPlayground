using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Animal { }
    public class Pig : Animal { }
    public class SeaHorse : Animal { }

    public class Kowariant<T>
    {
        int position;
        T[] data = new T[100];
        public void Push(T obj) => data[position++] = obj;
        public T Pop() => data[--position];
    }

    public class ZooCleaner
    {
        public static void Wash<T>(Kowariant<T> animals) where T : Animal
        {

        }
    }

    public class Tester1
    {
        void Work()
        {
            Kowariant<Pig> pigs = new Kowariant<Pig>();
            ZooCleaner.Wash(pigs);
        }
    }
}
