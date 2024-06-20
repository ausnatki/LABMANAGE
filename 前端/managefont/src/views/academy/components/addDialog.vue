<template>
  <div class="my-box">
    <el-form ref="form" :model="form" :rules="rules" label-width="80px">
      <el-form-item label="学院名" prop="Name">
        <el-col :span="14">
          <el-input v-model="form.Name" />
        </el-col>
      </el-form-item>
      <el-form-item label="是否启用">
        <el-switch
          v-model="form.IsDel"
          active-color="#13ce66"
          inactive-color="#ff4949"
        />
      </el-form-item>
      <el-form-item>
        <el-button type="primary" :loading="AddButtonLoading" @click="onSubmit">立即创建</el-button>
        <el-button @click="onCancel">取消</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { AddAcademy } from '@/api/academy.js'
export default {
  data() {
    return {
      form: {
        Name: '',
        IsDel: false
      },
      tform: {
        Name: '',
        IsDel: false
      },
      AddButtonLoading: false,
      rules: {
        Name: [
          { required: true, message: '学期名不能为空', trigger: 'blur' }
        ],
        IsDel: [
          { required: true, message: '是否启用不能为空', trigger: 'change' }
        ]
      }
    }
  },
  methods: {
    onSubmit() {
      this.$refs.form.validate((valid) => {
        if (valid) {
          this.addacademy(this.form)
        } else {
          console.log('表单验证失败')
          return false
        }
      })
    },
    onCancel() {
      this.$refs.form.resetFields()
    },
    // 添加逻辑
    async addacademy(data) {
      this.AddButtonLoading = true
      await AddAcademy(data).then(result => {
        console.log(result)
        this.$message({
          type: 'success',
          message: result.msg
        })
      }).catch(response => {
        console.error(response)
      }).finally(() => {
        this.$refs['form'].resetFields()
        this.AddButtonLoading = false
      })
    }
  }
}
</script>
