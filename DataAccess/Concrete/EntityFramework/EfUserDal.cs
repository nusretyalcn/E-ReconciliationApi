using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;

namespace DataAccess.Concrete.EntityFramework;

public class EfUserDal:EfEntityRepositoryBase<User,EfDbContext>,IUserDal
{
    public List<OperationClaim> GetClaims(User user, int companyId)
    {
        using (var context = new EfDbContext())
        {
            var result = from operationClaim in context.OperationClaims
                join userOperationClaim in context.UserOperationClaims 
                    on operationClaim.Id equals userOperationClaim.OperationClaimId
                    where userOperationClaim.CompanyId == companyId && userOperationClaim.UserId == user.Id
                    select new OperationClaim
                {
                    Id = operationClaim.Id,
                    Name = operationClaim.Name,
                };
            return result.ToList();
        }
    }
}