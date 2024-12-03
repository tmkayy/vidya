using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;

namespace vidya.Data.Seeder.Fakers
{
    public class ActivationKeyFaker : Faker<ActivationKey>
    {
        private readonly VidyaDbContext _context;
        private readonly List<int> _gameIds;

        public ActivationKeyFaker(VidyaDbContext context)
        {
            _context = context;
            _gameIds = _context.Games.Select(x => x.Id).ToList();

            RuleFor(ak => ak.Key, GenerateKey());
            RuleFor(ak => ak.GameId, f => f.PickRandom(_gameIds));
        }

        private Func<Faker, ActivationKey, string> GenerateKey()
        {
            return (f, ak) =>
            {
                int parts = f.Random.Int(4, 6);
                var keyParts = new string[parts];

                for (int i = 0; i < parts; i++)
                {
                    keyParts[i] = f.Random.String2(5, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
                }

                return string.Join("-", keyParts);
            };
        }
    }
}
