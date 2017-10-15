using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public abstract class Disposing : IDisposable
    {
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Usuwanie
                DisposingMethod();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Disposing()
        {
            Dispose(false);
        }

        public virtual void DisposingMethod() { }
    }
}
