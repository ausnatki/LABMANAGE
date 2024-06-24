<template>
  <div class="my-box">
    <!-- 这里是通过图书名查询 -->
    <el-input v-model="serchname" placeholder="图书名" style="width:200px;padding:0 10px 10px 0" />
    <el-button type="primary" @click="ClickSerchName">搜索</el-button>

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
        prop="number"
        label="实验室编号"
      >
        <template slot-scope="scope">
          <el-tag>{{ scope.row.number }}</el-tag>
        </template>
      </el-table-column>

      <el-table-column
        prop="builder"
        label="所属楼层"
      >
        <template slot-scope="scope">
          <el-tag>{{ scope.row.builder }}</el-tag>
        </template>
      </el-table-column>

      <el-table-column
        prop="count"
        label="楼层数实验室数量"
        width="150"
      >
        <template slot-scope="scope">
          <el-input :value="scope.row.count" readonly>
            <template slot="append">间</template>
          </el-input>
        </template>
      </el-table-column>
      <el-table-column
        prop="isDel"
        label="是否启用"
      >
        <template slot-scope="scope">
          <el-switch
            v-model="scope.row.isDel"
            active-color="#13ce66"
            inactive-color="#ff4949"
            @change="changeState(scope.row.id)"
          />
        </template>
      </el-table-column>
      <el-table-column
        label="操作"
      >
        <template slot-scope="scope">
          <el-link type="primary" icon="el-icon-edit" style="margin-right:10px" @click="ClickAdd(scope.row)">添加实验室</el-link>
          <el-link type="success" icon="el-icon-view" style="margin-right:10px" @click="ClickView(scope.row.id)">查看实验室</el-link>
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
    >
      <template v-if="dialogAdd">
        <AddDialog :dialogdata.sync="dialogData" />
      </template>
    </el-dialog>

    <!-- 对应楼层实验室表格 -->
    <el-dialog v-loading="isdLoading" title="收货地址" :visible.sync="dialogView">
      <el-table :data="gridData">
        <el-table-column type="index" label="编号" />
        <el-table-column property="name" label="名称" />
        <el-table-column property="academy" label="所属学院" />
        <el-table-column property="managerName" label="管理人员" />
        <el-table-column property="isDel" label="状态"><template slot-scope="scope">{{ StateText(scope.row.isDel) }}</template></el-table-column>
      </el-table>
    </el-dialog>

  </div>
</template>

<script>
import { GetList, ChangeState } from '@/api/floor.js'
import AddDialog from '@/views/floor/components/addDialog.vue'
import { GetByFloor } from '@/api/laboratories'
export default {
  name: 'FloorPage',
  components: {
    AddDialog
  },
  data() {
    return {
      tableData: [],
      isLoading: true,
      serchname: '',
      tserchname: '',
      isdLoading: true,
      dialogAdd: false,
      dialogView: false,
      currentPage: 1, // 当前页码
      total: 20, // 总条数
      pageSize: 10, // 每页的数据条数
      dialogData: {},
      gridData: []
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
          return item.builder.includes(name)
        })
      }

      return filtered.map(item => ({
        ...item,
        isDel: !item.isDel // 取反
      }))
      // return filtered
    }
  },
  watch: {
    dialogAdd(newVal) {
      if (newVal === false) { this.InitData() }
    }
  },
  mounted() {
    this.InitData()
  },
  methods: {
    // 初始化数据
    async InitData() {
      this.isLoading = true
      await GetList().then(result => {
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
    // 搜索
    ClickSerchName() {
      this.tserchname = this.serchname
    },
    // 点击添加
    ClickAdd(data) {
      this.dialogData = data
      this.dialogAdd = true
    },
    // 点击状态开关的选项
    changeState(FID) {
      this.ButtonLoading = true
      ChangeState(FID).then(result => {
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

      })
    },
    // 点击查看
    async ClickView(FID) {
      this.dialogView = true
      this.isdLoading = true
      await GetByFloor(FID).then(result => {
        this.gridData = result.data
      }).catch(response => {
        this.$message({
          type: 'error',
          message: '查询列表失败'
        })
      }).finally(() => {
        this.isdLoading = false
      })
    },
    StateText(state) {
      switch (state) {
        case true:
          return '停用'
        case false:
          return '启用'
        default:
          return '未知'
      }
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
    }
  }
}
</script>

<style scope>
.my-box{
  padding: 10px;
}
</style>
