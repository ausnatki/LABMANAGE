import request from '@/utils/request'

// 获取所有异常列表
export function GetAllHanding() {
  return request({
    url: '/Lab/Handing/GetAllHanding',
    method: 'get'
  })
}

// 通过实验室管理人员获取异常列表
export function GetHandingByUser(UID) {
  return request({
    url: '/Lab/Handing/GetHandingByUser',
    method: 'get',
    params: { UID }
  })
}

// 分配实验室维修人员
export function AssignRepair(HID, UID) {
  return request({
    url: '/Lab/Handing/AssignRepair',
    method: 'post',
    params: { HID, UID }
  })
}

