<template>
  <div v-loading="isLoading">
    <el-form :model="formData" label-width="100px">
      <el-form-item label="实验室编号">
        <el-input v-model="formData.labNumber" disabled />
      </el-form-item>
      <el-form-item label="异常详情">
        <el-input v-model="formData.incidentDetails" disabled />
      </el-form-item>
      <el-form-item label="上报时间">
        <el-input v-model="formData.inclidentTime" disabled />
      </el-form-item>
      <el-form-item label="学期">
        <el-input v-model="formData.semesters" disabled />
      </el-form-item>
      <el-form-item label="管理人员">
        <el-select v-model="formData.repairPersonnelID" placeholder="请选择管理人员">
          <el-option
            v-for="person in personnelList"
            :key="person.id"
            :label="person.userName"
            :value="person.id"
          />
        </el-select>
      </el-form-item>
    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button @click="handleClose">取消</el-button>
      <el-button type="primary" @click="handleAssign">确定</el-button>
    </div>
  </div>
</template>

<script>
import { GetRepairs } from '@/api/user.js'
import { AssignRepair } from '@/api/handing.js'
export default {
  name: 'AssignDialogVue',
  props: {
    dialogdata: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      formData: {
        id: '',
        labNumber: '',
        incidentDetails: '',
        inclidentTime: '',
        semesters: '',
        repairPersonnelID: ''
      },
      isLoading: true,
      personnelList: [

      ]
    }
  },
  watch: {
    dialogdata: {
      immediate: true,
      handler(newVal) {
        if (newVal) {
          this.formData = { ...newVal }
        }
      }
    }
  },
  mounted() {
    this.Initdata()
  },
  methods: {
    // 获取初始化数据
    Initdata() {
      this.isLoading = true
      GetRepairs().then(result => {
        this.personnelList = result.data
      }).catch(response => {
        this.$message({
          type: 'error',
          message: '初始化数据失败'
        })
      }).finally(() => {
        this.isLoading = false
      })
    },
    handleClose() {
      this.$emit('close')
    },
    handleAssign() {
      AssignRepair(this.formData.id, this.formData.repairPersonnelID).then(result => {
        this.$message({
          type: 'success',
          message: result.msg
        })
      }).catch((response) => {
        this.$message({
          type: 'error',
          message: response.msg
        })
      })
    }
  }
}
</script>

<style scoped>
.dialog-footer {
  text-align: right;
}
</style>
