using MicroCredentials.Data.Models;
using MicroCredentials.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroCredentials.Services
{
    public class QuoteService : IQuoteService
    {

        private IGenericRepository<Quote> _repo = null;
        public QuoteService(IGenericRepository<Quote> repo)
        {
            _repo = repo;
        }

        public List<Quote> GetAllQuotes()
        {
            return _repo.GetAll().ToList();
        }

        public Quote GetQuote(int id)
        {
            return _repo.GetById(id);
        }

        public bool IsQuoteExist(int customerId)
        {
            var quotes = _repo.GetAll();
            return quotes.Any(e => e.CustomerId == customerId);
        }

        public void AddQuote(Quote quote)
        {
            quote.MaturityAmount = GetMaturityAmount(quote);
            _repo.Insert(quote);
        }

        public void EditQuote(Quote quote)
        {
            quote.MaturityAmount = GetMaturityAmount(quote);
            _repo.Update(quote);
        }

        public void DeleteQuote(int id)
        {
            _repo.Delete(id);
        }

        static decimal GetMaturityAmount(Quote quote)
        {
            if (quote != null && quote.EndDate > quote.StartDate)
            {
                var durationInDays = ((TimeSpan)(quote.EndDate - quote.StartDate)).Days;
                decimal ROI = GetROI(durationInDays);
                return quote.ContributionAmount * ((100 + ROI) / 100);
            }
            return 0.00m;
        }

        static decimal GetROI(int durationInDays)
        {
            decimal interestRate = 0.00m;
            if (durationInDays <= 30)
            {
                interestRate = 0.5m;
            }
            else if (durationInDays > 30 && durationInDays <= 90)
            {
                interestRate = 1.5m;
            }
            else if (durationInDays > 90 && durationInDays <= 120)
            {
                interestRate = 2.0m;
            }
            else
            {
                interestRate = 5.0m;
            }
            return interestRate;
        }
    }
}
