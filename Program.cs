using HW_007_09_09_2024;
using Microsoft.EntityFrameworkCore;

class Program
{

    static void Main()
    {
        //Создайте базу данных со следующими таблицами:
        //Clients: Содержит информацию о клиентах, включая их имя, электронную почту и адрес.
        //Products: Содержит информацию о товарах, включая их название и стоимость.
        //Orders: Содержит информацию о заказах, включая покупателя, дату заказа, адрес доставки.
        //OrderDetails: Содержит информацию о товарах в каждом заказе, включая идентификатор товара, идентификатор заказа и количество товара.
        //Вам необходимо написать один запрос LINQ to Entities, который извлекает список клиентов вместе со следующей информацией:
        //•	Общее количество заказов для каждого клиента.
        //•	Общая сумма, потраченная каждым клиентов.
        //•	Самый дорогой товар, купленный каждым клиентом.
        //Запрос должен вернуть список объектов со следующими свойствами:
        //•	Имя клиента
        //•	Электронная почта
        //•	Адрес
        //•	Общее количество заказов
        //•	Общая потраченная сумма
        //•	Название самого дорогого приобретенного товара
        using (ApplicationContext db = new ApplicationContext())
        {
            var clientInfo = db.Clients
                            .Select(client => new
                            {
                                ClientName = client.Name,
                                Email = client.Email,
                                Address = client.Address,
                                TotalOrders = client.Orders.Count(),
                                TotalSpent = client.Orders
                                                 .SelectMany(order => order.OrderDetails)
                                                 .Sum(detail => detail.Product.Price * detail.Quantity),
                                MostExpensiveProduct = client.Orders
                                                             .SelectMany(order => order.OrderDetails)
                                                             .OrderByDescending(detail => detail.Product.Price)
                                                             .Select(detail => detail.Product.Name)
                                                             .FirstOrDefault()
                            })
                            .ToList();
        }
        //Создайте базу данных «Список рассылки», для списка рассылки об акционных товарах.Нужно хранить такую информацию:
        //■ ФИО покупателя; 
        //■ Дата рождения покупателя; 
        //■ Пол покупателя; 
        //■ Email покупателя; 
        //■ Страна покупателя; 
        //■ Город покупателя; 
        //■ Список разделов, в которых заинтересован покупатель.Например: мобильные телефоны, ноутбуки, кухонная техника и т.д.; 
        //■ Акционные товары по каждому разделу.Акции привязаны к стране. У каждой акции есть время действия(дата старта, дата конце).
        //Создайте приложение, которое позволит пользователю подключиться и отключиться от базы данных «Список рассылки». Используя Entity Framework,
        //добавьте к приложению следующую функциональность(каждое действие в отдельном методе):

        using (ApplicationContext2 db = new ApplicationContext2())
        {
            var service = new Service(db);

            // Отображение всех покупателей
            var customers = service.GetAllCustomers();
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.FullName} ({customer.Email})");
            }

            // Отображение email всех покупателей
            var emails = service.GetAllCustomerEmails();
            foreach (var email in emails)
            {
                Console.WriteLine($"Email: {email}");
            }

            // Отображение списка разделов
            var categories = service.GetAllCategories();
            foreach (var category in categories)
            {
                Console.WriteLine($"Category: {category.Name}");
            }

            // Отображение списка акционных товаров
            var promotions = service.GetAllPromotions();
            foreach (var promotion in promotions)
            {
                Console.WriteLine($"Promotion: {promotion.Category.Name} in {promotion.Country.Name} from {promotion.StartDate} to {promotion.EndDate}");
            }

            // Отображение всех городов
            var cities = service.GetAllCities();
            foreach (var city in cities)
            {
                Console.WriteLine($"City: {city.Name}, Country: {city.Country.Name}");
            }

            // Отображение всех стран
            var countries = service.GetAllCountries();
            foreach (var country in countries)
            {
                Console.WriteLine($"Country: {country.Name}");
            }

            // Отображение всех покупателей из конкретного города
            int cityId = 1; // Замените на нужный cityId
            var customersByCity = service.GetCustomersByCity(cityId);
            foreach (var customer in customersByCity)
            {
                Console.WriteLine($"Customer in City {cityId}: {customer.FullName}");
            }

            // Отображение всех покупателей из конкретной страны
            int countryId = 1; // Замените на нужный countryId
            var customersByCountry = service.GetCustomersByCountry(countryId);
            foreach (var customer in customersByCountry)
            {
                Console.WriteLine($"Customer in Country {countryId}: {customer.FullName}");
            }

            // Отображение всех акций для конкретной страны
            var promotionsByCountry = service.GetPromotionsByCountry(countryId);
            foreach (var promotion in promotionsByCountry)
            {
                Console.WriteLine($"Promotion in Country {countryId}: {promotion.Category.Name} from {promotion.StartDate} to {promotion.EndDate}");
            }

            // Вставка информации о новом покупателе
            var newCustomer = new Customer
            {
                FullName = "John Doe",
                BirthDate = new DateTime(1990, 5, 23),
                Gender = "Male",
                Email = "john.doe@example.com",
                CityId = cityId // Предположим, что город существует
            };
            service.AddCustomer(newCustomer);

            // Вставка новой страны
            var newCountry = new Country { Name = "New Country" };
            service.AddCountry(newCountry);

            // Вставка нового города
            var newCity = new City { Name = "New City", CountryId = newCountry.Id };
            service.AddCity(newCity);

            // Вставка нового раздела
            var newCategory = new Category { Name = "New Category" };
            service.AddCategory(newCategory);

            // Вставка нового акционного товара
            var newPromotion = new Promotion
            {
                CategoryId = newCategory.Id,
                CountryId = newCountry.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10)
            };
            service.AddPromotion(newPromotion);

            // Обновление информации о покупателях
            newCustomer.FullName = "John Updated";
            service.UpdateCustomer(newCustomer);

            // Обновление информации о странах
            newCountry.Name = "Updated Country";
            service.UpdateCountry(newCountry);

            // Обновление информации о городах
            newCity.Name = "Updated City";
            service.UpdateCity(newCity);

            // Обновление информации о разделах
            newCategory.Name = "Updated Category";
            service.UpdateCategory(newCategory);

            // Обновление информации об акционных товарах
            newPromotion.EndDate = DateTime.Now.AddDays(20);
            service.UpdatePromotion(newPromotion);

            // Удаление информации о покупателях
            service.DeleteCustomer(newCustomer.Id);

            // Удаление информации о странах
            service.DeleteCountry(newCountry.Id);

            // Удаление информации о городах
            service.DeleteCity(newCity.Id);

            // Удаление информации о разделах
            service.DeleteCategory(newCategory.Id);

            // Удаление информации об акционных товарах
            service.DeletePromotion(newPromotion.Id);

            // Отображение списка городов конкретной страны
            var citiesByCountry = service.GetCitiesByCountry(countryId);
            foreach (var city in citiesByCountry)
            {
                Console.WriteLine($"City in Country {countryId}: {city.Name}");
            }

            // Отображение списка разделов конкретного покупателя
            var categoriesByCustomer = service.GetCategoriesByCustomer(newCustomer.Id);
            foreach (var category in categoriesByCustomer)
            {
                Console.WriteLine($"Category for Customer {newCustomer.FullName}: {category.Name}");
            }

            // Отображение списка акционных товаров конкретного раздела
            var promotionsByCategory = service.GetPromotionsByCategory(newCategory.Id);
            foreach (var promotion in promotionsByCategory)
            {
                Console.WriteLine($"Promotion for Category {newCategory.Name}: from {promotion.StartDate} to {promotion.EndDate}");
            }
        }

    }
}