using Business.Abstract;
using DataAccess.Abstract;

namespace Business.Concrete;

public class CurrencyManager:ICurrencyService
{
    private ICurrencyDal _currencyDal;

    public CurrencyManager(ICurrencyDal currencyDal)
    {
        _currencyDal = currencyDal;
    }
}