using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Dapper;
using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;
using MySqlConnector;

namespace KLTN.DataLayer
{
    public class ProductDL : BaseDL<Product>, IProductDL
    {
        public async Task<PagingData<ProductStock>> GetPagingProduct(ParamPaging param, int pageSize, int pageNumber)
        {
            string tableName = "view_product_stock";

            string whereClause = "";
            if (param.filters != null && param.filters.Count > 0)
            {
                whereClause += "WHERE ";
                for (int i = 0; i < param.filters.Count; i++)
                {
                    Filter filter = param.filters[i];
                    if (i > 0)
                    {
                        whereClause += " AND ";
                    }
                    whereClause += $"{filter.columnName} {filter.operatorValue} {filter.filterValue} ";
                }
            }

            
            if(param.listCategoryId != null && param.listCategoryId.Count > 0)
            {
                List<Guid> listProductId = await GetProductOfCategory(param.listCategoryId);
                if(listProductId != null && listProductId.Count > 0)
                {
                    string idList = string.Join("','", listProductId);
                    string subWhere = $" product_id in ('{idList}')";
                    if (whereClause != "")
                    {
                        whereClause += " AND ";
                        whereClause += subWhere;
                    }
                    else
                    {
                        whereClause = "WHERE " + subWhere;
                    }
                } else
                {
                    return new PagingData<ProductStock>()
                    {
                        Data = new List<ProductStock>(),
                        TotalCount = 0
                    };
                }
                
            }
            String getPagingData = $"SELECT * FROM {tableName} {whereClause} LIMIT  {(pageNumber - 1) * pageSize}, {pageSize};";
            String getTotalData = $"SELECT COUNT(*) FROM {tableName} {whereClause};";
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            List<ProductStock> datas = (List<ProductStock>)await mySqlConnection.QueryAsync<ProductStock>(getPagingData);
            var total = await mySqlConnection.QueryAsync<int>(getTotalData);
            mySqlConnection.Close();
            return new PagingData<ProductStock>()
            {
                Data = datas,
                TotalCount = (int)total.First()
            };
        }

        private async Task<List<Guid>> GetProductOfCategory(List<Guid> listCategoryId)
        {
                string idList = string.Join("','", listCategoryId);
                string query = $"SELECT product_id from product_category where category_id in ('{idList}') group by product_id having count(*) = {listCategoryId.Count};";

                var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

                List<Guid> datas = (List<Guid>)await mySqlConnection.QueryAsync<Guid>(query);
                mySqlConnection.Close();
                return datas;
        }
    }
}