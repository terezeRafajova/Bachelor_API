using System.Drawing.Printing;
using Bachelor_API.Data;
using Bachelor_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace Bachelor_API.Tests
{
    public class LessonEndpointsTests
    {  
        [Fact]
        public async Task ShareLesson_TestResult()
        {
            var dbContextOptions = new DbContextOptionsBuilder<Bachelor_APIContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new Bachelor_APIContext(dbContextOptions))
            {
                //create lesson to share
                var lesson = new Lesson
                {
                    Title = "TestLeson",
                    Username = "TestUser",
                    SharingTime = 120,
                    NumberOfPages = 1,
                    Descriptions = new List<Description>
                    {
                        new Description
                        {
                            Slot = 2,
                            Text = "TestTextPage1",
                            PageNumber = 1
                        },
                        new Description
                        {
                            Slot = 4,
                            Text = "TestTextPage2",
                            PageNumber = 1
                        }
                    },
                    CodeBlocks = new List<CodeBlock>
                    {
                        new CodeBlock
                        {
                            Slot = 3,
                            JsonBlocks = "Blocks Json",
                            PageNumber = 1
                        },
                        new CodeBlock
                        {
                            Slot = 5,
                            JsonBlocks = "Blocks Json",
                            PageNumber = 1
                        }
                    },
                    Titles = new List<Title>
                    {
                        new Title
                        {
                            Slot = 1,
                            Text = "TestTitlePage1",
                            PageNumber = 1
                        }
                    }
                };

                var result = await LessonEndpoints.ShareLesson(lesson, dbContext);
                // Assert 
                //lesson type
                var createdLesson = Assert.IsType<Lesson>(result.Value);
                Assert.NotNull(createdLesson);

                //Lesson sharing code
                Assert.True(result.Value.SharingCode >= 100000 && result.Value.SharingCode <= 999999);     

                //lesson components
                Assert.Equal(2, result.Value.Descriptions.Count);       
                Assert.Equal(2, result.Value.CodeBlocks.Count);       
                Assert.Single(result.Value.Titles);
            };
            
        }

        [Fact]
        public async Task GetLessonByCode_ExistingCode()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Bachelor_APIContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using var context = new Bachelor_APIContext(options);

            var lesson = new Lesson
            {
                LessonId = 2,
                Title = "TestLeson",
                Username = "TestUser",
                SharingTime = 120,
                SharingCode = 123456,
                NumberOfPages = 1,
                Descriptions = new List<Description>
                {
                    new Description
                    {
                        Slot = 2,
                        Text = "TestTextPage1",
                        PageNumber = 1
                    },
                    new Description
                    {
                        Slot = 4,
                        Text = "TestTextPage2",
                        PageNumber = 1
                    }
                },
                CodeBlocks = new List<CodeBlock>
                {
                    new CodeBlock
                    {
                        Slot = 3,
                        JsonBlocks = "Blocks Json",
                        PageNumber = 1
                    },
                    new CodeBlock
                    {
                        Slot = 5,
                        JsonBlocks = "Blocks Json",
                        PageNumber = 1
                    }
                },
                Titles = new List<Title>
                {
                    new Title
                    {
                        Slot = 1,
                        Text = "TestTitlePage1",
                        PageNumber = 1
                    }
                }
            };

            context.Lesson.Add(lesson);
            await context.SaveChangesAsync();

            var result = await LessonEndpoints.GetLessonByCode(123456, context);

            // Assert
            var actionResult = Assert.IsType<Results<Ok<Lesson>, NotFound>>(result);
            var lessonResult = Assert.IsType<Ok<Lesson>>(actionResult.Result);
            var lessonValue = lessonResult.Value;
            Assert.Equal(lesson.LessonId, lessonValue.LessonId);
            Assert.Equal(lesson.SharingCode, lessonValue.SharingCode);
            Assert.Equal(lesson.Title, lessonValue.Title);
            Assert.Equal(lesson.Username, lessonValue.Username);
            Assert.Equal(lesson.SharingTime, lessonValue.SharingTime);
            Assert.Equal(lesson.NumberOfPages, lessonValue.NumberOfPages);
            Assert.Equal(lesson.Descriptions.Count, lessonValue.Descriptions.Count);
            Assert.Equal(lesson.CodeBlocks.Count, lessonValue.CodeBlocks.Count);   
            Assert.Equal(lesson.Titles.Count, lessonValue.Titles.Count);
        }
        [Fact]
        public async Task GetLessonByCode_NonExistingCode()
        {
            var options = new DbContextOptionsBuilder<Bachelor_APIContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new Bachelor_APIContext(options))
            {
                var result = await LessonEndpoints.GetLessonByCode(123457, context);	

                // Assert
                var actionResult = Assert.IsType<Results<Ok<Lesson>, NotFound>>(result);
                Assert.IsType<NotFound>(actionResult.Result);
            }
        }
    
    }
}	