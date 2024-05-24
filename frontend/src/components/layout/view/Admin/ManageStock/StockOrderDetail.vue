<template>
  <div class="stock-order-detail flex-column">
    <div class="header-label">
      {{ orderType == 2 ? "Phiếu Nhập kho" : "Phiếu Thanh lý" }}
    </div>
    <div class="flex-row mid-view">
      <div class="detail-list flex-column" v-if="state == 1">
        <div class="box-search mb-1">
          <StyleInput
            :rightIcon="'search'"
            :placeholder="'Nhập mã sản phẩm'"
            v-model:value="searchText"
            @change="getProductData"
            @enter="getProductData"
            v-if="state != 2"
          ></StyleInput>
          <div
            class="flex-column list-option"
            v-if="productSearch.length > 0"
            v-click-outside="close"
          >
            <div
              v-for="(item, index) in productSearch"
              :key="index"
              @click="addOrderDetail(item)"
            >
              {{ item.product_name }}
            </div>
          </div>
        </div>
        <div
          v-for="(orderDetail, index) in orderDetails"
          :key="index"
          class="order-detail-item flex-row"
        >
          <div
            class="product-img"
            :style="
              'background-image : url(' +
              getFirstImage(orderDetail.product_image_url) +
              ')'
            "
          ></div>
          <div class="flex-column w-100">
            <div class="bold flex-row">
              <div>
                {{ orderDetail.product_name }} - {{ orderDetail.product_code }}
              </div>
              <div class="red" @click="remove(index)">Xóa</div>
              <div class="green" @click="addDetail(orderDetail)">Thêm</div>
            </div>
            <div class="flex-row" v-if="state != 2">
              <StyleInput
                v-model:value="orderDetail.option_code"
                :label="'Mã chọn'"
              ></StyleInput>
              <InputNumber
                v-model:numberValue="orderDetail.quantity"
                :label="'Số lượng'"
              ></InputNumber>
              <InputNumber
                v-if="orderType == 3"
                v-model:numberValue="orderDetail.product_payment"
                :label="'Tổng tiền'"
                @changeValue="calculateOrderMoney"
              ></InputNumber>
              <InputNumber
                v-if="orderType == 2"
                v-model:numberValue="orderDetail.product_return"
                :label="'Tổng tiền'"
                @changeValue="calculateOrderMoney"
              ></InputNumber>
            </div>
            <div class="flex-row" v-else>
              <div class="flex-column flex1">
                <div>Mã chọn</div>
                <div>{{ orderDetail.option_code }}</div>
              </div>
              <div class="flex-column flex1">
                <div>Số lượng</div>
                <div>{{ replaceNumber(orderDetail.quantity) }}</div>
              </div>
              <div class="flex-column flex1">
                <div>Tổng tiền</div>
                <div>
                  {{
                    replaceNumber(
                      orderType == 3
                        ? orderDetail.product_payment
                        : orderDetail.product_return
                    )
                  }}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="detail-list2" v-else>
        <table class="table">
          <thead>
            <tr>
              <th class="w-15"></th>
              <th class="left w-15">Mã sản phẩm</th>
              <th class="left w-25">Tên sản phẩm</th>
              <th class="left w-15">Phân loại</th>
              <th class="right w-10">Số lượng</th>
              <th class="right w-20">Thành tiền</th>
            </tr>
          </thead>
          <tbody>
            <div
              v-for="(orderDetail, index) in orderDetails"
              :key="index"
              class="order-detail-item"
            >
              <div
                class="product-img w-15"
                :style="
                  'background-image : url(' +
                  getFirstImage(orderDetail.product_image_url) +
                  ')'
                "
              ></div>
              <div class="left w-15 blue bold">
                {{ orderDetail.product_code }}
              </div>
              <div class="left w-25">{{ orderDetail.product_name }}</div>
              <div class="left w-15">{{ orderDetail.option_code }}</div>
              <div class="right w-10">
                {{ replaceNumber(orderDetail.quantity) }}
              </div>
              <div class="right w-20 bold">
                {{
                  order.order_type == 3
                    ? replaceNumber(orderDetail.product_payment)
                    : replaceNumber(orderDetail.product_return)
                }}
                VNĐ
              </div>
            </div>
          </tbody>
        </table>
      </div>
      <div class="total-order flex-column">
        <div class="header-label center">Thông tin phiếu</div>
        <hr class="w-100" />
        <div class="amount-row">
          <div>Giá trị đơn hàng</div>
          <div v-if="order.order_type == 2" class="bold">
            {{ replaceNumber(order.total_order_return) }} VNĐ
          </div>
          <div v-else class="bold">
            {{ replaceNumber(order.total_order_payment) }} VNĐ
          </div>
        </div>
        <hr class="w-100" />
        <div class="flex-row" v-if="state == 1">
          <style-input
            :label="'Người tạo'"
            class="flex1"
            v-model:value="order.user_name"
          ></style-input>
          <style-input
            :label="'Ngày phiếu kho'"
            class="flex1"
            v-model:value="order.order_date"
            type="date"
          ></style-input>
        </div>
        <div v-else class="flex-column">
          <div class="bold">Người tạo</div>
          <div>{{ order.user_name }}</div>
          <div class="bold">Ngày phiếu kho</div>
          <div>{{ datetimeToDate(order.order_date) }}</div>
          <div class="bold">Ghi chú</div>
          <div>
            {{
              order.description ? order.description : "<< Không có ghi chú >>"
            }}
          </div>
        </div>
        <style-input
          :label="'Ghi chú'"
          type="textarea"
          class="description"
          v-model:value="order.description"
          v-if="state == 1"
        ></style-input>
        <div class="flex-row center">
          <button class="form-btn btn1" v-on:click="closeForm">Hủy</button>

          <button class="form-btn btn3" @click="saveOrder" v-if="state == 1">
            Lưu
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import StyleInput from "@/components/base/StyleInput/StyleInput.vue";
import InputNumber from "@/components/base/StyleInput/InputNumber.vue";
import Images from "@/assets/icon/images";
import { replaceNumber, datetimeToDate } from "@/method/methodFormat";
import { apiAddStockOrder, apiGetOrder, apiDeleteOrder } from "@/api/orderApi";
import { apiGetPagingProduct } from "@/api/productApi";
export default {
  setup() {},
  data() {
    return {
      state: 0,
      orderType: 0,
      order: {},
      orderDetails: [],
      products: [],
      productSearch: [],
      Images,
      searchText: "",
    };
  },
  components: { StyleInput, InputNumber },
  methods: {
    remove(index) {
      this.orderDetails.splice(index, 1);
    },
    close() {
      this.productSearch = [];
    },
    addOrderDetail(item) {
      this.products.push(item);
      this.orderDetails.push({
        product_id: item.product_id,
        product_code: item.product_code,
        product_name: item.product_name,
        product_image_url: item.product_image_url,
        product_return: 0,
        product_payment: 0,
        option_code: "",
      });
      this.productSearch = [];
    },
    addDetail(item) {
      this.orderDetails.push({
        product_id: item.product_id,
        product_code: item.product_code,
        product_name: item.product_name,
        product_image_url: item.product_image_url,
        product_return: 0,
        product_payment: 0,
        option_code: "",
      });
    },
    async getProductData() {
      let filter = [];
      if (this.searchText) {
        filter.push({
          columnName: "product_code",
          filterValue: "'%" + this.searchText + "%'",
          operatorValue: "LIKE",
        });
        await apiGetPagingProduct(filter, 10, 1).then((response) => {
          this.productSearch = response.data.data;
        });
      } else {
        this.productSearch = [];
      }
    },
    datetimeToDate,
    async removeOrder() {
      await apiDeleteOrder(this.order.order_id).then(() => {
        this.closeForm();
      });
    },
    async saveOrder() {
      await apiAddStockOrder(this.order, this.orderDetails).then(() => {
        this.closeForm();
      });
    },
    async loadData() {
      await apiGetOrder(this.$route.params.id).then((response) => {
        this.order = response.data.order;
        this.orderDetails = response.data.orderDetails;
        this.products = response.data.products;
      });
    },
    replaceNumber,
    calculateOrderMoney() {
      this.order.total_order_return = 0;
      this.order.total_order_payment = 0;
      this.orderDetails.forEach((item) => {
        this.order.total_order_return += item.product_return;
        this.order.total_order_payment += item.product_payment;
      });
    },
    closeForm() {
      this.$router.replace(this.$router.path);
      this.$router.push("/managestock");
    },
    getFirstImage(listImage) {
      let arrImg = listImage.split(";");
      return arrImg[0];
    },
  },
  created() {},
  async mounted() {
    this.orderType = this.$route.params.type;
    if (!this.orderType) {
      this.$router.replace(this.$router.path);
      this.$router.push("/managestock");
    }
    this.state = this.$route.params.state;
    if (this.state == 2) {
      await this.loadData();
    } else {
      this.order.order_type = this.orderType;
    }
    this.order.order_date = this.datetimeToDate(new Date());
    this.order.user_name = this.$cookies.get("userName");
  },
  watch: {
    orderDetails: {
      deep: true,
      handler: function () {
        this.calculateOrderMoney();
      },
    },
  },
};
</script>
<style>
@import url(./StockOrderDetail.scss);
</style>