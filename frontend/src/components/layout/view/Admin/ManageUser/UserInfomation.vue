<template>
  <div class="user-info">
    <div class="login-list-input">
      <label class="form-title">Thông tin tài khoản</label>
      <StyleInput
        rightIcon="user"
        v-model:value="user.user_name"
        :label="'Họ và tên'"
      />
      <StyleInput
        rightIcon="home"
        v-model:value="user.user_address"
        :label="'Địa chỉ'"
      />
      <StyleInput
        rightIcon="phone"
        v-model:value="user.phone_number"
        :label="'Số điện thoại'"
        :disabled="true"
      />
      <button class="login-button" @click="UpdateUserInfo()">
        Cập nhật thông tin
      </button>
    </div>
    <div class="list-order">
      <label class="form-title">Danh sách đơn hàng</label>
      <table class="table">
        <thead>
          <tr>
            <th class="w-5 center">STT</th>
            <th class="w-15 center">Ngày đặt hàng</th>
            <th class="w-15 center">Ngày bắt đầu thuê</th>
            <th class="w-15 center">Ngày kết thúc thuê</th>
            <th class="w-20 right">Tổng tiền đơn hàng</th>
            <th class="w-30 center">Trạng thái đơn hàng</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(item, index) in listOrder"
            :key="item.order_id"
            @dblclick="viewOrder(item.order_id)"
          >
            <td class="w-5 center">{{ index + 1 }}</td>
            <td class="w-15 center">{{ datetimeToDate(item.order_date) }}</td>
            <td class="w-15 center">{{ datetimeToDate(item.from_date) }}</td>
            <td class="w-15 center">{{ datetimeToDate(item.to_date) }}</td>
            <td class="w-20 right">
              {{ replaceNumber(item.total_order_deposit) }}
            </td>
            <td class="w-30 center">{{ checkStatusOrder(item.status) }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>
<script>
import StyleInput from "@/components/base/StyleInput/StyleInput.vue";
import { apiUpdateUserInfo, apiGetInfoUser } from "@/api/userApi";
import { apiGetOrderOfUser } from "@/api/orderApi";
import {
  replaceNumber,
  datetimeToDate,
  checkStatusOrder,
} from "@/method/methodFormat";
import Images from "@/assets/icon/images";
export default {
  data() {
    return {
      typePassword: "password",
      check: false,
      user: {},
      listOrder: [],
      Images,
    };
  },
  components: { StyleInput },
  methods: {
    openUploadWidget() {
      const myWidget = window.cloudinary.createUploadWidget(
        {
          cloudName: "dmci423da",
          uploadPreset: "kltn-image",
        },
        (error, result) => {
          if (!error && result && result.event === "success") {
            this.user.qr_code_url = result.info.url;
          }
        }
      );

      myWidget.open();
    },
    replaceNumber,
    datetimeToDate,
    checkStatusOrder,
    async UpdateUserInfo() {
      await apiUpdateUserInfo(this.user).then(async () => {});
    },
    async getUserInfo() {
      await apiGetInfoUser(this.$route.params.id).then((response) => {
        this.user = response.data;
      });
    },
    async getOrderOfUser() {
      await apiGetOrderOfUser(this.$route.params.id).then((response) => {
        this.listOrder = response.data;
      });
    },
    viewOrder(orderId) {
      this.$router.replace(this.$router.path);
      this.$router.push(`/manageorder/${orderId}`, {
        params: { id: orderId },
      });
    },
  },
  created() {
    this.getUserInfo();
    this.getOrderOfUser();
  },
  async mounted() {},
};
</script>
<style>
@import url(./userInfomation.scss);
@import url(@/css/layout/table.scss);
</style>