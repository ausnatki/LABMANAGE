<template>
  <div class="my-box">
    <el-form ref="form" v-loading="isLoading" :model="form" :rules="rules" label-width="80px">
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
        <el-button type="primary" :loading="ButtonLoading" @click="onSubmit">确定修改</el-button>
        <el-button @click="onCancel">取消</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { Edit } from '@/api/academy.js'
export default {
  props: {
    editdata: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      form: {
        Id: '',
        Name: '',
        IsDel: ''
      },
      ButtonLoading: false,
      isLoading: true,
      rules: {
        Name: [
          { required: true, message: '学院名不能为空', trigger: 'blur' }
        ],
        IsDel: [
          { required: true, message: '是否启用不能为空', trigger: 'change' }
        ]
      }
    }
  },
  async mounted() {
    await this.initData().finally(() => { this.isLoading = false })
  },
  methods: {
    async onSubmit() {
      await this.$refs.form.validate((valid) => {
        if (valid) {
          this.edtiSemesteres(this.form)
        } else {
          console.log('表单验证失败')
          return false
        }
      })
    },
    onCancel() {
      this.$refs.form.resetFields()
    },
    // 修改逻辑
    async edtiSemesteres(data) {
      this.ButtonLoading = true
      await Edit(data).then(result => {
        this.$message({
          type: 'success',
          message: result.msg
        })
      }).catch(response => {
        console.error(response)
        this.$message({
          type: 'error',
          message: response.msg
        })
      }).finally(() => {
        this.ButtonLoading = false
      })
    },
    // 初始化数据
    async initData() {
      this.isLoading = true
      console.log(this.editdata)
      this.form.Id = this.editdata.id
      this.form.IsDel = this.editdata.isDel
      this.form.Name = this.editdata.name
    }
  }
}
</script>
