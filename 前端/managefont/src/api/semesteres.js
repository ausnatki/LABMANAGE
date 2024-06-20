import request from '@/utils/request'

export function GetList() {
  return request({
    url: '/Lab/Semesteres/GetAllList',
    method: 'get'
  })
}

export function AddSemesteres(data) {
  return request({
    url: '/Lab/Semesteres/AddSemesteres',
    method: 'post',
    data
  })
}

export function EditSemesteres(data) {
  return request({
    url: '/Lab/Semesteres/EditSemesteres',
    method: 'post',
    data
  })
}
