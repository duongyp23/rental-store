<template>
  <div class="manage-stock">
    <div class="flex-row between">
      <div class="list-header mt-1 mb-1">Quản lý nhập kho, thanh lý</div>
      <div class="flex-end">
        <button class="form-btn btn3" @click="openFormAdd(2)">Nhập kho</button>
        <button class="form-btn btn3 ml-1" @click="openFormAdd(3)">
          Thanh lý
        </button>
      </div>
    </div>
    <table class="table mt-1">
      <thead>
        <tr>
          <th class="w-5 center">STT</th>
          <th class="left w-10">Loại chứng từ</th>
          <th class="center w-15">Ngày chứng từ chứng từ</th>
          <th class="left w-15">Người thực hiện</th>
          <th class="right w-10">Đơn giá</th>
          <th class="left">Diễn giải</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(item, index) in listData"
          :key="item.order_id"
          @click="viewStockOrder(item.order_id, item.order_type)"
        >
          <th class="w-5 center">{{ index + 1 }}</th>
          <td class="left w-10 bold blue" v-if="item.order_type == 2">
            Nhập kho
          </td>
          <td class="left w-10 bold red" v-if="item.order_type == 3">
            Thanh lý
          </td>
          <td class="center w-15">{{ datetimeToDate(item.order_date) }}</td>
          <td class="left w-15">{{ item.user_name }}</td>
          <td class="right w-10">
            {{
              item.order_type == 3
                ? replaceNumber(item.total_order_payment)
                : replaceNumber(item.total_order_return)
            }}
          </td>
          <td class="left">{{ item.description }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
import { apiGetPagingOrder } from "@/api/orderApi";
import {
  replaceNumber,
  getOrderTypeName,
  datetimeToDate,
} from "@/method/methodFormat";
export default {
  data() {
    return {
      listData: [],
      pageNumber: 1,
    };
  },
  components: {},
  methods: {
    replaceNumber,
    getOrderTypeName,
    datetimeToDate,
    async getPagingData() {
      let filter = [];
      filter.push({
        columnName: "order_type",
        filterValue: 1,
        operatorValue: "!=",
      });
      await apiGetPagingOrder(filter, 20, this.pageNumber).then((response) => {
        this.listData = response.data.data;
      });
    },
    openFormAdd(orderType) {
      this.$router.push({
        name: "DetailStock",
        params: { type: orderType, state: 1 },
      });
    },
    viewStockOrder(id, orderType) {
      this.$router.push({
        name: "DetailStock",
        params: { type: orderType, state: 2, id: id },
      });
    },
  },
  created() {
    this.getPagingData();
  },
  mounted() {},
};
</script>
<style>
@import url(./ManageStock.scss);
@import url(@/css/layout/table.scss);
</style>