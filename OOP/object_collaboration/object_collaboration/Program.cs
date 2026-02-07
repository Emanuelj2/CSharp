public class Program
{
    public static void Main(string[] args)
    {

    }

    //classes
    public class Ram
    {
        public int RamId { get; set; }
        public string? RamName { get; set; }
        public Company? Company { get; set; }
        public double Power { get; set; }
    }

    public class Company
    {
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
    }

    public class Cpu
    { 
        public int CpuId { get; set; }
        public string? ModelNumber { get; set; }
        public Company? Company { get; set; }
        public double Power { get; set; }
    }

    public class Computer
    {
        public int ComputerId { get; set; }
        public string? ModelNumber { get; set; }
        public Ram? Ram { get; set; }
        public Cpu? Cpu { get; set; }
        public MotherBoard? MotherBoard { get; set; }
    }

    public class MotherBoard
    {
        public int MotherBoardId { get; set; }
        public string? ModelNumber { get; set; }
        public Computer? Computer { get; set; }
        public double Power { get; set; }
    }

}