using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleConApp
{

    interface ICustomerManager
    {
        bool AddNewCustomer(Customer cust);
        bool DeleteCustomer(int id);
        bool UpdateCustomer(Customer cust);
        Customer[] GetAllCustomer(string name);
    }
    class CustomerManager : ICustomerManager
    {
        HashSet<Customer> customer = new HashSet<Customer>();
        public bool AddNewCustomer(Customer cust)
        {
            return customer.Add(cust);
        }
        public bool UpdateCustomer(Customer cust)
        {
            foreach (Customer i in customer)
            {
                if (cust.CustomerID == i.CustomerID)
                {
                    i.Name = cust.Name;
                    i.Address = cust.Address;

                    return true;
                }
            }
            return false;
        }

        public bool DeleteCustomer(int id)
        {
            foreach (Customer cust in customer)
            {
                if (cust.CustomerID == id)
                {
                    customer.Remove(cust);
                    return true;
                }
            }
            return false;
        }

        public Customer[] GetAllCustomer(string name)
        {

            List<Customer> temp = new List<Customer>();

            foreach (var cust in customer)
            {
                if (cust.Name.Contains(name))
                    temp.Add(cust);
            }            
            return temp.ToArray();
        }

        
    }

    class Customer
    {
        public int CustomerID{ get; set;}
        public string Name { get; set; }
        public string Address { get; set; }




    }
    class UIClient
    {
        static string menu = string.Empty;
        static ICustomerManager mgr = new CustomerManager();
        static void InitializeComponent()
        {
            menu = string.Format("*******************CUSTOMER MANAGEMENT SOFTWARE * ********************\nTO ADD A NEW CUSTOMER------------->PRESS 1\nTO UPDATE CUSTOMER-------------- > PRESS 2\nTO REMOVE A CUSTOMER-------------- > PRESS 3\nTO FIND CUSTOMER---------------- > PRESS 4\nNOTE: ANY OTHER KEY PRESSED IS CONSIDERED AS EXIT OF APP\n");


        }

        static void Main(string[] args)
        {
            InitializeComponent();
            bool @continue = true;
            do
            {
                string choice = MyConsole.getString(menu);
                @continue = processing(choice);
            } while (@continue);
        }

        private static bool processing(string choice)
        {
            switch (choice)
            {
                case "1":
                    addingCustomerData();
                    break;
                case "2":
                    updatingCustomerData();
                    break;
                case "3":
                    deletingCustomer();
                    break;
                case "4":
                    readingCustomerData();
                    break;
                default:
                    return false;
            }
            return true;
        }

        private static void readingCustomerData()
        {
            string name = MyConsole.getString("Enter the name of the customer");
            Customer[] customer = mgr.GetAllCustomer(name);
            foreach (var cus in customer)
            {
                if (cus != null)
                    Console.WriteLine(cus.Name);
                Console.WriteLine(cus.CustomerID);
                Console.WriteLine(cus.Address);
            }
        }

        private static void deletingCustomer()
        {
            int id = MyConsole.getNumber("Enter the ID of the customer to remove");
            if (mgr.DeleteCustomer(id))
                Console.WriteLine("Customer Deleted successfully");
            else
                Console.WriteLine("Could not find the Customer to delete");
        }

        private static void updatingCustomerData()
        {
            Customer cus = new Customer();
            cus.CustomerID = MyConsole.getNumber("Enter the ID  of the Customer U wish to update");
            cus.Name = MyConsole.getString("Enter the new name of this customer");
            cus.Address = MyConsole.getString("Enter the new Address of this customer");

            bool result = mgr.UpdateCustomer(cus);
            if (!result)
                Console.WriteLine($"No Customer by this id {cus.CustomerID} found to update");
            else
                Console.WriteLine($"Customer by ID {cus.CustomerID} is updated successfully to the database");
        }

        private static void addingCustomerData()
        {
            Customer cus = new Customer();
            cus.CustomerID = MyConsole.getNumber("Enter the ID of the Customer");
            cus.Name = MyConsole.getString("Enter the Name of Customer");
            cus.Address = MyConsole.getString("Enter the Address of Customer");

            try
            {
                bool result = mgr.AddNewCustomer(cus);
                Console.WriteLine($"Customer by name {cus.Name} is added successfully to the database");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

   

}