using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract;

public interface ICompanyService
{
    IDataResult<List<Company>> GetAll();
    IResult Add(CompanyDto companyDto);
    IResult Delete(Company company);
    IResult Update(Company company);
    IDataResult<Company> GetById(int companyId);
}