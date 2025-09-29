using System;

class Program
{
    static void Main(string[] args)
    {
        // First Order
        Address address1 = new Address("123 Main St", "Plateau", "ID", "NIGERIA");
        Customer customer1 = new Customer("Ebuka", address1);
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Laptop", "P001", 999.99, 1));
        order1.AddProduct(new Product("Mouse", "P002", 25.50, 2));

        // Second Order
        Address address2 = new Address("10 Downing St", "London", "Greater London", "UK");
        Customer customer2 = new Customer("Mary Adaugo", address2);
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Book", "B100", 15.75, 3));
        order2.AddProduct(new Product("Pen", "B200", 1.50, 10));
        order2.AddProduct(new Product("Notebook", "B300", 5.00, 2));

        // Display Order 1
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalPrice():0.00}\n");

        // Display Order 2
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalPrice():0.00}\n");
    }
}
