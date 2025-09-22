using AntimicrobialPrescriptions.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AntimicrobialPrescriptions.Infrastructure.Tests.Data
{
    public class ApplicationDbContextTests : IDisposable
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
        }

        [Fact]
        public void CanCreateDbContext_AndHasPrescriptionsDbSet()
        {
            Assert.NotNull(_context);
            Assert.NotNull(_context.Prescriptions);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
