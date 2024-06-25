<template>
  <div>
    <!-- 表格部分 -->

    <el-table
      v-loading="isLoading"
      :data="filteredData"
      border
      style="width: 100%"
    >
      <el-table-column label="编号" width="80">
        <template slot-scope="scope">
          {{ scope.$index + 1 }}
        </template>
      </el-table-column>
      <el-table-column label="基本检查信息">
        <el-table-column prop="semester" label="学期" width="200" />
        <el-table-column prop="checkDate" label="检查日期" width="200">
          <template slot-scope="scope">
            {{ formatDate(scope.row.checkDate) }}
          </template>
        </el-table-column>
        <el-table-column prop="labNumber" label="房楼位置" width="200">
          <template slot-scope="scope">
            <el-tag>{{ scope.row.labNumber }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="managerName" label="负责人名" width="200">
          <template slot-scope="scope">
            <el-tag>{{ scope.row.managerName }}</el-tag>
          </template>
        </el-table-column>
      </el-table-column>

      <el-table-column prop="cleanliness" label="清洁状况" width="150">
        <template slot-scope="scope">
          <el-tag :type="CheckState(tableData[scope.$index].cleanliness)">{{ scope.row.cleanliness }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="doorsAndWindows" label="门窗状况" width="150">
        <template slot-scope="scope">
          <el-tag :type="CheckState(tableData[scope.$index].doorsAndWindows)">{{ scope.row.doorsAndWindows }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="electricalLines" label="电线状况" width="150">
        <template slot-scope="scope">
          <el-tag :type="CheckState(tableData[scope.$index].electricalLines)">{{ scope.row.electricalLines }}</el-tag>
        </template>
      </el-table-column>

      <el-table-column label="消防设备检查结果">
        <el-table-column prop="fireSafetyEquipmentAvailable" label="消防设备可用性" width="200">
          <template slot-scope="scope">
            <el-tag :type="CheckState(tableData[scope.$index].fireSafetyEquipmentAvailable)">{{ scope.row.fireSafetyEquipmentAvailable }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="fireSafetyEquipmentExpiry" label="消防设备有效期" width="200">
          <template slot-scope="scope">
            <el-tag :type="CheckState(tableData[scope.$index].fireSafetyEquipmentExpiry)">{{ scope.row.fireSafetyEquipmentExpiry }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="instrumentEquipmentIntact" label="仪器设备完整性" width="200">
          <template slot-scope="scope">
            <el-tag :type="CheckState(tableData[scope.$index].instrumentEquipmentIntact)">{{ scope.row.instrumentEquipmentIntact }}</el-tag>
          </template>
        </el-table-column>
      </el-table-column>
      <el-table-column label="仪器设备检查结果">
        <el-table-column prop="instrumentEquipmentWorking" label="仪器设备工作状况" width="200">
          <template slot-scope="scope">
            <el-tag :type="CheckState(tableData[scope.$index].instrumentEquipmentWorking)">{{ scope.row.instrumentEquipmentWorking }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="instrumentWarningLabelsIntact" label="仪器警示标签完整性" width="200">
          <template slot-scope="scope">
            <el-tag :type="CheckState(tableData[scope.$index].instrumentWarningLabelsIntact)">{{ scope.row.instrumentWarningLabelsIntact }}</el-tag>
          </template>
        </el-table-column>
      </el-table-column>
      <el-table-column prop="otherHazards" label="其他危害" width="200" />
      <el-table-column prop="otherSafetyHazards" label="其他安全隐患" width="200" />
      <el-table-column prop="state" label="状态" width="100" />

    </el-table>
  </div>
</template>

<script>
import { GetByLab } from '@/api/dailSafetyCheck.js'

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
  computed: {
    // 过滤后的数据
    filteredData() {
      const filtered = this.tableData.map(item => ({
        ...item,
        cleanliness: item.cleanliness ? '正常' : '异常',
        doorsAndWindows: item.doorsAndWindows ? '正常' : '异常',
        electricalLines: item.electricalLines ? '正常' : '异常',
        fireSafetyEquipmentAvailable: item.fireSafetyEquipmentAvailable ? '正常' : '异常',
        fireSafetyEquipmentExpiry: item.fireSafetyEquipmentExpiry ? '正常' : '异常',
        instrumentEquipmentIntact: item.instrumentEquipmentIntact ? '正常' : '异常',
        instrumentEquipmentWorking: item.instrumentEquipmentWorking ? '正常' : '异常',
        instrumentWarningLabelsIntact: item.instrumentWarningLabelsIntact ? '正常' : '异常',
        otherSafetyHazards: item.otherSafetyHazards || '无',
        state: item.state ? '正常' : '异常'
      }))

      return filtered
    }
  },
  mounted() {
    this.initData().finally(() => { this.isLoading = false })
  },
  methods: {
    async initData() {
      await GetByLab(this.dialogdata).then(result => {
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
    },
    // 判断表格标签状态
    CheckState(state) {
      return state ? 'success' : 'danger'
    },
    // 导出数据操作
    handleExportData() {
      // TODO: 实现导出数据的逻辑
      console.log('Export data')
    }
  }

}
</script>

<style>

</style>
