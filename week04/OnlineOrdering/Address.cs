using System;

public class Address
{
    private string _street;
    private string _city;
    private string _stateProvince;
    private string _country;

    public Address(string street, string city, string stateProvince, string country)
    {
        _street = street;
        _city = city;
        _stateProvince = stateProvince;
        _country = country;
    }

    public string GetStreet() => _street;
    public void SetStreet(string street) => _street = street;

    public string GetCity() => _city;
    public void SetCity(string city) => _city = city;

    public string GetStateProvince() => _stateProvince;
    public void SetStateProvince(string stateProvince) => _stateProvince = stateProvince;

    public string GetCountry() => _country;
    public void SetCountry(string country) => _country = country;

    // Returns true if the address is in the USA
    public bool IsInUSA()
    {
        string country = _country.Trim();
        return country.Equals("USA", StringComparison.OrdinalIgnoreCase)
            || country.Equals("United States", StringComparison.OrdinalIgnoreCase)
            || country.Equals("US", StringComparison.OrdinalIgnoreCase);
    }

    // Returns the full address as a multi-line string
    public string GetFullAddress()
    {
        return $"{_street}\n{_city}, {_stateProvince}\n{_country}";
    }
}