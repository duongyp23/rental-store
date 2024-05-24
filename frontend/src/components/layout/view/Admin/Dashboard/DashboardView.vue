<template>
  <div class="dashboard flex-column">
    <div class="flex-row">
      <div class="flex-column info-group flex1">
        <div class="">Tổng doanh thu:</div>
        <div class="bold">{{ replaceNumber(TotalPrice) }} VNĐ</div>
      </div>
      <div class="flex-column info-group flex1">
        <div class="">Lợi nhuận:</div>
        <div class="bold">{{ replaceNumber(TotalProfit) }} VNĐ</div>
      </div>
      <div class="flex-column info-group flex1">
        <div class="">Chi phí nhập hàng:</div>
        <div class="bold">{{ replaceNumber(TotalInward) }} VNĐ</div>
      </div>
      <div class="flex-column info-group flex1">
        <div class="">Doanh thu thanh lý:</div>
        <div class="bold">{{ replaceNumber(TotalOutward) }} VNĐ</div>
      </div>
    </div>
    <div class="flex-row">
      <div class="flex-column flex1 chart-group">
        <div class="bold center title">Thống kê doanh thu</div>
        <div class="chart">
          <VueApexCharts
            height="90%"
            type="donut"
            :options="data1.chartOptions"
            :series="data1.series"
          ></VueApexCharts>
        </div>
      </div>
      <div class="flex-column flex1 chart-group">
        <div class="bold center title">Thống kê doanh thu hàng tháng</div>
        <div class="chart">
          <VueApexCharts
            height="90%"
            type="bar"
            :options="data2.chartOptions"
            :series="data2.series"
          ></VueApexCharts>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import VueApexCharts from "vue3-apexcharts";
import { replaceNumber } from "@/method/methodFormat";
import { apiDashboard } from "@/api/orderApi";
export default {
  components: { VueApexCharts },
  data() {
    return {
      TotalPrice: 0,
      TotalProfit: 0,
      TotalInward: 0,
      TotalOutward: 0,
      data1: {
        series: [this.TotalInward, this.TotalPrice, this.TotalOutward],
        chartOptions: {
          chart: {
            type: "donut",
          },
          responsive: [
            {
              breakpoint: 480,
              options: {
                chart: {
                  width: 200,
                },
                legend: {
                  position: "bottom",
                },
              },
            },
          ],
          labels: [
            "Chi phí nhập hàng",
            "Doanh thu cho thuê",
            "Doanh thu thanh lý",
          ],
        },
      },
      data2: {
        series: [
          {
            name: "Chi phí nhập hàng",
            data: [58, 63, 60, 66],
          },
          {
            name: "Lợi nhuận",
            data: [105, 91, 114, 94],
          },
          {
            name: "Thanh lý",
            data: [48, 52, 53, 41],
          },
        ],
        chartOptions: {
          chart: {
            type: "bar",
            height: 350,
          },
          plotOptions: {
            bar: {},
          },
          dataLabels: {
            enabled: false,
          },
          stroke: {
            show: true,
            width: 2,
            colors: ["transparent"],
          },
          xaxis: {
            categories: ["Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5"],
          },
          yaxis: {},
          fill: {
            opacity: 1,
          },
          tooltip: {},
        },
      },
    };
  },
  methods: {
    replaceNumber,
    async getDashboardData() {
      await apiDashboard().then((response) => {
        this.TotalPrice = response.data.totalPrice;
        this.TotalProfit = response.data.totalProfit;
        this.TotalInward = response.data.totalInward;
        this.TotalOutward = response.data.totalOutward;
        this.data1.series = [
          this.TotalInward,
          this.TotalPrice,
          this.TotalOutward,
        ];
      });
    },
  },
  created() {
    this.getDashboardData();
  },
};
</script>
<style>
@import url(./dashboard.scss);
</style>