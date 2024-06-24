<template>
  <div class="my-box">
    <!-- 搜索栏 -->
    <el-input v-model="searchLabNumber" placeholder="请输入实验室编号" style="width:200px;padding:0 10px 10px 0" />
    <el-button type="primary" @click="handleSearch">搜索</el-button>
    <el-button type="success" @click="handleExport">导出数据</el-button>

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
      <el-table-column prop="labNumber" label="实验室编号">
        <template slot-scope="scope">
          <el-tag> {{ scope.row.labNumber }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="incidentDetails" label="异常详情" />
      <el-table-column prop="repairName" label="维修人" />
      <el-table-column prop="reportedName" label="上报人" />
      <el-table-column prop="inclidentTime" label="上报时间">
        <template slot-scope="scope">
          {{ formatDate(scope.row.inclidentTime) }}
        </template>
      </el-table-column>
      <el-table-column label="操作" width="150">
        <template slot-scope="scope">
          <el-link type="primary" icon="el-icon-user" @click="handleAssign(scope.row)">分配维修人员</el-link>
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

    <!-- 分配管理人员弹出层 -->
    <el-dialog
      title="提示"
      :visible.sync="dialogAssign"
      width="30%"
    >
      <template v-if="dialogAssign">
        <AssignDialogVue :dialogdata.sync="dialogdata" @close="dialogAssign = false" />
      </template>
    </el-dialog>
  </div>
</template>

<script>
import { GetAllHanding, GetHandingByUser } from '@/api/handing.js'
import AssignDialogVue from '@/views/handing/components/AssignDailog.vue'
import { mapGetters } from 'vuex'

export default {
  name: 'LogPage',
  components: {
    AssignDialogVue
  },
  data() {
    return {
      tableData: [], // 日志数据
      isLoading: false,
      dialogAssign: false,
      dialogdata: {},
      tHeader: '',
      searchLabNumber: '', // 搜索关键字
      currentPage: 1, // 当前页码
      pageSize: 10 // 每页的数据条数
    }
  },
  computed: {
    // 过滤后的数据
    filteredData() {
      let filtered = this.tableData
      const labNumber = this.searchLabNumber.trim()
      if (labNumber) {
        filtered = filtered.filter(item => item.labNumber.includes(labNumber))
      }
      return filtered.slice((this.currentPage - 1) * this.pageSize, this.currentPage * this.pageSize)
    },
    ...mapGetters([
      'roles',
      'uid'
    ])
  },
  watch: {
    dialogAssign(newVal) {
      if (newVal === false) this.initData()
    }
  },
  mounted() {
    this.initData()
  },
  methods: {
    // 初始化数据 获取全部的日志信息
    initData() {
      this.isLoading = true
      if (this.roles.includes('admin')) {
        GetAllHanding().then(result => {
          this.tableData = result.data
        }).catch(response => {
          this.$message({
            type: 'error',
            message: response.msg
          })
        }).finally(() => {
          this.isLoading = false
        })
      } else {
        GetHandingByUser(this.uid).then(result => {
          this.tableData = result.data
        }).catch(response => {
          this.$message({
            type: 'error',
            message: response.msg
          })
        }).finally(() => {
          this.isLoading = false
        })
      }
    },
    // 搜索操作
    handleSearch() {
      // 重新从第一页开始显示
      this.currentPage = 1
    },
    // 分配管理人员操作
    handleAssign(row) {
      this.dialogdata = row
      this.dialogAssign = true
    },
    // 分页：每页条数变化
    handleSizeChange(val) {
      this.pageSize = val
    },
    // 分页：当前页变化
    handleCurrentChange(val) {
      this.currentPage = val
    },
    // 异常数据初始化
    fromatHandingData(list) {
      console.log(list)
      const map = {
        id: '编号',
        labNumber: '实验室名称',
        inclidentTime: '上报时间',
        incidentDetails: '问题',
        repairName: '维修人员',
        reportedName: '上报人员',
        semesters: '学期'
      }

      this.tHeader = Object.values(map)
      return list.map(item => {
        return Object.keys(map).map(key => {
          if (key === 'inclidentTime') {
            // 格式化日期
            return new Date(item[key]).toLocaleDateString()
          }
          return item[key]
        })
      })
    },
    // 导出excel
    handleExport() {
      this.downloadLoading = true
      import('@/vendor/Export2Excel').then(excel => {
        const list = this.filteredData
        // 格式化表体
        const data = this.fromatHandingData(list)
        // 格式化表头
        const tHeader = this.tHeader
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: '异常',
          autoWidth: this.autoWidth,
          bookType: this.bookType
        })
        this.downloadLoading = false
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

<style scoped>
.my-box {
  padding: 10px;
}
</style>
