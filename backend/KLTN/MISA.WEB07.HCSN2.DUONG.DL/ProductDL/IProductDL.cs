using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;

namespace KLTN.DataLayer
{
    public interface IProductDL : IBaseDL<Product>
    {
        Task<PagingData<ProductStock>> GetPagingProduct(ParamPaging param, int pageSize, int pageNumber);
    }
}