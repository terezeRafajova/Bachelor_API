using System;
using System.Linq;
using Bachelor_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Bachelor_API.Model
{
    public class CodeGenerator
    {
        private readonly Bachelor_APIContext _dbContext;
        private readonly Random _random;

        public CodeGenerator(Bachelor_APIContext dbContext)
        {
            _dbContext = dbContext;
            _random = new Random();
        }

        public int GenerateUniqueCode()
        {
            int code;
            do
            {
                code = GenerateRandomCode();
            } while (CodeExistsInDatabase(code));

            return code;
        }

        private int GenerateRandomCode()
        {
            const int minCode = 100000; // Smallest 6-digit number
            const int maxCode = 999999; // Largest 6-digit number
            return _random.Next(minCode, maxCode + 1);
        }

        private bool CodeExistsInDatabase(int code)
        {
            // Replace this with your actual Entity Framework query to check if the code exists in the database.
            return _dbContext.Set<UnitPlan>()
                .Any(obj => obj.Code == code);
        }
    }
}
