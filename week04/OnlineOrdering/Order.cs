using System.Collections.Generic;
using System.Text;

public class Order
{
    private const double USAShippingCost = 5.0;
    private const double InternationalShippingCost = 35.0;

    private List<Product> _products;
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public List<Product> GetProducts() => _products;

    public Customer GetCustomer() => _customer;
    public void SetCustomer(Customer customer) => _customer = customer;

    // Sum of all product totals plus one-time shipping cost
    public double CalculateTotalCost()
    {
        double total = 0;
        foreach (Product product in _products)
        {
            total += product.GetTotalCost();
        }

        double shipping = _customer.LivesInUSA() ? USAShippingCost : InternationalShippingCost;
        return total + shipping;
    }

    // Name and product id of each product
    public string GetPackingLabel()
    {
        StringBuilder label = new StringBuilder();
        foreach (Product product in _products)
        {
            label.AppendLine($"{product.GetName()} (ID: {product.GetProductId()})");
        }
        return label.ToString().TrimEnd();
    }

    // Name and address of the customer
    public string GetShippingLabel()
    {
        return $"{_customer.GetName()}\n{_customer.GetAddress().GetFullAddress()}";
    }
}