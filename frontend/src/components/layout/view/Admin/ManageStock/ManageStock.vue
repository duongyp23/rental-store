<template>
  <div class="manage-stock">
    <div class="flex-row between">
      <div class="list-header mt-1 mb-1">Quản lý nhập, xuất kho</div>
      <div class="flex-end">
        <button class="form-btn btn3" @click="openFormAdd(2)">Nhập kho</button>
        <button class="form-btn btn3 ml-1" @click="openFormAdd(3)">
          Xuất kho
        </button>
      </div>
    </div>
    <table class="table mt-1">
      <thead>
        <tr>
          <th class="left w-15">Loại chứng từ</th>
          <th class="left w-15">Người thực hiện</th>
          <th class="center w-15">Đơn giá</th>
          <th class="left">Diễn giải</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in listData" :key="item.order_id">
          <td class="left w-15">{{ getOrderTypeName(item.order_type) }}</td>
          <td class="left w-15">{{ item.user_name }}</td>
          <td class="center w-15">
            {{ replaceNumber(item.total_order_payment) }}
          </td>
          <td class="left">{{ item.description }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
import { apiGetPagingOrder } from "@/api/orderApi";
import { replaceNumber, getOrderTypeName } from "@/method/methodFormat";
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