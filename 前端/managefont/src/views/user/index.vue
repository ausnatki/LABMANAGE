<template>
  <div class="my-box">
    <!-- 这里是通过姓名查询 -->
    <el-input v-model="serchUsername" placeholder="姓名" style="width:200px;padding:0 10px 10px 0" />
    <el-button type="primary" @click="ClickSerchUserName">搜索</el-button>

    <!-- 这里是我的多选框的部分 -->
    <el-select
      v-model="serchCategory"
      multiple
      collapse-tags
      style="margin-left: 20px;"
      placeholder="请选择"
    >
      <el-option
        v-for="item in options"
        :key="item.id"
        :label="item.name"
        :value="item.name"
      />
    </el-select>

    <!-- 添加用户信息 -->
    <el-button type="warning" style="margin-left:20px" @click="ClickAdd()">添加用户信息</el-button>

    <!-- 角色筛选 -->
    <el-checkbox v-model="checked1" label="超级管理员" style="margin-left:20px;padding: 9px 20px 7px 10px;" border />
    <el-checkbox v-model="checked2" label="实验室管理员" style="margin-left:-10px;padding: 9px 20px 7px 10px;" border />
    <el-checkbox v-model="checked3" label="维修人员" style="margin-left:-10px;padding: 9px 20px 7px 10px;" border />

    <!-- 表格部分 -->
    <el-table
      v-loading="isLoading"
      :data="filteredData"
      :default-sort="{prop: 'id', order: 'descending'}"
      border
      style="width: 100%"
    >
      <el-table-column label="编号">
        <template slot-scope="scope">
          {{ scope.$index+1+(currentPage-1)*pageSize }}
        </template>
      </el-table-column>
      <el-table-column prop="userName" label="姓名" />
      <el-table-column prop="loginName" label="学号" />
      <el-table-column prop="email" label="邮箱" />
      <el-table-column prop="phone" label="电话" />
      <el-table-column label="性别">
        <template slot-scope="scope">
          <el-tag :type="SexType(scope.row.sex)">{{ SexText(scope.row.sex) }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="academy" label="所属学院">
        <template slot-scope="scope">
          <el-tag>{{ scope.row.academy }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="操作">
        <template slot-scope="scope">
          <el-link type="primary" icon="el-icon-view" style="margin-right:10px" @click="ClickEdit(scope.row)">修改</el-link>
          <el-link type="danger" icon="el-icon-edit">删除</el-link>
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
      <AddDialog />
    </el-dialog>

    <!-- 修改弹出层 -->
    <el-dialog
      title="提示"
      :visible.sync="dialogEdit"
      width="30%"
    >
      <template v-if="dialogEdit">
        <EditDialog :editdata.sync="editData" />
      </template>
    </el-dialog>
  </div>
</template>

<script>
import { GetSelectCheckList } from '@/api/academy.js'
import { GetAllList } from '@/api/user.js'
import EditDialog from '@/views/user/components/editDialog.vue'
import AddDialog from '@/views/user/components/addDialog.vue'
export default {
  name: 'UserPage',
  components: {
    AddDialog,
    EditDialog
  },
  data() {
    return {
      tableData: [],
      serchUsername: '',
      tserchUsername: '',
      dialogAdd: false,
      dialogEdit: false,
      isLoading: true,
      serchCategory: [],
      currentPage: 1, // 当前页码
      total: 20, // 总条数
      pageSize: 10, // 每页的数据条数
      options: [],
      editData: {},
      checked1: '',
      checked2: '',
      checked3: ''
    }
  },
  computed: {
    filteredData() {
      // console.log(this.tableData)
      let filtered = this.tableData
      const username = this.tserchUsername
      const category = this.serchCategory
      const checked1 = this.checked1
      const checked2 = this.checked2
      const checked3 = this.checked3
      filtered = filtered.slice((this.currentPage - 1) * this.pageSize, this.currentPage * this.pageSize)

      // console.log(category.length)
      // 判断是否有值
      if (username) {
        // console.log('进行我的图书查询')
        filtered = filtered.filter(item => {
          return item.userName.includes(username)
        })
      }
      // console.log(filtered)
      if (category.length) {
        // console.log('进行我的类别查询')
        filtered = filtered.filter(item => {
          return category.includes(item.academy)
        })
      }

      // 假设 roleFilter 是一个包含您想要根据 checked 值来检查的 roleid 的数组
      const roleFilter = []
      if (checked1) {
        // 如果 checked1 为真，我们可能想要包含某些 roleid
        // 这里假设您想要包含 roleid 为 1 的项（或者根据您的需求进行修改）
        roleFilter.push(1)
      }
      if (checked2) {
        // 如果 checked2 为真，我们可能想要包含其他 roleid
        // 这里假设您想要包含 roleid 为 2 的项
        roleFilter.push(2)
      }
      // 如果有 checked3，您可以在这里添加相应的逻辑
      if (checked3) {
        roleFilter.push(3)
      }

      // 使用 includes 方法来检查 roleid 是否在 roleFilter 中
      filtered = filtered.filter(item => {
        // 假设 item.role.roleid 是您要比较的 roleid
        // 如果 roleFilter 为空，表示没有选中任何 checkbox，因此返回 false（即不显示任何项）
        // 或者如果 item 的 roleid 在 roleFilter 中，就返回 true（即显示该项）
        return roleFilter.length === 0 || roleFilter.includes(item.roleId) // 修改为 item.role.roleid 或其他适当的属性
      })

      // console.log(filtered);
      return filtered
    }
  },
  watch: {
    dialogAdd(newVal) {
      if (newVal === false) this.InitData()
    },

    dialogEdit(newVal) {
      if (newVal === false) this.InitData()
    }
  },
  mounted() {
    this.InitData()
    this.InitSelect()
  },
  methods: {
    // 初始化列表数据
    async InitData() {
      this.isLoading = true
      await GetAllList().then(result => {
        // console.log(result)
        this.tableData = result.data
      }).catch(response => {
        console.error(response)
      }).finally(() => {
        this.isLoading = false
      })
    },
    // 初始化搜索框选项
    async InitSelect() {
      await GetSelectCheckList().then(result => {
        console.log(result)
        this.options = result.data
      }).catch(response => {
        console.log(response)
      })
    },
    // 性别标签文本
    SexText(sex) {
      switch (sex) {
        case true:
          return '男'
        case false:
          return '女'
        default:
          return '未知'
      }
    },
    // 性别标签样式
    SexType(sex) {
      switch (sex) {
        case true:
          return ''
        case false:
          return 'success'
        default:
          return 'info'
      }
    },
    // 点击添加
    ClickAdd() {
      this.dialogAdd = true
    },
    // 点击修改
    ClickEdit(data) {
      this.editData = data
      this.dialogEdit = true
    },
    // 点击搜索
    ClickSerchUserName() {
      this.tserchUsername = this.serchUsername
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
