using Business.Abstract;
using DataAccess.Abstract;

namespace Business.Concrete;

public class AccountReconciliationDetailManager: IAccountReconciliationDetailService
{
    private IAccountReconciliationDetailDal _accountReconciliationDetailDal;

    public AccountReconciliationDetailManager(IAccountReconciliationDetailDal accountReconciliationDetailDal)
    {
        _accountReconciliationDetailDal = accountReconciliationDetailDal;
    }
}