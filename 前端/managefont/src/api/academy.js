import request from '@/utils/request'

export function GetList() {
  return request({
    url: '/Lab/Academy/GetList',
    method: 'get'
  })
}

export function AddAcademy(data) {
  return request({
    url: '/Lab/Academy/AddAcademy',
    method: 'post',
    data
  })
}

export function GetSelectCheckList() {
  return request({
    url: '/Lab/Academy/GetSelectCHeckList',
    method: 'get'
  })
}

export function ChangeState(CID) {
  return request({
    url: '/Lab/Academy/ChangeState',
    method: 'post',
    params: { CID }
  })
}

export function Edit(data) {
  return request({
    url: '/Lab/Academy/Edit',
    method: 'post',
    data
  })
}
