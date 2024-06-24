import request from '@/utils/request'

export function GetAllList() {
  return request({
    url: '/Lab/Builder/GetAllList',
    method: 'get'
  })
}

export function GetCheckList() {
  return request({
    url: '/Lab/Builder/GetCheckList',
    method: 'get'
  })
}

export function AddBuilder(data) {
  return request({
    url: '/Lab/Builder/AddBuilder',
    method: 'post',
    data
  })
}

export function EditBuilder(data) {
  return request({
    url: '/Lab/Builder/EditBuilder',
    method: 'post',
    data
  })
}
export function ChangeState(BID) {
  return request({
    url: '/Lab/Builder/ChangeState',
    method: 'post',
    params: { BID }
  })
}

