using System.Collections.Generic;

Solve solve = new Solve();
List<int> a = solve.FindPrimeNb(26);

foreach(int value in a)
{
    Console.WriteLine(value);
}

Berthold berthold = new Berthold();