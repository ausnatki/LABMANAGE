<template>
  <div>
    <!-- 表格部分 -->
    <el-table
      v-loading="isLoading"
      :data="tableData"
      border
      style="width: 100%"
    >
      <el-table-column label="编号" width="80">
        <template slot-scope="scope">
          {{ scope.$index + 1 }}
        </template>
      </el-table-column>
      <el-table-column prop="labNumber" label="实验室编号">
        <template slot-scope="scope">
          <el-tag>{{ scope.row.labNumber }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="incidentDetails" label="异常详情" />
      <el-table-column prop="repairName" label="维修人" />
      <el-table-column prop="reportedName" label="上报人" />
      <el-table-column label="上报时间">
        <template slot-scope="scope">
          {{ formatDate(scope.row.inclidentTime) }}
        </template>
      </el-table-column>
      <el-table-column prop="semesters" label="学期" />
    </el-table>
  </div>
</template>

<script>
import { GetHandingByDaily } from '@/api/dailSafetyCheck.js'

export default {
  props: {
    dialogdata: {
      type: Number,
      required: true
    }
  },
  data() {
    return {
      isLoading: true,
      tableData: [] // Log data
    }
  },
  mounted() {
    this.initData().finally(() => { this.isLoading = false })
  },
  methods: {
    async initData() {
      await GetHandingByDaily(this.dialogdata).then(result => {
        this.tableData = result.data
      }).catch(response => {
        this.$message({
          type: 'error',
          message: '获取信息失败'
        })
      })
    },
    formatDate(dateString) {
      const date = new Date(dateString)
      const year = date.getFullYear()
      const month = String(date.getMonth() + 1).padStart(2, '0')
      const day = String(date.getDate()).padStart(2, '0')
      const hours = String(date.getHours()).padStart(2, '0')
      const minutes = String(date.getMinutes()).padStart(2, '0')
      const seconds = String(date.getSeconds()).padStart(2, '0')
      return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`
    }
  }
}
</script>

<style>

</style>
