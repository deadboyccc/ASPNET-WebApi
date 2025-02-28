using BGwalks.API.Data;
using BGwalks.API.Migrations;
using BGwalks.API.Models.Domain;

using Bogus;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BGwalks.API.Generators;

public class WalkDataGenerator
{
    // Injected dependencies
    private readonly Faker _faker;
    private readonly BGWalksDbContext _context;
    private readonly Random _random = new Random();

    public WalkDataGenerator(IConfiguration configuration)
    {
        // Initialize Faker and DB Context
        string connectionString = configuration.GetConnectionString("BGWalksConnectionString")!;


        // Check if connection string is valid
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Database connection string is missing or invalid.");
        }

        // Create or update database schema
        var options = new DbContextOptionsBuilder<BGWalksDbContext>()
            .UseSqlServer(connectionString)
            .Options;






        // Create or update the database schema
        _context = new BGWalksDbContext(options);
        _faker = new Faker();
    }

    public void GenerateWalkData(int numberOfWalks)
    {
        // Ensure that the necessary tables exist in the database
        var difficulties = _context.Difficulties.ToList();
        var regions = _context.Regions.ToList();

        // Check if there are any difficulties and regions in the database
        if (!difficulties.Any() || !regions.Any())
        {
            Console.WriteLine("Error: Ensure difficulties and regions exist in the database.");
            return;
        }

        // Generate random walk records
        for (int i = 0; i < numberOfWalks; i++)
        {
            var walk = new WalkDomain();
            walk.Id = Guid.NewGuid();
            walk.Name = _faker.Address.City();
            walk.Description = _faker.Lorem.Sentance();
            walk.Distance = _faker.Random.Double();
            walk.ImageUrl = _faker.Image.ToString();
            walk.DifficultyId = difficulties[_random.Next(difficulties.Count)].Id;
            walk.regionId = regions[_random.Next(regions.Count)].Id;



            _context.Walks.Add(walk);
        }



        // Save changes to the database
        _context.SaveChanges();
        Console.WriteLine($"Generated {numberOfWalks} walk records.");
    }
}