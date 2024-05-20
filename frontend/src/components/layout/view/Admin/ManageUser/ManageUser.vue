<template>
  <div class="manage-user">
    <div class="list-header mt-1 mb-1">Danh sách người dùng</div>
    <table class="table">
      <thead>
        <tr>
          <th class="w-5 center">STT</th>
          <th class="w-15 left">Tên người dùng</th>
          <th class="w-15 left">Số điện thoại</th>
          <th class="left">Địa chỉ</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(item, index) in listData" :key="item.user_id">
          <th class="w-5 center">{{ index + 1 }}</th>
          <td class="w-15 left">{{ item.user_name }}</td>
          <td class="w-15 left">{{ item.phone_number }}</td>
          <td class="left">{{ item.user_address }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
import { apiGetAllUser } from "@/api/userApi";
export default {
  data() {
    return {
      listData: [],
      pagaSize: 20,
      pageNumber: 1,
    };
  },
  methods: {
    async getDataUser() {
      let filter = [];
      filter.push({
        columnName: "is_manager",
        filterValue: 0,
        operatorValue: "=",
      });
      await apiGetAllUser(filter).then((response) => {
        this.listData = response.data;
      });
    },
  },
  created() {
    this.getDataUser();
  },
};
</script>
<style>
@import url(./ManageUser.scss);
@import url(@/css/layout/table.scss);
</style>