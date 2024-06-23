<template>
  <div>
    <baidu-map class="map" :center="center" :scroll-wheel-zoom="true" :zoom="19">
      <bm-marker
        v-for="building in mybuild"
        :key="building.id"
        :position="{ lng: parseFloat(building.lng), lat: parseFloat(building.lat) }"
        raise-on-drag="true"
        :dragging="true"
        @click="markerClick(building)"
        @dragend="dragendClick(building, $event)"
      >
        <!-- <bm-info-window
          title="大楼信息"
          :show="showInfoWindow(building.id)"
          @close="infoWindowClose(building.id)"
        >
          {{ building.name }}
        </bm-info-window> -->
      </bm-marker>
    </baidu-map>
    <BuildingInfoCard
      v-if="selectedBuilding"
      :building="selectedBuilding"
      :visible="infoCardVisible"
      @close="closeInfoCard"
    />
  </div>
</template>

<script>
import { GetAllList } from '@/api/building.js'
import BuildingInfoCard from '@/views/ditu/dialogview.vue'

export default {
  components: {
    BuildingInfoCard
  },
  data() {
    return {
      BMap: null,
      map: null,
      zoom: 15, // Adjusted zoom level for better view
      center: { lng: 106.60310697599532, lat: 29.43205776646374 }, // Map center coordinates
      show: {}, // Object to track which info windows are open
      dragendPosition: {},
      mybuild: [], // Data for buildings
      selectedBuilding: null, // Selected building data
      infoCardVisible: false // Info card visibility
    }
  },
  mounted() {
    this.initData()
  },
  methods: {
    infoWindowClose(buildingId) {
      this.$set(this.show, buildingId, false)
    },
    infoWindowOpen(buildingId) {
      this.$set(this.show, buildingId, true)
    },
    showInfoWindow(buildingId) {
      return this.show[buildingId] || false
    },
    markerClick(building) {
      this.selectedBuilding = building
      this.infoCardVisible = true
    },
    dragendClick(building, e) {
      console.log(`Building ID: ${building.id}`, e)
      // 如果你需要更新dragendPosition为最新位置，可以这样做（但通常这不是必要的，因为dragend事件已经提供了最新的位置）
      // this.dragendPosition = e.latLng
    },
    closeInfoCard() {
      this.infoCardVisible = false
      this.selectedBuilding = null
    },
    initData() {
      GetAllList().then(result => {
        this.mybuild = result.data
      }).catch(response => {
        this.$message({
          type: 'error',
          message: '获取列表失败'
        })
      })
    }
  }
}
</script>

<style scoped>
.map {
  height: 500px;
}
</style>
