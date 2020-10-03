using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace SampleConApp
{
    [Serializable]
    public class Employee
    {
        public string EmpName { get; set; }
        public string EmpAddress { get; set; }
        public long EmpPhone { get; set; }

    }
    class Serialization
    {
        static void Main()
        {
            binaryWorking();            
            Console.ReadKey();
        }
        private static void binaryWorking()
        {
            Console.WriteLine("Your wish to proceed: WRITE or READ");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "read")
                deserializingBinary();
            else
                serializingBinary();

        }
        private static void deserializingBinary()
        {
            try
            {
                FileStream fileStr = new FileStream("Details.bin", FileMode.Open, FileAccess.Read);
                BinaryFormatter binF = new BinaryFormatter();
                Employee emp = binF.Deserialize(fileStr) as Employee;
                Console.WriteLine($"Name: {emp.EmpName}\nAddress: {emp.EmpAddress}\nPhone Number: {emp.EmpPhone}");
                fileStr.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("No file available to read..Kindly write(create) file before reading.....Thank you :)....");
            }
        }

        private static void serializingBinary()
        {

            Employee emp = new Employee { EmpName = "NITHYA P", EmpAddress = "OOTY",  EmpPhone = 24312345 };
            BinaryFormatter binF = new BinaryFormatter();
            FileStream fileStr = new FileStream("Details.bin", FileMode.OpenOrCreate, FileAccess.Write);
            binF.Serialize(fileStr, emp);
            fileStr.Close();
        }  
       
    }
}