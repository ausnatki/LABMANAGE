<template>
  <div class="my-box">
    <!-- 这里是通过图书名查询 -->
    <el-input v-model="serchname" placeholder="搜索学期" style="width:200px;padding:0 10px 10px 0" />
    <el-button type="primary" @click="ClickSerchName">搜索</el-button>

    <!-- 添加用户信息 -->
    <el-button type="warning" style="margin-left:20px" @click="ClickAdd()">添加学期名</el-button>

    <!-- 表格部分 -->
    <el-table
      v-loading="isLoading"
      :data="filteredData"
      border
      style="width: 100%"
    >
      <el-table-column
        label="编号"
      >
        <template slot-scope="scope">
          {{ scope.$index+1+(currentPage-1)*pageSize }}
        </template>
      </el-table-column>
      <el-table-column
        prop="name"
        label="学期名"
      />
      <el-table-column
        prop="isDel"
        label="是否启用"
      >
        <template slot-scope="scope">
          <el-switch
            v-model="scope.row.isDel"
            active-color="#13ce66"
            inactive-color="#ff4949"
            disabled
          />
        </template>
      </el-table-column>
      <el-table-column
        label="操作"
      >
        <template slot-scope="scope">
          <el-link type="primary" icon="el-icon-view" style="margin-right:10px" @click="ClickEdit(scope.row)">修改</el-link>
        </template>
      </el-table-column>

    </el-table>

    <!-- 这里是我的分页组件 -->
    <el-pagination
      align="center"
      :current-page="currentPage"
      :page-sizes="[1, 5, 10, 20]"
      :page-size="pageSize"
      layout="total, sizes, prev, pager, next, jumper"
      :total="tableData.length"
      @size-change="handleSizeChange"
      @current-change="handleCurrentChange"
    />

    <!-- 添加弹出层 -->
    <el-dialog
      title="提示"
      :visible.sync="dialogAdd"
      width="30%"
      @close="handleCloseEditDialog"
    >
      <AddDialog />
    </el-dialog>

    <!-- 修改弹出层 -->
    <el-dialog
      v-if="dialogEdit"
      title="提示"
      :visible.sync="dialogEdit"
      width="30%"
      @close="handleCloseEditDialog"
    >
      <template v-if="dialogEdit">
        <EditDialog :editd.sync="editData" />
      </template>
    </el-dialog>

  </div>
</template>

<script>
import { GetList } from '@/api/semesteres.js'
import EditDialog from '@/views/semesteres/components/editDialog.vue'
import AddDialog from '@/views/semesteres/components/addDialog.vue'
export default {
  name: 'SemesteresPage',
  components: {
    AddDialog,
    EditDialog
  },
  data() {
    return {
      tableData: [],
      editData: {},
      serchname: '',
      tserchname: '',
      dialogAdd: false,
      dialogEdit: false,
      isLoading: true,
      serchCategory: [],
      // 初始化标志变量
      isInitialLoad: true,
      currentPage: 1, // 当前页码
      total: 20, // 总条数
      pageSize: 10 // 每页的数据条数
    }
  },
  computed: {
    filteredData() {
      // console.log(this.tableData)
      let filtered = this.tableData
      const name = this.tserchname

      filtered = filtered.slice((this.currentPage - 1) * this.pageSize, this.currentPage * this.pageSize)

      // console.log(category.length)
      // 判断是否有值
      if (name) {
        // console.log('进行我的图书查询')
        filtered = filtered.filter(item => {
          return item.name.includes(name)
        })
      }
      // console.log(filtered)
      // return filtered
      return filtered.map(item => ({
        ...item,
        isDel: !item.isDel // 取反
      }))
    }
  },
  watch: {
    dialogAdd(newVal) {
      if (this.isInitialLoad) {
      // 首次加载时跳过执行逻辑
        this.isInitialLoad = false
      } else {
      // 非首次加载时执行逻辑
        if (newVal === false) this.InitData()
      }
    },

    dialogEdit(newVal) {
      if (this.isInitialLoad) {
      // 首次加载时跳过执行逻辑
        this.isInitialLoad = false
      } else {
      // 非首次加载时执行逻辑
        if (newVal === false) {
          this.InitData()
        }
      }
    }
  },
  mounted() {
    this.InitData()
  },
  methods: {
    // 点击添加
    ClickAdd() {
      this.dialogAdd = true
    },
    // 点击修改
    ClickEdit(edit) {
      console.log(edit)
      this.editData = edit
      this.dialogEdit = true
    },
    // 初始化数据
    async InitData() {
      await GetList().then(result => {
        // console.log(result)
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
    // 点击搜索
    ClickSerchName() {
      this.tserchname = this.serchname
    },
    // 删除弹出层事件
    handleCloseEditDialog() {
      this.dialogEdit = false
      this.editData = {} // 重置 editData
    }
  }
}
</script>

<style scope>
.my-box{
  padding: 10px;
}
</style>
