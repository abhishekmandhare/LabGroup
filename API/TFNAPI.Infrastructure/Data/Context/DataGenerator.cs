using System.Linq;
using TFNAPI.Infrastructure.Data.Entities;

namespace TFNAPI.Infrastructure.Data.Context
{
    public class DataGenerator
    {
        public static void Initialize(TFNDBContext context)
        {
            if (context.WFactors.Any()) return;

            context.WFactors.AddRange(new WeightingFactors
            {
                WeightFactors = "10, 7, 8, 4, 6, 3, 5, 2, 1 ",
                Digits = 9
            },
            new WeightingFactors
            {
                WeightFactors = "10, 7, 8, 4, 6, 3, 5, 1 ",
                Digits = 8
            });

            context.SaveChanges();

        }
    }
}
