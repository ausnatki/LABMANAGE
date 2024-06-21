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
      <el-table-column prop="inclidentTime" label="上报时间" />
      <el-table-column prop="semesters" label="学期" />
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
import { GetAllHanding } from '@/api/handing.js'
import AssignDialogVue from '@/views/handing/components/AssignDailog.vue'

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
    }
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
    // 导出数据
    handleExport() {
      // TODO: 实现导出数据的逻辑
      console.log('Export data')
    },
    // 分页：每页条数变化
    handleSizeChange(val) {
      this.pageSize = val
    },
    // 分页：当前页变化
    handleCurrentChange(val) {
      this.currentPage = val
    }
  }
}
</script>

<style scoped>
.my-box {
  padding: 10px;
}
</style>
