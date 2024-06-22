<template>
  <div class="my-box">
    <!-- 搜索栏 -->
    <el-input v-model="searchName" placeholder="请输入楼层号" style="width:200px;padding:0 10px 10px 0" />
    <!-- <el-button type="primary" @click="handleSearch">搜索</el-button> -->
    <el-button type="primary" @click="handleDownload">导出数据</el-button>

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

    <el-dialog title="添加检查记录" width="35%" :visible.sync="dialogadd">
      <template v-if="dialogadd">
        <DialogAdd :dialogdata.sync="dialogAddData" @updatadata="handleDataFromChild" />
      </template>
    </el-dialog>

    <el-dialog title="异常信息" width="70%" :visible.sync="dialogHanding">
      <template v-if="dialogHanding">
        <DialogHanding :dialogdata.sync="dialogHandingData" />
      </template>
    </el-dialog>

    <el-dialog title="维修信息" width="70%" :visible.sync="dialogRepair">
      <template v-if="dialogRepair">
        <DialogRepair :dialogdata.sync="dialogRepairData" />
      </template>
    </el-dialog>
  </div>
</template>

<script>
import DialogHanding from '@/views/dailSafetyCheck/components/dialogHanding.vue'
import DialogRepair from '@/views/dailSafetyCheck/components/dialogRepair.vue'
import DialogAdd from '@/views/dailSafetyCheck/components/addDialog.vue'
import { GetAllDailyCheck, GetDailyCheckByUser, GetNotifyInitdata } from '@/api/dailSafetyCheck.js'
import { mapGetters } from 'vuex'

export default {
  name: 'LabLogPage',
  components: {
    DialogHanding,
    DialogRepair,
    DialogAdd
  },
  data() {
    return {
      tableData: [],
      isLoading: true,
      notifyPromise: Promise.resolve(),
      dialogAddData: '',
      searchName: '', // 搜索关键字
      currentPage: 1, // 当前页码
      pageSize: 10, // 每页的数据条数
      dialogHandingData: '',
      dialogRepairData: '',
      dialogadd: false,
      dialogHanding: false,
      dialogRepair: false,
      tHeader: []
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
    },
    excelData() {
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

      return filtered
    },
    ...mapGetters([
      'roles',
      'uid'
    ])
  },
  watch: {
    dialogadd(newVal) {
      if (!newVal) this.initData()
    }
  },
  created() {
    this.handleNotifications()
  },
  mounted() {
    this.initData()
  },
  methods: {
    async initData() {
      this.isLoading = true

      try {
        if (this.roles.includes('admin')) {
          const allDailyCheckResult = await GetAllDailyCheck()
          this.tableData = allDailyCheckResult.data
        } else {
          const dailyCheckByUserResult = await GetDailyCheckByUser(this.uid)
          this.tableData = dailyCheckByUserResult.data
        }
      } catch (error) {
        this.$message({
          type: 'error',
          message: error.response.msg || 'An error occurred'
        })
      } finally {
        this.isLoading = false
      }
    },
    async handleNotifications() {
      const notifyInitDataResult = await GetNotifyInitdata(this.uid)
      const h = this.$createElement
      notifyInitDataResult.data.forEach(lab => {
        this.notifyPromise = this.notifyPromise.then(() => {
          const notification = this.$notify({
            title: '实验室通知',
            message: h('i', { style: 'color: teal' }, `${lab.labName}`),
            type: 'warning',
            duration: 0,
            onClick: () => {
              this.addLabToForm(lab)
              notification.close() // 关闭通知
            }
          })
        })
      })
    },

    addLabToForm(data) {
      console.log(data)
      this.dialogAddData = data
      this.dialogadd = true
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
      this.dialogHandingData = row.id
      this.dialogHanding = true
    },
    // 查看维修信息操作
    handleViewRepairs(row) {
      this.dialogRepairData = row.id
      this.dialogRepair = true
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
    },
    // 格式化表格数据 用户导出
    formatJson(list) {
      const map = {
        id: '编号',
        semester: '学期',
        checkDate: '检查日期',
        labNumber: '房楼位置',
        managerName: '小负责人名称',
        cleanliness: '清洁情况',
        doorsAndWindows: '门窗状况',
        electricalLines: '电线状态',
        fireSafetyEquipmentAvailable: '消防设备是否可以用',
        fireSafetyEquipmentExpiry: '消防设备是否已过有效期',
        instrumentEquipmentIntact: '仪器设备完整性',
        instrumentEquipmentWorking: '仪器设备是否可以正常工作',
        instrumentWarningLabelsIntact: '仪器设备标签是否完成',
        otherSafetyHazards: '其他安全隐患',
        otherHazards: '其他危害',
        state: '状态'
      }

      this.tHeader = Object.values(map)
      return list.map(item => {
        return Object.keys(map).map(key => {
          return item[key]
        })
      })
    },
    // 导出excel
    handleDownload() {
      this.downloadLoading = true
      import('@/vendor/Export2Excel').then(excel => {
        const list = this.excelData
        // 格式化表体
        const data = this.formatJson(list)
        // 格式化表头
        const tHeader = this.tHeader
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: '日志记录',
          autoWidth: this.autoWidth,
          bookType: this.bookType
        })
        this.downloadLoading = false
      })
    },
    handleDataFromChild(updata) {
      this.dialogadd = false
    }
  }
}
</script>

<style scoped>
.my-box {
  padding: 10px;
}
</style>
