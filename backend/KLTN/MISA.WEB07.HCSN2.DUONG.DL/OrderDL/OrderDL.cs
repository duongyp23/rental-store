using Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using KLTN.Common.Entity;
using MySqlConnector;
using KLTN.Common.Entity.DTO;
using System.Data;

namespace KLTN.DataLayer
{
    public class OrderDL : BaseDL<Order>, IOrderDL
    {
        public async Task<object> GetDashboardData()
        {
            string query1 = "SELECT SUM(o.total_order_deposit) FROM orders o WHERE o.order_type = 1 AND o.status IN (3, 4, 5);";
            string query2 = "SELECT SUM(o.total_order_payment) FROM orders o WHERE o.order_type = 1 AND o.status IN (3, 4, 5);";
            string query3 = "SELECT SUM(o.total_order_return) FROM orders o WHERE o.order_type = 2; ";
            string query4 = "SELECT SUM(o.total_order_payment) FROM orders o WHERE o.order_type = 3;";
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);
            var total1 = await mySqlConnection.QueryAsync<int>(query1);
            var total2 = await mySqlConnection.QueryAsync<int>(query2);
            var total3 = await mySqlConnection.QueryAsync<int>(query3);
            var total4  = await mySqlConnection.QueryAsync<int>(query4);
            mySqlConnection.Close();

            return new 
            {
                TotalPrice = (int)total1.First(),
                TotalProfit = (int)total2.First(),
                TotalInward = (int)total3.First(),
                TotalOutward = (int)total4.First(),
            };
        }

        public async Task<List<Product>> GetProductInOrder(Guid id)
        {
            var param = new DynamicParameters();
            param.Add("@order_id", id);
            String queryString = $"SELECT * FROM product WHERE product_id in (SELECT product_id FROM order_detail WHERE order_id = @order_id)";
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            List<Product> products = (List<Product>)await mySqlConnection.QueryAsync<Product>(queryString, param);
            mySqlConnection.Close();
            return products;
        }

        public async Task<Order> GetWaitOrder(Guid userId)
        {

            var sb = new StringBuilder();
            var param = new DynamicParameters();
            param.Add("@user_id", userId);
            sb.AppendFormat($"SELECT * FROM orders WHERE status = 1 and user_id = @user_id;");
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            List<Order> result = (List<Order>)await mySqlConnection.QueryAsync<Order>(sb.ToString(), param);
            mySqlConnection.Close();
            if(result != null && result.Count > 0)
            {
                return result[0];
            }
            return null;
        }

        public async Task UpdateQuantityProduct(Guid? product_id)
        {
            var param = new DynamicParameters();
            param.Add("@product_id", product_id);
            String queryString = $"UPDATE product p SET p.quantity = p.quantity - 1, p.quantity_rental = IFNULL(p.quantity_rental, 0) + 1 WHERE product_id = @product_id;";
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            await mySqlConnection.ExecuteAsync(queryString, param);
            mySqlConnection.Close();
        }
    }
}