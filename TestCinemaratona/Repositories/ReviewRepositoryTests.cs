using Microsoft.EntityFrameworkCore;
using cinemaratona.Data;
using cinemaratona.Models;

public class ReviewRepositoryTests
{
    private DbContextOptions<CinemaratonaContext> BuildInMemoryOptions(string dbName) =>
        new DbContextOptionsBuilder<CinemaratonaContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

    [Fact]
    public void AddReview_Should_PersistToDatabase()
    {
        // arrange: cria options e instancia context
        var options = BuildInMemoryOptions("AddReviewDb");
        using (var context = new CinemaratonaContext(options))
        {
            var review = new Review {
                UserId = 1,
                MovieId = 42,
                Opinion = "Ótimo filme!",
                CreatedAt = DateTime.UtcNow,
                Rating = 5,
                Recommended = true,
                Watched = true
            };

            // act: adiciona e salva
            context.Review.Add(review);
            context.SaveChanges();
        }

        // assert: abre novo contexto para simular outra unidade de trabalho
        using (var context = new CinemaratonaContext(options))
        {
            var persisted = context.Review.Single();
            Assert.Equal(42, persisted.MovieId);
            Assert.True(persisted.Recommended);
            Assert.Equal("Ótimo filme!", persisted.Opinion);
        }
    }
}