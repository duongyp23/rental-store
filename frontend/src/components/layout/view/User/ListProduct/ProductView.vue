<template>
  <div class="product-view" @keyup.esc="closeForm()">
    <div class="form-center mb-1">
      <div class="img-upload">
        <div
          class="big-img mb-1"
          :style="
            product_image_url
              ? 'background-image: url(' + product_image_url + ')'
              : 'border : 2px solid #afafaf'
          "
        ></div>
        <hr class="w-100" />
        <div class="list-small-img mt-1">
          <div
            class="small-img"
            v-for="(item, index) in list_img_url"
            :key="index"
            :style="'background-image: url(' + item + ')'"
            @click="product_image_url = item"
          ></div>
        </div>
      </div>

      <div class="information flex-column">
        <div class="header-label">{{ product.product_name }}</div>
        <div>Mã sản phẩm: {{ product.product_code }}</div>
        <div class="deposit-amount blue">
          {{ replaceNumber(product.product_price) }} VNĐ
        </div>
        <div class="row-info">
          <div class="title">Giá thuê theo ngày:</div>

          <span class="bold green"
            >{{ replaceNumber(product.rental_price_day) }} VNĐ
          </span>
        </div>
        <div class="row-info">
          <div class="title">Giá thuê theo tuần:</div>
          <span class="bold green">
            {{ replaceNumber(product.rental_price_week) }} VNĐ
          </span>
        </div>
        <div class="row-info">
          <div class="title">Giá thuê theo tháng:</div>
          <span class="bold green">
            {{ replaceNumber(product.rental_price_month) }} VND
          </span>
        </div>
        <div class="bold">Lựa chọn:</div>
        <div class="flex-row wrap">
          <div
            class="option"
            :class="{ selected: selectOption == item }"
            v-for="item in stocks"
            :key="item.stock_id"
            @click="
              selectOption == item ? (selectOption = {}) : (selectOption = item)
            "
          >
            {{ item.option_code }}
          </div>
        </div>
        <div v-if="selectOption.option_code">
          <span class="green bold">{{ selectOption.quantity_remain }}</span> sản
          phẩm có sẵn
        </div>
        <div class="flex-row">
          <InputNumber
            class="quantity-order"
            :label="'Số lượng thuê:'"
            v-model:numberValue="numberRental"
          ></InputNumber>
          <button class="btn-add-cart" @click="addToCart">
            Thêm vào giỏ hàng
          </button>
        </div>

        <StyleInput
          class="description mt-1"
          :label="'Mô tả sản phẩm'"
          v-model:value="product.description"
          type="textarea"
          :disabled="true"
        ></StyleInput>
      </div>
    </div>
  </div>
</template>
<script>
import Images from "@/assets/icon/images";
import { apiAddProductToCart, apiGetProduct } from "@/api/productApi";
import { replaceNumber } from "@/method/methodFormat";
import { apiGetAllStock } from "@/api/stockApi";
import InputNumber from "@/components/base/StyleInput/InputNumber.vue";
import StyleInput from "@/components/base/StyleInput/StyleInput.vue";
export default {
  data() {
    return {
      formStatus: 0,
      label: "", // tiêu đề
      product: {},
      list_img_url: [],
      Images,
      selectCategory: [],
      product_image_url: "",
      stocks: [],
      selectOption: {},
      numberRental: 0,
    };
  },
  components: { InputNumber, StyleInput },
  methods: {
    replaceNumber,
    async addToCart() {
      if (this.$cookies.get("token")) {
        if (
          this.selectOption != {} &&
          this.numberRental > 0 &&
          this.numberRental <= this.selectOption.quantity_remain
        ) {
          await apiAddProductToCart(
            this.product.product_id,
            this.$cookies.get("userId"),
            this.selectOption.option_code,
            this.numberRental
          )
            .then(() => {
              this.emitter.emit(
                "openToastMessage",
                "Thêm sản phẩm vào giỏ hàng thành công"
              );
              this.closeForm();
            })
            .catch(() => {});
        } else {
          this.emitter.emit(
            "openToastMessageError",
            "Vui lòng chọn lại sản phẩm thêm vào giỏ hàng"
          );
        }
      } else {
        this.emitter.emit(
          "openToastMessageError",
          "Vui lòng đăng nhậ để thực hiện chức năng"
        );
        this.$router.replace(this.$router.path);
        this.$router.push("/login");
      }
    },
    closeForm() {
      this.isOpen = false;
    },
    async getDataById() {
      await apiGetProduct(this.$route.params.id).then((response) => {
        this.product = response.data;
        this.list_img_url = this.product.product_image_url.split(";");
        this.product_image_url = this.list_img_url[0];
      });
    },
    async getStockData() {
      let filter = [];
      filter.push({
        columnName: "product_id",
        filterValue: "'" + this.$route.params.id + "'",
        operatorValue: "=",
      });
      await apiGetAllStock(filter).then((response) => {
        this.stocks = response.data;
      });
    },
  },

  async mounted() {
    await this.getDataById();
    await this.getStockData();
  },
  watch: {},
  created() {},
};
</script>

<style>
@import url(./productView.scss);
@import url(@/css/layout/form.scss);
</style>