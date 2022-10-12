// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text;


#region Generate random numbers
int Min = 100000;
int limit = 200000;

////By loop
//Random randNum = new Random();
//HashSet<int> setNum = new HashSet<int>();
//while (setNum.Count != Min)
//{
//    var num = randNum.Next(1, 100001);
//    if (!setNum.Contains(num))
//        setNum.Add(num);
//}

//by swapping values

var listNum = Enumerable
    .Range(Min, limit).Select(i => i).ToList();
Random rng = new Random();

int n = listNum.Count;
while (n > 1)
{
    n--;
    int k = rng.Next(n + 1);
    Int32 value = listNum[k];
    listNum[k] = listNum[n];
    listNum[n] = value;
}

#endregion

await File.WriteAllTextAsync("WriteLines.txt", string.Join(Environment.NewLine, listNum.ToArray()));
Console.WriteLine("File generated");
Console.ReadLine();

