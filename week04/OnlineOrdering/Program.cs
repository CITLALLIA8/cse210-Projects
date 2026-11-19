using System;

class Program
{
    static void Main(string[] args)
    {
        // Order 1: customer in the USA
        Address address1 = new Address("123 Main St", "Rexburg", "ID", "USA");
        Customer customer1 = new Customer("John Smith", address1);

        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Wireless Mouse", "WM-100", 15.99, 2));
        order1.AddProduct(new Product("USB-C Cable", "UC-205", 8.50, 3));
        order1.AddProduct(new Product("Laptop Stand", "LS-310", 29.95, 1));

        // Order 2: customer outside the USA
        Address address2 = new Address("Av. Constitución 456", "Monterrey", "Nuevo León", "Mexico");
        Customer customer2 = new Customer("Citlalli Ahumada", address2);

        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Mechanical Keyboard", "MK-450", 79.99, 1));
        order2.AddProduct(new Product("Webcam HD", "WC-120", 45.00, 2));

        DisplayOrder(1, order1);
        Console.WriteLine();
        DisplayOrder(2, order2);
    }

    static void DisplayOrder(int number, Order order)
    {
        Console.WriteLine($"===== ORDER {number} =====");
        Console.WriteLine();
        Console.WriteLine("--- Packing Label ---");
        Console.WriteLine(order.GetPackingLabel());
        Console.WriteLine();
        Console.WriteLine("--- Shipping Label ---");
        Console.WriteLine(order.GetShippingLabel());
        Console.WriteLine();
        Console.WriteLine($"Total Price: ${order.CalculateTotalCost():F2}");
    }
}