public class UniversityMember
{
    private string name;
    public string Name
    {
        get => name;
        set
        {
            if (string.IsNullOrEmpty(value)) throw new Exception("name cannot be empty");
            name = value;
        }
    }
    public string MemberId { get; }

    protected List<string> ActionLog { get; set; }

    public int ActionsCount => ActionLog.Count;

    public UniversityMember(string name, string memberId)
    {
        Name = name;
        MemberId = memberId;
        ActionLog = new List<string>();
    }
    public virtual void PerformDuties()
    {
        if (ActionLog.Count >= 5)
        {
            throw new Exception("Limit reached for " + Name);
        }
    }
}

public class Professor : UniversityMember
{
    public Professor(string name, string memberId) : base(name, memberId) {}
    public override void PerformDuties()
    {
        base.PerformDuties();
        ActionLog.Add("Lecture delivered");
    }
    public void ConductResearch(string topic)
    {
        ActionLog.Add("Research: " + topic);
    }
}
public class UndergraduateStudent : UniversityMember
{
    public UndergraduateStudent(string name, string memberId) : base(name, memberId) { }

    public override void PerformDuties()
    {
        base.PerformDuties();
        ActionLog.Add("Lab work completed");
    }
}
public class GraduateStudent : UniversityMember
{
    public GraduateStudent(string name, string memberId) : base(name, memberId) { }

    public override void PerformDuties()
    {
        base.PerformDuties();
        ActionLog.Add("Thesis research update");
    }
}
public class UniversityRegistry
{
    private List<UniversityMember> members = new List<UniversityMember>();

    public void AddMember(UniversityMember m)
    {
        members.Add(m);
    }
    public void ExecuteAllDuties()
    {
        foreach (var m in members)
        {
            try
            {
                m.PerformDuties();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    public int GetMemberStatistics()
    {
        int total = 0;
        foreach (var m in members)
        {
            total += m.ActionsCount;
        }
        return total;
    }
}
class Program
{
    static void Main()
{
    UniversityRegistry registry = new UniversityRegistry();

    Professor prof = new Professor("Dr. llia", "P1");
    UndergraduateStudent stud = new UndergraduateStudent("Ivan", "S1");
    GraduateStudent grad = new GraduateStudent("Olena", "G1");

    registry.AddMember(prof);
    registry.AddMember(stud);
    registry.AddMember(grad);

    // додат виклик спец методу професора
    prof.ConductResearch("AI in Education");
    
    registry.ExecuteAllDuties();

        Console.WriteLine("Total actions: " + registry.GetMemberStatistics());

        // додат ліміт дій
        try
        {
            for (int i = 0; i < 10; i++) stud.PerformDuties();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Check limit: " + ex.Message);
        }
    }
}//jg