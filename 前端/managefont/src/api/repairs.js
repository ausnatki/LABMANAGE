import request from '@/utils/request'

// 获取所有维修列表
export function GetAllRepairs() {
  return request({
    url: '/Lab/Repairs/GetAllRepairs',
    method: 'get'
  })
}

// 通过维修人员获取列表
export function GetRepairsByUser(UID) {
  return request({
    url: '/api/Repairs/GetRepairsByUser',
    method: 'get',
    params: { UID }
  })
}

// 填写修改单
export function UpdataRepairs(data) {
  return request({
    url: '/Lab/Repairs/UpdataRepairs',
    method: 'post',
    data
  })
}

