<template>
  <div class="manage-product">
    <div class="flex-row between">
      <div class="list-header mt-1 mb-1">Danh sách sản phẩm</div>
      <button class="form-btn btn3" @click="openFormAdd">Thêm sản phẩm</button>
    </div>
    <div class="category-product">
      <CategoryTable v-model:selectCategory="selectCategory"></CategoryTable>
      <table class="table">
        <thead>
          <tr>
            <th class="center w-5">STT</th>
            <th class="left w-15">Mã sản phẩm</th>
            <th class="left w-20">Tên sản phẩm</th>
            <td class="right w-20">Giá sản phẩm</td>
            <th class="right w-15">Tồn Kho</th>
            <th class="right w-15">Đang cho thuê</th>
            <th class="right w-15">Đã thanh lý</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(item, index) in listData"
            :key="item.product_id"
            @dblclick="openFormEdit(item)"
          >
            <th class="center w-5">{{ index + 1 }}</th>
            <td class="left w-15 blue">{{ item.product_code }}</td>
            <td class="left w-20">{{ item.product_name }}</td>
            <td class="right w-20">{{ replaceNumber(item.product_price) }}</td>
            <td class="right w-15">
              {{ replaceNumber(item.quantity_remain) }}
            </td>
            <td class="right w-15">
              {{ replaceNumber(item.quantity_rental) }}
            </td>
            <td class="right w-15">{{ replaceNumber(item.quantity_sold) }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>
<script>
import { apiGetPagingProduct } from "@/api/productApi";
import { replaceNumber } from "@/method/methodFormat";
import CategoryTable from "@/components/layout/view/Share/ListCategory/CategoryTable.vue";
export default {
  data() {
    return {
      listData: [],
      pagaSize: 20,
      pageNumber: 1,
      selectCategory: [],
    };
  },
  components: { CategoryTable },
  methods: {
    replaceNumber,
    async getPagingData() {
      let filter = [];
      let ListCategoryId = this.selectCategory.map((x) => x.category_id);
      await apiGetPagingProduct(
        filter,
        this.pagaSize,
        this.pageNumber,
        ListCategoryId
      ).then((response) => {
        this.listData = response.data.data;
      });
    },
    openFormEdit(item) {
      this.$router.replace(this.$router.path);
      this.$router.push(`/manageproduct/editproduct/${item.product_id}`, {
        params: { id: item.product_id },
      });
    },
    openFormAdd() {
      this.$router.replace(this.$router.path);
      this.$router.push(`/manageproduct/addproduct`);
    },
  },
  created() {
    this.getPagingData();
  },
  mounted() {
    this.emitter.on("reloadProductList", () => {
      this.getPagingData();
    });
  },
  watch: {
    selectCategory: {
      deep: true,
      handler: async function () {
        await this.getPagingData();
      },
    },
  },
};
</script>
<style>
@import url(./ManageProduct.scss);
@import url(@/css/layout/table.scss);
</style>