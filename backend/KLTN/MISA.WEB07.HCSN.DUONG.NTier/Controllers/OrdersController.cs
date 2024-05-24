﻿using KLTN.BussinesLayer;
using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;
using KLTN.Common.Enums;
using KLTN.NTier.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KLTN.NTier.Controllers
{
    public class OrdersController : BasesController<Order>
    {
        #region Field

        private IOrderBL _orderBL;

        #endregion

        #region Constructor

        public OrdersController(IOrderBL orderBL) : base(orderBL)
        {
            _orderBL = orderBL;
        }

        #endregion

        #region Method
        [Authorize]
        [HttpGet("{id}")]

        public override async Task<IActionResult> GetDataById(Guid id)
        {
            try
            {
                OrderData record = await _orderBL.GetOrderData(id);
                return StatusCode(StatusCodes.Status200OK, record);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }
        [Authorize]
        [HttpGet("GetOrderOfUser/{id}")]

        public async Task<IActionResult> GetOrderOfUser(Guid id)
        {
            try
            {
                List<Order> records = await _orderBL.GetOrderOfUser(id);
                return StatusCode(StatusCodes.Status200OK, records);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }
        [Authorize]
        [HttpPost("UpdateOrderData")]
        public async Task<IActionResult> UpdateOrderData([FromBody]OrderData orderData)
        {
            bool result = await _orderBL.UpdateOrderData(orderData);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [Authorize]
        [HttpPost("AddStockOrder")]
        public async Task<IActionResult> AddStockOrder([FromBody] OrderData orderData)
        {
            bool result = await _orderBL.AddStockOrder(orderData);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [Authorize]
        [HttpPost("Payment")]

        public async Task<IActionResult> Payment(Guid id)
        {
            try
            {
                object result = await _orderBL.Payment(id);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }
        [HttpPost("Dashboard")]

        public async Task<IActionResult> Dashboard()
        {
            try
            {
                object result = await _orderBL.Dashboard();
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }
        #endregion
    }
}
