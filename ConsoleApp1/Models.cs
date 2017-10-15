using System;
using static System.Console;

namespace ConsoleApp1
{
    public struct SomeHomme
    {
        public char gender;
        public byte age;

        public SomeHomme(char g, byte a)
        {
            gender = g;
            age = a;
        }
    }

    public struct TStruct<T>
    {
        public Type GetType<T>()
        {
            return typeof(T);
        }
    }

    class Models
    {
        public Models()
        {
            SomeHomme h = new SomeHomme();
            h.age = 5;

        }
    }

    public sealed class Utilizer
    {
        public decimal someVal => 1.0M / 0xf;

        public static void Do()
        {
            TStruct<int> ts = new TStruct<int>();
            WriteLine(ts.GetType());

            Man m = new Man();
            Human hum = m;
            m.Piss();
            hum.Piss();

            Woman w = new Woman();
            hum = w;
            hum.Piss();

            m = null;
        }

        public Utilizer()
        {

        }

        public Utilizer(string cos) : this()
        {

        }
    }

    public abstract class Human
    {
        public virtual void Piss()
        {
            WriteLine("byle jak");            
        }
        
    }

    public class Man : Human
    {
        public sealed override void Piss()
        {
            WriteLine("stojaco");
        }

        ~Man()
        {
            WriteLine("Finalize");
        }
    }

    public class Woman : Human
    {
        public new void Piss()
        { WriteLine("siedzaco"); }
    }
}
