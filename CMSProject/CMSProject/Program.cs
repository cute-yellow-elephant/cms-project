using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore;
using Domain;
using Ninject;


namespace CMSProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new CoreHolder();
            foreach(var m in x.RoleRepository.ReadAll())
                Console.WriteLine(m.Name+" "+m.ID);
            Console.ReadLine();
        }
    }
}
