using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KageApp
{
    class Program
    {
        static void Main(string[] args)
        {
            KagerWorker worker = new KagerWorker();
            worker.Start();


            Console.ReadLine();
        }
    }
}
