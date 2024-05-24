<template>
  <div class="manage-category">
    <div class="flex-row between">
      <div class="list-header mt-1 mb-1">Danh sách nhãn dán</div>
      <button class="form-btn btn3" @click="openFormAdd">Thêm nhãn dán</button>
    </div>
    <table class="table">
      <thead>
        <tr>
          <th class="w-5 center">STT</th>
          <th class="left w-15">Mã nhãn dán</th>
          <th class="left w-15">Loại nhãn dán</th>
          <th class="left">Diễn giải</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(item, index) in listData"
          :key="item.category_id"
          @dblclick="openFormEdit(item)"
        >
          <th class="w-5 center">{{ index + 1 }}</th>
          <td class="left w-15 blue bold">{{ item.category_code }}</td>
          <td class="left w-15">{{ getCategoryTypeName(item.type) }}</td>
          <td class="left">{{ item.description }}</td>
        </tr>
      </tbody>
    </table>
    <CategoryForm></CategoryForm>
  </div>
</template>
<script>
import { apiGetAllCategory } from "@/api/categoryApi";
import { getCategoryTypeName } from "@/method/methodFormat";
import CategoryForm from "../ManageCategory/CategoryForm.vue";
export default {
  data() {
    return {
      listData: [],
      pagaSize: 20,
      pageNumber: 1,
    };
  },
  components: { CategoryForm },
  methods: {
    openFormAdd() {
      this.emitter.emit("addNewCategory");
    },
    getCategoryTypeName,
    async getData() {
      await apiGetAllCategory([]).then((response) => {
        this.listData = response.data;
      });
    },
    openFormEdit(category) {
      this.emitter.emit("updateCategory", category);
    },
  },
  created() {
    this.getData();
  },
  mounted() {
    this.emitter.on("loadDataCategory", () => {
      this.getData();
    });
  },
};
</script>
<style>
@import url(./ManageCategory.scss);
@import url(@/css/layout/table.scss);
</style>