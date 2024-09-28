/*Произведите корректную (правильную) по вашему мнению реализацию с применением принципа Single-Responsibility Principle (SRP):
Рассмотрим класс Invoice, который нарушает принцип SRP, поскольку он занимается как расчетом стоимости, так и сохранением счета-фактуры в базу данных:
public class Invoice
{
    public int Id { get; set; }
    public List<Item> Items { get; set; }
    public double TaxRate { get; set; }
    public double CalculateTotal()
    {
        double subTotal = 0;
        foreach (var item in Items)
        {
            subTotal += item.Price;
        }
        return subTotal + (subTotal * TaxRate);
    }
    public void SaveToDatabase()
    {
        // Логика для сохранения счета-фактуры в базу данных
    }
}

В этом примере класс Invoice имеет две ответственности: расчёт стоимости и сохранение в базу данных. Необходимо разделить обязанности на два класса: один для расчета суммы, другой — для сохранения в базу данных.
Код должен содержать три класса:
•	Invoice — отвечает за представление данных счета-фактуры.
•	InvoiceCalculator — отвечает за расчет суммы счета-фактуры.
•	InvoiceRepository — отвечает за сохранение счета-фактуры в базу данных.
*/
/*мой ответ*/
/*public class Item
{
    public string Name { get; set; }
    public double Price { get; set; }
    public Item(string name, double price)
    {
        Name = name;
        Price = price;
    }
}*/ /*для списка Itme в Invoice*/
public class Invoice
{
    public int Id { get; set; }
    public List<Item> Items { get; set; }
    public double TaxRate { get; set; }
    public Invoice(int id, List<Item> items, double TaxRate) 
    {
        Id = id;
        Items = items;
    }
}
public class InvoiceCalculator
{
    public double CalculateTotal(Invoice invoice)
    {
        double subTotal = 0;
        foreach (var item in invoice.Items)
        {
            subTotal += item.Price;
        }
        return subTotal + (subTotal * invoice.TaxRate);
    }
}
public class InvoiceRepository
{
    public void SaveToDatabase(Invoice invoice)
    {
        // Логика для сохранения счета-фактуры в базу данных
    }
}
