<template>
  <div class="homepage flex-column">
    <div class="banner">
      <img
        src="https://res.cloudinary.com/dmci423da/image/upload/v1716072842/kltn-image/hltntiqbqsrt4twjfust.jpg"
        alt=""
        class="img-banner"
      />
    </div>
    <div class="group">
      <h2 class="title">Sản phẩm được thuê nhiều</h2>
      <div class="list-item">
        <StyleProduct
          v-for="item in datalist"
          :key="item.product_id"
          :product="item"
          @click="viewProduct(item)"
        ></StyleProduct>
      </div>
    </div>
  </div>
</template>
<script>
import { apiGetPagingProduct } from "@/api/productApi";
import StyleProduct from "@/components/base/StyleProduct/StyleProduct.vue";
export default {
  data() {
    return {
      datalist: [],
    };
  },
  components: { StyleProduct },
  methods: {
    /**
     * Lấy dữ liệu từ backend
     */
    async getProductData() {
      let filter = [];
      await apiGetPagingProduct(filter, 8, 1, []).then((response) => {
        this.datalist = response.data.data;
      });
    },
    viewProduct(product) {
      this.$router.replace(this.$router.path);
      this.$router.push(`/products/${product.product_id}`, {
        params: { id: product.product_id },
      });
    },
  },
  created() {
    this.getProductData();
  },
};
</script>
<style>
@import url(./homePage.scss);
</style>