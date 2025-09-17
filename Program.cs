var file = File.OpenText("26.txt");
List<Value> vals = new();
while (!file.EndOfStream)
{
    var row = file.ReadLine().Split(' ');
    vals.Add(new(int.Parse(row[0]), int.Parse(row[1])));
}

vals.Sort();
List<Value> events = [vals[0]];
for (int i = 1; i < vals.Count; i++)
{
    if (vals[i].Start > events.Last().End)
        events.Add(vals[i]);
}
var values = from v in vals
             where v.Start > events[^2].End
             orderby v.Duration
             select v;

Console.WriteLine(events.Count + " " + (values.Last().End - events.First().Start));

public class Value : IComparable<Value>
{
    public int Start;

    public int End;

    public int Duration => End - Start;

    public Value(int a, int b)
    {
        Start = a;
        End = b;
    }

    public int CompareTo(Value? other) => other?.End < End ? 1 : -1;

    public override string ToString() => Start + " " + End;
}