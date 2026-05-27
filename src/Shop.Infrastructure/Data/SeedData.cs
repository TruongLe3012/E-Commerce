using BCrypt.Net;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            // Categories
            var categories = new List<Category>
            {
                new Category { Name = "Laptop" },
                new Category { Name = "Phone" },
                new Category { Name = "Accessory" }
            };

            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();

            // Users
            var admin = new User
            {
                FullName = "Admin",
                Email = "admin@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "Admin"
            };

            var customer = new User
            {
                FullName = "Nguyen Van A",
                Email = "user@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "Customer"
            };

            context.Users.AddRange(admin, customer);
            await context.SaveChangesAsync();

            // Products
            var products = new List<Product>
            {
                new Product
                {
                    Name = "MacBook Air M3",
                    Description = "Laptop Apple M3",
                    Price = 32000000,
                    Stock = 10,
                    CategoryId = categories[0].Id
                },

                new Product
                {
                    Name = "Dell XPS 15",
                    Description = "Laptop Dell cao cấp",
                    Price = 28000000,
                    Stock = 5,
                    CategoryId = categories[0].Id
                },

                new Product
                {
                    Name = "iPhone 15 Pro",
                    Description = "Điện thoại Apple",
                    Price = 27000000,
                    Stock = 15,
                    CategoryId = categories[1].Id
                },

                new Product
                {
                    Name = "Samsung S25",
                    Description = "Flagship Samsung",
                    Price = 24000000,
                    Stock = 12,
                    CategoryId = categories[1].Id
                },

                new Product
                {
                    Name = "Logitech G Pro X",
                    Description = "Gaming headset",
                    Price = 3500000,
                    Stock = 20,
                    CategoryId = categories[2].Id
                }
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();

            // Cart
            var cartItems = new List<CartItem>
            {
                new CartItem
                {
                    UserId = customer.Id,
                    ProductId = products[0].Id,
                    Quantity = 1
                },

                new CartItem
                {
                    UserId = customer.Id,
                    ProductId = products[4].Id,
                    Quantity = 2
                }
            };

            context.CartItems.AddRange(cartItems);
            await context.SaveChangesAsync();

            // Orders
            var order = new Order
            {
                UserId = customer.Id,
                OrderDate = DateTime.UtcNow,
                Status = "Completed",
                TotalPrice = 39000000
            };

            context.Orders.Add(order);
            await context.SaveChangesAsync();

            // OrderItems
            var orderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = products[0].Id,
                    Quantity = 1,
                    Price = 32000000
                },

                new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = products[4].Id,
                    Quantity = 2,
                    Price = 3500000
                }
            };

            context.OrderItems.AddRange(orderItems);
            await context.SaveChangesAsync();
        }
    }
}