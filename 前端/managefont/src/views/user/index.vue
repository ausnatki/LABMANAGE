<template>
  <div class="my-box">
    <!-- 这里是通过图书名查询 -->
    <el-input v-model="serchBookname" placeholder="图书名" style="width:200px;padding:0 10px 10px 0" />
    <el-button type="primary" @click="ClickSerchBookName">搜索</el-button>

    <!-- 这里是通过我的作者查询 -->
    <el-input v-model="serchBookauth" placeholder="作者" style="width:200px;padding:0 10px 10px 10px" />
    <el-button type="primary" @click="ClickSerchBookAuth">搜索</el-button>

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
        :key="item.value"
        :label="item.label"
        :value="item.value"
      />
    </el-select>

    <!-- 添加用户信息 -->
    <el-button type="warning" style="margin-left:20px" @click="ClickAdd()">添加用户信息</el-button>

    <!-- 表格部分 -->
    <el-table
      :data="tableData"
      border
      style="width: 100%"
    >
      <el-table-column
        label="编号"
      >
        <template slot-scope="scope">
          {{ scope.$index+1 }}
        </template>
      </el-table-column>
      <el-table-column
        prop="userName"
        label="姓名"
      />
      <el-table-column
        prop="loginName"
        label="学号"
      />
      <el-table-column
        prop="email"
        label="邮箱"
      />
      <el-table-column
        prop="phone"
        label="电话"
      />
      <el-table-column

        label="性别"
      >
        <template slot-scope="scope">
          <el-tag :type="SexType(scope.row.sex)">{{ SexText(scope.row.sex) }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column
        prop="academy"
        label="所属学院"
      >
        <template slot-scope="scope">
          <el-tag>{{ scope.row.academy }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column
        label="操作"
      >
        <template slot-scope="scope">
          <el-link type="primary" icon="el-icon-view" style="margin-right:10px" @click="ClickEdit(scope.row.id)">修改</el-link>
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
      :before-close="handleClose"
    >
      <AddDialog />
    </el-dialog>

    <!-- 修改弹出层 -->
    <el-dialog
      title="提示"
      :visible.sync="dialogEdit"
      width="30%"
      :before-close="handleClose"
    >
      <EditDialog />
    </el-dialog>
  </div>
</template>

<script>
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
      tableData: [{
        id: '1',
        userName: '王小虎',
        loginName: '2199070306',
        email: '1132332@outlook.com',
        phone: '15123535388',
        sex: true,
        academy: '软件学院'
      },
      {
        id: '2',
        userName: '王小虎',
        loginName: '2199070306',
        email: '1132332@outlook.com',
        phone: '15123535388',
        sex: true,
        academy: '软件学院'
      },
      {
        id: '3',
        userName: '王小虎',
        loginName: '2199070306',
        email: '1132332@outlook.com',
        phone: '15123535388',
        sex: true,
        academy: '软件学院'
      },
      {
        id: '4',
        userName: '王小虎',
        loginName: '2199070306',
        email: '1132332@outlook.com',
        phone: '15123535388',
        sex: true,
        academy: '软件学院'
      }],
      serchBookname: '',
      tserchBookname: '',
      serchBookauth: '',
      tserchBookauth: '',
      dialogAdd: false,
      dialogEdit: false
    }
  },
  computed: {
    filteredData() {
      // console.log(this.tableData)
      let filtered = this.tableData
      const bookname = this.tserchBookname
      const auth = this.tserchBookauth
      const category = this.serchCategory
      // filtered = filtered.slice((this.currentPage - 1) * this.pageSize, this.currentPage * this.pageSize)

      // console.log(category.length)
      // 判断是否有值
      if (bookname) {
        console.log('进行我的图书查询')
        filtered = filtered.filter(item => {
          return item.bookName.includes(bookname)
        })
      }
      // console.log(filtered)
      if (auth) {
        console.log('进行我的作者查询')
        filtered = filtered.filter(item => {
          return item.author.includes(auth)
        })
      }
      // console.log(filtered)
      if (category.length) {
        console.log('进行我的类别查询')
        filtered = filtered.filter(item => {
          return category.includes(item.category)
        })
      }
      // console.log(filtered)
      return filtered
    }
  },
  methods: {
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
    ClickEdit(UID) {
      this.dialogEdit = true
    }
  }
}
</script>

<style scope>
.my-box{
  padding: 10px;
}
</style>
