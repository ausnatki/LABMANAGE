<template>
  <el-form v-loading="editLoading" :model="addForm">
    <el-form-item label="楼房名称" :label-width="formLabelWidth">
      <el-input v-model="addForm.name" />
    </el-form-item>
    <!-- 这里可以添加更多的输入项 -->
    <baidu-map class="map" :center="center" :scroll-wheel-zoom="true" :zoom="19">
      <bm-marker :raise-on-drag="true" :position="markerPosition" :dragging="true" @dragend="dragendClick" />
    </baidu-map>
    <el-form-item label="楼房坐标" :label-width="formLabelWidth">
      <el-row :gutter="20">
        <el-col :span="10">
          <el-input v-model="dragendPosition.lng" />
        </el-col>
        <el-col :span="10">
          <el-input v-model="dragendPosition.lat" />
        </el-col>
      </el-row>
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="submitEdit">提交</el-button>
    </el-form-item>
  </el-form>

</template>

<script>
import { EditBuilder } from '@/api/building.js'

export default {
  name: 'EditFloorDialog',
  props: {
    editata: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      addForm: {
        id: '',
        name: '',
        number: 0,
        lng: '',
        lat: ''
      },
      editLoading: true,
      BMap: null,
      map: null,
      zoom: 22, // 地图放大缩小的值
      center: { lng: 106.60310697599532, lat: 29.43205776646374 }, // 地图中心坐标
      markerPosition: { lng: 106.60310697599532, lat: 29.43205776646374 }, // 标记位置
      show: false,
      dragendPosition: { lng: 106.60310697599532, lat: 29.43205776646374 },
      formLabelWidth: '120px'
    }
  },
  async mounted() {
    await this.Initdata().finally(() => { this.editLoading = false })
  },
  methods: {
    async Initdata() {
      this.editLoading = true
      this.addForm = this.editata
      if (this.editata.lng !== null) {
        this.center = {
          lng: this.editata.lng,
          lat: this.editata.lat
        }
        this.markerPosition = this.center
        this.dragendPosition = this.center
      }
    },
    submitEdit() {
      this.addForm.lat = (this.dragendPosition.lat).toString()
      this.addForm.lng = (this.dragendPosition.lng).toString()

      EditBuilder(this.addForm).then(result => {
        this.$message({
          type: 'success',
          message: '修改成功'
        })
        this.$emit('submit', this.addForm)
      }).catch(result => {
        this.$message({
          type: 'error',
          message: '修改失败'
        })
      })

      // this.$emit('submit', this.addForm)
    },
    infoWindowClose() {
      this.show = false
    },
    infoWindowOpen() {
      this.show = true
    },
    dragendClick(e) {
      console.log(e)
      this.dragendPosition = e.latLng
    }
  }
}
</script>

<style scoped>
.map{
    margin-top: 40px;
    padding-left: 120px;
    width: 100%;
  height: 300px;
  margin-bottom: 20px;
}
</style>
