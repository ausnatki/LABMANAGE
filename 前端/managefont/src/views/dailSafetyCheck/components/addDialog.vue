<template>
  <el-form ref="ruleForm" label-position="top" :model="ruleForm" :rules="rules" label-width="100px" class="demo-ruleForm">
    <el-form-item label="实验室">
      <el-row :gutter="20">
        <el-col :span="12">
          <el-input v-model="dialogdata.labName" disabled />
        </el-col>
      </el-row>
    </el-form-item>
    <el-form-item label="检查人员">
      <el-row :gutter="20">
        <el-col :span="12">
          <el-input v-model="name" disabled />
        </el-col>
      </el-row>
    </el-form-item>

    <el-row :gutter="20">
      <el-col :span="12">
        <el-form-item label="检查日期" prop="CheckDate">
          <el-date-picker
            v-model="ruleForm.CheckDate"
            type="datetime"
            value-format="yyyy-MM-ddTHH:mm:ssZ"
            placeholder="选择检查日期"
          />
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="学期" prop="SemesterID">
          <el-select v-model="ruleForm.SemesterID" placeholder="请选择">
            <el-option
              v-for="item in semester"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            />
          </el-select>
        </el-form-item>
      </el-col>
    </el-row>

    <el-row :gutter="20">
      <el-col :span="12">
        <el-form-item label="实验室是否清洁" prop="Cleanliness">
          <el-radio-group v-model="ruleForm.Cleanliness">
            <el-radio :label="true">是</el-radio>
            <el-radio :label="false">否</el-radio>
          </el-radio-group>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="实验室门窗是否安全" prop="DoorsAndWindows">
          <el-radio-group v-model="ruleForm.DoorsAndWindows">
            <el-radio :label="true">是</el-radio>
            <el-radio :label="false">否</el-radio>
          </el-radio-group>
        </el-form-item>
      </el-col>
    </el-row>

    <el-row :gutter="20">
      <el-col :span="12">
        <el-form-item label="线路是否安全" prop="ElectricalLines">
          <el-radio-group v-model="ruleForm.ElectricalLines">
            <el-radio :label="true">是</el-radio>
            <el-radio :label="false">否</el-radio>
          </el-radio-group>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="消防设备有无" prop="FireSafetyEquipmentAvailable">
          <el-radio-group v-model="ruleForm.FireSafetyEquipmentAvailable">
            <el-radio :label="true">有</el-radio>
            <el-radio :label="false">无</el-radio>
          </el-radio-group>
        </el-form-item>
      </el-col>
    </el-row>

    <el-row :gutter="20">
      <el-col :span="12">
        <el-form-item label="消防设施是否在使用期" prop="FireSafetyEquipmentExpiry">
          <el-radio-group v-model="ruleForm.FireSafetyEquipmentExpiry">
            <el-radio :label="true">是</el-radio>
            <el-radio :label="false">否</el-radio>
          </el-radio-group>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="仪器设备是否正常使用" prop="InstrumentEquipmentIntact">
          <el-radio-group v-model="ruleForm.InstrumentEquipmentIntact">
            <el-radio :label="true">是</el-radio>
            <el-radio :label="false">否</el-radio>
          </el-radio-group>
        </el-form-item>
      </el-col>
    </el-row>

    <el-row :gutter="20">
      <el-col :span="12">
        <el-form-item label="仪器设备是否完好" prop="InstrumentEquipmentWorking">
          <el-radio-group v-model="ruleForm.InstrumentEquipmentWorking">
            <el-radio :label="true">是</el-radio>
            <el-radio :label="false">否</el-radio>
          </el-radio-group>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="仪器设备警示标志是否完好" prop="InstrumentWarningLabelsIntact">
          <el-radio-group v-model="ruleForm.InstrumentWarningLabelsIntact">
            <el-radio :label="true">是</el-radio>
            <el-radio :label="false">否</el-radio>
          </el-radio-group>
        </el-form-item>
      </el-col>
    </el-row>

    <el-form-item label="其他安全隐患" prop="OtherHazards"><el-input v-model="ruleForm.OtherHazards" type="textarea" /></el-form-item>
    <el-form-item label="仪器设备其他安全隐患" prop="OtherSafetyHazards"><el-input v-model="ruleForm.OtherSafetyHazards" type="textarea" /></el-form-item>

    <el-form-item>
      <el-button type="primary" @click="submitForm('ruleForm')">提交</el-button>
      <el-button @click="resetForm('ruleForm')">重置</el-button>
    </el-form-item>
  </el-form>
</template>

<script>
import { mapGetters } from 'vuex'
import { GetList } from '@/api/semesteres.js'
import { AddCheck } from '@/api/dailSafetyCheck.js'
export default {
  props: {
    dialogdata: {
      type: Number,
      required: true
    }
  },
  data() {
    return {
      ruleForm: {
        LabID: '',
        SemesterID: '',
        UID: '',
        CheckDate: '',
        Cleanliness: null,
        DoorsAndWindows: null,
        ElectricalLines: null,
        FireSafetyEquipmentAvailable: null,
        FireSafetyEquipmentExpiry: null,
        InstrumentEquipmentIntact: null,
        InstrumentEquipmentWorking: null,
        InstrumentWarningLabelsIntact: null,
        OtherHazards: '',
        OtherSafetyHazards: ''
      },
      semester: [],
      rules: {
        SemesterID: [{ required: true, message: '请选择学期', trigger: 'change' }],
        UID: [{ required: true, message: '请输入检查人员ID', trigger: 'blur' }],
        CheckDate: [{ required: true, message: '请选择检查日期', trigger: 'change' }],
        Cleanliness: [{ required: true, message: '请确认实验室是否清洁', trigger: 'change' }],
        DoorsAndWindows: [{ required: true, message: '请确认实验室门窗是否安全', trigger: 'change' }],
        ElectricalLines: [{ required: true, message: '请确认线路是否安全', trigger: 'change' }],
        FireSafetyEquipmentAvailable: [{ required: true, message: '请确认消防设备是否有无', trigger: 'change' }],
        FireSafetyEquipmentExpiry: [{ required: true, message: '请确认消防设施是否在使用期', trigger: 'change' }],
        InstrumentEquipmentIntact: [{ required: true, message: '请确认仪器设备是否正常使用', trigger: 'change' }],
        InstrumentEquipmentWorking: [{ required: true, message: '请确认仪器设备是否完好', trigger: 'change' }],
        InstrumentWarningLabelsIntact: [{ required: true, message: '请确认仪器设备警示标志是否完好', trigger: 'change' }],
        OtherHazards: [{ message: '请填写其他安全隐患', trigger: 'blur' }],
        OtherSafetyHazards: [{ message: '请填写仪器设备其他安全隐患', trigger: 'blur' }]
      }
    }
  },
  computed: {
    ...mapGetters([
      'roles',
      'uid',
      'name'
    ])
  },
  mounted() {
    this.Initdata()
  },
  methods: {
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
        // 确保CheckDate是ISO格式字符串
          const formData = {
            ...this.ruleForm,
            CheckDate: new Date(this.ruleForm.CheckDate).toISOString()
          }
          AddCheck(formData).then(result => {
            this.$message({
              type: 'success',
              message: '添加成功'
            })
          }).catch(response => {
            this.$message({
              type: 'error',
              message: '添加失败'
            })
          }).finally(() => {
            const updata = true
            this.$emit('updatadata', updata)
          })
        } else {
          console.log('提交失败!!')
          return false
        }
      })
    },
    resetForm(formName) {
      this.$refs[formName].resetFields()
    },
    async Initdata() {
      await GetList().then(result => {
        this.semester = result.data.filter(item => !item.isDel)
      }).catch(response => {
        this.$message({
          type: 'error',
          message: '获取学期列表失败'
        })
      }).finally(() => {
        this.ruleForm.LabID = this.dialogdata.labID
        this.ruleForm.UID = this.uid
      })
    }
  }
}
</script>

<style scoped>
.demo-ruleForm {
  width: 600px;
  padding:  10px;
}
</style>
