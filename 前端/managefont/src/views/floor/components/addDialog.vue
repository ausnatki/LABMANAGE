<template>
  <el-form ref="labForm" v-loading="isLoading" :model="labForm" :rules="labRules" label-width="100px" class="demo-labForm">
    <el-form-item label="所属楼房">
      <el-tag effect="dark">{{ labForm.LabNumber }}</el-tag>
    </el-form-item>
    <el-form-item label="所属楼层">
      <el-col :span="12">
        <el-input v-model="dialogdata.number" readonly placeholder="请输入内容">
          <template slot="append">号</template>
        </el-input>
      </el-col>
    </el-form-item>
    <el-form-item label="所属学院" prop="AcademyId">
      <el-select v-model="labForm.AcademyId" placeholder="请选择">
        <el-option v-for="item in options" :key="item.id" :label="item.name" :value="item.id" />
      </el-select>
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="submitLabForm('labForm')">立即添加</el-button>
      <el-button @click="resetLabForm('labForm')">重置</el-button>
    </el-form-item>
  </el-form>
</template>

<script>
import { AddLab } from '@/api/laboratories.js'
import { GetSelectCheckList } from '@/api/academy.js'

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
        LabNumber: '',
        AcademyId: '',
        FloorId: ''
      },
      isLoading: true,
      options: [],
      labRules: {
        AcademyId: [
          { required: true, message: '请选择所属学院', trigger: 'change' }
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
          this.addLab(this.labForm) // 确保传递正确的数据
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
      this.isLoading = true
      GetSelectCheckList().then(result => {
        this.options = result.data
      }).catch(response => {
        this.$message({
          type: 'error',
          message: '初始化数据错误'
        })
      }).finally(() => {
        this.$nextTick(() => {
          this.labForm.LabNumber = this.dialogdata.builder
          this.labForm.FloorId = this.dialogdata.id
          this.isLoading = false
        })
      })
    },
    // 添加实验室
    addLab(data) {
      AddLab(data).then(result => {
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
    }
  }
}
</script>
