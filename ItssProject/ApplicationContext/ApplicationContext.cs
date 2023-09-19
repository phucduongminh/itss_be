using ItssProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ItssProject.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {

        }
        public DbSet<BookMark> BookMarks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CoffeeCategory> CoffeeCategories { get; set; }
        public DbSet<CoffeeShop> CoffeeShops { get; set; }
        public DbSet<SubCoffeeShop> SubCoffeeShops { get; set; }
        public DbSet<ReviewImage> ReviewImages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ReviewReaction> ReviewReactions { get; set; }
    }
}
