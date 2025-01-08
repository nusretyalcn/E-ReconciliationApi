using Business.Abstract;
using DataAccess.Abstract;

namespace Business.Concrete;

public class BaBsReconciliationDetailManager:IBaBsReconciliationDetailService
{
    private IBaBsReconciliationDetailDal _baBsReconciliationDal;

    public BaBsReconciliationDetailManager(IBaBsReconciliationDetailDal baBsReconciliationDal)
    {
        _baBsReconciliationDal = baBsReconciliationDal;
    }
}