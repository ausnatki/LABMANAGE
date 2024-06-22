<template>
  <div class="my-box">
    <!-- 搜索栏 -->
    <el-input v-model="searchName" placeholder="请输入学院名" style="width:200px;padding:0 10px 10px 0" />
    <el-button type="primary" @click="handleSearch">搜索</el-button>

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
      <el-table-column prop="name" label="实验室名称" />
      <el-table-column prop="builder" label="房楼位置" />
      <el-table-column prop="academy" label="学院名称">
        <template slot-scope="scope">
          <el-tag>{{ scope.row.academy }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="managerName" label="负责人名">
        <template slot-scope="scope">
          <el-tag>{{ scope.row.managerName }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="isDel" label="是否启用">
        <template slot-scope="scope">
          <el-switch
            v-model="scope.row.isDel"
            active-color="#13ce66"
            inactive-color="#ff4949"
            @change="handleChangeState(scope.row.id)"
          />
        </template>
      </el-table-column>
      <el-table-column label="操作" width="400">
        <template slot-scope="scope">
          <el-link type="primary" icon="el-icon-edit" style="margin-right:10px" @click="handleView(scope.row)">查看日志</el-link>
          <el-link type="success" icon="el-icon-user" style="margin-right:10px" @click="handleAssign(scope.row)">分配人员</el-link>
          <el-link type="warning" icon="el-icon-download" style="margin-right:10px" @click="handleExportError(scope.row)">导出异常</el-link>
          <el-link type="danger" icon="el-icon-download" @click="handleExportRepair(scope.row)">导出维修数据</el-link>
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

    <!-- 查看日志弹出层 -->
    <el-dialog
      title="日志数据"
      :visible.sync="dialogCheck"
      width="70%"
    >
      <template v-if="dialogCheck">
        <CheckDialog :dialogdata.sync="dialogdata" />
      </template>
    </el-dialog>

    <!-- 分配人员弹出层 -->
    <el-dialog
      title="分配人员"
      :visible.sync="dialogAssign"
      width="30%"
    >
      <template v-if="dialogAssign">
        <AssignDialogVue :dialogdata.sync="dialogdata" />
      </template>
    </el-dialog>

  </div>
</template>

<script>
import { GetList, ChangeState } from '@/api/laboratories.js'
import { GetRepairByLab, GetHandingByLab } from '@/api/dailSafetyCheck.js'
import AssignDialogVue from '@/views/laboratories/components/AssignDialog.vue'
import CheckDialog from '@/views/laboratories/components/checkDialog.vue'
export default {
  name: 'LabPage',
  components: {
    AssignDialogVue,
    CheckDialog
  },
  data() {
    return {
      tableData: [], // 学院数据
      isLoading: true,
      dialogAssign: false,
      dialogCheck: false,
      dialogdata: {},
      tHeader: '',
      searchName: '', // 搜索关键字
      currentPage: 1, // 当前页码
      pageSize: 10 // 每页的数据条数
    }
  },
  computed: {
    // 过滤后的数据
    filteredData() {
      let filtered = this.tableData
      const name = this.searchName.trim()

      if (name) {
        filtered = filtered.filter(item => item.builder.includes(name))
      }

      filtered = filtered.slice((this.currentPage - 1) * this.pageSize, this.currentPage * this.pageSize)
      return filtered.map(item => ({
        ...item,
        isDel: !item.isDel // 取反
      }))
    }
  },
  mounted() {
    this.loadData()
  },
  methods: {
    // 加载数据
    loadData() {
      GetList()
        .then(response => {
          this.tableData = response.data
        })
        .catch(error => {
          console.error('Error loading data: ', error)
        })
        .finally(() => {
          this.isLoading = false
        })
    },
    // 搜索操作
    handleSearch() {
      // 重新从第一页开始显示
      this.currentPage = 1
    },
    // 添加学院
    handleAdd() {
      // TODO: 弹出添加学院的对话框或页面
      console.log('Add academy')
    },
    // 查看实验室历史检查记录
    handleView(row) {
      this.dialogdata = row.id
      this.dialogCheck = true
    },
    // 修改状态（启用/停用）
    handleChangeState(id) {
      ChangeState(id).then(result => {
        this.$message({
          type: 'success',
          message: result.msg
        }).catch(response => {
          this.$message({
            type: 'error',
            message: response.msg
          })
        })
      })
    },
    // 分页：每页条数变化
    handleSizeChange(val) {
      this.pageSize = val
    },
    // 分页：当前页变化
    handleCurrentChange(val) {
      this.currentPage = val
    },
    // 分配人员操作
    handleAssign(row) {
      this.dialogdata = row
      this.dialogAssign = true
    },
    // 导出异常数据
    handleExportError(row) {
      GetHandingByLab(row.id).then(result => {
        this.downloadLoading = true
        import('@/vendor/Export2Excel').then(excel => {
          const list = result.data
          const tfilename = row.name
          // 格式化表体
          const data = this.fromatHandingData(list)
          // 格式化表头
          const tHeader = this.tHeader
          excel.export_json_to_excel({
            header: tHeader,
            data,
            filename: tfilename + '维修记录',
            autoWidth: this.autoWidth,
            bookType: this.bookType
          })
          this.downloadLoading = false
        })
      }).catch(response => {
        this.$message({
          type: 'error',
          message: '导出失败'
        })
      })
    },
    // 维修记录导出
    handleExportRepair(row) {
      GetRepairByLab(row.id).then(result => {
        this.downloadLoading = true
        import('@/vendor/Export2Excel').then(excel => {
          const list = result.data
          const tfilename = row.name
          // 格式化表体
          const data = this.fromatReparsData(list)
          // 格式化表头
          const tHeader = this.tHeader
          excel.export_json_to_excel({
            header: tHeader,
            data,
            filename: tfilename + '维修记录',
            autoWidth: this.autoWidth,
            bookType: this.bookType
          })
          this.downloadLoading = false
        })
      }).catch(response => {
        this.$message({
          type: 'error',
          message: '导出失败'
        })
      })
    },
    // 维修记录格式化
    fromatReparsData(list) {
      console.log(list)
      const map = {
        id: '编号',
        labName: '实验室名称',
        repairDate: '维修时间',
        issuesFound: '问题',
        repairPersonnelName: '维修人员',
        suggestions: '实施建议',
        comletionStatus: '完成状态',
        remark: '备注'
      }

      this.tHeader = Object.values(map)
      return list.map(item => {
        return Object.keys(map).map(key => {
          if (key === 'repairDate') {
            // 格式化日期
            return new Date(item[key]).toLocaleDateString()
          }
          if (key === 'comletionStatus') {
            // 格式化日期
            return item[key] ? '完成' : '未完成'
          }
          return item[key]
        })
      })
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
    }
  }
}
</script>

<style scoped>
.my-box {
  padding: 10px;
}
</style>
