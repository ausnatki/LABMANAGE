
<template>
  <div class="my-box">
    <el-form ref="form" :model="form" :rules="rules" label-width="80px">
      <el-form-item label="姓名" prop="UserName">
        <el-col :span="14">
          <el-input v-model="form.UserName" />
        </el-col>
      </el-form-item>
      <el-form-item label="帐号" prop="LoginName">
        <el-input v-model="form.LoginName" />
      </el-form-item>
      <el-form-item label="电子邮箱" prop="Email">
        <el-input v-model="form.Email" />
      </el-form-item>
      <el-form-item label="联系电话" prop="Phone">
        <el-input v-model="form.Phone" />
      </el-form-item>
      <el-form-item label="密码" prop="Password">
        <el-input v-model="form.Password" type="password" />
      </el-form-item>
      <el-form-item label="性别" prop="Sex">
        <el-radio-group v-model="form.Sex">
          <el-radio :label="true">男</el-radio>
          <el-radio :label="false">女</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="所属学院" prop="CID">
        <el-select v-model="form.CID" placeholder="请选择所属学院">
          <el-option label="软件学院" value="1" />
          <el-option label="管理学院" value="2" />
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="onSubmit">立即创建</el-button>
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
        UserName: '冯思锦',
        LoginName: '219970305',
        Email: '12121@outlook.com',
        Phone: '18581348911',
        Sex: true,
        CID: '1',
        Password: '123456'
      },
      rules: {
        UserName: [
          { required: true, message: '请输入姓名', trigger: 'blur' }
        ],
        LoginName: [
          { required: true, message: '请输入帐号', trigger: 'blur' },
          { pattern: /^\d{9}$/, message: '帐号必须为9位数字', trigger: 'blur' }
        ],
        Email: [
          { required: true, message: '请输入电子邮箱', trigger: 'blur' },
          { type: 'email', message: '请输入有效的电子邮箱', trigger: ['blur', 'change'] }
        ],
        Phone: [
          { required: true, message: '请输入联系电话', trigger: 'blur' },
          { pattern: /^[0-9]+$/, message: '联系电话只能包含数字', trigger: 'blur' }
        ],
        Password: [
          { required: true, message: '请输入密码', trigger: 'blur' },
          { min: 6, message: '密码长度不能小于6位', trigger: 'blur' }
        ],
        Sex: [
          { required: true, message: '请选择性别', trigger: 'change' }
        ],
        CID: [
          { required: true, message: '请选择所属学院', trigger: 'change' }
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
