import request from '@/utils/request'

// 通过实验室获取日常检查记录
export function GetByLab(LID) {
  return request({
    url: '/Lab/DailSafetyCheck/GetByLab',
    method: 'get',
    params: { LID }
  })
}

// 通过实验室获取维修表
export function GetRepairByLab(LID) {
  return request({
    url: '/Lab/DailSafetyCheck/GetRepairByLab',
    method: 'get',
    params: { LID }
  })
}

// 通过实验室获取异常记录表
export function GetHandingByLab(LID) {
  return request({
    url: '/Lab/DailSafetyCheck/GetHandingByLab',
    method: 'get',
    params: { LID }
  })
}

// 获取所有记录表
export function GetAllDailyCheck() {
  return request({
    url: '/Lab/DailSafetyCheck/GetAllDailyCheck',
    method: 'get'
  })
}

// 改变日志记录状态
export function ChangeState(DID) {
  return request({
    url: '/Lab/DailSafetyCheck/ChangeState',
    method: 'post',
    params: { DID }
  })
}

// 添加检查日志
export function AddCheck(data) {
  return request({
    url: '/Lab/DailSafetyCheck/AddCheck',
    method: 'post',
    data
  })
}

// 分配维修人员
export function Assignmen(DHLID, UID) {
  return request({
    url: '/Lab/DailSafetyCheck/Assignmen',
    method: 'post',
    params: { DHLID, UID }
  })
}

// 填写维修记录
export function UpdataRepairs(data) {
  return request({
    url: '/Lab/DailSafetyCheck/UpdataRepairs',
    method: 'post',
    data
  })
}
