public class Customer
{
    private string _name;
    private Address _address;

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    public string GetName() => _name;
    public void SetName(string name) => _name = name;

    public Address GetAddress() => _address;
    public void SetAddress(Address address) => _address = address;

    // Delegates to the Address class
    public bool LivesInUSA()
    {
        return _address.IsInUSA();
    }
}