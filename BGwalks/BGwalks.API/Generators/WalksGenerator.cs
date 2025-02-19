using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BGwalks.API.Data;
using BGwalks.API.Models.Domain;
using BGwalks.API.Migrations;

namespace BGwalks.API.Generators;

public class WalkDataGenerator
{
  private readonly Faker _faker; // Bogus Faker instance
  private readonly BGWalksDbContext _context;
  private readonly Random _random = new Random();

  public WalkDataGenerator(IConfiguration configuration)
  {
    string connectionString = configuration.GetConnectionString("BGWalksConnectionString")!;

    if (string.IsNullOrEmpty(connectionString))
    {
      throw new InvalidOperationException("Database connection string is missing or invalid.");
    }

    var options = new DbContextOptionsBuilder<BGWalksDbContext>()
        .UseSqlServer(connectionString)
        .Options;

    _context = new BGWalksDbContext(options);
    _faker = new Faker();
  }

  public void GenerateWalkData(int numberOfWalks)
  {
    var difficulties = _context.Difficulties.ToList();
    var regions = _context.Regions.ToList();

    if (!difficulties.Any() || !regions.Any())
    {
      Console.WriteLine("Error: Ensure difficulties and regions exist in the database.");
      return;
    }

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



    _context.SaveChanges();
    Console.WriteLine($"Generated {numberOfWalks} walk records.");
  }
}
