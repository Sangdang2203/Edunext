using Edunext_API.Models;

namespace Edunext_API.Services
{
    public class OrderDetailService
    {
        private readonly DatabaseContext databaseContext;

        public OrderDetailService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
    }
}
