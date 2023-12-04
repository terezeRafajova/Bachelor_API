using Bachelor_API.Data;
using Bachelor_API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace Bachelor_API.Tests
{
    public class LessonEndpointsTests
    {  
        [Fact]
        public async Task GetAllLessons_ReturnsAllLessons()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<Bachelor_APIContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new Bachelor_APIContext(dbContextOptions))
            {
                // Add test data to the in-memory database
                dbContext.Lesson.Add(new Lesson { LessonId = 1, Title = "Lesson 1" });
                dbContext.Lesson.Add(new Lesson { LessonId = 2, Title = "Lesson 2" });
                await dbContext.SaveChangesAsync();
            }

            using (var dbContext = new Bachelor_APIContext(dbContextOptions))
            {
                // Act
                var result = await LessonEndpoints.GetAllLessons(dbContext);

                // Assert
                Assert.Equal(2, result.Length);
            }
        }
    
    }
}	