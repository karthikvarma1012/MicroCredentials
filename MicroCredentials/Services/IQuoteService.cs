using MicroCredentials.Data.Models;
using System.Collections.Generic;

namespace MicroCredentials.Services
{
    public interface IQuoteService
    {
        List<Quote> GetAllQuotes();

        Quote GetQuote(int id);

        bool IsQuoteExist(int customerId);

        void AddQuote(Quote quote);

        void EditQuote(Quote quote);

        void DeleteQuote(int id);
    }
}
