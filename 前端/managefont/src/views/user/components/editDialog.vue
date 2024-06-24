<template>
  <div class="my-box">
    <el-form ref="form" v-loading="isLoading" :model="form" :rules="rules" label-width="80px">
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
        <el-input v-model="form.Password" />
      </el-form-item>
      <el-form-item label="性别" prop="Sex">
        <el-radio-group v-model="form.Sex">
          <el-radio :label="true">男</el-radio>
          <el-radio :label="false">女</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="所属学院" prop="CID">
        <el-select
          v-model="form.CID"
          placeholder="请选择"
        >
          <el-option
            v-for="item in options"
            :key="item.id"
            :label="item.name"
            :value="item.id"
          />
        </el-select>

      </el-form-item>
      <el-form-item label="角色" prop="RID">
        <el-select v-model="form.RID" placeholder="请选择">
          <el-option
            v-for="item in roleoption"
            :key="item.id"
            :label="item.roleName"
            :value="item.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" :loading="EditButtonLoading" @click="onSubmit">保存</el-button>
        <el-button @click="onCancel">取消</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { GetSelectCheckList } from '@/api/academy.js'
import { GetRoleList, EditUser } from '@/api/user.js'
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
        UserName: '',
        LoginName: '',
        Email: '',
        Phone: '',
        Sex: null,
        CID: '',
        Password: '',
        RID: ''
      },
      isLoading: true,
      roleoption: [],
      options: [],
      EditButtonLoading: false,
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
        RID: [
          { required: true, message: '请选择所属学院', trigger: 'change' }
        ]
      }
    }
  },
  async mounted() {
    await this.InitData().finally(() => { this.isLoading = false })
  },
  methods: {
    onSubmit() {
      this.$refs.form.validate((valid) => {
        if (valid) {
          this.editUser(this.form)
        } else {
          console.log('表单验证失败')
        }
      })
    },
    onCancel() {
      this.$refs.form.resetFields()
    },
    // 初始化数据
    async InitData() {
      await GetRoleList().then(result => {
        // console.log(result)
        this.roleoption = result.data
      }).catch(response => {
        console.error(response)
      })

      await GetSelectCheckList().then(result => {
        console.log(result)
        this.options = result.data
      }).catch(response => {
        console.log(response)
      })

      this.form.Id = this.editdata.id
      this.form.UserName = this.editdata.userName
      this.form.CID = this.editdata.academyId
      this.form.RID = this.editdata.roleId
      this.form.Password = this.editdata.password
      this.form.Sex = this.editdata.sex
      this.form.Phone = this.editdata.phone
      this.form.Email = this.editdata.email
      this.form.LoginName = this.editdata.loginName
    },
    // 修改用户的逻辑
    editUser(data) {
      this.AddButtonLoading = true
      EditUser(data).then(result => {
        // console.log(result)
        this.$message({
          type: 'success',
          message: result.msg
        })
        // this.form = this.tform
      }).catch(response => {
        console.error(response)
        this.$message({
          type: 'error',
          message: response.msg
        })
      }).finally(() => {
        this.EditButtonLoading = false
      })
    }
  }
}
</script>
