using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_007_09_09_2024
{
    public class Service
    {
        private readonly ApplicationContext2 _db;

        public Service (ApplicationContext2 db)
        {
            _db = db;
        }

        // Отображение всех покупателей
        public List<Customer> GetAllCustomers()
        {
            return _db.Customers.Include(c => c.City).ToList();
        }

        // Отображение email всех покупателей
        public List<string> GetAllCustomerEmails()
        {
            return _db.Customers.Select(c => c.Email).ToList();
        }

        // Отображение списка разделов
        public List<Category> GetAllCategories()
        {
            return _db.Categories.ToList();
        }

        // Отображение списка аукционных товаров
        public List<Promotion> GetAllPromotions()
        {
            return _db.Promotions.Include(p => p.Category).Include(p => p.Country).ToList();
        }

        // Отображение всех городов
        public List<City> GetAllCities()
        {
            return _db.Cities.Include(c => c.Country).ToList();
        }

        // Отображение всех стран
        public List<Country> GetAllCountries()
        {
            return _db.Countries.ToList();
        }

        // Отображение всех покупателей из конкретного города
        public List<Customer> GetCustomersByCity(int cityId)
        {
            return _db.Customers.Where(c => c.CityId == cityId).Include(c => c.City).ToList();
        }

        // Отображение всех покупателей из конкретной страны
        public List<Customer> GetCustomersByCountry(int countryId)
        {
            return _db.Customers.Include(c => c.City)
                                     .Where(c => c.City.CountryId == countryId)
                                     .ToList();
        }

        // Отображение всех акций для конкретной страны
        public List<Promotion> GetPromotionsByCountry(int countryId)
        {
            return _db.Promotions.Where(p => p.CountryId == countryId)
                                      .Include(p => p.Category)
                                      .ToList();
        }

        // Вставка информации о новых покупателях
        public void AddCustomer(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
        }

        // Вставка новых стран
        public void AddCountry(Country country)
        {
            _db.Countries.Add(country);
            _db.SaveChanges();
        }

        // Вставка новых городов
        public void AddCity(City city)
        {
            _db.Cities.Add(city);
            _db.SaveChanges();
        }

        // Вставка информации о новых разделах
        public void AddCategory(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
        }

        // Вставка информации о новых акционных товарах
        public void AddPromotion(Promotion promotion)
        {
            _db.Promotions.Add(promotion);
            _db.SaveChanges();
        }

        // Обновление информации о покупателях
        public void UpdateCustomer(Customer customer)
        {
            _db.Customers.Update(customer);
            _db.SaveChanges();
        }

        // Обновление информации о странах
        public void UpdateCountry(Country country)
        {
            _db.Countries.Update(country);
            _db.SaveChanges();
        }

        // Обновление информации о городах
        public void UpdateCity(City city)
        {
            _db.Cities.Update(city);
            _db.SaveChanges();
        }

        // Обновление информации о разделах
        public void UpdateCategory(Category category)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
        }

        // Обновление информации об аукционных товарах
        public void UpdatePromotion(Promotion promotion)
        {
            _db.Promotions.Update(promotion);
            _db.SaveChanges();
        }

        // Удаление информации о покупателях
        public void DeleteCustomer(int customerId)
        {
            var customer = _db.Customers.Find(customerId);
            if (customer != null)
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();
            }
        }

        // Удаление информации о странах
        public void DeleteCountry(int countryId)
        {
            var country = _db.Countries.Find(countryId);
            if (country != null)
            {
                _db.Countries.Remove(country);
                _db.SaveChanges();
            }
        }

        // Удаление информации о городах
        public void DeleteCity(int cityId)
        {
            var city = _db.Cities.Find(cityId);
            if (city != null)
            {
                _db.Cities.Remove(city);
                _db.SaveChanges();
            }
        }

        // Удаление информации о разделах
        public void DeleteCategory(int categoryId)
        {
            var category = _db.Categories.Find(categoryId);
            if (category != null)
            {
                _db.Categories.Remove(category);
                _db.SaveChanges();
            }
        }

        // Удаление информации об аукционных товарах
        public void DeletePromotion(int promotionId)
        {
            var promotion = _db.Promotions.Find(promotionId);
            if (promotion != null)
            {
                _db.Promotions.Remove(promotion);
                _db.SaveChanges();
            }
        }

        // Отображение списка городов конкретной страны
        public List<City> GetCitiesByCountry(int countryId)
        {
            return _db.Cities.Where(c => c.CountryId == countryId).ToList();
        }

        // Отображение списка разделов конкретного покупателя
        public List<Category> GetCategoriesByCustomer(int customerId)
        {
            return _db.CustomerCategories
                           .Where(cc => cc.Id == customerId)
                           .Include(cc => cc.Category)
                           .Select(cc => cc.Category)
                           .ToList();
        }

        // Отображение списка аукционных товаров конкретного раздела
        public List<Promotion> GetPromotionsByCategory(int categoryId)
        {
            return _db.Promotions.Where(p => p.Id == categoryId).ToList();
        }
    }

}
