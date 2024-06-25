<template>
  <el-dialog :visible.sync="visible" width="50%" @close="closeCard">
    <template #title>
      <h2>{{ building.name }}</h2>
    </template>
    <el-carousel height="300px">
      <el-carousel-item v-for="(image, index) in images" :key="index">
        <img :src="image" alt="Building Image" class="building-image">
      </el-carousel-item>
    </el-carousel>
    <div class="info-content">
      <p><strong>层数:</strong> {{ building.number }} 层</p>
      <p><strong>启用状态:</strong> {{ SateText(building.isDel) }}</p>
    </div>
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="closeCard">关闭</el-button>
      </span>
    </template>
  </el-dialog>
</template>

<script>
export default {
  props: {
    building: {
      type: Object,
      required: true
    },
    visible: {
      type: Boolean,
      required: true
    }
  },
  data() {
    return {
      images: ['https://cube.elemecdn.com/6/94/4d3ea53c084bad6931a56d5158a48jpeg.jpeg',
        'https://cube.elemecdn.com/6/94/4d3ea53c084bad6931a56d5158a48jpeg.jpeg',
        'https://cube.elemecdn.com/6/94/4d3ea53c084bad6931a56d5158a48jpeg.jpeg'
      ]
    }
  },
  methods: {
    closeCard() {
      this.$emit('close')
    },
    SateText(state) {
      return state === true ? '弃用' : '启用'
    }
  }
}
</script>

<style scoped>
.building-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.info-content {
  margin-top: 20px;
  font-size: 18px;
  line-height: 1.6;
}

.info-content p {
  margin: 10px 0;
}

.info-content strong {
  font-weight: bold;
}

.dialog-footer {
  text-align: right;
}
</style>
