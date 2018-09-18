using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] outputsBool = UnitTest1. GetOutputBool();
            var perfectDisjunctionNormalForm = new PerfectDisjunctionNormalForm(outputsBool);
            var perfectConjunctionNormalForm = new PerfectConjunctionNormalForm(outputsBool);
        }
    }
}
