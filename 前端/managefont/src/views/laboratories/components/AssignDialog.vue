<template>
  <div>
    <!-- 使用 v-if 来控制表单显示，只有当 isLoading 为 false 时才显示表单 -->
    <el-form ref="labForm" v-loading="isLoading" :model="labForm" :rules="labRules" label-width="120px" class="demo-labForm">
      <el-form-item label="所属楼房:">
        <el-tag effect="dark">{{ dialogdata.builder }}</el-tag>
      </el-form-item>

      <el-form-item label="所属学院:">
        {{ dialogdata.academy }}
      </el-form-item>

      <el-form-item label="实验室名称:">
        {{ dialogdata.name }}
      </el-form-item>

      <el-form-item label="分配管理人员" prop="UID">
        <el-select v-model="labForm.UID" placeholder="请选择">
          <el-option v-for="item in options" :key="item.id" :label="item.userName" :value="item.id" />
        </el-select>
      </el-form-item>

      <el-form-item>
        <el-button type="primary" @click="submitLabForm('labForm')">立即添加</el-button>
        <el-button @click="resetLabForm('labForm')">重置</el-button>
      </el-form-item>
    </el-form>
    <!-- 显示加载动画 -->
    <!-- <el-loading v-if="isLoading" :fullscreen="true" /> -->
  </div>
</template>

<script>
import { GetByAcademy, AssignMent } from '@/api/user.js'

export default {
  props: {
    dialogdata: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      labForm: {
        LID: '',
        UID: ''
      },
      isLoading: true, // 初始化为 true 表示正在加载
      options: [],
      labRules: {
        UID: [
          { required: true, message: '请选择管理人员', trigger: 'change' }
        ]
      }
    }
  },
  mounted() {
    this.Initdata()
  },
  methods: {
    submitLabForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.Assign(this.labForm.LID, this.labForm.UID)
        } else {
          console.log('表单验证失败')
          return false
        }
      })
    },
    resetLabForm(formName) {
      this.$refs[formName].resetFields()
    },
    // 初始化数据
    Initdata() {
      GetByAcademy(this.dialogdata.academyId).then(result => {
        this.options = result.data
        this.labForm.LID = this.dialogdata.id
        this.labForm.UID = this.dialogdata.manageId
      }).catch(response => {
        this.$message({
          type: 'error',
          message: '初始化数据错误'
        })
      }).finally(() => {
        this.isLoading = false // 数据加载完成，设置 isLoading 为 false
      })
    },
    // 分配实验室管理人员
    Assign(LID, UID) {
      AssignMent(LID, UID).then(result => {
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
    }
  }
}
</script>
