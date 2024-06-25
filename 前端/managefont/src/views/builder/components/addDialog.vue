<template>
  <el-dialog title="添加楼层" :visible="dialogVisible" @update:visible="updateVisible">
    <el-form :model="addForm">
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
        <el-button type="primary" @click="submitAdd">提交</el-button>
        <el-button @click="closeDialog">取消</el-button>
      </el-form-item>
    </el-form>

  </el-dialog>
</template>

<script>
export default {
  name: 'AddFloorDialog',
  props: {
    visible: {
      type: Boolean,
      required: true

    }
  },
  data() {
    return {
      addForm: {
        name: '',
        number: 0,
        lng: '',
        lat: ''
      },
      dialogVisible: this.visible, // 使用一个本地的 dialogVisible 来存储 visible 的值
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
  watch: {
    visible(newValue) {
      this.dialogVisible = newValue // 监听 visible 的变化，同步到 dialogVisible
    }
  },
  methods: {
    submitAdd() {
      this.addForm.lat = (this.dragendPosition.lat).toString()
      this.addForm.lng = (this.dragendPosition.lng).toString()
      this.$emit('submit', this.addForm)
      this.addForm.name = ''
      this.addForm.number = 0
    },
    updateVisible(newValue) {
      this.$emit('update:visible', newValue) // 将修改后的值通过事件传递给父组件
    },
    closeDialog() {
      this.$emit('update:visible', false) // 关闭对话框
      this.clearForm() // 清空表单数据
    },
    infoWindowClose() {
      this.show = false
    },
    infoWindowOpen() {
      this.show = true
    },
    clearForm() {
      this.addForm.name = ''
      this.addForm.number = 0
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
