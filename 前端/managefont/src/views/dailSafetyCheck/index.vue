<template>
  <div class="my-box">
    <!-- 搜索栏 -->
    <el-input v-model="searchName" placeholder="请输入楼层号" style="width:200px;padding:0 10px 10px 0" />
    <!-- <el-button type="primary" @click="handleSearch">搜索</el-button> -->
    <el-button type="primary" @click="handleExportData">导出数据</el-button>

    <!-- 表格部分 -->
    <el-table
      v-loading="isLoading"
      :data="filteredData"
      border
      style="width: 100%"
    >
      <el-table-column label="编号" width="80">
        <template slot-scope="scope">
          {{ scope.$index + 1 + (currentPage - 1) * pageSize }}
        </template>
      </el-table-column>
      <el-table-column label="基本检查信息">
        <el-table-column prop="semester" label="学期" width="200" />
        <el-table-column prop="checkDate" label="检查日期" width="200" />
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

      <el-table-column label="操作" fixed="right" width="300">
        <template slot-scope="scope">
          <el-link type="danger" icon="el-icon-warning" style="margin-right:10px" @click="handleViewExceptions(scope.row)">查看异常信息</el-link>
          <el-link type="success" icon="el-icon-tools" @click="handleViewRepairs(scope.row)">查看维修信息</el-link>
        </template>
      </el-table-column>
    </el-table>

    <!-- 分页组件 -->
    <el-pagination
      align="center"
      :current-page="currentPage"
      :page-sizes="[10, 20, 50, 100]"
      :page-size="pageSize"
      layout="total, sizes, prev, pager, next, jumper"
      :total="tableData.length"
      @size-change="handleSizeChange"
      @current-change="handleCurrentChange"
    />
  </div>
</template>

<script>
import { GetAllDailyCheck } from '@/api/dailSafetyCheck.js'
export default {
  name: 'LabLogPage',
  data() {
    return {
      tableData: [],
      isLoading: true,
      searchName: '', // 搜索关键字
      currentPage: 1, // 当前页码
      pageSize: 10 // 每页的数据条数
    }
  },
  computed: {
    // 过滤后的数据
    filteredData() {
      let filtered = this.tableData.map(item => ({
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

      const name = this.searchName.trim()
      if (name) {
        filtered = filtered.filter(item => item.labNumber.includes(name) || item.managerName.includes(name))
      }

      return filtered.slice((this.currentPage - 1) * this.pageSize, this.currentPage * this.pageSize)
    }
  },
  mounted() {
    this.initData()
  },
  methods: {
    // 初始化数据 获取全部的日志信息
    initData() {
      this.isLoading = true
      GetAllDailyCheck().then(reuslt => {
        this.tableData = reuslt.data
      }).catch(response => {
        this.$message({
          type: 'error',
          message: response.msg
        })
      }).finally(() => {
        this.isLoading = false
      })
    },
    // 搜索操作
    handleSearch() {
      // 重新从第一页开始显示
      this.currentPage = 1
    },
    // 导出数据操作
    handleExportData() {
      // TODO: 实现导出数据的逻辑
      console.log('Export data')
    },
    // 查看异常信息操作
    handleViewExceptions(row) {
      // TODO: 实现查看异常信息的逻辑
      console.log('View exceptions for lab:', row.labNumber)
    },
    // 查看维修信息操作
    handleViewRepairs(row) {
      // TODO: 实现查看维修信息的逻辑
      console.log('View repairs for lab:', row.labNumber)
    },
    // 分页：每页条数变化
    handleSizeChange(val) {
      this.pageSize = val
    },
    // 分页：当前页变化
    handleCurrentChange(val) {
      this.currentPage = val
    },
    // 判断表格标签状态
    CheckState(state) {
      return state ? 'success' : 'danger'
    }
  }
}
</script>

<style scoped>
.my-box {
  padding: 10px;
}
</style>
