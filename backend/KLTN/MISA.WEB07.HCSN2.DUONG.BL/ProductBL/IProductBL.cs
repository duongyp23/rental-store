using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;

namespace KLTN.BussinesLayer
{
    public interface IProductBL : IBaseBL<Product>
    {
        Task<bool> AddProductToCart(Guid productId, Guid userId, string optionCode, int quantity);
        Task<PagingData<ProductStock>> GetPagingProduct(ParamPaging param, int pageSize, int pageNumber);
        Task<Guid> InsertProduct(ProductData data);
        Task<bool> UpdateProduct(ProductData data);
    }
}