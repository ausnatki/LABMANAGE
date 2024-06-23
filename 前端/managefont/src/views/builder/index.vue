<template>
  <div class="my-box">
    <!-- 这里是通过图书名查询 -->
    <el-input v-model="serchname" placeholder="图书名" style="width:200px;padding:0 10px 10px 0" />
    <el-button type="primary" @click="ClickSerchName">搜索</el-button>

    <!-- 添加用户信息 -->
    <el-button type="warning" style="margin-left:20px" @click="openAddDialog">添加学期名</el-button>

    <!-- 表格部分 -->
    <el-table v-loading="isLoading" :data="filteredData" border style="width: 100%">
      <el-table-column label="编号">
        <template slot-scope="scope">{{ scope.$index + 1 }}</template>
      </el-table-column>
      <el-table-column prop="name" label="楼房名称" />
      <el-table-column prop="number" label="楼房层数">
        <template slot-scope="scope">
          <el-tag>{{ scope.row.number }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="isDel" label="是否启用">
        <template slot-scope="scope">
          <el-switch v-model="scope.row.isDel" active-color="#13ce66" inactive-color="#ff4949" :loading="ButtonLoading" @change="changeState(scope.row.id)" />
        </template>
      </el-table-column>
      <el-table-column label="操作">
        <template slot-scope="scope">
          <el-link type="primary" style="margin-right:10px" @click="ClickEdit(scope.row)">修改</el-link>
          <el-link type="primary" style="margin-right:10px" @click="ClickAddFloor(scope.row.id)">添加楼层</el-link>
        </template>
      </el-table-column>
    </el-table>

    <!-- 这里是我的分页组件 -->
    <el-pagination align="center" :current-page="currentPage" :page-sizes="[1, 5, 10, 20]" :page-size="pageSize" layout="total, sizes, prev, pager, next, jumper" :total="tableData.length" @size-change="handleSizeChange" @current-change="handleCurrentChange" />

    <!-- 添加楼层的对话框 -->
    <AddFloorDialog :visible.sync="addDialogVisible" @submit="submitAdd" />
  </div>
</template>
<script>
import { AddBuilder, GetAllList, EditBuilder, ChangeState } from '@/api/building'
import { AddFloor } from '@/api/floor.js'
import AddFloorDialog from '@/views/builder/components/addDialog.vue' // 导入新组件
export default {
  name: 'BuilderPage',
  components: {
    AddFloorDialog
  },
  data() {
    return {
      tableData: [],
      isLoading: true,
      serchname: '',
      tserchname: '',
      dialogEdit: false,
      currentPage: 1, // 当前页码
      total: 20, // 总条数
      pageSize: 10, // 每页的数据条数
      addDialogVisible: false // 控制对话框的显示
    }
  },
  computed: {
    filteredData() {
      let filtered = this.tableData
      const name = this.tserchname
      filtered = filtered.slice((this.currentPage - 1) * this.pageSize, this.currentPage * this.pageSize)

      if (name) {
        filtered = filtered.filter(item => {
          return item.name.includes(name)
        })
      }
      // Apply pagination after filtering

      return filtered.map(item => ({
        ...item,
        isDel: !item.isDel // 取反
      }))
    }
  },
  mounted() {
    this.InitData()
  },
  methods: {
    // 打开添加对话框
    openAddDialog() {
      this.addDialogVisible = true
    },
    // 提交添加表单
    async submitAdd(form) {
      const data = {
        id: 0,
        ...form,
        isDel: false
      }
      try {
        console.log(data)
        const result = await AddBuilder(data)
        this.tableData.push(data) // Add new item to tableData
        this.$message({
          type: 'success',
          message: result.msg
        })
        this.addDialogVisible = false // 关闭对话框
      } catch (response) {
        this.$message({
          type: 'error',
          message: response.msg
        })
      }
    },
    // 点击修改
    ClickEdit(data) {
      this.dialogEdit = true
      this.$prompt('请输入楼房名称', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        inputPattern: /^(?!\s*$).+/,
        inputErrorMessage: '请输入修改后的名称',
        inputValue: data.name
      }).then(({ value }) => {
        data.name = value
        this.EditBuilder(data)
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '取消输入'
        })
      })
    },
    // 初始化数据
    InitData() {
      console.log('dasdada')
      this.isLoading = true
      GetAllList().then(result => {
        console.log(result)
        this.tableData = result.data
      }).catch(response => {
        console.log('发生错误')
        console.error(response)
      }).finally(() => {
        this.isLoading = false
      })
    },
    // 点击搜索
    ClickSerchName() {
      this.tserchname = this.serchname
    },
    // 每页条数改变时触发 选择一页显示多少行
    handleSizeChange(val) {
      // console.log(`每页 ${val} 条`)
      this.currentPage = 1
      this.pageSize = val
    },
    // 当前页改变时触发 跳转其他页
    handleCurrentChange(val) {
      // console.log(`当前页: ${val}`)
      this.currentPage = val
    },
    // 点击添加楼层
    ClickAddFloor(BID) {
      this.$confirm('是否添加楼层此操作会导致不可逆, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        AddFloor(BID) // Pass BID as an argument to AddFloor
          .then(result => { // Chain the .then method to handle the result
            this.$message({
              type: 'success',
              message: result.msg
            })
          })
          .catch(response => { // Handle errors
            this.$message({
              type: 'error', // Changed to 'error' type for better clarity
              message: response.msg
            })
          }).finally(() => {
            this.InitData()
          })
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消操作'
        })
      })
    },
    // 点击修改
    EditBuilder(data) {
      EditBuilder(data).then(result => {
        this.$message({
          type: 'success',
          message: result.msg
        })
      }).catch(response => {
        this.$message({
          type: 'error',
          message: response.msg
        })
      })
    },
    // 点击状态开关的选项
    changeState(BID) {
      this.ButtonLoading = true
      ChangeState(BID).then(result => {
        this.$message({
          type: 'success',
          message: result.msg
        }).catch(response => {
          this.$message({
            type: 'error',
            message: response.msg
          })
        })
      }).finally(() => {
        this.ButtonLoading = false
      })
    }
  }
}

</script>

<style scope>
.my-box{
  padding: 10px;
}
</style>
