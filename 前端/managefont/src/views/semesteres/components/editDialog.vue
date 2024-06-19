<template>
  <div class="my-box">
    <el-form ref="form" :model="form" :rules="rules" label-width="80px">
      <el-form-item label="学期名" prop="Name">
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
        <el-button type="primary" @click="onSubmit">确定修改</el-button>
        <el-button @click="onCancel">取消</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      form: {
        Name: '学期名',
        IsDel: false
      },
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
          alert('提交成功')
        } else {
          console.log('表单验证失败')
          return false
        }
      })
    },
    onCancel() {
      this.$refs.form.resetFields()
    }
  }
}
</script>
