<template>
  <div>
    <template>
      <el-form :model="editData" label-width="100px">
        <el-form-item label="实验室编号">
          <el-input v-model="editData.labNumber" readonly />
        </el-form-item>
        <el-form-item label="异常信息">
          <el-input v-model="editData.issuesFound" readonly />
        </el-form-item>
        <el-form-item label="维修人员">
          <el-input v-model="editData.repairName" readonly />
        </el-form-item>
        <el-form-item label="是否完成">
          <el-switch
            v-model="editData.comletionStatus"
            active-color="#13ce66"
            inactive-color="#ff4949"
          />
        </el-form-item>
        <el-form-item label="维修日期">
          <el-date-picker
            v-model="editData.repairDate"
            type="datetime"
            :default-value="currentDate"
            disabled
          />
        </el-form-item>
        <el-form-item label="备注">
          <el-input
            v-model="editData.remarks"
            type="textarea"
            :rows="4"
            placeholder="请输入备注信息"
          />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="handleClose">取消</el-button>
        <el-button type="primary" @click="handleSave">保存</el-button>
      </div>
    </template>
  </div>
</template>

<script>
import { UpdataRepairs } from '@/api/repairs.js'
export default {
  name: 'EditLogDialog',
  props: {
    dialogdata: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      editData: {
        labNumber: '',
        repairDate: '',
        issuesFound: '',
        repairName: '',
        comletionStatus: '',
        remarks: ''
      }
    }
  },
  computed: {
    currentDate() {
      return new Date()
    }
  },
  watch: {
    dialogdata: {
      immediate: true,
      handler(newVal) {
        if (newVal) {
          this.editData = { ...newVal }
          this.getNowTime() // Set repairDate when dialogdata changes
        }
      }
    }
  },
  mounted() {
    // Initially set repairDate
    this.getNowTime()
  },
  methods: {
    // 定义一个函数，用于将对象中每个属性的首字母变为大写

    capitalizeFirstLetter(obj) {
      const newObj = {}
      Object.keys(obj).forEach(key => {
        const capitalizedKey = key.charAt(0).toUpperCase() + key.slice(1)
        newObj[capitalizedKey] = obj[key]
      })
      return newObj
    },
    handleClose() {
      this.$emit('close')
    },
    handleSave() {
      // 调用 capitalizeFirstLetter 函数处理 editData
      // const capitalizedData = this.capitalizeFirstLetter(this.editData)
      // this.$emit('save', this.editData)
      UpdataRepairs(this.editData).then(result => {
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
    },
    getNowTime() {
      var now = new Date()
      var year = now.getFullYear()
      var month = now.getMonth() + 1
      var date = now.getDate()
      var hours = now.getHours()
      var minutes = now.getMinutes()
      var seconds = now.getSeconds()

      // Format date to yyyy-MM-ddTHH:mm:ss
      var isoDate = `${year}-${month.toString().padStart(2, '0')}-${date.toString().padStart(2, '0')}T${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`

      // Set repairDate in editData
      this.$set(this.editData, 'repairDate', isoDate)
    }

  }
}
</script>

<style scoped>
.dialog-footer {
  text-align: right;
}
</style>
